using UnityEngine;
using System.Collections;

public class C3P4Pkeeper : CCpu {
	enum GK_State
	{
		STAY,
		ON_ALERT,
		PASS,
		GK_STATE_MAX
	};

	GK_State gkState = GK_State.ON_ALERT;

	//----------------------------------------------------------------------
	// コンストラクタ
	//----------------------------------------------------------------------
	// @Param   none			
	// @Return	none
	// @Date	2014/12/1  @Update 2014/12/1  @Author T.Kawashita       
	//----------------------------------------------------------------------
	void Start()
	{


		this.Init();
		m_pos = this.transform.localPosition;
	}

	//----------------------------------------------------------------------
	// 更新
	//----------------------------------------------------------------------
	// @Param   none			
	// @Return	none
	// @Date	2014/12/1  @Update 2014/12/1  @Author T.Kawashita      
	//----------------------------------------------------------------------
	void Update()
	{

		//m_pos = this.transform.localPosition;

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
	void LateUpdate()
	{

		m_speed = new Vector3(0.0f, 0.0f, 0.0f);    // 最後にスピードを初期化
		this.rigidbody.MovePosition(m_pos);

		CCpuManager.m_cpuManager.m_cpuP3P4Keeper = this.transform;
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
		//
		//this.Move(new Vector3(0.01f, 0.01f, 0.01f));
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
