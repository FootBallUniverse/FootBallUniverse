using UnityEngine;
using System.Collections;

public class CDefaultMesh : MonoBehaviour {

    protected GameObject m_p12DPanel;   // 2Dのプレイヤー1用パネル
    protected GameObject m_p22DPanel;   // 2Dのプレイヤー2用パネル
    protected GameObject m_p32DPanel;   // 2Dのプレイヤー3用パネル
    protected GameObject m_p42DPanel;   // 2Dのプレイヤー4用パネル

    //----------------------------------------------------------------------
    // コンストラクタ
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/11/12  @Update 2014/11/12  @Author T.Kawashita     
    //----------------------------------------------------------------------
    void Start () {
        m_p12DPanel = null;
        m_p22DPanel = null;
        m_p32DPanel = null;
        m_p42DPanel = null;	
	}

    //----------------------------------------------------------------------
    // 更新
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/11/12  @Update 2014/11/12  @Author T.Kawashita      
    //----------------------------------------------------------------------
	void Update () {
	    
	}
}
