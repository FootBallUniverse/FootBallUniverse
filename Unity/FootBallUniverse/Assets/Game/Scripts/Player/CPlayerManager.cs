﻿using UnityEngine;
using System.Collections;

public class CPlayerManager : MonoBehaviour{

    public const int m_playerNum = 8;
    public const int m_dataNum = 7;

    // マップのオブジェクト
    public static GameObject m_mapObject;

    // ボールの管理クラス
    public static CSoccerBallManager m_soccerBallManager;

    // 視点のモード
    public enum eVIEW_POINT_STATUS
    {
        ePLAYER,
        eENEMY
    }

    public static Transform m_player1Transform;
    public static Transform m_player2Transform;
    public static Transform m_player3Transform;
    public static Transform m_player4Transform;

    // プレイヤーのステータス
    public enum ePLAYER_STATUS
    {
        eWAIT,
        eCOUNTDOWN,
        eNONE,
        eDASH,
        eSHOOTCHARGE,
        eSHOOT,
        eSMASHSHOOT,
        eTACKLE,
        eTACKLESUCCESS,
        eTACKLEDAMAGE,
        eDASHCHARGE,
        eHOLD,
        ePASS,
        eGOAL,
        eGOALPERFOMANCE,
        eGOALWAIT,
        eEND
    };

    // プレイヤーの番号
    public const int PLAYER_1 = 0;
    public const int PLAYER_2 = 1;
    public const int AI_1 = 2;
    public const int AI_2 = 3;
    public const int PLAYER_3 = 4;
    public const int PLAYER_4 = 5;
    public const int AI_3 = 6;
    public const int AI_4 = 7;
    
    // プレイヤーのCSVデータ
    public static string[,] m_csvData;

    //----------------------------------------------------------------------
    // コンストラクタ
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/10/31  @Update 2014/10/31  @Author T.Kawashita      
    //----------------------------------------------------------------------
    void Awake()
    {
        m_soccerBallManager = GameObject.Find("BallGameObject").GetComponent<CSoccerBallManager>();
        CPlayerManager.SetData();
    }

    //----------------------------------------------------------------------
    // Playerをマップにセット
    //----------------------------------------------------------------------
    // @Param	GameObject  セットしたいObject
	// @Param   Color       セットしたい色
    // @Return	bool        成功か失敗
    // @Date	2014/10/31  @Update 2014/10/31  @Author T.Kawasita      
    //----------------------------------------------------------------------
    public static bool SetMap(GameObject _object, Color _color)
    {
        m_mapObject.GetComponent<Map>().CreateMaker(_object, _color);

        return true;
    }

    //----------------------------------------------------------------------
    // データをセット
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/11/20  @Update 2014/11/20  @Author T.Kawashita      
    //----------------------------------------------------------------------
    public static void SetData()
    {
        // CSVファイルをロード
        string path = Application.dataPath + "/Resources/CSV/PlayerData.csv";
        m_csvData = new string[m_playerNum, m_dataNum];
        CCSVLoader.GetInstance().Loader(ref m_csvData, path, m_playerNum);
    }

    //----------------------------------------------------------------------
    // プレイヤーのデータをセット
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/11/20  @Update 2014/11/20  @Author T.Kawashita      
    //----------------------------------------------------------------------
    public static void SetPlayerData(CPlayerData _playerData,int _playerNo)
    {
        string[] work = new string[m_dataNum];

        _playerData.Set(CUtility.ChangeArray(ref work, m_csvData, _playerNo));

    }

}
