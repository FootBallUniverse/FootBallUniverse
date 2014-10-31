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
			Debug.Log ("3P4Pゴール！");
            CGameManager.m_isPoint[1] += 1;

            Debug.Log("1P&2P:" + CGameManager.m_isPoint[0] + " 3P&4P:" + CGameManager.m_isPoint[1]);
		}
	}
}
