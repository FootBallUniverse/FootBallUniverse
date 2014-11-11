using UnityEngine;
using System.Collections;

public class CPlayer1 : CPlayer {

    //----------------------------------------------------------------------
    // コンストラクタ
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/10/15  @Update 2014/10/31  @Author T.Kawashita      
    //----------------------------------------------------------------------
    void Start () {

        this.Init();
        m_pos = this.transform.localPosition;
        m_speed = new Vector3(0.0f,0.0f,0.0f);
        m_angle = new Vector3(0.0f, 0.0f);
        m_status = CPlayerManager.ePLAYER_STATUS.eNONE;
        m_cameraStatus = CPlayerManager.eCAMERA_STATUS.eNORMAL;

        // 国の情報をセット
        m_human = CHumanManager.GetInstance().GetWorldInstance(CHumanManager.eWORLD.eBRAZIL);
        
        // プレイヤーの情報をマップにセット
        Color color = Color.red;
        CPlayerManager.m_playerManager.SetMap(this.gameObject, color);

        // プレイヤーのアニメーターをセット
        m_animator = this.gameObject.transform.parent.GetComponent<CPlayerAnimator>();
    }

    //----------------------------------------------------------------------
    // 更新
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/10/15  @Update 2014/11/11  @Author T.Kawashita      
    //----------------------------------------------------------------------
    void Update () 
    {
        switch (m_status)
        {
            case CPlayerManager.ePLAYER_STATUS.eNONE: PlayerStatusNone(); break;    // 何もしてない状態
            case CPlayerManager.ePLAYER_STATUS.eDASH: PlayerStatusDash(); break;    // ダッシュ中
            case CPlayerManager.ePLAYER_STATUS.eSHOOT: PlayerStatusShoot(); break;  // シュート中
            case CPlayerManager.ePLAYER_STATUS.eEND: break;                         // 終了
        }
    }

    //----------------------------------------------------------------------
    // フレームの最後の更新
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/11/11  @Update 2014/11/11  @Author T.Kawashita      
    //----------------------------------------------------------------------
    void LateUpdate()
    {
        // アニメーション
        this.Animation();

        m_speed = new Vector3(0.0f, 0.0f, 0.0f);    // 最後にスピードを初期化
        this.transform.localPosition = m_pos;       // 保存用位置座標を更新

        // ゲームが終了しているかどうか判定
        this.CheckGamePlay();
    }

    //----------------------------------------------------------------------
    // プレイヤーの通常状態
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/10/28  @Update 2014/10/28  @Author T.Kawashita      
    //----------------------------------------------------------------------
    private void PlayerStatusNone()
    {
        // 移動
        Vector3 speed = new Vector3(Input.GetAxis(InputXBOX360.P1_XBOX_LEFT_ANALOG_X), 0.0f, Input.GetAxis(InputXBOX360.P1_XBOX_LEFT_ANALOG_Y));
        this.Move(speed);
        
        this.Rotation();    // 回転
        this.Dash();        // ダッシュ
        this.Shoot();       // シュート

        this.DebugKey();    // デバッグ
    }

    //----------------------------------------------------------------------
    // プレイヤーがダッシュ中の状態
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/10/28  @Update 2014/11/11  @Author T.Kawashita      
    //----------------------------------------------------------------------
    private void PlayerStatusDash()
    {
        // ダッシュ状態が終わったらプレイヤーのステータス変更
        if (this.Dash() == true)
            m_status = CPlayerManager.ePLAYER_STATUS.eNONE;
    }

    //----------------------------------------------------------------------
    // プレイヤーがシュート中の状態
    //----------------------------------------------------------------------
    // @Param   none		
    // @Return	none
    // @Date	2014/10/28  @Update 2014/10/28  @Author T.Kawashita      
    //----------------------------------------------------------------------
    private void PlayerStatusShoot()
    {
        // シュート状態が終わったら通常状態に遷移
        if (this.Shoot() == true)
            m_status = CPlayerManager.ePLAYER_STATUS.eNONE;
    }

