using UnityEngine;
using System.Collections;

public class CPlayer2 : CPlayer {


    //----------------------------------------------------------------------
    // コンストラクタ
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/10/30  @Update 2014/10/30  @Author T.Kawashita      
    //----------------------------------------------------------------------
    void Start () {

        this.Init();
        m_pos = this.transform.localPosition;
        m_human = CHumanManager.GetInstance().GetWorldInstance(CHumanManager.eWORLD.eSPAIN);

        // プレイヤーの情報をマップにセット
        Color color = Color.red;
        CPlayerManager.m_playerManager.SetMap(this.gameObject, color);

	}

    //----------------------------------------------------------------------
    // 更新
    //----------------------------------------------------------------------
    // @Param   none			
    // @Return	none
    // @Date	2014/10/31  @Update 2014/10/31  @Author T.Kawashita      
    //----------------------------------------------------------------------
	void Update () {

	}

}
