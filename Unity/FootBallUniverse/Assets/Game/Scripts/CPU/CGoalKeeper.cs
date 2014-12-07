using UnityEngine;
using System.Collections;

public class CGoalKeeper : CCpu {
	protected enum GK_State
	{
		STAY,
		ON_ALERT,
		TAKE_BALL,
		CAT,
		PASS,
		BACK_HOME,
		GK_STATE_MAX
	};

	protected GK_State gkState = GK_State.STAY;

	protected Vector3 HOME_POSITION = new Vector3(0.0f, 0.0f, 25.0f);

	protected const float ARAT_SPACE      = 8.0f;
	protected const float TAKE_BALL_SPACE = 5.0f;

	protected void CGoalKeeperInit()
	{
		this.Init();

		// プレイヤーのデータをセット
		CPlayerManager.SetPlayerData(this.m_playerData, CPlayerManager.AI_2);
		this.SetData();
		m_pos = this.transform.localPosition;

		// 国の情報をセット / 国によってマテリアルを変更
		m_human = CHumanManager.GetWorldInstance(TeamData.teamNationality[0]);
		this.transform.FindChild("polySurface14").GetComponent<CGoalKeeper1Mesh>().ChangeMaterial(TeamData.teamNationality[0]);

		Debug.Log(this.m_human.m_passInitSpeed);

		// サッカーボールの情報を取得
		this.soccerBallObject = GameObject.Find("SoccerBall");

		// プレイヤーのアニメーターをセット
		m_animator = this.gameObject.transform.parent.GetComponent<CPlayerAnimator>();

		// 向きをセット
		this.transform.LookAt(new Vector3(0.0f, 0.0f, 0.0f));
	}



	protected void CGoalKeeperUpdate()
	{
		if (m_isBall == true)
			this.transform.FindChild("SoccerBall").GetComponent<CSoccerBall>().SetPosition(new Vector3(0.0f, 0.05f, 0.1f));

		m_pos = this.transform.localPosition;

		switch (this.gkState)
		{
			case GK_State.STAY: Stay(); break;
			case GK_State.ON_ALERT: OnAlert(); break;
			case GK_State.TAKE_BALL: TakeBall(); break;
			case GK_State.CAT: Cach(); break;
			case GK_State.PASS: Pass(); break;
			case GK_State.BACK_HOME: BackHome(); break;
		}
	}



	protected void CGoalKeeperLateUpdate()
	{
		// アニメーション
		m_speed = new Vector3(0.0f, 0.0f, 0.0f);    // 最後にスピードを初期化
		this.rigidbody.MovePosition(m_pos);

		CCpuManager.m_cpuManager.m_cpuP1P2Keeper = this.transform;
	}



	void Stay()
	{
		BackHome();

		// ボールの監視
		if (Vector3.Distance(this.transform.position, this.soccerBallObject.transform.position) <= ARAT_SPACE)
		{
			this.gkState = GK_State.ON_ALERT;
		}
	}



	void OnAlert()
	{
		// ボールが範囲外に出たら待機へ戻る
		if (Vector3.Distance(this.transform.position, this.soccerBallObject.transform.position) >= ARAT_SPACE)
		{
			this.gkState = GK_State.STAY;
		}

		// ボールがフリーだと判断（→取りに行く）
		if (Vector3.Distance(this.transform.position, GameObject.Find("Player3").transform.FindChild("player").transform.position) >= ARAT_SPACE &&
			Vector3.Distance(this.transform.position, GameObject.Find("Player4").transform.FindChild("player").transform.position) >= ARAT_SPACE &&
			Vector3.Distance(this.transform.position, GameObject.Find("CPU2").transform.FindChild("cpu").transform.position) >= ARAT_SPACE &&
			Vector3.Distance(this.transform.position, GameObject.Find("GoalKeeper2").transform.FindChild("cpu").transform.position) >= ARAT_SPACE &&
			Vector3.Distance(this.transform.position, this.soccerBallObject.transform.position) <= TAKE_BALL_SPACE)
		{
			this.gkState = GK_State.TAKE_BALL;
		}
	}



	void TakeBall()
	{
		Vector3 targetPosition = new Vector3();

#if false
		// 本来のパス
		// ボールを取りに行く
		this.transform.LookAt(this.soccerBallObject.transform.position);
		Move(new Vector3(0.0f,0.0f,1.0f));
#else
		// ボールカット（仮用）
		targetPosition = GetFuterBallPosition();
		this.transform.LookAt(targetPosition);
		Move(new Vector3(0.0f, 0.0f, 1.0f));
		this.transform.LookAt(new Vector3(0.0f, 0.0f, 0.0f));
#endif
		Debug.Log(this.m_isBall);
		// ボールをキャッチ（→パス）
		if (this.m_isBall)
		{
			this.gkState = GK_State.CAT;
			this.transform.LookAt(GameObject.Find("Player1").transform.FindChild("player").transform.position);
			this.m_action.InitPass(this.m_human.m_passInitSpeed, this.m_human.m_passMotionLength, this.m_human.m_passTakeOfFrame);
		}
	}



	void Cach()
	{
		if (this.m_action.Pass(this.gameObject, this.transform.forward, ref this.m_isBall))
		{
			this.gkState = GK_State.PASS;
		}
	}



	void Pass()
	{
		//BackHome();

		if (Vector3.Distance(this.transform.position, this.soccerBallObject.transform.position) >= TAKE_BALL_SPACE)
		{
			this.gkState = GK_State.STAY;
		}
	}



	void BackHome()
	{
		if (this.transform.position != HOME_POSITION)
		{
			this.transform.LookAt(HOME_POSITION);
			Move(new Vector3(0.0f, 0.0f, 0.1f));
			this.transform.LookAt(new Vector3(0.0f, 0.0f, 0.0f));
		}
	}



	Vector3 GetFuterBallPosition()
	{
		Vector3 refData = new Vector3();
		refData = this.soccerBallObject.transform.position;
		float work;

		// rigidbodyがZ軸で移動していない（＝ボールがこないので無効）
		if (this.soccerBallObject.rigidbody.velocity.z == 0)
		{
			return new Vector3(0.0f, 0.0f, 0.0f);
		}

		work = HOME_POSITION.z - this.soccerBallObject.transform.position.z;
		work /= this.soccerBallObject.rigidbody.velocity.z;

		// ゴール指定地点まで到着するまで移動
		refData.x += this.soccerBallObject.rigidbody.velocity.x * work;
		refData.y += this.soccerBallObject.rigidbody.velocity.y * work;
		refData.z += this.soccerBallObject.rigidbody.velocity.z * work;

		// ボールの未来予想座標を返す
		return refData;
	}
}
