﻿using UnityEngine;
using System.Collections;

public class CSoccerBallManager : MonoBehaviour {

    // サッカーボールのインスタンス
    public GameObject m_soccerBall;
    public bool m_isStartGame;

    //----------------------------------------------------------------------
    // コンストラクタ
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/11/11  @Update 2014/11/11  @Author T.Kawashita      
    //----------------------------------------------------------------------
	void Start () {

        m_isStartGame = false;

        // サッカーボールをセット
        m_soccerBall = this.gameObject.transform.FindChild("SoccerBall").gameObject;
	}

    //----------------------------------------------------------------------
    // 更新
    //----------------------------------------------------------------------
    // @Param   none			
    // @Return	none
    // @Date	2014/11/11  @Update 2014/11/11  @Author T.Kawashita      
    //----------------------------------------------------------------------
	void Update () {
        if (CGameManager.m_nowStatus == CGameManager.eSTATUS.eGAME && m_isStartGame == false)
        {
            m_soccerBall.GetComponent<CSoccerBall>().StartGame();
            m_isStartGame = true;
        }
	}

    //----------------------------------------------------------------------
    // サッカーボールの持ち主を変更
    //----------------------------------------------------------------------
    // @Param	Transform   親
	// @Param   Vector3     設定したい位置	
    // @Return	bool        成功か失敗
    // @Date	2014/11/11  @Update 2014/11/11  @Author T.Kawashita       
    //----------------------------------------------------------------------
    public bool ChangeOwner(Transform _parent,Vector3 _pos)
    {
        // サッカーボール自体をプレイヤーを親にする
        m_soccerBall.transform.parent = _parent; 

        // サッカーボールの位置変更
        m_soccerBall.GetComponent<CSoccerBall>().Init(_pos);

        return true;
    }
}
