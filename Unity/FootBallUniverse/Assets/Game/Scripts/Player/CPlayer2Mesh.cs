using UnityEngine;
using System.Collections;

public class CPlayer2Mesh : CPlayerMesh {

    //----------------------------------------------------------------------
    // コンストラクタ
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/11/12  @Update 2014/11/12  @Author T.Kawashita      
    //----------------------------------------------------------------------
	void Start () {
        GameObject obj = this.transform.parent.transform.FindChild("Player2Camera").gameObject;
        obj = obj.transform.FindChild("Player2NGUI").gameObject;
        m_p12DPanel = obj.transform.FindChild("Player1Panel").gameObject;
        m_p22DPanel = obj.transform.FindChild("Player2Panel").gameObject;
        m_p32DPanel = obj.transform.FindChild("Player3Panel").gameObject;
        m_p42DPanel = obj.transform.FindChild("Player4Panel").gameObject;
	}

    //----------------------------------------------------------------------
    // 更新
    //----------------------------------------------------------------------
    // @Param   none		
    // @Return	none
    // @Date	2014/11/2  @Update 2014/11/12  @Author T.Kawashita      
    //----------------------------------------------------------------------
	void Update () {
	
	}

    //----------------------------------------------------------------------
    // カメラの中に潜入したら
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Othre   CallBack
    // @Date	2014/10/30 @Update 2014/10/31  @Author T.Kawashita      
    //----------------------------------------------------------------------
    void OnWillRenderObject()
    {

        // 自分のカメラなら無効
        if ("Player2Camera" == Camera.current.name ||
            "SceneCamera" == Camera.current.name)
            return;

        GameObject camera = GameObject.Find(Camera.current.name);
        camera = camera.transform.parent.gameObject;

        // プレイヤー1のカメラにプレイヤー2が入ったら
        if ("Player1Camera" == Camera.current.name)
            m_p12DPanel.transform.localRotation = camera.transform.localRotation;
    }
}
