using UnityEngine;
using System.Collections;

public class CPlayerData{

    public int m_id;                // ID
    public int m_playerNo;          // プレイヤーの番号
    public float m_xPos;            // X座標
    public float m_yPos;            // Y座標
    public float m_zPos;            // Z座標

    //----------------------------------------------------------------------
    // コンストラクタ
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/11/20  @Update 2014/11/20  @Author T.Kawashita      
    //----------------------------------------------------------------------
    public CPlayerData()
    { 
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
        m_xPos = float.Parse(_value[2]);
        m_yPos = float.Parse(_value[3]);
        m_zPos = float.Parse(_value[4]);
    }

}
