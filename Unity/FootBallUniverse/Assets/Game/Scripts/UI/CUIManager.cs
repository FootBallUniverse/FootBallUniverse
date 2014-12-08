﻿using UnityEngine;
using System.Collections;

//----------------------------------------------------------------------
// UI用のマネージャクラス
//----------------------------------------------------------------------
// @Date	2014/11/18  @Update 2014/11/18  @Author T.Kawashita      
//----------------------------------------------------------------------
public class CUIManager : MonoBehaviour {

    // UIに何を表示するのかどうか
    public enum eUISTATUS
    {
        eNONE,
        eWAIT,
        eFADEIN,
        eFADEOUT,
        eCOUNTDOWN,
        eGAME,
        eENDWAIT,
        eGOAL,
        eGOALFADEOUT,
        eGOALFADEIN,
    }
    public eUISTATUS m_uiStatus;               // UIの状態

    public GameObject m_gameObjectP1P2;        // P1P2UI用GameObject
    public GameObject m_gameObjectP3P4;        // P3P4UI用GameObject
    public GameObject m_uiPanelP1P2;           // P1P2UI用パネル
    public GameObject m_uiPanelP3P4;           // P3P4UI用パネル

    // UIの時間用
    public UISprite m_TimeMinP1P2;
    public UISprite m_TimeTenSecP1P2;
    public UISprite m_TimeSecP1P2;

    public UISprite m_TimeMinP3P4;
    public UISprite m_TimeTenSecP3P4;
    public UISprite m_TimeSecP3P4;

    //----------------------------------------------------------------------
    // コンストラクタ
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/12/8  @Update 2014/12/8  @Author T.Kawashita   
    //----------------------------------------------------------------------
	void Start () {
        
        // パネルを取得
        m_uiPanelP1P2 = GameObject.Find("P1&P2").transform.FindChild("UI").transform.FindChild("Camera").transform.FindChild("Anchor").transform.FindChild("Panel").gameObject;
        m_uiPanelP3P4 = GameObject.Find("P3&P4").transform.FindChild("UI").transform.FindChild("Camera").transform.FindChild("Anchor").transform.FindChild("Panel").gameObject;

        // フェードイン・フェードアウト用ゲームオブジェクト作成
        m_gameObjectP1P2 = m_uiPanelP1P2.transform.FindChild("BlackOut").gameObject;
        m_gameObjectP3P4 = m_uiPanelP3P4.transform.FindChild("BlackOut").gameObject;


        m_TimeMinP1P2 = m_uiPanelP1P2.transform.FindChild("time_min").gameObject.GetComponent<UISprite>();
        m_TimeTenSecP1P2 = m_uiPanelP1P2.transform.FindChild("time_tensec").gameObject.GetComponent<UISprite>();
        m_TimeSecP1P2 = m_uiPanelP1P2.transform.FindChild("time_sec").gameObject.GetComponent<UISprite>();

        m_TimeMinP3P4 = m_uiPanelP3P4.transform.FindChild("time_min").gameObject.GetComponent<UISprite>();
        m_TimeTenSecP3P4 = m_uiPanelP3P4.transform.FindChild("time_tensec").gameObject.GetComponent<UISprite>();
        m_TimeSecP3P4 = m_uiPanelP3P4.transform.FindChild("time_sec").gameObject.GetComponent<UISprite>();

        // 最初の時間設定
        CalcTime();

        m_uiStatus = eUISTATUS.eWAIT;

	}

