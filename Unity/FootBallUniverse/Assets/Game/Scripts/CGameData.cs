using UnityEngine;
using System.Collections;

public class CGameData
{

    // シングルトンのためのただ一つのインスタンス
    private static CGameData m_gameData = new CGameData();

    public const int m_dataNum = 2;     // データ数
    public static int m_gamePlayTime;   // ゲームのプレイ時間
    public static float m_ballDecRec;   // ゲーム時のボールにかかる空気抵抗（仮）
    public static bool m_isTimer;       // ゲームのタイマーのON/OFF

    //----------------------------------------------------------------------
    // コンストラクタ
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/10/27  @Update 2014/10/27  @Author T.Kawashita      
    //----------------------------------------------------------------------
    private CGameData()
    {
        m_gamePlayTime = 0;
        m_ballDecRec = 0.0f;
        m_isTimer = true;
    }

    //----------------------------------------------------------------------
    // 初期化
    //----------------------------------------------------------------------
    // @Param   none			
    // @Return	bool    成功か失敗
    // @Date	2014/10/27  @Update 2014/10/27  @Author T.Kawashita      
    //----------------------------------------------------------------------
    public bool Init()
    {
        m_gamePlayTime = 0;
        m_ballDecRec = 0.0f;
        m_isTimer = true;

        return true;
    }

    //----------------------------------------------------------------------
    // シングルトン実装
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/10/27  @Update 2014/10/27  @Author T.Kawashita      
    //----------------------------------------------------------------------
    public static CGameData GetInstance()
    {
        return m_gameData;
    }

    //----------------------------------------------------------------------
    // ゲームに使われるデータのセット
    //----------------------------------------------------------------------
    // @Param	string[]	データが格納されたstring配列	
    // @Return	bool        成功か失敗
    // @Date	2014/10/27  @Update 2014/10/27  @Author T.Kawashita      
    //----------------------------------------------------------------------
    public static bool SetData(string[] _arrayData)
    {
        m_gamePlayTime = int.Parse(_arrayData[0]);
        m_ballDecRec = float.Parse(_arrayData[1]);
        
        return true;
    }

}
