using UnityEngine;
using System.Collections;

public class CPlayer1 : CPlayer {

    // プレイヤーの動きクラス
    private CActionPlayer m_action;

    const float DASH_SPEED = 1.0f;
    private Vector3 m_speed;

	// Use this for initialization
	void Start () {

        m_pos = this.transform.localPosition;
        m_pos = m_old_pos;
        m_angle = new Vector3(0.0f, 0.0f);
        m_status = CPlayerManager.ePLAYER_STATUS.eNONE;
        m_cameraStatus = CPlayerManager.eCAMERA_STATUS.eNORMAL;

        m_action = new CActionPlayer();
	}
	
	// Update is called once per frame
	void Update () {
        switch (m_cameraStatus)
        { 
            // 通常移動モード
            case CPlayerManager.eCAMERA_STATUS.eNORMAL:
                switch (m_status)
                {
                    // 何もしてない状態
                    case CPlayerManager.ePLAYER_STATUS.eNONE:
                        this.Move();
                        this.Rotation();
                        // ダッシュ初期化
                        if (Input.GetKeyDown(InputXBOX360.P1_XBOX_LEFT_ANALOG_PRESS))
                        {
                            m_action.InitDash(5.0f);
                            m_status = CPlayerManager.ePLAYER_STATUS.eDASH;
                        }

                        // カメラモード変更
                        if (Input.GetKeyDown(InputXBOX360.P1_XBOX_RIGHT_ANALOG_PRESS))
                        {
                            m_cameraStatus = CPlayerManager.eCAMERA_STATUS.eROCKON;
                        }

                        break;

                    // ダッシュ中
                    case CPlayerManager.ePLAYER_STATUS.eDASH:
                        if (this.Dash() == true)
                            m_status = CPlayerManager.ePLAYER_STATUS.eNONE;
                        break;


                }

                this.transform.localPosition = m_pos;
                break;

            // ロックオン移動モード
            case  CPlayerManager.eCAMERA_STATUS.eROCKON:
                switch (m_status)
                {
                    case CPlayerManager.ePLAYER_STATUS.eNONE:
                        break;

                }
                break;


        }
    }

    //----------------------------------------------------------------------
    // プレイヤーの移動
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/10/16  @Update 2014/10/16  @Author T.Kawashita      
    //----------------------------------------------------------------------
    private void Move()
    {
        Vector3 speed = new Vector3(0.0f, 0.0f, 0.0f);
        speed.x = Input.GetAxis(InputXBOX360.P1_XBOX_LEFT_ANALOG_X) * 1.0f;
        speed.z = Input.GetAxis(InputXBOX360.P1_XBOX_LEFT_ANALOG_Y) * 1.0f;
        m_action.Move(ref m_pos, speed, this.transform.forward, this.transform.right);
    }

    //----------------------------------------------------------------------
    // プレイヤーの回転
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/10/16  @Update 2014/10/16   @Author T.Kawashita      
    //----------------------------------------------------------------------
    private void Rotation()
    {
        Vector2 angle;
        Quaternion q;
        angle.x = Input.GetAxis(InputXBOX360.P1_XBOX_RIGHT_ANALOG_X) * 5;
        angle.y = Input.GetAxis(InputXBOX360.P1_XBOX_RIGHT_ANALOG_Y) * 5;
        this.transform.localRotation = m_action.Rotation(ref m_angle, angle.x, angle.y);
    }

    //----------------------------------------------------------------------
    // プレイヤーのダッシュ
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	bool    ダッシュが終了したかどうか
    // @Date	2014/10/16  @Update 2014/10/17  @Author T.Kawashita     
    //----------------------------------------------------------------------
    private bool Dash()
    {
        return m_action.Dash(ref m_pos, this.transform.forward);
    }
}
