using UnityEngine;
using System.Collections;

//----------------------------------------------------------------------
// CGameManager
//----------------------------------------------------------------------
// @Info ゲームの管理クラス
// @Date 2014/10/27 @Update 2014/10/27 @Author T.Kawashita      
//----------------------------------------------------------------------
public class CGameManager : MonoBehaviour {

    // ゲームのステータス
    public enum eSTATUS
    { 
        eWAIT,              // 開始待機状態
        eCOUNTDOWN,         // 開始カウントダウン状態
        eGAME,              // ゲームプレイ中状態
        eENDWAIT,           // ゲーム終了待機状態
        eFADEOUT,           // 最後のフェードアウト状態
        eGOAL,              // ゴール状態
        eGOALWAIT,          // ゴール終わった後の待機状態
        eGOALPERFOMANCE,    // ゴールのパフォーマンス中
		eGOALFADEOUT,		// ゴール後のフェードアウト
        eRESTART,           // ゴール後のリスタート
        eEND,               // ゲーム終了状態
    }
        
    public static eSTATUS m_nowStatus;  // ゲームの現在のステータス
    public static int m_nowTime;        // 現在の時間
    private float m_frame;              // タイマー調整用フレーム
   
    public static bool m_isGamePlay;    // ゲームのが進行中かどうか

    public static int m_redPoint;              // 1P2Pのポイント
    public static int m_bluePoint;             // 3P4Pのポイント

    public static CSoundPlayer m_soundPlayer; // サウンドプレイヤー

    //----------------------------------------------------------------------
    // コンストラクタ
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/10/27  @Update 2014/10/27  @Author T.Kawashita      
	//          2014/11/13  @Update 2014/11/13  @Author T.Takeuchi       シーン移動しても消滅しない処理追加
    //----------------------------------------------------------------------
    void Awake () {

        m_nowStatus = eSTATUS.eWAIT;
        m_frame = 0;
        m_isGamePlay = true;

        CGameData.GetInstance().Init();
        this.LoadData();

        m_redPoint = 0;
        m_bluePoint = 0;

        m_soundPlayer = new CSoundPlayer();
        m_soundPlayer.PlayBGMFadeIn("game/bgm_01", 0.05f);

	}

    //----------------------------------------------------------------------
    // 更新
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/10/27  @Update 2014/10/27  @Author T.Kawashita 
    // @Update  2014/11/18  GameEndをGameEndWaitに変更  @Author T.Kawashita 
    //----------------------------------------------------------------------
	void Update () {

        switch (m_nowStatus)
        {
            case eSTATUS.eWAIT: GameWait();        break;           // ゲーム開始待機状態
            case eSTATUS.eCOUNTDOWN: CountDown();  break;           // ゲーム開始カウントダウン中
            case eSTATUS.eGAME: GamePlay();        break;           // ゲームプレイ状態
            case eSTATUS.eGOAL: Goal();            break;           // ゴールした後の状態
            case eSTATUS.eGOALPERFOMANCE: GoalPerfomance(); break;  // ゴール後のパフォーマンス状態
			case eSTATUS.eGOALWAIT:	GoalWait();break;           // ゴール後の待機状態
            case eSTATUS.eENDWAIT: GameEndWait();  break;           // ゲーム終了待機状態
            case eSTATUS.eEND:                 
                // リザルト画面に遷移させる
			Application.LoadLevel("VictoryPerformance");
			break;

            default:                               break;
        }
    }

	//----------------------------------------------------------------------
	// フレーム最後の更新
	//----------------------------------------------------------------------
	// @Param	none		
	// @Return	none
	// @Date	2014/12/9 @Update 2014/12/9 @Author T.Kawashita  
	//----------------------------------------------------------------------
	void LateUpdate()
	{
	}

    //----------------------------------------------------------------------
    // ゲーム待機状態
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/10/28  @Update 2014/11/10  @Author T.Kawashita      
    //----------------------------------------------------------------------
    private void GameWait()
    {
        m_frame += Time.deltaTime;

        // 60Fカウント
        if (m_frame >= 1.0f)
        {

        }

        // セレクトボタンでレディ画面をスキップ
        if (InputXBOX360.IsGetAllSelectButton() == true)
        {
            m_nowStatus = eSTATUS.eGAME;
            m_frame = 0;
            m_isGamePlay = true;
        }
    }

    //----------------------------------------------------------------------
    // カウントダウン状態
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/11/18  @Update 2014/11/18  @Author T.Kawashita      
    //----------------------------------------------------------------------
    private void CountDown()
    { 

    }

    //----------------------------------------------------------------------
    // ゴール状態
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/11/21  @Update 2014/11/21  @Author T.Kawashita      
    //----------------------------------------------------------------------
    private void Goal()
    {
        // プレイヤーのアニメーションを再生

        // ステータス変更
        m_nowStatus = eSTATUS.eGOALPERFOMANCE;
        m_frame = 0.0f;

    }

    //----------------------------------------------------------------------
    // ゴールパフォーマンス状態
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/11/21  @Update 2014/11/21  @Author T.Kawashita      
    //----------------------------------------------------------------------
    private void GoalPerfomance()
    {
        m_frame += Time.deltaTime;
        // アニメーション終わって少し時間がたったらフェードアウト
        if (m_frame >= 1.0f)
        {
            m_nowStatus = eSTATUS.eGOALWAIT;
            m_frame = 0.0f;
        }
    }

    //----------------------------------------------------------------------
    // ゴール後の待機状態
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/11/21  @Update 2014/11/21  @Author T.Kawashita      
    //----------------------------------------------------------------------
    private void GoalWait()
    { 
    }

