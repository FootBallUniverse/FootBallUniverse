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
		if (collision.gameObject == GameObject.Find ("SoccerBall"))
        {
            // グローバルのゴールのデータに追加
            TeamData.AddLog(CGameManager.m_nowTime, CSoccerBallManager.m_shootPlayerNo,CSoccerBallManager.m_shootTeamNo, 1, true);
            CGameManager.m_redPoint++;

            if (collision.gameObject.transform.parent.name != "BallGameObject")
            {
                collision.transform.parent.gameObject.GetComponent<CPlayer>().m_isBall = false;
            }

            collision.gameObject.GetComponent<CSoccerBall>().Restart(new Vector3(0.0f, 0.0f, 0.0f));
            CGameManager.m_nowStatus = CGameManager.eSTATUS.eGOAL;
        }
	}
}
