﻿using UnityEngine;
using System.Collections;

public class CPlayer1 : CPlayer {

    const float DASH_SPEED = 1.0f;
    private Vector3 m_speed;

	// Use this for initialization
	void Start () {

        this.Init();
        m_pos = this.transform.localPosition;
        m_angle = new Vector3(0.0f, 0.0f);
        m_status = CPlayerManager.ePLAYER_STATUS.eNONE;
        m_cameraStatus = CPlayerManager.eCAMERA_STATUS.eNORMAL;

        m_human = CHumanManager.GetInstance().GetWorldInstance(CHumanManager.eWORLD.eBRAZIL);
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
                        this.DebugKey();
                        // ダッシュ初期化
                        if (Input.GetKeyDown(InputXBOX360.P1_XBOX_LEFT_ANALOG_PRESS))
                        {
                            m_action.InitDash(5.0f);
                            m_status = CPlayerManager.ePLAYER_STATUS.eDASH;
                        }

                        // シュート
                        if (Input.GetKeyDown(InputXBOX360.P1_XBOX_R))
                        {
                            m_action.InitShoot(m_human.m_shootInitSpeed, m_human.m_shootMotionLength, m_human.m_shootTakeOfFrame);
                            m_status = CPlayerManager.ePLAYER_STATUS.eSHOOT;
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

                    // シュートモーション
                    case CPlayerManager.ePLAYER_STATUS.eSHOOT:
                        if (this.Shoot() == true)
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
        speed.x = Input.GetAxis(InputXBOX360.P1_XBOX_LEFT_ANALOG_X) * m_human.m_playerMoveSpeed;
        speed.z = Input.GetAxis(InputXBOX360.P1_XBOX_LEFT_ANALOG_Y) * m_human.m_playerMoveSpeed;
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

    //----------------------------------------------------------------------
    // プレイヤーのシュート
    //----------------------------------------------------------------------
    // @Param			
    // @Return	
    // @Date	  @Update   @Author       
    //----------------------------------------------------------------------
    private bool Shoot()
    {
        return m_action.Shoot(this.transform.FindChild("SoccerBall").GetComponent<CSoccerBall>(),this.transform.forward);
    }


    //----------------------------------------------------------------------
    // デバッグ用メソッド
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/10/27  @Update 2014/10/27  @Author T.Kawashita      
    //----------------------------------------------------------------------
    private void DebugKey()
    {
        // Bが押されたらサッカーボールをプレイヤーの足元にセットして
        // サッカーボールをこのプレイヤーにセット
        if (Input.GetKeyDown(KeyCode.B))
        {
            Vector3 pos = new Vector3(this.transform.FindChild("player").transform.localPosition.x, 
                                      this.transform.FindChild("player").transform.localPosition.y + 0.03f,
                                      this.transform.FindChild("player").transform.localPosition.z + 0.1f);
            this.transform.FindChild("SoccerBall").GetComponent<CSoccerBall>().SetPosition(pos);
        }
    }
}
