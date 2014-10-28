using UnityEngine;
using System.Collections;

//----------------------------------------------------
// プレイヤーの基本的なクラス
// 基底クラス
//----------------------------------------------------

public class CPlayer : MonoBehaviour {

    protected CPlayerManager.ePLAYER_STATUS m_status;
    protected CPlayerManager.eCAMERA_STATUS m_cameraStatus;
    protected Vector3 m_pos;        // 位置座標
    protected Vector3 m_old_pos;    // 前回座標
    protected Vector3 m_angle;      // 回転角度

    protected CActionPlayer m_action;   // プレイヤーのアクション
    protected CHuman m_human;           // プレイヤーの国のインスタンス
    protected bool m_isBall;            // ボールを持っているかどうか

    //----------------------------------------------------------------------
    // コンストラクタ
    //----------------------------------------------------------------------
    // @Param   none			
    // @Return	none
    // @Date	2014/10/14  @Update 2014/10/14  @Author T.Kawashita      
    //----------------------------------------------------------------------
	void Start () {
        m_pos = new Vector3();
        m_old_pos = new Vector3();
        m_angle = new Vector3();
        m_status = CPlayerManager.ePLAYER_STATUS.eNONE;
        m_cameraStatus = CPlayerManager.eCAMERA_STATUS.eNORMAL;

        m_human = new CHuman();
        m_action = new CActionPlayer();

        m_isBall = false;
    }

    //----------------------------------------------------------------------
    // 初期化
    //----------------------------------------------------------------------
    // @Param   none			
    // @Return	bool    成功か失敗
    // @Date	2014/10/24  @Update 2014/10/24  @Author T.Kawashita      
    //----------------------------------------------------------------------
    protected bool Init()
    {
        m_pos = new Vector3();
        m_old_pos = new Vector3();
        m_angle = new Vector3();
        m_status = CPlayerManager.ePLAYER_STATUS.eNONE;
        m_cameraStatus = CPlayerManager.eCAMERA_STATUS.eNORMAL;

        m_human = new CHuman();
        m_action = new CActionPlayer();

        m_isBall = false;

        return true;
    }

	// Update is called once per frame
	void Update () {	
	}

    //----------------------------------------------------------------------
    // ゲームがプレイ中かどうか判定
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/10/28  @Update 2014/10/28  @Author T.Kawashita      
    //----------------------------------------------------------------------
    protected void CheckGamePlay()
    { 
        // ゲーム終了かどうか判定
        if (CGameManager.m_isGamePlay == false)
            m_status = CPlayerManager.ePLAYER_STATUS.eEND;  // 終了していたらステータス変更
    }
}
