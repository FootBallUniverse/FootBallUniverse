using UnityEngine;
using System.Collections;

public class CGoalKeeper : CCpu {
	public enum GK_State
	{
		WAIT,
		STAY,
		ON_ALERT,
		TAKE_BALL,
		CAT,
		PASS,
		BACK_HOME,
		GK_STATE_MAX
	};

	protected GK_State gkState            = GK_State.STAY;
	protected Vector3 HOME_POSITION       = new Vector3(0.0f, 0.0f, 0.0f);
	protected CGameManager gameManager    = new CGameManager();
	protected const float ARAT_SPACE      = 15.0f;
	protected const float TAKE_BALL_SPACE = 10.0f;

	//----------------------------------------------------------------------
	// コンストラクタ
	//----------------------------------------------------------------------
	// @Param   none
	// @Return  none
	// @Date    2014/12/7  @Update 2014/12/7  @Author T.Takeuchi
	//----------------------------------------------------------------------
	protected void CGoalKeeperInit(int teamNo)
	{
		this.Init();

		// プレイヤーのデータをセット
		if(teamNo == 0) CPlayerManager.SetPlayerData(this.m_playerData, CPlayerManager.AI_2);
		else            CPlayerManager.SetPlayerData(this.m_playerData, CPlayerManager.AI_4);
		m_pos = this.transform.localPosition;

		// 国の情報をセット / 国によってマテリアルを変更
		m_human = CHumanManager.GetWorldInstance(TeamData.teamNationality[teamNo]);
		if (teamNo == 0) this.transform.FindChild("polySurface14").GetComponent<CGoalKeeper1Mesh>().ChangeMaterial(TeamData.teamNationality[0]);
		else             this.transform.FindChild("polySurface14").GetComponent<CGoalKeeper2Mesh>().ChangeMaterial(TeamData.teamNationality[1]);
		// CPU用の値をセット
		this.SetData();

		// サッカーボールの情報を取得
		this.soccerBallObject = GameObject.Find("SoccerBall");

		// プレイヤーのアニメーターをセット
		m_animator = this.gameObject.transform.parent.GetComponent<CPlayerAnimator>();

		// 向きをセット
		this.transform.LookAt(new Vector3(0.0f, 0.0f, 0.0f));

		// ホームポジションをセット
		this.HOME_POSITION = new Vector3(this.m_playerData.m_xPos, this.m_playerData.m_yPos, this.m_playerData.m_zPos);

		// ゲームマネージャをセット
		this.gameManager = GameObject.Find("GameManager").GetComponent<CGameManager>();
	}


	//----------------------------------------------------------------------
	// 初期位置へ戻る
	//----------------------------------------------------------------------
	// @Param   none
	// @Return  none
	// @Date    2014/12/7  @Update 2014/12/7  @Author T.Takeuchi
	//----------------------------------------------------------------------
	void KeeperRestart()
	{
		this.Restart();

		this.gkState = GK_State.STAY;
		//this.transform.position = HOME_POSITION;
		this.transform.LookAt(new Vector3(0.0f,0.0f,0.0f));
	}


	//----------------------------------------------------------------------
	// 更新
	//----------------------------------------------------------------------
	// @Param   none
	// @Return  none
	// @Date    2014/12/7  @Update 2014/12/7  @Author T.Takeuchi
    // @Update  2014/12/11 アニメーションのための追加 @Author T.Kawashita
	//----------------------------------------------------------------------
	protected void CGoalKeeperUpdate()
	{
		if (m_isBall == true)
			this.transform.FindChild("SoccerBall").GetComponent<CSoccerBall>().SetPosition(new Vector3(0.0f, 0.05f, 0.1f));

		m_pos = this.transform.localPosition;

		this.CheckGamePlay();

		Debug.Log(CGameManager.m_nowStatus);
		if (CGameManager.m_nowStatus == CGameManager.eSTATUS.eGAME)
		{
			switch (m_status)
			{
				case CPlayerManager.ePLAYER_STATUS.eNONE: break;
				case CPlayerManager.ePLAYER_STATUS.eWAIT: break;
				case CPlayerManager.ePLAYER_STATUS.ePASS: break;

				// タックルのやられモーション中
				case CPlayerManager.ePLAYER_STATUS.eTACKLEDAMAGE:
					if (m_action.TackleDamage(ref m_pos, -this.transform.forward) == true)
					{
						// やられモーション終了
						m_animator.Wait();
						m_status = CPlayerManager.ePLAYER_STATUS.eNONE;
					}

					break;
			}

			switch (this.gkState)
			{
				case GK_State.WAIT: Wait(); break;
				case GK_State.STAY: Stay(); break;
				case GK_State.ON_ALERT: OnAlert(); break;
				case GK_State.TAKE_BALL: TakeBall(); break;
				case GK_State.CAT: Cach(); break;
				case GK_State.PASS: Pass(); break;
				case GK_State.BACK_HOME: BackHome(); break;
			}
		}
		else
		{
			KeeperRestart();
		}
	}


