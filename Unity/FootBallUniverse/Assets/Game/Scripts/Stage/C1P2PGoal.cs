﻿using UnityEngine;
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
			Debug.Log ("1P&2Pゴール!");
            CGameManager.m_isPoint[0] += 1;
            
            Debug.Log("1P&2P:" + CGameManager.m_isPoint[0] + " 3P&4P:" + CGameManager.m_isPoint[1] );
		}
	}
}
