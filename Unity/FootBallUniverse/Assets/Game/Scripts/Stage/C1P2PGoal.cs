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
        if (collider.transform.tag == "SoccerBall")
        {
			// グローバルのゴールのデータに追加
			TeamData.AddLog(CGameManager.m_nowTime, CSoccerBallManager.m_shootPlayerNo, CSoccerBallManager.m_shootTeamNo, 0, true);
			CGameManager.m_bluePoint++;
			
			CGameManager.m_nowStatus = CGameManager.eSTATUS.eGOAL;

            // サポーター追加
            int redSupporter = 0;
            int blueSupporter = 0;

            // オウンゴールではない場合
            if (CSoccerBallManager.m_shootTeamNo != 1)
            {
                // 同点に追いつくシュート
                if (TeamData.GetTeamScore(0) == TeamData.GetTeamScore(1))
                    blueSupporter += CSupporterData.m_getDrawPointSupporter;

                // 同点から逆転するシュート
                else if (TeamData.GetTeamScore(1) - 1 == TeamData.GetTeamScore(0))
                    blueSupporter += CSupporterData.m_getDrawReversPointSupporter;

                blueSupporter += CSupporterData.m_getPointSupporter;

            }

            // オウンゴールの場合は点数は入る
            else if (CSoccerBallManager.m_shootTeamNo == 2)
            {
                redSupporter += CSupporterData.m_getPointSupporter;
            }

            // 最後にサポーター追加
            CSupporterManager.AddSupporter(redSupporter, blueSupporter, true);

        }

    }
}