	//----------------------------------------------------------------------
	// フレーム最後の更新
	//----------------------------------------------------------------------
	// @Param   none
	// @Return  none
	// @Date    2014/12/7  @Update 2014/12/7  @Author T.Takeuchi
    // @Update  2014/12/11 アニメーションの関数呼び出し @Author T.Kawashita
	//----------------------------------------------------------------------
	protected void CGoalKeeperLateUpdate()
	{
		// アニメーション
		this.Animation();

		m_speed = new Vector3(0.0f, 0.0f, 0.0f);    // 最後にスピードを初期化
		this.rigidbody.MovePosition(m_pos);

		switch (this.gkState)
		{
			case GK_State.CAT:
			case GK_State.TAKE_BALL:
				if (this.soccerBallObject.GetComponent<CSoccerBall>().m_isPlayer)
				{
					this.gkState = GK_State.ON_ALERT;
				}
				break;
		}

	}

	void Wait()
	{
		BackHome();
		if (this.soccerBallObject.GetComponent<CSoccerBall>().m_isPlayer == true)
			this.gkState = GK_State.STAY;
	}


	//----------------------------------------------------------------------
	// AI:待機中の判断
	//----------------------------------------------------------------------
	// @Param   none
	// @Return  none
	// @Date    2014/12/7  @Update 2014/12/7  @Author T.Takeuchi
	//----------------------------------------------------------------------
	void Stay()
	{
		BackHome();

		// ボールの監視
		if (Vector3.Distance(this.HOME_POSITION, this.soccerBallObject.transform.position) <= ARAT_SPACE)
			this.gkState = GK_State.ON_ALERT;
	}


	//----------------------------------------------------------------------
	// AI:警戒態勢中の判断
	//----------------------------------------------------------------------
	// @Param   none
	// @Return  none
	// @Date    2014/12/7  @Update 2014/12/7  @Author T.Takeuchi
	//----------------------------------------------------------------------
	void OnAlert()
	{
		// ボールが範囲外に出たら待機へ戻る
		if (Vector3.Distance(this.HOME_POSITION, this.soccerBallObject.transform.position) >= ARAT_SPACE)
			this.gkState = GK_State.STAY;

		// フリーボールが範囲内＋敵が近くにいる
		if ((Vector3.Distance(this.HOME_POSITION, this.enemyData[0].transform.position) <= ARAT_SPACE ||
			 Vector3.Distance(this.HOME_POSITION, this.enemyData[1].transform.position) <= ARAT_SPACE ||
			 Vector3.Distance(this.HOME_POSITION, this.enemyData[2].transform.position) <= ARAT_SPACE ||
			 Vector3.Distance(this.HOME_POSITION, this.enemyData[3].transform.position) <= ARAT_SPACE) &&
			this.soccerBallObject.GetComponent<CSoccerBall>().m_isPlayer == false)
		{
			this.gkState = GK_State.CAT;
		}

		// フリーボールが範囲内＋敵が近くにいない
		if (Vector3.Distance(this.HOME_POSITION, this.enemyData[0].transform.position) >= ARAT_SPACE &&
			Vector3.Distance(this.HOME_POSITION, this.enemyData[1].transform.position) >= ARAT_SPACE &&
			Vector3.Distance(this.HOME_POSITION, this.enemyData[2].transform.position) >= ARAT_SPACE &&
			Vector3.Distance(this.HOME_POSITION, this.enemyData[3].transform.position) >= ARAT_SPACE &&
			Vector3.Distance(this.HOME_POSITION, this.soccerBallObject.transform.position) <= ARAT_SPACE &&
			this.soccerBallObject.GetComponent<CSoccerBall>().m_isPlayer == false)
		{
			if (Vector3.Distance(this.HOME_POSITION, this.soccerBallObject.transform.position) <= TAKE_BALL_SPACE)
			{
				// かなりの近距離の場合（普通のカットと同じ動作）
				this.gkState = GK_State.CAT;
			}else{
				// そこそこの距離の場合（普通にとりにいく）
				this.gkState = GK_State.TAKE_BALL;
			}
		}
	}


	//----------------------------------------------------------------------
	// AI:フリーのボールが流れてきたときに取る
	//----------------------------------------------------------------------
	// @Param   none
	// @Return  none
	// @Date    2014/12/7  @Update 2014/12/7  @Author T.Takeuchi
    // @Update  2014/12/11 パスアニメーションのためのステータス変更 @Author T.Kawashita
	//----------------------------------------------------------------------
	void TakeBall()
	{
		// ボールを取りに行く
		this.transform.LookAt(this.soccerBallObject.transform.position);
		Move(new Vector3(0.0f, 0.0f, 1.0f));


		// ボールをキャッチ（→パス）
		if (this.m_isBall)
		{
			this.gkState = GK_State.PASS;
			this.m_status = CPlayerManager.ePLAYER_STATUS.ePASS;
			this.transform.LookAt(this.frendryData[0].transform.position);
			this.m_action.InitPass(this.m_human.m_passInitSpeed, this.m_human.m_passMotionLength, this.m_human.m_passTakeOfFrame);
		}
		// ボールが範囲外に出たら待機へ戻る
		if (Vector3.Distance(this.HOME_POSITION, this.soccerBallObject.transform.position) >= ARAT_SPACE)
			this.gkState = GK_State.STAY;
	}


