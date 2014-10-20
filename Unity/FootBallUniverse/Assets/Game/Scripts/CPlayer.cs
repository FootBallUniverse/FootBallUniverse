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

    //----------------------------------------------------------------------
    // コンストラクタのようなもの
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
	}
	
	// Update is called once per frame
	void Update () {	
	}
}
