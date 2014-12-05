using UnityEngine;
using System.Collections;

public class C1P2PKeeper : CCpu {
	enum GK_State
	{
		STAY,
		ON_ALERT,
		TAKE_BALL,
		CAT,
		PASS,
		BACK_HOME,
		GK_STATE_MAX
	};

	GK_State gkState = GK_State.STAY;
	
	Vector3 HOME_POSITION = new Vector3(0.0f, 0.0f, 25.0f);

	const float ARAT_SPACE      = 10.0f;
	const float TAKE_BALL_SPACE =  8.0f;

	//----------------------------------------------------------------------
	// コンストラクタ
	//----------------------------------------------------------------------
	// @Param   none
	// @Return  none
	// @Date    2014/12/1  @Update 2014/12/1  @Author T.Kawashita       
	//----------------------------------------------------------------------
	void Start () {
		this.Init();

		// プレイヤーのデータをセット
		CPlayerManager.m_playerManager.SetPlayerData(this.m_playerData, CPlayerManager.AI_2);
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

	}

	//----------------------------------------------------------------------
	// 更新
	//----------------------------------------------------------------------
	// @Param   none			
	// @Return	none
	// @Date	2014/12/1  @Update 2014/12/1  @Author T.Kawashita      
	//----------------------------------------------------------------------
	void Update () {

		if (m_isBall == true)
			this.transform.FindChild("SoccerBall").GetComponent<CSoccerBall>().SetPosition(new Vector3(0.0f, 0.05f, 0.1f));

		m_pos = this.transform.localPosition;


		//Debug.Log(this.gkState);
		switch (this.gkState)
		{
			case GK_State.STAY:      Stay();     break;
			case GK_State.ON_ALERT:  OnAlert();  break;
			case GK_State.TAKE_BALL: TakeBall(); break;
			case GK_State.CAT:       Cach();      break;
			case GK_State.PASS:      Pass();     break;
			case GK_State.BACK_HOME: BackHome(); break;
		}
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
		m_speed = new Vector3(0.0f, 0.0f, 0.0f);    // 最後にスピードを初期化
		this.rigidbody.MovePosition(m_pos);
		
		CCpuManager.m_cpuManager.m_cpuP1P2Keeper = this.transform;
	}


	void Stay()
	{
		BackHome();

		// ボールの監視
		if(Vector3.Distance(this.transform.position,this.soccerBallObject.transform.position) <= ARAT_SPACE)
		{
			this.gkState = GK_State.ON_ALERT;
		}
	}

	void OnAlert()
	{
		// ボールが範囲外に出たら待機へ戻る
		if (Vector3.Distance(this.transform.position, this.soccerBallObject.transform.position) >= ARAT_SPACE)
		{
			this.gkState = GK_State.STAY ;
		}

		// ボールがフリーだと判断（→取りに行く）
		if (Vector3.Distance(this.transform.position, GameObject.Find("Player3").transform.FindChild("player").transform.position)  >= ARAT_SPACE &&
			Vector3.Distance(this.transform.position, GameObject.Find("Player4").transform.FindChild("player").transform.position)  >= ARAT_SPACE &&
			Vector3.Distance(this.transform.position, GameObject.Find("CPU2").transform.FindChild("cpu").transform.position)        >= ARAT_SPACE &&
			Vector3.Distance(this.transform.position, GameObject.Find("GoalKeeper1").transform.FindChild("cpu").transform.position) >= ARAT_SPACE &&
			Vector3.Distance(this.transform.position, this.soccerBallObject.transform.position)                                     <= TAKE_BALL_SPACE)
		{
			this.gkState = GK_State.TAKE_BALL;
		}
	}

	void TakeBall()
	{
		// ボールを取りに行く
		this.transform.LookAt(this.soccerBallObject.transform.position);
		Move(new Vector3(0.0f,0.0f,1.0f));

		// ボールをキャッチ（→パス）
		if (this.m_isBall)
		{
			this.transform.LookAt(GameObject.Find("Player1").transform.FindChild("player").transform.position);
			this.m_action.InitPass(this.m_human.m_passInitSpeed, this.m_human.m_passMotionLength, this.m_human.m_passTakeOfFrame);
			this.m_action.Pass(this.gameObject, this.transform.forward, ref this.m_isBall);
		}
	}

	void Cach()
	{
	}

	void Pass()
	{
		this.gkState = GK_State.PASS;

		// パス後しばらくボールをとらない
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
			this.Move(new Vector3(0.0f, 0.0f, 1.0f));
		}
	}
}
