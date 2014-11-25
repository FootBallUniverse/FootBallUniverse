using UnityEngine;
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
    public eUISTATUS m_uiStatus;           // UIの状態

    public GameObject m_gameObject;        // UI用GameObject
    public GameObject m_uiPanel;           // UI用パネル

	// Use this for initialization
	void Start () {
        
        // フェードイン・フェードアウト用ゲームオブジェクト作成
        m_gameObject = (GameObject)Instantiate(Resources.Load("Prefab/Game/BlackOut"));
        
        // パネルを取得
        m_uiPanel = this.transform.FindChild("Camera").transform.FindChild("Anchor").transform.FindChild("Panel").gameObject;
        m_gameObject.transform.parent = m_uiPanel.transform;

        m_uiStatus = eUISTATUS.eWAIT;

	}
	
	// Update is called once per frame
	void Update () {

        // 今のゲームの状態によってUIを切り替える
        switch (CGameManager.m_nowStatus)
        {
           // 待機中状態
           case CGameManager.eSTATUS.eWAIT:
                switch (m_uiStatus)
                {
                    case eUISTATUS.eWAIT:
                        m_gameObject.AddComponent<CFadeIn>(); // フェードイン用スクリプト追加
                        m_uiStatus = eUISTATUS.eFADEIN;
                        break;

                    case eUISTATUS.eFADEIN:
                        if (m_gameObject == false)
                        {
                            m_gameObject = (GameObject)Instantiate(Resources.Load("Prefab/Game/CountDownManager"));
                            m_gameObject.transform.parent = m_uiPanel.transform;
                            m_uiStatus = eUISTATUS.eCOUNTDOWN;
                            CGameManager.m_nowStatus = CGameManager.eSTATUS.eCOUNTDOWN;
                        }
                        break;
                }

                break;

            // カウントダウン中
            case CGameManager.eSTATUS.eCOUNTDOWN:
                if (m_gameObject == false)
                {
                    m_uiStatus = eUISTATUS.eGAME;
                    CGameManager.m_nowStatus = CGameManager.eSTATUS.eGAME;
                }
                break;

           // ゲーム中
           case CGameManager.eSTATUS.eGAME:
                break;

            // ゴールした後のUI
            case CGameManager.eSTATUS.eGOAL:
                m_uiStatus = eUISTATUS.eGOAL;
                break;

            case CGameManager.eSTATUS.eFADEOUT:
                switch (m_uiStatus)
                {   
                    case eUISTATUS.eGAME:
                        // ゲーム中の状態から遷移した場合はフェードアウトの準備をする
                        m_gameObject = (GameObject)Instantiate(Resources.Load("Prefab/Game/BlackOut"));
                        m_gameObject.AddComponent<CFadeOut>();
                        m_gameObject.transform.parent = m_uiPanel.transform;
                        m_uiStatus = eUISTATUS.eFADEOUT;
                        break;

                    case eUISTATUS.eFADEOUT:
                        // フェードアウトが終了したらゲームを終了させる
                        if (m_gameObject.GetComponent<TweenAlpha>().enabled == false)
                        {
                            CGameManager.m_nowStatus = CGameManager.eSTATUS.eEND;
                        }   
                        break;

                }
                break;

            case CGameManager.eSTATUS.eEND:
                break;

            // ゴールの待機状態に変わった場合
            case CGameManager.eSTATUS.eGOALWAIT:
                switch (m_uiStatus)
                {
                    case eUISTATUS.eGOAL:
                        m_gameObject = (GameObject)Instantiate(Resources.Load("Prefab/Game/BlackOut"));
                        m_gameObject.AddComponent<CFadeOut>();
                        m_gameObject.transform.parent = m_uiPanel.transform;
                        m_uiStatus = eUISTATUS.eGOALFADEOUT;
                        break;

                    case eUISTATUS.eGOALFADEOUT:
                        // フェードアウトが終了したらフェードインスタート
                        if (m_gameObject.GetComponent<TweenAlpha>().enabled == false)
                        {
                            m_uiStatus = eUISTATUS.eGOALFADEIN;
                            GameObject.Destroy(m_gameObject);
                            m_gameObject = (GameObject)Instantiate(Resources.Load("Prefab/Game/BlackOut"));
                            m_gameObject.AddComponent<CFadeIn>();
                            m_gameObject.transform.parent = m_uiPanel.transform;
                        }
                        break;

                    case eUISTATUS.eGOALFADEIN:
                        // フェードインが終了したらゲームの待機状態を変更
                        if (m_gameObject.GetComponent<TweenAlpha>().enabled == false)
                        {
                            m_uiStatus = eUISTATUS.eGAME;
                            CGameManager.m_nowStatus = CGameManager.eSTATUS.eGAME;
                        }
                        break;
                }

                break;
        }
	}
}
