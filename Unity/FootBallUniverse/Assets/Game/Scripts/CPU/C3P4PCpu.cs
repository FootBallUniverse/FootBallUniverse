using UnityEngine;
using System.Collections;

public class C3P4PCpu : CCpu {

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

        // プレイヤーのデータをセット
        CPlayerManager.SetPlayerData(this.m_playerData, CPlayerManager.AI_3);
        this.SetData();
        m_pos = this.transform.localPosition;

        // 国の情報をセット / 国によってマテリアルを変更
        m_human = CHumanManager.GetWorldInstance(TeamData.teamNationality[1]);
        this.transform.FindChild("polySurface14").GetComponent<CCpu2Mesh>().ChangeMaterial(TeamData.teamNationality[1]);

        Debug.Log(this.m_human.m_passInitSpeed);

        // サッカーボールの情報を取得
        this.soccerBallObject = GameObject.Find("SoccerBall");

        // プレイヤーのアニメーターをセット
        m_animator = this.gameObject.transform.parent.GetComponent<CPlayerAnimator>();
	}


	//----------------------------------------------------------------------
	// 更新
	//----------------------------------------------------------------------
	// @Param	none		
	// @Return	none
	// @Date	2014/12/1  @Update 2014/12/1  @Author T.Kawashita      
	//----------------------------------------------------------------------
	void Update()
	{
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
	    CCpuManager.m_cpuManager.m_cpuP3P4 = this.transform;
	}
}
