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
        eGOALWAIT,
        eGOALFADEOUT,
        eGOALFADEIN,
    }
    public static eUISTATUS m_uiStatus;               // UIの状態

    // どっちのゴールに入れたか
    public enum ePOINTSTATUS
    {
        RED,
        BLUE
    };

	public GameObject m_gameObjectMain;        // Main用のGameObject
    public GameObject m_gameObjectP1P2;        // P1P2UI用GameObject
    public GameObject m_gameObjectP3P4;        // P3P4UI用GameObject

    public GameObject m_blackoutMain;          // Mainブラックアウト用パネル
    public GameObject m_blackoutP1P2;          // P1P2ブラックアウト用パネル
    public GameObject m_blackoutP3P4;          // P3P4ブラックアウト用パネル
    
    public GameObject m_uiPanelMain;           // MainUI用パネル
    public GameObject m_uiPanelP1P2;           // P1P2UI用パネル
    public GameObject m_uiPanelP3P4;           // P3P4UI用パネル

    public GameObject m_goalPanelMain;
    public GameObject m_goalPanelP1P2;
    public GameObject m_goalPanelP3P4;


    // UIの時間用
    public UISprite m_TimeMinP1P2;
    public UISprite m_TimeTenSecP1P2;
    public UISprite m_TimeSecP1P2;

    public UISprite m_TimeMinP3P4;
    public UISprite m_TimeTenSecP3P4;
    public UISprite m_TimeSecP3P4;

	public UISprite m_TimeMinMain;
	public UISprite m_TimeTenSecMain;
	public UISprite m_TimeSecMain;

    // UIのポイント用
    public UISprite m_pointRedP1P2;
    public UISprite m_pointBlueP1P2;

    public UISprite m_pointRedP3P4;
    public UISprite m_pointBlueP3P4;

	public UISprite m_pointRedMain;
	public UISprite m_pointBlueMain;

    public string[] GOAL;
                                     
    public int m_count;
    public bool m_isEnd;
    public float m_frame;

    //----------------------------------------------------------------------
    // コンストラクタ
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/12/8  @Update 2014/12/8  @Author T.Kawashita
	//          2014/12/13 @Update 2014/12/13 @Author T.Takeuchi
    //----------------------------------------------------------------------
	void Start () {
        
        // パネルを取得
        m_uiPanelMain = GameObject.Find("MainUI").transform.FindChild("Camera").FindChild("Anchor").FindChild("Panel").gameObject;
        m_uiPanelP1P2 = GameObject.Find("P1&P2").transform.FindChild("UI").transform.FindChild("Camera").transform.FindChild("Anchor").transform.FindChild("Panel").gameObject;
        m_uiPanelP3P4 = GameObject.Find("P3&P4").transform.FindChild("UI").transform.FindChild("Camera").transform.FindChild("Anchor").transform.FindChild("Panel").gameObject;

        // フェードイン・フェードアウト用ゲームオブジェクト作成
        m_blackoutMain = m_uiPanelMain.transform.FindChild("BlackOut").gameObject;
        m_blackoutP1P2 = m_uiPanelP1P2.transform.FindChild("BlackOut").gameObject;
        m_blackoutP3P4 = m_uiPanelP3P4.transform.FindChild("BlackOut").gameObject;

        m_TimeMinP1P2 = m_uiPanelP1P2.transform.FindChild("time_min").gameObject.GetComponent<UISprite>();
        m_TimeTenSecP1P2 = m_uiPanelP1P2.transform.FindChild("time_tensec").gameObject.GetComponent<UISprite>();
        m_TimeSecP1P2 = m_uiPanelP1P2.transform.FindChild("time_sec").gameObject.GetComponent<UISprite>();

        m_TimeMinP3P4 = m_uiPanelP3P4.transform.FindChild("time_min").gameObject.GetComponent<UISprite>();
        m_TimeTenSecP3P4 = m_uiPanelP3P4.transform.FindChild("time_tensec").gameObject.GetComponent<UISprite>();
        m_TimeSecP3P4 = m_uiPanelP3P4.transform.FindChild("time_sec").gameObject.GetComponent<UISprite>();

		m_TimeMinMain    = m_uiPanelMain.transform.FindChild("Viewer").transform.FindChild("time_min").gameObject.GetComponent<UISprite>();
		m_TimeTenSecMain = m_uiPanelMain.transform.FindChild("Viewer").transform.FindChild("time_tensec").gameObject.GetComponent<UISprite>();
		m_TimeSecMain = m_uiPanelMain.transform.FindChild("Viewer").transform.FindChild("time_sec").gameObject.GetComponent<UISprite>();

        m_pointRedP1P2 = m_uiPanelP1P2.transform.FindChild("1p2pteam_point").gameObject.GetComponent<UISprite>();
        m_pointBlueP1P2 = m_uiPanelP1P2.transform.FindChild("3p4pteam_point").gameObject.GetComponent<UISprite>();

        m_pointRedP3P4 = m_uiPanelP3P4.transform.FindChild("1p2pteam_point").gameObject.GetComponent<UISprite>();
        m_pointBlueP3P4 = m_uiPanelP3P4.transform.FindChild("3p4pteam_point").gameObject.GetComponent<UISprite>();

		m_pointRedMain = m_uiPanelMain.transform.FindChild("Viewer").transform.FindChild("1p2pteam_point").gameObject.GetComponent<UISprite>();
		m_pointBlueMain = m_uiPanelMain.transform.FindChild("Viewer").transform.FindChild("3p4pteam_point").gameObject.GetComponent<UISprite>();

        // 最初の時間設定
        CalcTime();

        // 最初の得点設定
        Goal();

		// 配信画面の大型得点表示を削除
		m_uiPanelMain.transform.FindChild("BigViewer").transform.FindChild("1p2pteam_point2").GetComponent<UISprite>().spriteName = "num_" + CGameManager.m_redPoint + "_red";
		m_uiPanelMain.transform.FindChild("BigViewer").transform.FindChild("3p4pteam_point2").GetComponent<UISprite>().spriteName = "num_" + CGameManager.m_bluePoint + "_blue";
		m_uiPanelMain.transform.FindChild("BigViewer").gameObject.SetActive(false);

        GOAL = new string[6];
        GOAL[0] = "G";
        GOAL[1] = "O";
        GOAL[2] = "A";
        GOAL[3] = "L";
        GOAL[4] = "EX";
        GOAL[5] = "BACK";

        m_uiStatus = eUISTATUS.eWAIT;

        m_isEnd = false;
        m_count = 0;
        m_frame = 0.0f;

	}

    //----------------------------------------------------------------------
    // 更新
    //----------------------------------------------------------------------
    // @Param   none			
    // @Return	none
    // @Date	2014/12/8  @Update 2014/12/8  @Author T.Kawashita      
	//          2014/12/13 @Update 2014/12/13 @Author T.Takeuchi
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
                        m_blackoutMain.AddComponent<CFadeIn>();
                        m_blackoutP1P2.AddComponent<CFadeIn>(); // フェードイン用スクリプト追加
                        m_blackoutP3P4.AddComponent<CFadeIn>();
                        m_uiStatus = eUISTATUS.eFADEIN;
                        break;

                    case eUISTATUS.eFADEIN:
                        if (m_blackoutMain.GetComponent<TweenAlpha>().enabled == false &&
                            m_blackoutP1P2.GetComponent<TweenAlpha>().enabled == false && 
                            m_blackoutP3P4.GetComponent<TweenAlpha>().enabled == false)
                        {
                            // カウントダウンのマネージャー用GameObjectを生成
                            m_gameObjectP1P2 = (GameObject)Instantiate(Resources.Load("Prefab/Game/CountDownManager"));
                            m_gameObjectP3P4 = (GameObject)Instantiate(Resources.Load("Prefab/Game/CountDownManager"));
							m_gameObjectMain = (GameObject)Instantiate(Resources.Load("Prefab/Game/CountDownManager"));
							m_gameObjectMain.transform.parent = m_uiPanelMain.transform;
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
                if (m_gameObjectMain == false && m_gameObjectP1P2 == false && m_gameObjectP3P4 == false)
                {
                    CGameManager.m_soundPlayer.PlaySE("game/kickoff");
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
            case CGameManager.eSTATUS.eGOALPERFOMANCE:
                m_uiStatus = eUISTATUS.eGOAL;
                break;

            // ゲーム終了時の待機中状態
            case CGameManager.eSTATUS.eENDWAIT:
                if (m_isEnd == false)
                {
                    m_isEnd = true;
                    GameObject obj = (GameObject)Instantiate(Resources.Load("Prefab/Game/TimeUP"));
                    obj.transform.parent = m_uiPanelMain.transform;
                    obj.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
                    obj.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
                    obj = (GameObject)Instantiate(Resources.Load("Prefab/Game/TimeUP"));
                    obj.transform.parent = m_uiPanelP1P2.transform;
                    obj.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
                    obj.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
                    obj = (GameObject)Instantiate(Resources.Load("Prefab/Game/TimeUP"));
                    obj.transform.parent = m_uiPanelP3P4.transform;
                    obj.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
                    obj.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
                }
                CalcTime();
                break;

            // ゲーム終了時のフェードアウト
            case CGameManager.eSTATUS.eFADEOUT:
                switch (m_uiStatus)
                {
                    case eUISTATUS.eGAME:
                        // ゲーム中の状態から遷移した場合はフェードアウトの準備をする
                        m_blackoutMain.AddComponent<CFadeOut>();
                        m_blackoutP1P2.AddComponent<CFadeOut>();
                        m_blackoutP3P4.AddComponent<CFadeOut>();
                        m_uiStatus = eUISTATUS.eFADEOUT;
                        break;

                    case eUISTATUS.eFADEOUT:
                        // フェードアウトが終了したらゲームを終了させる
                        if (m_blackoutMain.GetComponent<TweenAlpha>().enabled == false &&
                            m_blackoutP1P2.GetComponent<TweenAlpha>().enabled == false &&
                            m_blackoutP3P4.GetComponent<TweenAlpha>().enabled == false)
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
                        m_count = 0;
                        m_goalPanelMain = (GameObject)Instantiate(Resources.Load("Prefab/Game/Goal/GoalPanel"));
                        m_goalPanelP1P2 = (GameObject)Instantiate(Resources.Load("Prefab/Game/Goal/GoalPanel"));
                        m_goalPanelP3P4 = (GameObject)Instantiate(Resources.Load("Prefab/Game/Goal/GoalPanel"));

                        m_goalPanelMain.transform.parent = m_uiPanelMain.transform;
                        m_goalPanelP1P2.transform.parent = m_uiPanelP1P2.transform;
                        m_goalPanelP3P4.transform.parent = m_uiPanelP3P4.transform;

                        m_goalPanelMain.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
                        m_goalPanelP1P2.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
                        m_goalPanelP3P4.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);

                        m_goalPanelMain.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                        m_goalPanelP1P2.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                        m_goalPanelP3P4.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

                        m_gameObjectP1P2 = (GameObject)Instantiate(Resources.Load("Prefab/Game/Goal/" + GOAL[m_count] + "_sprite"));
                        m_gameObjectP3P4 = (GameObject)Instantiate(Resources.Load("Prefab/Game/Goal/" + GOAL[m_count] + "_sprite"));
						m_gameObjectMain = (GameObject)Instantiate(Resources.Load("Prefab/Game/Goal/" + GOAL[m_count] + "_sprite"));

                        m_gameObjectMain.transform.parent = m_goalPanelMain.transform;
                        m_gameObjectP1P2.transform.parent = m_goalPanelP1P2.transform;
                        m_gameObjectP3P4.transform.parent = m_goalPanelP3P4.transform;

                        m_uiStatus = eUISTATUS.eGOALWAIT;
                        break;

                    case eUISTATUS.eGOALWAIT:
                        m_frame += Time.deltaTime;

                        if (m_gameObjectP1P2.GetComponent<TweenPosition>().enabled == false && m_count < 3)
                        {
                            m_count++;
                            m_gameObjectP1P2 = (GameObject)Instantiate(Resources.Load("Prefab/Game/Goal/" + GOAL[m_count] + "_sprite"));
                            m_gameObjectP3P4 = (GameObject)Instantiate(Resources.Load("Prefab/Game/Goal/" + GOAL[m_count] + "_sprite"));
                            m_gameObjectMain = (GameObject)Instantiate(Resources.Load("Prefab/Game/Goal/" + GOAL[m_count] + "_sprite"));

                            m_gameObjectMain.transform.parent = m_goalPanelMain.transform;
                            m_gameObjectP1P2.transform.parent = m_goalPanelP1P2.transform;
                            m_gameObjectP3P4.transform.parent = m_goalPanelP3P4.transform;
                        }

                        if (m_gameObjectP1P2.GetComponent<TweenPosition>().enabled == false && m_count == 3)
                        {
                            m_count++;
                            m_gameObjectP1P2 = (GameObject)Instantiate(Resources.Load("Prefab/Game/Goal/" + GOAL[m_count+1] + "_sprite"));
                            m_gameObjectP3P4 = (GameObject)Instantiate(Resources.Load("Prefab/Game/Goal/" + GOAL[m_count+1] + "_sprite"));
                            m_gameObjectMain = (GameObject)Instantiate(Resources.Load("Prefab/Game/Goal/" + GOAL[m_count+1] + "_sprite"));

                            m_gameObjectMain.transform.parent = m_goalPanelMain.transform;
                            m_gameObjectP1P2.transform.parent = m_goalPanelP1P2.transform;
                            m_gameObjectP3P4.transform.parent = m_goalPanelP3P4.transform;
                            m_gameObjectMain.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
                            m_gameObjectP1P2.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
                            m_gameObjectP3P4.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);

                            m_gameObjectP1P2 = (GameObject)Instantiate(Resources.Load("Prefab/Game/Goal/" + GOAL[m_count] + "_sprite"));
                            m_gameObjectP3P4 = (GameObject)Instantiate(Resources.Load("Prefab/Game/Goal/" + GOAL[m_count] + "_sprite"));
                            m_gameObjectMain = (GameObject)Instantiate(Resources.Load("Prefab/Game/Goal/" + GOAL[m_count] + "_sprite"));

                            m_gameObjectMain.transform.parent = m_goalPanelMain.transform;
                            m_gameObjectP1P2.transform.parent = m_goalPanelP1P2.transform;
                            m_gameObjectP3P4.transform.parent = m_goalPanelP3P4.transform;
                            m_frame = 0.0f;
                        }

						// 点数表示
						if (m_gameObjectP1P2.GetComponent<TweenPosition>().enabled == false && m_count == 4 && m_frame >= 2.0f)
						{
							m_uiPanelMain.transform.FindChild("BigViewer").gameObject.SetActive(true);
						}

						// 点数加算
						if (m_gameObjectP1P2.GetComponent<TweenPosition>().enabled == false && m_count == 4 && m_frame >= 3.0f)
						{
							// ここで勝利がわに１加算
							m_uiPanelMain.transform.FindChild("BigViewer").transform.FindChild("1p2pteam_point2").GetComponent<UISprite>().spriteName = "num_" + CGameManager.m_redPoint + "_red";
							m_uiPanelMain.transform.FindChild("BigViewer").transform.FindChild("3p4pteam_point2").GetComponent<UISprite>().spriteName = "num_" + CGameManager.m_bluePoint + "_blue";
						}


						// フェードアウト開始
                        if (m_gameObjectP1P2.GetComponent<TweenPosition>().enabled == false && m_count == 4 && m_frame >= 5.0f)
                        {
							m_uiPanelMain.transform.FindChild("BigViewer").gameObject.SetActive(false);
                            m_blackoutMain.AddComponent<CFadeOut>();
                            m_blackoutP1P2.AddComponent<CFadeOut>();
                            m_blackoutP3P4.AddComponent<CFadeOut>();
                            m_uiStatus = eUISTATUS.eGOALFADEOUT;
                        }
                        break;

                    case eUISTATUS.eGOALFADEOUT:
                        // フェードアウトが終了したらフェードインスタート
                        if (m_blackoutMain.GetComponent<TweenAlpha>().enabled == false &&
                            m_blackoutP1P2.GetComponent<TweenAlpha>().enabled == false &&
                            m_blackoutP3P4.GetComponent<TweenAlpha>().enabled == false)
                        {

                            Destroy(m_goalPanelMain);
                            Destroy(m_goalPanelP1P2);
                            Destroy(m_goalPanelP3P4);

                            this.Goal();
							CGameManager.RestartGame();
                            m_uiStatus = eUISTATUS.eGOALFADEIN;
                            m_blackoutMain.AddComponent<CFadeIn>();
                            m_blackoutP1P2.AddComponent<CFadeIn>();
                            m_blackoutP3P4.AddComponent<CFadeIn>();
                            CGameManager.m_nowStatus = CGameManager.eSTATUS.eRESTART;   // リスタート状態に変更
                        }
                        break;
                }
                break;

            // リスタート
            case CGameManager.eSTATUS.eRESTART:
                // フェードインが終了したらゲームの待機状態を変更
                if (m_blackoutMain.GetComponent<TweenAlpha>().enabled == false &&
                    m_blackoutP1P2.GetComponent<TweenAlpha>().enabled == false &&
                    m_blackoutP3P4.GetComponent<TweenAlpha>().enabled == false)
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
	//          2014/12/13 @Update 2014/12/13 @Author T.Takeuchi
    //----------------------------------------------------------------------
    private void CalcTime()
    {
        int minTime = 0;
        int tenSecTime = 0;
        int secTime = 0;

        if (CGameData.m_gamePlayTime >= 600)
        {
            minTime = 9;
            tenSecTime = 5;
            secTime = 9;
        }
        else
        {
            minTime = (int)(CGameData.m_gamePlayTime / 60);
            tenSecTime = (int)((CGameData.m_gamePlayTime - (minTime * 60)) / 10);
            secTime = (int)((CGameData.m_gamePlayTime - (minTime * 60)) % 10);
        }
        m_TimeMinP1P2.spriteName = "num_" + minTime.ToString();
        m_TimeTenSecP1P2.spriteName = "num_" + tenSecTime.ToString();
        m_TimeSecP1P2.spriteName = "num_" + secTime.ToString();

        m_TimeMinP3P4.spriteName = "num_" + minTime.ToString();
        m_TimeTenSecP3P4.spriteName = "num_" + tenSecTime.ToString();
        m_TimeSecP3P4.spriteName = "num_" + secTime.ToString();

		m_TimeMinMain.spriteName = "num_" + minTime.ToString();
		m_TimeTenSecMain.spriteName = "num_" + tenSecTime.ToString();
		m_TimeSecMain.spriteName = "num_" + secTime.ToString();

	}

	//----------------------------------------------------------------------
	// ゴールした時のUIの変更
	//----------------------------------------------------------------------
	// @Param	none		
	// @Return	none
	// @Date	2014/12/8  @Update 2014/12/8  @Author T.Kawashita      
	//          2014/12/13 @Update 2014/12/13 @Author T.Takeuchi
	//----------------------------------------------------------------------
	public void Goal()
	{
		m_pointRedP1P2.spriteName = "num_" + CGameManager.m_redPoint + "_red";
		m_pointRedP3P4.spriteName = "num_" + CGameManager.m_redPoint + "_red";
		m_pointRedMain.spriteName = "num_" + CGameManager.m_redPoint + "_red";
		m_pointBlueP1P2.spriteName = "num_" + CGameManager.m_bluePoint + "_blue";
		m_pointBlueP3P4.spriteName = "num_" + CGameManager.m_bluePoint + "_blue";
		m_pointBlueMain.spriteName = "num_" + CGameManager.m_bluePoint + "_blue";
	}
}
