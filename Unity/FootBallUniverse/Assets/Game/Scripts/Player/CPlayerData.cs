using UnityEngine;
using System.Collections;

public class CPlayerData{

    public int m_id;                // ID
    public int m_playerNo;          // プレイヤーの番号
    public int m_teamNo;            // チームの番号
    public float m_xPos;            // X座標
    public float m_yPos;            // Y座標
    public float m_zPos;            // Z座標
    public string m_button;         // ボタン

    //----------------------------------------------------------------------
    // コンストラクタ
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/11/20  @Update 2014/11/20  @Author T.Kawashita      
    //----------------------------------------------------------------------
    public CPlayerData()
    {
        m_id = 0;
        m_playerNo = 0;
        m_teamNo = 0;
        m_xPos = 0.0f;
        m_yPos = 0.0f;
        m_zPos = 0.0f;
        m_button = "";
    }

    //----------------------------------------------------------------------
    // 値をセット
    //----------------------------------------------------------------------
    // @Param	string[]    セットしたい値が格納されている配列		
    // @Return	none
    // @Date	2014/11/20  @Update 2014/11/20  @Author T.Kawashita      
    //----------------------------------------------------------------------
    public void Set(string[] _value)
    {
        m_id = int.Parse(_value[0]);
        m_playerNo = int.Parse(_value[1]);
        m_teamNo = int.Parse(_value[2]);
        m_xPos = float.Parse(_value[3]);
        m_yPos = float.Parse(_value[4]);
        m_zPos = float.Parse(_value[5]);
        m_button = _value[6].ToString();
    }

}