    //----------------------------------------------------------------------
    // 更新
    //----------------------------------------------------------------------
    // @Param   none			
    // @Return	none
    // @Date	2014/12/8  @Update 2014/12/8  @Author T.Kawashita      
    //----------------------------------------------------------------------
	void Update () {

        // 今のゲームの状態によってUIを切り替える
        switch (CGameManager.m_nowStatus)
        {
            // 待機中状態
            case CGameManager.eSTATUS.eWAIT:
                switch (m_uiStatus)
                {
                    case eUISTATUS.eWAIT:
                        m_gameObjectP1P2.AddComponent<CFadeIn>(); // フェードイン用スクリプト追加
                        m_gameObjectP3P4.AddComponent<CFadeIn>();
                        m_uiStatus = eUISTATUS.eFADEIN;
                        break;

                    case eUISTATUS.eFADEIN:
                        if (m_gameObjectP1P2 == false && m_gameObjectP3P4 == false)
                        {
                            m_gameObjectP1P2 = (GameObject)Instantiate(Resources.Load("Prefab/Game/CountDownManager"));
                            m_gameObjectP3P4 = (GameObject)Instantiate(Resources.Load("Prefab/Game/CountDownManager"));
                            m_gameObjectP1P2.transform.parent = m_uiPanelP1P2.transform;
                            m_gameObjectP3P4.transform.parent = m_uiPanelP3P4.transform;
                            m_uiStatus = eUISTATUS.eCOUNTDOWN;
                            CGameManager.m_nowStatus = CGameManager.eSTATUS.eCOUNTDOWN;
                        }
                        break;
                }
                break;

            // カウントダウン中
            case CGameManager.eSTATUS.eCOUNTDOWN:
                if (m_gameObjectP1P2 == false && m_gameObjectP3P4 == false)
                {
                    m_uiStatus = eUISTATUS.eGAME;
                    CGameManager.m_nowStatus = CGameManager.eSTATUS.eGAME;
                }
                break;

            // ゲーム中
            case CGameManager.eSTATUS.eGAME:
                CalcTime();
                break;

            // ゴールした後のUI
            case CGameManager.eSTATUS.eGOAL:
                m_uiStatus = eUISTATUS.eGOAL;
                break;

            // ゲーム終了時のフェードアウト
            case CGameManager.eSTATUS.eFADEOUT:
                switch (m_uiStatus)
                {
                    case eUISTATUS.eGAME:
                        // ゲーム中の状態から遷移した場合はフェードアウトの準備をする
                        m_gameObjectP1P2 = (GameObject)Instantiate(Resources.Load("Prefab/Game/BlackOut"));
                        m_gameObjectP3P4 = (GameObject)Instantiate(Resources.Load("Prefab/Game/BlackOut"));
                        m_gameObjectP1P2.AddComponent<CFadeOut>();
                        m_gameObjectP3P4.AddComponent<CFadeOut>();
                        m_gameObjectP1P2.transform.parent = m_uiPanelP1P2.transform;
                        m_gameObjectP3P4.transform.parent = m_uiPanelP3P4.transform;
                        m_uiStatus = eUISTATUS.eFADEOUT;
                        break;

                    case eUISTATUS.eFADEOUT:
                        // フェードアウトが終了したらゲームを終了させる
                        if (m_gameObjectP1P2.GetComponent<TweenAlpha>().enabled == false)
                        {
                            CGameManager.m_nowStatus = CGameManager.eSTATUS.eEND;
                        }
                        break;
                }
                break;

            // 終了状態
            case CGameManager.eSTATUS.eEND:
                break;

            // ゴールの待機状態に変わった場合
            case CGameManager.eSTATUS.eGOALWAIT:
                switch (m_uiStatus)
                {
                    case eUISTATUS.eGOAL:
                        m_gameObjectP1P2 = (GameObject)Instantiate(Resources.Load("Prefab/Game/BlackOut"));
                        m_gameObjectP3P4 = (GameObject)Instantiate(Resources.Load("Prefab/Game/BlackOut"));
                        m_gameObjectP1P2.AddComponent<CFadeOut>();
                        m_gameObjectP3P4.AddComponent<CFadeOut>();
                        m_gameObjectP1P2.transform.parent = m_uiPanelP1P2.transform;
                        m_gameObjectP3P4.transform.parent = m_uiPanelP3P4.transform;
                        m_uiStatus = eUISTATUS.eGOALFADEOUT;
                        break;

                    case eUISTATUS.eGOALFADEOUT:
                        // フェードアウトが終了したらフェードインスタート
                        if (m_gameObjectP1P2.GetComponent<TweenAlpha>().enabled == false)
                        {
                            m_uiStatus = eUISTATUS.eGOALFADEIN;
                            GameObject.Destroy(m_gameObjectP1P2);
                            GameObject.Destroy(m_gameObjectP3P4);
                            m_gameObjectP1P2 = (GameObject)Instantiate(Resources.Load("Prefab/Game/BlackOut"));
                            m_gameObjectP3P4 = (GameObject)Instantiate(Resources.Load("Prefab/Game/BlackOut"));
                            m_gameObjectP1P2.AddComponent<CFadeIn>();
                            m_gameObjectP3P4.AddComponent<CFadeIn>();
                            m_gameObjectP1P2.transform.parent = m_uiPanelP1P2.transform;
                            m_gameObjectP3P4.transform.parent = m_uiPanelP3P4.transform;
                            CGameManager.m_nowStatus = CGameManager.eSTATUS.eRESTART;   // リスタート状態に変更
                        }
                        break;
                }
                break;

            // リスタート
            case CGameManager.eSTATUS.eRESTART:
                // フェードインが終了したらゲームの待機状態を変更
                if (m_gameObjectP1P2.GetComponent<TweenAlpha>().enabled == false)
                {
                    m_uiStatus = eUISTATUS.eGAME;
                    CGameManager.m_nowStatus = CGameManager.eSTATUS.eGAME;
                }
                break;
        }
	}

    //----------------------------------------------------------------------
    // 時間を計算して表示
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/12/8  @Update 2014/12/8  @Author T.Kawashita      
    //----------------------------------------------------------------------
    private void CalcTime()
    {
        int minTime = 0;
        int tenSecTime = 0;
        int secTime = 0;

        if (CGameManager.m_nowTime >= 600)
        {
        }

        minTime = (int)(CGameData.m_gamePlayTime / 60);
        tenSecTime = (int)((CGameData.m_gamePlayTime - (minTime * 60)) / 10);
        secTime = (int)((CGameData.m_gamePlayTime - (minTime * 60)) % 10);

        m_TimeMinP1P2.spriteName = "num_" + minTime.ToString();
        m_TimeTenSecP1P2.spriteName = "num_" + tenSecTime.ToString();
        m_TimeSecP1P2.spriteName = "num_" + secTime.ToString();

        m_TimeMinP3P4.spriteName = "num_" + minTime.ToString();
        m_TimeTenSecP3P4.spriteName = "num_" + tenSecTime.ToString();
        m_TimeSecP3P4.spriteName = "num_" + secTime.ToString();

    }
}
