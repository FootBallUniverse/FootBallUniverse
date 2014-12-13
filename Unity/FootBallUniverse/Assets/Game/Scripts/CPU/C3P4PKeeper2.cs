using UnityEngine;
using System.Collections;

public class C3P4PKeeper2 : CGoalKeeper {
	//----------------------------------------------------------------------
	// コンストラクタ
	//----------------------------------------------------------------------
	// @Param   none
	// @Return  none
	// @Date    2014/12/1  @Update 2014/12/1  @Author T.Kawashita       
	//----------------------------------------------------------------------
	void Start()
	{
		this.enemyData[0] = GameObject.Find("Player1").transform.FindChild("player").gameObject;
		this.enemyData[1] = GameObject.Find("Player2").transform.FindChild("player").gameObject;
		this.enemyData[2] = GameObject.Find("CPU1").transform.FindChild("cpu").gameObject;
		this.enemyData[3] = GameObject.Find("GoalKeeper1").transform.FindChild("cpu").gameObject;

		this.frendryData[0] = GameObject.Find("Player3").transform.FindChild("player").gameObject;
		this.frendryData[1] = GameObject.Find("Player4").transform.FindChild("player").gameObject;
		this.frendryData[2] = GameObject.Find("CPU2").transform.FindChild("cpu").gameObject;

		this.CGoalKeeperInit(1);
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
		Debug.Log("gkState = " + this.gkState + " m_state = " + this.m_status);
		this.CGoalKeeperUpdate();
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
		CGoalKeeperLateUpdate();

        CCpuManager.m_cpuManager.m_cpuP3P4Keeper = this.transform;
	}
}
