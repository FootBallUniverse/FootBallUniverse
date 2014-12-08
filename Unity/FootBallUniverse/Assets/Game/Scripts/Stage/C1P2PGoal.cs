using UnityEngine;
using System.Collections;


//----------------------------------------------------------------------
// C1P2PGoal
//----------------------------------------------------------------------
// @Info    1Pと2Pのゴール
// @Date	2014/10/30  @Update 2014/10/30  @Author T.Kaneko      
//----------------------------------------------------------------------
public class C1P2PGoal : MonoBehaviour {

    //----------------------------------------------------------------------
    // コンストラクタ
    //----------------------------------------------------------------------
    // @Param	none		
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
    // 1P2Pゴールにボールが当たった時の判定
    //----------------------------------------------------------------------
    // @Param	Collision   当たったオブジェクト	
    // @Return	none
    // @Date	2014/10/30  @Update 2014/010/30  @Author T.Kaneko      
    //----------------------------------------------------------------------
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject == GameObject.Find ("SoccerBall")) 
        {
            // グローバルのゴールのデータに追加
            TeamData.AddLog(CGameManager.m_nowTime, CSoccerBallManager.m_shootPlayerNo, CSoccerBallManager.m_shootTeamNo, 0, true);
            CGameManager.m_bluePoint++;

            if (collision.gameObject.transform.parent.name != "BallGameObject")
            {
                collision.transform.parent.gameObject.GetComponent<CPlayer>().m_isBall = false;
            }
            collision.gameObject.GetComponent<CSoccerBall>().Restart(new Vector3(0.0f, 0.0f, 0.0f));
            CGameManager.m_nowStatus = CGameManager.eSTATUS.eGOAL;
        }
	}
}