    //----------------------------------------------------------------------
    // プレイヤーの移動
    //----------------------------------------------------------------------
    // @Param	Vector3     移動量		
    // @Return	none
    // @Date	2014/10/16  @Update 2014/11/11  @Author T.Kawashita      
    //----------------------------------------------------------------------
    public override void Move(Vector3 _speed)
    {
        // ボールを持っている場合は遅くなる
        if (m_isBall == true)
        {
            m_speed.x += _speed.x * m_human.m_playerMoveSpeedHold;
            m_speed.z += _speed.z * m_human.m_playerMoveSpeedHold;
        }
        else
        {
            m_speed.x += _speed.x * m_human.m_playerMoveSpeed;
            m_speed.z += _speed.z * m_human.m_playerMoveSpeed;
        }
     
        // 移動アクション
        m_action.Move(ref m_pos, m_speed, this.transform.forward, this.transform.right);
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
        angle.x = Input.GetAxis(InputXBOX360.P1_XBOX_RIGHT_ANALOG_X) * m_human.m_cameraMoveSpeed;
        angle.y = Input.GetAxis(InputXBOX360.P1_XBOX_RIGHT_ANALOG_Y) * m_human.m_cameraMoveSpeed;
        this.transform.localRotation = m_action.Rotation(ref m_angle, angle.x, angle.y);
    }

    //----------------------------------------------------------------------
    // プレイヤーのダッシュ関連処理
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	bool    ダッシュが終了したかどうか
    // @Date	2014/10/16  @Update 2014/10/28  @Author T.Kawashita     
    //----------------------------------------------------------------------
    private bool Dash()
    {
        // ダッシュ状態に遷移
        if (Input.GetKeyDown(InputXBOX360.P1_XBOX_LEFT_ANALOG_PRESS))
        {
            m_action.InitDash(5.0f);
            m_status = CPlayerManager.ePLAYER_STATUS.eDASH;
        }

        else if (m_status == CPlayerManager.ePLAYER_STATUS.eDASH)
            return m_action.Dash(ref m_pos, this.transform.forward);

        return false;
    }

    //----------------------------------------------------------------------
    // プレイヤーのシュート関連処理
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	bool    シュート状態が終わったかどうか
    // @Date	2014/10/27  @Update 2014/10/28  @Author T.Kawashita      
    //----------------------------------------------------------------------
    private bool Shoot()
    {
        // シュートが打てる状態になったら(ボールが手持ちにある場合）
        if (m_status == CPlayerManager.ePLAYER_STATUS.eNONE)
        {
            if (InputXBOX360.IsGetRTButton(InputXBOX360.P1_XBOX_RTLT) && m_isBall)
            {
                m_action.InitShoot(m_human.m_shootInitSpeed, m_human.m_shootMotionLength, m_human.m_shootTakeOfFrame);
                m_status = CPlayerManager.ePLAYER_STATUS.eSHOOT;
                return false;
            }
        }
        
        // シュート中
        else if (m_status == CPlayerManager.ePLAYER_STATUS.eSHOOT)
            return m_action.Shoot(this.gameObject, this.transform.forward, ref m_isBall);

        return false;
    }

    //----------------------------------------------------------------------
    // デバッグ用メソッド
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/10/27  @Update 2014/10/28  @Author T.Kawashita      
    //----------------------------------------------------------------------
    private void DebugKey()
    {
        // Bが押されたらサッカーボールをプレイヤーの足元にセットして
        // サッカーボールをこのプレイヤーにセット
        /*
        if (Input.GetKeyDown(KeyCode.B))
        {
            Vector3 pos = new Vector3(this.transform.FindChild("Player1Camera").transform.localPosition.x, 
                                      this.transform.FindChild("Player1Camera").transform.localPosition.y + 0.03f,
                                      this.transform.FindChild("Player1Camera").transform.localPosition.z + 0.1f);

            // まだプレイヤーのボールではない場合はプレイヤーのボールに設定
            if (m_isBall == false)
            {
                GameObject.Find("BallGameObject").transform.FindChild("SoccerBall").parent = this.transform;
                m_isBall = true;
            }

            this.transform.FindChild("SoccerBall").GetComponent<CSoccerBall>().Init(pos);
        }
         * */
    }

    //----------------------------------------------------------------------
    // アニメーション
    //----------------------------------------------------------------------
    // @Param	none
    // @Return	none
    // @Date	2014/11/11  @Update 2014/11/11  @Author T.Kawashita      
    //----------------------------------------------------------------------
    private void Animation()
    {
        // 移動アニメーション
        m_animator.Move(m_speed);
    }
}
