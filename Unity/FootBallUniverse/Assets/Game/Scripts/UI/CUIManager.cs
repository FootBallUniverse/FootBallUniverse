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
    }
    public eUISTATUS m_uiStatus;           // UIの状態

    private float m_gameFrame;              // フレーム調整用
    public GameObject m_gameObject;         // UI用GameObject
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

        m_gameFrame += Time.deltaTime;

        // 今のゲームの状態によってUIを切り替える
        switch (CGameManager.m_nowStatus)
        {
           // 待機中状態
           case CGameManager.eSTATUS.eWAIT:
                if (m_gameFrame >= 1.0f)
                {
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

                    m_gameFrame = 0.0f;
                }

                break;

            case CGameManager.eSTATUS.eCOUNTDOWN:
                if (m_gameFrame >= 1.0f && m_gameObject == false)
                {
                    m_uiStatus = eUISTATUS.eGAME;
                    CGameManager.m_nowStatus = CGameManager.eSTATUS.eGAME;
                    m_gameFrame = 0.0f;
                }
                break;

           case CGameManager.eSTATUS.eGAME:
                break;

            case CGameManager.eSTATUS.eEND:
                break;
        }
	}
}
