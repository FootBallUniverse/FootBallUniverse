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

	GK_State gkState = GK_State.STAY;

	//----------------------------------------------------------------------
	// コンストラクタ
	//----------------------------------------------------------------------
	// @Param   none			
	// @Return	none
	// @Date	2014/12/1  @Update 2014/12/1  @Author T.Kawashita       
	//----------------------------------------------------------------------
	void Start () {
	}

	//----------------------------------------------------------------------
	// 更新
	//----------------------------------------------------------------------
	// @Param   none			
	// @Return	none
	// @Date	2014/12/1  @Update 2014/12/1  @Author T.Kawashita      
	//----------------------------------------------------------------------
	void Update () {
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
		CCpuManager.m_cpuManager.m_cpuP1P2Keeper = this.transform;
	}



	void Stay()
	{
		// 中心地へ移動（待機）

		// 遷移
		// 条件： 敵がボールを持ってる
		//        ボールが範囲内にきた
		if(GameObject.Find("Player" + CSoccerBallManager.m_nowPlayer))
		{
			this.gkState = GK_State.ON_ALERT;
		}
	}










	void OnAlert()
	{

	}

	void Pass()
	{
	}
}
