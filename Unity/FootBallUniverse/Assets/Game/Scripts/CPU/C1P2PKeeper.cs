using UnityEngine;
using System.Collections;

public class C1P2PKeeper : CCpu {
	enum GK_State
	{
		STAY,
		ON_ALERT,
		PASS,
		GK_STATE_MAX
	};

	GK_State gkState = GK_State.ON_ALERT;
	
	Vector3 HOME_POSITION = new Vector3(0.0f, 0.0f, 25.0f);

	//----------------------------------------------------------------------
	// コンストラクタ
	//----------------------------------------------------------------------
	// @Param   none			
	// @Return	none
	// @Date	2014/12/1  @Update 2014/12/1  @Author T.Kawashita       
	//----------------------------------------------------------------------
	void Start () {
		this.Init();

		// プレイヤーのデータをセット
		CPlayerManager.m_playerManager.SetPlayerData(this.m_playerData, CPlayerManager.AI_2);
		this.SetData();

		m_pos = this.transform.localPosition;

		// 国の情報をセット / 国によってマテリアルを変更
		m_human = CHumanManager.GetWorldInstance(TeamData.teamNationality[0]);

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

		//this.Move(new Vector3(0.0f, 0.1f, 0.0f));
		switch (this.gkState)
		{
			case GK_State.STAY:
				Stay();
				break;
			case GK_State.ON_ALERT:
				OnAlert();
				break;
			case GK_State.PASS:
				Pass();
				break;
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

		m_speed = new Vector3(0.0f, 0.0f, 0.0f);    // 最後にスピードを初期化
		this.rigidbody.MovePosition(m_pos);
		
		CCpuManager.m_cpuManager.m_cpuP1P2Keeper = this.transform;
	}



	void Stay()
	{
		// 中心地へ移動（待機）

		// 遷移
		// 条件： 敵がボールを持ってる
		//        ボールが範囲内にきた
	}

	void ReturnHome()
	{
	}

	void OnAlert()
	{
	}

	void CatchBall()
	{
	}

	void Pass()
	{
		// 評価システム
		// パス相手選定
		// パスを行う
	}
}
