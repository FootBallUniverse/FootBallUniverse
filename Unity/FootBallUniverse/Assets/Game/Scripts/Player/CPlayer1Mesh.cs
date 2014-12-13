﻿using UnityEngine;
using System.Collections;

public class CPlayer1Mesh : CDefaultMesh {

    //----------------------------------------------------------------------
    // コンストラクタ
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/11/28  @Update 2014/11/28  @Author T.Kawashita      
    //----------------------------------------------------------------------
	void Start () {
        GameObject obj = this.gameObject;
        obj = obj.transform.FindChild("Player1NGUI").gameObject;
        m_p12DPanel = obj.transform.FindChild("Player1Panel").gameObject;
        m_p22DPanel = obj.transform.FindChild("Player2Panel").gameObject;
        m_p32DPanel = obj.transform.FindChild("Player3Panel").gameObject;
        m_p42DPanel = obj.transform.FindChild("Player4Panel").gameObject;
		m_deli2DPanel = obj.transform.FindChild("DeliveryPanel").gameObject;
	}

    //----------------------------------------------------------------------
    // 更新
    //----------------------------------------------------------------------
    // @Param   none			
    // @Return	none
    // @Date	2014/11/28  @Update 2014/11/28  @Author T.Kawashita      
    //----------------------------------------------------------------------
	void Update () {
	
	}

    //----------------------------------------------------------------------
    // マテリアルの変更
    //----------------------------------------------------------------------
    // @Param	TeamData.TEAM_NATIONALITY   選択されたチーム		
    // @Return	none
    // @Date	2014/11/28  @Update 2014/11/28  @Author T.Kawashita      
    //----------------------------------------------------------------------
    public void ChangeMaterial(TeamData.TEAM_NATIONALITY _world)
    {
        // モデルのマテリアルを変更
        switch (_world)
        {
            // 日本
            case TeamData.TEAM_NATIONALITY.JAPAN:
                this.renderer.material = (Material)Instantiate(Resources.Load("Model/Player/Materials/lambert_japan"));
                break;

            // ブラジル
            case TeamData.TEAM_NATIONALITY.BRASIL:
                this.renderer.material = (Material)Instantiate(Resources.Load("Model/Player/Materials/lambert_brasil"));
                break;

            // イングランド
            case TeamData.TEAM_NATIONALITY.ENGLAND:
                this.renderer.material = (Material)Instantiate(Resources.Load("Model/Player/Materials/lambert_england"));
                break;

            // スペイン
            case TeamData.TEAM_NATIONALITY.ESPANA:
                this.renderer.material = (Material)Instantiate(Resources.Load("Model/Player/Materials/lambert_spain"));
                break;
        }
    }

    //----------------------------------------------------------------------
    // カメラの中に潜入したら
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Other   CallBack
    // @Date	2014/11/28  @Update 2014/11/28  @Author T.Kawashia      
    //----------------------------------------------------------------------
    void OnWillRenderObject()   
    {
        // 自分のカメラなら無効
        if ("Player1Camera" == Camera.current.name )
			return;

        GameObject camera = GameObject.Find(Camera.current.name );

        if ("Player2Camera" == Camera.current.name)
			m_p22DPanel.transform.rotation = camera.transform.localRotation;

        if ("Player3Camera" == Camera.current.name)
			m_p32DPanel.transform.rotation = camera.transform.localRotation;

        if ("Player4Camera" == Camera.current.name)
			m_p42DPanel.transform.rotation = camera.transform.localRotation;

		if ("DeliveryCamera" == Camera.current.name)
			m_deli2DPanel.transform.rotation = camera.transform.localRotation;
	}
}