	//----------------------------------------------------------------------
	// AI:シュートされてたと判断したときの判断
	//----------------------------------------------------------------------
	// @Param   none
	// @Return  none
	// @Date    2014/12/7  @Update 2014/12/7  @Author T.Takeuchi
    // @Update  2014/12/11 パスアニメーションのためのステータス変更 @Author T.Kawashita
	//----------------------------------------------------------------------
	void Cach()
	{
		Vector3 targetPosition = new Vector3();

		targetPosition = GetFuterBallPosition();
		this.transform.LookAt(targetPosition);

		//if (1 < Vector3.Distance(this.transform.position, targetPosition)) Move(new Vector3(0.0f, 0.0f, 1.0f));
		//else this.transform.position = targetPosition;
		Move(new Vector3(0.0f, 0.0f, 1.0f));
		
		this.transform.LookAt(new Vector3(0.0f, 0.0f, 0.0f));

		// ボールをキャッチ（→パス）
		if (this.m_isBall)
		{
			this.gkState = GK_State.PASS;
			this.m_status = CPlayerManager.ePLAYER_STATUS.ePASS;
			this.transform.LookAt(this.frendryData[0].transform.position);
			this.m_action.InitPass(this.m_human.m_passInitSpeed, this.m_human.m_passMotionLength, this.m_human.m_passTakeOfFrame);
		}
		// ボールが範囲外に出たら待機へ戻る
		if (Vector3.Distance(this.HOME_POSITION, this.soccerBallObject.transform.position) >= ARAT_SPACE)
			this.gkState = GK_State.STAY;
	}


	//----------------------------------------------------------------------
	// AI:パス中の判断
	//----------------------------------------------------------------------
	// @Param   none
	// @Return  none
	// @Date    2014/12/7  @Update 2014/12/7  @Author T.Takeuchi
	// @Update  2014/12/11 パスアニメーションのためのステータス変更 @Author T.Kawashita
	//----------------------------------------------------------------------
	void Pass()
	{
		if (this.m_action.Pass(this.gameObject, this.transform.forward, ref this.m_isBall))
		{
			this.gkState = GK_State.WAIT;
			this.m_status = CPlayerManager.ePLAYER_STATUS.eNONE;
		}
	}


	//----------------------------------------------------------------------
	// AI:ホームポジションに戻る
	//----------------------------------------------------------------------
	// @Param   none
	// @Return  none
	// @Date    2014/12/7  @Update 2014/12/7  @Author T.Takeuchi
	//----------------------------------------------------------------------
	void BackHome()
	{
		if (this.transform.position != HOME_POSITION)
		{
			this.transform.LookAt(HOME_POSITION);
			if (0.5f < Vector3.Distance(this.transform.position, HOME_POSITION)) Move(new Vector3(0.0f, 0.0f, 0.5f));
			else this.transform.position = HOME_POSITION;

			this.transform.LookAt(new Vector3(0.0f, 0.0f, 0.0f));
		}
	}


	//----------------------------------------------------------------------
	// 飛んできたボールの未来予測座標を出力する
	//----------------------------------------------------------------------
	// @Param   none
	// @Return  Vector3  ボールの未来予測座標
	// @Date    2014/12/7  @Update 2014/12/7  @Author T.Takeuchi
	//----------------------------------------------------------------------
	Vector3 GetFuterBallPosition()
	{
		Vector3 refData = new Vector3();
		refData = this.soccerBallObject.transform.position;
		float work;

		// rigidbodyがZ軸で移動していない（＝ボールがこないので無効）
		if (this.soccerBallObject.rigidbody.velocity.z == 0)
			return new Vector3(0.0f, 0.0f, 0.0f);

		work = HOME_POSITION.z - this.soccerBallObject.transform.position.z;
		work /= this.soccerBallObject.rigidbody.velocity.z;

		// ゴール指定地点まで到着するまで移動
		refData.x += this.soccerBallObject.rigidbody.velocity.x * work;
		refData.y += this.soccerBallObject.rigidbody.velocity.y * work;
		refData.z += this.soccerBallObject.rigidbody.velocity.z * work;

		// ボールの未来予想座標を返す
		return refData;
	}


	//----------------------------------------------------------------------
	// アニメーション
	//----------------------------------------------------------------------
	// @Param	none		
	// @Return	none
	// @Date	2014/12/11  @Update 2014/12/11  @Author T.Kawashita      
	//----------------------------------------------------------------------
	private void Animation()
	{
		switch (m_status)
		{
			case CPlayerManager.ePLAYER_STATUS.eNONE:
			case CPlayerManager.ePLAYER_STATUS.eWAIT:
			case CPlayerManager.ePLAYER_STATUS.eCOUNTDOWN:
				m_animator.Move(m_speed);
				break;

			case CPlayerManager.ePLAYER_STATUS.ePASS:
				m_animator.Pass();
				break;
		}
	}

	/*取得関数*/
	public GK_State GetGKState()     { return this.gkState; }
	public Vector3 GetHomePosition() { return this.HOME_POSITION; }
}
