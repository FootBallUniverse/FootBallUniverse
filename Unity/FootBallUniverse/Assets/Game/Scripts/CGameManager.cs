﻿using UnityEngine;
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
        eGOALPERFOMANCE,    // ゴール時の演出
        eEND,               // ゲーム終了状態
    }
        
    public static eSTATUS m_nowStatus;  // ゲームの現在のステータス
    private float m_frame;              // タイマー調整用フレーム
   
    public static bool m_isGamePlay;    // ゲームがプレイ中かどうか

    public static int[] m_isPoint;      // 両チームの得点
	public static int[] m_playerPoint;  // 両チームプレイヤーの個人的得点

    private CSoundPlayer m_soundPlayer; // サウンドプレイヤー

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
        m_isPoint = new int[2];
        m_isPoint[0] = 0;       // 1P&2Pチーム初期化
        m_isPoint[1] = 0;       // 3P&4Pチーム初期化

        CGameData.GetInstance().Init();
        this.LoadData();

        m_soundPlayer = new CSoundPlayer();
		m_playerPoint = new int[4];

		// シーン移動しても消滅しない
		//DontDestroyOnLoad(this);
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
            case eSTATUS.eWAIT: GameWait();        break;   // ゲーム開始待機状態
            case eSTATUS.eCOUNTDOWN: CountDown();  break;   // ゲーム開始カウントダウン中
            case eSTATUS.eGAME: GamePlay();        break;   // ゲームプレイ状態
            case eSTATUS.eGOAL: Goal();            break;   // ゴールした後の状態  
            case eSTATUS.eENDWAIT: GameEndWait();  break;   // ゲーム終了待機状態
            case eSTATUS.eEND:                 
                // リザルト画面に遷移させる
                Application.LoadLevel("Result");
                break;

            default:                               break;
        }
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
            Debug.Log("Resultまで残り:" + CGameData.m_gameEndTime);
            if (CGameData.m_gameEndTime <= 0)
            {
                // ゲーム終了待機が終わったらフェードアウトさせる
                m_nowStatus = eSTATUS.eFADEOUT;
            } 
        }
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
            m_nowStatus = eSTATUS.eEND;
            m_frame = 0.0f;
            Debug.Log("残り時間を0秒にしました");
        }

    }

}