    //----------------------------------------------------------------------
    // ゲームプレイ中状態
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/10/28  @Update 2014/10/28  @Author T.Kawashita      
    //----------------------------------------------------------------------
    private void GamePlay()
    {
        this.PlayTime();    // 時間計測用
        this.DebugKey();    // デバッグ用
    }

    //----------------------------------------------------------------------
    // ゲーム終了待機状態
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/10/28  @Update 2014/10/28  @Author T.Kawashita     
    // @Update  2014/11/18  ゲーム終了待機状態に変更
    //----------------------------------------------------------------------
    private void GameEndWait()
    {
        // 60Fたったかどうか計算
        m_frame += Time.deltaTime;
        if (m_frame >= 1.0f)
        {
            m_frame = 0;
            CGameData.m_gameEndTime--;

            // ここで何か終わりの表示
            if (CGameData.m_gameEndTime <= 0)
            {
                // ゲーム終了待機が終わったらフェードアウトさせる
                m_nowStatus = eSTATUS.eFADEOUT;
                m_soundPlayer.PlayBGMFadeOut(0.02f);

                int teamPoint = 0;
                int redSupporter = 0;
                int blueSupporter = 0;
                if (TeamData.GetWinTeamNo() == 0)
                {
                    redSupporter += CSupporterData.m_winSupporter;

                    // 得点差を求める
                    teamPoint = TeamData.GetTeamScore(0) - TeamData.GetTeamScore(1);
                    if (teamPoint >= 2)
                        redSupporter += CSupporterData.m_point2WinSupporter;

                    blueSupporter += CSupporterData.m_loseSupporter;
                }
                else if (TeamData.GetWinTeamNo() == 1)
                {
                    blueSupporter += CSupporterData.m_winSupporter;
                    // 得点差を求める
                    teamPoint = TeamData.GetTeamScore(1) - TeamData.GetTeamScore(0);
                    if (teamPoint >= 2)
                        blueSupporter += CSupporterData.m_point2WinSupporter;

                    redSupporter += CSupporterData.m_loseSupporter;
                }
                else if (TeamData.GetWinTeamNo() == 2)
                {
                    redSupporter += CSupporterData.m_drowSupporter;
                    blueSupporter += CSupporterData.m_drowSupporter;

                    teamPoint = TeamData.GetTeamScore(0);
                    if (teamPoint >= 2)
                    {
                        redSupporter += CSupporterData.m_point2Drow;
                        blueSupporter += CSupporterData.m_point2Drow;
                    }
                }

                // 最後にサポーター追加
                CSupporterManager.AddSupporter(redSupporter, blueSupporter, true);
 
            } 
        }
    }

	//----------------------------------------------------------------------
	// ゲームのリスタート
	//----------------------------------------------------------------------
	// @Param	none		
	// @Return	none
	// @Date	2014/12/9  @Update 2014/12/9  @Author T.Kawashita      
	//----------------------------------------------------------------------
	public static void RestartGame()
	{
		CSoccerBallManager.Restart ();
	}

    //----------------------------------------------------------------------
    // ゲームタイマー
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	bool
    // @Date	2014/10/27  @Update 2014/10/27  @Author T.Kawashita      
    //----------------------------------------------------------------------
    private bool PlayTime()
    {
        // タイマーがONなら時間を進める
        if (CGameData.m_isTimer == true)
        {
            m_frame += Time.deltaTime;

            if (m_frame >= 1.0f)
            {
                m_frame = 0;
                CGameData.m_gamePlayTime--;
                m_nowTime++;

                if (CGameData.m_gamePlayTime <= 0)
                {
                    m_isGamePlay = false;
                    m_nowStatus = eSTATUS.eENDWAIT;
                    m_frame = 0.0f;
                    return true;
                }
            }
        }

        return false;
    }

    //----------------------------------------------------------------------
    // CSVファイルからグローバルデータを読み込み
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	bool    成功か失敗
    // @Date	2014/10/27  @Update 2014/10/27  @Author 2014/10/27      
    //----------------------------------------------------------------------
    private bool LoadData()
    {
        // CSVファイルをロード
        string path = Application.dataPath + "/Resources/CSV/GlobalData.csv";
        string[,] csvData = new string[1, CGameData.m_dataNum];
        CCSVLoader.GetInstance().Loader(ref csvData, path, 1);

        // 変換用ワーク作成
        string[] work = new string[CGameData.m_dataNum];

        // データをセット
        CGameData.SetData(CUtility.ChangeArray(ref work,csvData,0 ));

        return true;
    }

    //----------------------------------------------------------------------
    // デバッグ用ボタン
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/10/27  @Update 2014/10/28  @Author T.Kawashita      
    //----------------------------------------------------------------------
    private void DebugKey()
    {
        // Tキーもしくは1Pのスタートボタンが押されたらタイマをON/OFF
        if (Input.GetKeyDown(KeyCode.T) ||
            Input.GetKeyDown(InputXBOX360.P1_XBOX_START))
        {
            CGameData.m_isTimer ^= true;
            Debug.Log("Stop : " + CGameData.m_isTimer);
        }

        // 残り時間を0秒にする
        if (Input.GetKeyDown(KeyCode.Escape) ||
            InputXBOX360.IsGetAllSelectButton())
        {
            m_isGamePlay = false;
            CGameData.m_gamePlayTime = 0;
            m_nowStatus = eSTATUS.eENDWAIT;
            m_frame = 0.0f;
        }
    }
}
