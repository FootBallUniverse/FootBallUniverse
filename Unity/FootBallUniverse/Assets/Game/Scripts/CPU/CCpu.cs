using UnityEngine;
using System.Collections;

public class CCpu : CPlayer {
	protected GameObject soccerBallManager;

	//----------------------------------------------------------------------
	// コンストラクタ
	//----------------------------------------------------------------------
	// @Param	none		
	// @Return	none
	// @Date	2014/12/1  @Update 2014/12/1  @Author T.Kawashita      
	//----------------------------------------------------------------------
	void Start () {
		this.soccerBallManager = GameObject.Find("BallGameObject");


		this.Init();

		// プレイヤーのデータをセット
		CPlayerManager.m_playerManager.SetPlayerData(this.m_playerData, CPlayerManager.AI_1);
		this.SetData();

		m_pos = this.transform.localPosition;

		// 国の情報をセット / 国によってマテリアルを変更
		m_human = CHumanManager.GetWorldInstance(TeamData.teamNationality[0]);

		/*
		// プレイヤーの情報をマップにセット
		Color color = Color.red;
		CPlayerManager.m_playerManager.SetMap(this.gameObject, color);
		*/
		// プレイヤーのアニメーターをセット
		m_animator = this.gameObject.transform.parent.GetComponent<CPlayerAnimator>();

	}

	//----------------------------------------------------------------------
	// 更新
	//----------------------------------------------------------------------
	// @Param   none			
	// @Return	none
	// @Date	2014/12/1  @Update 2014/12/1  @Author T.Kawashita      
	//----------------------------------------------------------------------
	void Update () {
		
	}

	//----------------------------------------------------------------------
	// フレーム最後の更新
	//----------------------------------------------------------------------
	// @Param	none		
	// @Return	none
	// @Date	2014/12/1  @Update 2014/12/1  @Author T.Kawashita      
	//----------------------------------------------------------------------
	void LateUpdate(){
		// アニメーション
		/*
		this.Animation();
		*/
		m_speed = new Vector3(0.0f, 0.0f, 0.0f);    // 最後にスピードを初期化
		this.rigidbody.MovePosition(m_pos);

		// 最後に位置をマネージャークラスにセットしておく
		//CPlayerManager.m_playerManager.m_player1Transform = this.transform;

		// ゲームが終了しているかどうか判定
		this.CheckGamePlay();
	}


	//----------------------------------------------------------------------
	// 移動
	//----------------------------------------------------------------------
	// @Param	
	// @Return	none
	// @Date	2014/12/1  @Update 2014/12/1  @Author T.Takeuchi
	//----------------------------------------------------------------------
	public override void Move(Vector3 _speed)
	{
		// ボールを持っている場合は遅くなる
		if (m_isBall == true)
		{
			this.transform.FindChild("SoccerBall").GetComponent<CSoccerBall>().SetPosition(new Vector3(0.0f, 0.05f, 0.1f));
			m_speed.x += _speed.x * m_human.m_playerMoveSpeedHold;
			m_speed.z += _speed.z * m_human.m_playerMoveSpeedHold;
		}
		else
		{
			m_speed.x += _speed.x * m_human.m_playerMoveSpeed;
			m_speed.z += _speed.z * m_human.m_playerMoveSpeed;
		}

		m_pos = this.transform.localPosition;

		// 移動アクション
		m_action.Move(ref m_pos, m_speed, this.transform.forward, this.transform.right);
	}

	//----------------------------------------------------------------------
	// プレイヤーの回転
	//----------------------------------------------------------------------
	// @Param	Vector2     回転量		
	// @Return	none
	// @Date	2014/10/16  @Update 2014/11/12   @Author T.Kawashita      
	//----------------------------------------------------------------------
	public override void Rotation(Vector2 _angle)
	{
		Vector3 angle = new Vector3(0.0f, 0.0f, 0.0f);
		if (m_status == CPlayerManager.ePLAYER_STATUS.eNONE ||
			m_status == CPlayerManager.ePLAYER_STATUS.eCOUNTDOWN)
		{
			angle.y = _angle.x * m_human.m_cameraMoveSpeed;
			angle.x = _angle.y * m_human.m_cameraMoveSpeed;
		}
		else if (m_status == CPlayerManager.ePLAYER_STATUS.eSHOOTCHARGE ||
				 m_status == CPlayerManager.ePLAYER_STATUS.eDASHCHARGE)
		{
			angle.y = _angle.x * m_human.m_cameraMoveSpeedCharging;
			angle.x = _angle.y * m_human.m_cameraMoveSpeedCharging;
		}

		this.transform.Rotate(angle);
	}


	//----------------------------------------------------------------------
	// 目的地へ自動移動
	//----------------------------------------------------------------------
	// @Param	targetPosition
	// @Return	none
	// @Date	2014/12/1  @Update 2014/12/1  @Author T.Takeuchi
	//----------------------------------------------------------------------
	public void MoveAuto(Vector3 targetPosition)
	{
		
	}
}
