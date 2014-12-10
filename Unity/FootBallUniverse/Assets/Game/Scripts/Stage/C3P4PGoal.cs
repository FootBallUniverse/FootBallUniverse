using UnityEngine;
using System.Collections;

public class C3P4PGoal : MonoBehaviour {

    //----------------------------------------------------------------------
    // コンストラクタ
    //----------------------------------------------------------------------
    // @Param   none		
    // @Return	none
    // @Date	2014/10/30  @Update 2014/10/30  @Author T.Kaneko      
    //----------------------------------------------------------------------
	void Start () {
		
	}

    //----------------------------------------------------------------------
    // 更新
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/10/30  @Update 2014/10/30  @Author T.Kaneko      
    //----------------------------------------------------------------------
	void Update () {
		
	}

    //----------------------------------------------------------------------
    // 3P4Pゴールにボールが当たった時の判定
    //----------------------------------------------------------------------
    // @Param	Collision   当たったオブジェクト		
    // @Return	none
    // @Date	2014/10/30  @Update 2014/10/30  @Author T.Kaneko      
    //----------------------------------------------------------------------
	private void OnCollisionEnter(Collision collision)
	{

	}

	//----------------------------------------------------------------------
	// ゴールのメッシュに当たったらその時点でゴール
	//----------------------------------------------------------------------
	// @Param	collider    当たったオブジェクトの当たり判定		
	// @Return	none
	// @Date	2014/12/9  @Update 2014/12/9  @Author T.Kawashita      
	//----------------------------------------------------------------------
	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject == GameObject.Find ("SoccerBall"))
		{
			// グローバルのゴールのデータに追加
			TeamData.AddLog(CGameManager.m_nowTime, CSoccerBallManager.m_shootPlayerNo,CSoccerBallManager.m_shootTeamNo, 1, true);
			CGameManager.m_redPoint++;
			
			CGameManager.m_nowStatus = CGameManager.eSTATUS.eGOAL;
		}
	}
}
