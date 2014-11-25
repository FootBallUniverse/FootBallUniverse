using UnityEngine;
using System.Collections;

public class CPlayer3 : CPlayer {


    //----------------------------------------------------------------------
    // コンストラクタ
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/10/31  @Update 2014/10/31  @Author T.Kawashita      
    //----------------------------------------------------------------------
	void Start () {
        this.Init();
        m_pos = this.transform.localPosition;
        m_human = CHumanManager.GetWorldInstance(TeamData.teamNationality[1]);
	    
        // プレイヤーの情報をマップにセット
        Color color = Color.blue;
        CPlayerManager.m_playerManager.SetMap(this.gameObject, color);
    
    }

    //----------------------------------------------------------------------
    // 更新
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/10/31  @Update 2014/10/31  @Author T.Kawashita      
    //----------------------------------------------------------------------
	void Update () {    
	
	}
}
