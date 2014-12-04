using UnityEngine;
using System.Collections;

public class CCpu2Mesh : CDefaultMesh {

    //----------------------------------------------------------------------
    // コンストラクタ
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/12/4  @Update 2014/12/4  @Author T.Kawashita      
    //----------------------------------------------------------------------
    void Start()
    {
        GameObject obj = this.transform.parent.transform.FindChild("CPU2NGUI").gameObject;
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
    // @Date	2014/12/4  @Update 2014/12/4  @Author T.Kawashita      
    //----------------------------------------------------------------------
    void Update()
    {

    }

    //----------------------------------------------------------------------
    // マテリアルの変更
    //----------------------------------------------------------------------
    // @Param	TeamData.TEAM_NATIONALITY   選択されたチーム		
    // @Return	none
    // @Date	2014/12/4  @Update 2014/12/4  @Author T.Kawashita      
    //----------------------------------------------------------------------
    public void ChangeMaterial(TeamData.TEAM_NATIONALITY _world)
    {
        // モデルのマテリアルを変更
        switch (_world)
        {
            // 日本
            case TeamData.TEAM_NATIONALITY.JAPAN:
                this.renderer.material = (Material)Instantiate(Resources.Load("Model/Player/Materials/lambert_japan2"));
                break;

            // ブラジル
            case TeamData.TEAM_NATIONALITY.BRASIL:
                this.renderer.material = (Material)Instantiate(Resources.Load("Model/Player/Materials/lambert_brasil2"));
                break;

            // イングランド
            case TeamData.TEAM_NATIONALITY.ENGLAND:
                this.renderer.material = (Material)Instantiate(Resources.Load("Model/Player/Materials/lambert_england2"));
                break;

            // スペイン
            case TeamData.TEAM_NATIONALITY.ESPANA:
                this.renderer.material = (Material)Instantiate(Resources.Load("Model/Player/Materials/lambert_spain2"));
                break;
        }
    }

    //----------------------------------------------------------------------
    // カメラの中に潜入したら
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/12/4  @Update 2014/12/4  @Author T.Kawashita      
    //----------------------------------------------------------------------
    void OnWillRenderObject()
    {
        // 配信画面用のカメラなら無効
        if ("DeliveryCamera" == Camera.current.name)
            return;

        GameObject camera = GameObject.Find(Camera.current.name);

        // プレイヤー１のカメラにCPUが映ったら
        if ("Player1Camera" == Camera.current.name)
            m_p12DPanel.transform.localRotation = camera.transform.parent.transform.localRotation;

        // プレイヤー２のカメラにCPUが映ったら
        if ("Player2Camera" == Camera.current.name)
            m_p22DPanel.transform.localRotation = camera.transform.parent.transform.localRotation;

        // プレイヤー３のカメラにCPUが映ったら
        if ("Player3Camera" == Camera.current.name)
            m_p32DPanel.transform.localRotation = camera.transform.parent.transform.localRotation;

        // プレイヤー４のカメラにCPUが映ったら
        if ("Player4Camera" == Camera.current.name)
            m_p42DPanel.transform.localRotation = camera.transform.parent.transform.localRotation;
    
    }
}
