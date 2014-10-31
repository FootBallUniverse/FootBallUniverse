using UnityEngine;
using System.Collections;

public class CPlayer2 : CPlayer {



	// Use this for initialization
	void Start () {

        this.Init();
        m_pos = this.transform.localPosition;
        m_human = CHumanManager.GetInstance().GetWorldInstance(CHumanManager.eWORLD.eSPAIN);

        GameObject obj = this.transform.FindChild("Player2Camera").gameObject;
        obj = obj.transform.FindChild("Player2NGUI").gameObject;
        m_p12DPanel = obj.transform.FindChild("Player1Panel").gameObject;
        m_p22DPanel = obj.transform.FindChild("Player2Panel").gameObject;
        m_p32DPanel = obj.transform.FindChild("Player3Panel").gameObject;
        m_p42DPanel = obj.transform.FindChild("Player4Panel").gameObject;

	}
	
	// Update is called once per frame
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
        if ("Player2Camera" == Camera.current.name)
            return;

        GameObject camera = GameObject.Find(Camera.current.name);
        camera = camera.transform.parent.gameObject;

        // プレイヤー1のカメラにプレイヤー2が入ったら
        if ("Player1Camera" == Camera.current.name)
            m_p12DPanel.transform.localRotation = camera.transform.localRotation;
    
        // プレイヤー3のカメラにプレイヤー2が入ったら
        if ("Player3Camera" == Camera.current.name)
            m_p32DPanel.transform.localRotation = camera.transform.localRotation;

        // プレイヤー4のカメラにプレイヤー2が入ったら
        if ("Player4Camera" == Camera.current.name)
            m_p42DPanel.transform.localRotation = camera.transform.localRotation;
    }
}
