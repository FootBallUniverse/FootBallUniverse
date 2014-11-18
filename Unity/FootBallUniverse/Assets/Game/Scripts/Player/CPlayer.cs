using UnityEngine;
using System.Collections;

//----------------------------------------------------
// プレイヤーの基本的なクラス
// 基底クラス
//----------------------------------------------------
public class CPlayer : MonoBehaviour {

    public CPlayerManager.ePLAYER_STATUS m_status;
    public CPlayerManager.eCAMERA_STATUS m_cameraStatus;
    public Vector3 m_pos;        // 位置座標
    public Vector3 m_speed;      // 移動量
    public Vector3 m_angle;      // 回転角度

    protected CPlayerAction m_action;       // プレイヤーのアクション
    protected CPlayerAnimator m_animator;   // プレイヤーのアニメーション
    public CHuman m_human;                  // プレイヤーの国のインスタンス

    public int m_chargeFrame;               // チャージ時のフレーム数
    public bool m_isBall;                   // ボールを持っているかどうか

    //----------------------------------------------------------------------
    // コンストラクタ
    //----------------------------------------------------------------------
    // @Param   none			
    // @Return	none
    // @Date	2014/10/14  @Update 2014/10/14  @Author T.Kawashita      
    //----------------------------------------------------------------------
	void Start () {
        m_pos = new Vector3();
        m_speed = new Vector3();
        m_angle = new Vector3();
        m_status = CPlayerManager.ePLAYER_STATUS.eNONE;
        m_cameraStatus = CPlayerManager.eCAMERA_STATUS.eNORMAL;

        m_human = new CHuman();
        m_action = new CPlayerAction();

        m_chargeFrame = 0;
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
        m_pos = new Vector3(0.0f,0.0f,0.0f);
        m_speed = new Vector3(0.0f,0.0f,0.0f);
        m_angle = new Vector3(0.0f,0.0f,0.0f);
        m_status = CPlayerManager.ePLAYER_STATUS.eWAIT;
        m_cameraStatus = CPlayerManager.eCAMERA_STATUS.eNORMAL;

        m_human = new CHuman();
        m_action = new CPlayerAction();

        m_chargeFrame = 0;
        m_isBall = false;

        return true;
    }

    //----------------------------------------------------------------------
    // 更新
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/10/28  @Update 2014/10/28  @Author T.Kawashita      
    //----------------------------------------------------------------------
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

    //----------------------------------------------------------------------
    // 移動(仮想関数)
    //----------------------------------------------------------------------
    // @Param	Vector3     移動量
    // @Return	none
    // @Date	2014/11/11  @Update 2014/11/11  @Author T.Kawashita      
    //----------------------------------------------------------------------
    public virtual void Move(Vector3 _speed)
    {
    }

    //----------------------------------------------------------------------
    // 回転(仮想関数)
    //----------------------------------------------------------------------
    // @Param	Vector2     回転量		
    // @Return	none
    // @Date	2014/11/12  @Update 2014/11/12  @Author T.Kawashita      
    //----------------------------------------------------------------------
    public virtual void Rotation(Vector2 _angle)
    { 
    }

}
