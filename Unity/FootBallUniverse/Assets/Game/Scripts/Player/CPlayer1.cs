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
            case CPlayerManager.ePLAYER_STATUS.eNONE: PlayerStatusNone();          break;    // 何もしてない状態
            case CPlayerManager.ePLAYER_STATUS.eDASH: PlayerStatusDash();          break;    // ダッシュ中
            case CPlayerManager.ePLAYER_STATUS.eSHOOT: PlayerStatusShoot();        break;    // シュート中
            case CPlayerManager.ePLAYER_STATUS.ePASS: PlayerStatusPass();          break;    // パス中
            case CPlayerManager.ePLAYER_STATUS.eSHOOTCHARGE:                                 // チャージ中
            case CPlayerManager.ePLAYER_STATUS.eDASHCHARGE: PlayerStatusCharge();  break;    // チャージ中
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

        // 回転
        Vector2 angle = new Vector2(Input.GetAxis(InputXBOX360.P1_XBOX_RIGHT_ANALOG_X), Input.GetAxis(InputXBOX360.P1_XBOX_RIGHT_ANALOG_Y));
        this.Rotation(angle);
    
        this.DashTackleDecision();      // ダッシュ
        this.PassShootDecision();       // パスかシュートの判定

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
    // プレイヤーがパス中の状態
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/11/13  @Update 2014/11/13  @Author T.Kawashita      
    //----------------------------------------------------------------------
    private void PlayerStatusPass()
    {
        // パス状態が終わったら通常状態に遷移
        if (this.Pass() == true)
            m_status = CPlayerManager.ePLAYER_STATUS.eNONE;
    }

    //----------------------------------------------------------------------
    // プレイヤーがチャージ中の状態
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/11/14  @Update 2014/11/14  @Author T.Kawashita      
    //----------------------------------------------------------------------
    private void PlayerStatusCharge()
    {
        // 回転
        Vector2 angle = new Vector2(Input.GetAxisRaw(InputXBOX360.P1_XBOX_RIGHT_ANALOG_X), Input.GetAxisRaw(InputXBOX360.P1_XBOX_RIGHT_ANALOG_Y));
        this.Rotation(angle);

        // シュートチャージ中ならシュートの処理のみ
        if( m_status == CPlayerManager.ePLAYER_STATUS.eSHOOTCHARGE )
            this.PassShootDecision();       // パスかシュートの判定

        // ダッシュチャージ中ならダッシュの処理のみ
        if (m_status == CPlayerManager.ePLAYER_STATUS.eDASHCHARGE)
            this.DashTackleDecision();      // ダッシュかタックルの判定
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
    // @Param	Vector2     回転量		
    // @Return	none
    // @Date	2014/10/16  @Update 2014/11/12   @Author T.Kawashita      
    //----------------------------------------------------------------------
    public override void Rotation(Vector2 _angle)
    {
        Vector3 angle = new Vector3(0.0f,0.0f,0.0f);
        if (m_status == CPlayerManager.ePLAYER_STATUS.eNONE)
        {
            angle.y = _angle.x * m_human.m_cameraMoveSpeed;
            angle.x = _angle.y * m_human.m_cameraMoveSpeed;
        }
        else if (m_status == CPlayerManager.ePLAYER_STATUS.eSHOOTCHARGE)
        {
            angle.y = _angle.x * m_human.m_cameraMoveSpeedCharging;
            angle.x = _angle.y * m_human.m_cameraMoveSpeedCharging;
        }

        this.transform.Rotate(angle);
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
         if (m_status == CPlayerManager.ePLAYER_STATUS.eDASH)
            return m_action.Dash(ref m_pos, this.transform.forward);

        return false;
    }

    //----------------------------------------------------------------------
    // プレイヤーのシュート関連処理
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	bool    シュート状態が終わったかどうか
    // @Date	2014/10/27  @Update 2014/11/13  @Author T.Kawashita      
    //----------------------------------------------------------------------
    private bool Shoot()
    {        
        // シュート中
        if (m_status == CPlayerManager.ePLAYER_STATUS.eSHOOT)
            return m_action.Shoot(this.gameObject, this.transform.forward, ref m_isBall);

        return false;
    }

    //----------------------------------------------------------------------
    // プレイヤーのパス関連処理
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	bool    パス状態が終わったかどうか
    // @Date	2014/11/13  @Update 2014/11/13  @Author T.Kawashita      
    //----------------------------------------------------------------------
    private bool Pass()
    {
        // パス中
        if (m_status == CPlayerManager.ePLAYER_STATUS.ePASS)
            return m_action.Pass(this.gameObject, this.transform.forward, ref m_isBall);

        return false;
    }

    //----------------------------------------------------------------------
    // パスとシュートの判定
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/11/13  @Update 2014/11/13  @Author T.Kawashita      
    //----------------------------------------------------------------------
    private void PassShootDecision()
    {
        // シュートかパスが打てる状態になったら(ボールが手持ちにある場合）
        if ((m_status == CPlayerManager.ePLAYER_STATUS.eNONE || m_status == CPlayerManager.ePLAYER_STATUS.eSHOOTCHARGE )
            && m_isBall == true && InputXBOX360.IsGetRTButton(InputXBOX360.P1_XBOX_RT) == true)
        {
            m_chargeFrame = InputXBOX360.RTButtonPress(InputXBOX360.P1_XBOX_RT);
            m_status = CPlayerManager.ePLAYER_STATUS.eSHOOTCHARGE;
            m_animator.ChangeAnimation("KickCharge");
            return;
        }
        
        if(m_status == CPlayerManager.ePLAYER_STATUS.eSHOOTCHARGE && m_isBall == true &&
           InputXBOX360.IsGetRTButton(InputXBOX360.P1_XBOX_RT) == false)
        {
            // チャージ時間が一定量以上ならシュート
            if (m_chargeFrame >= m_human.m_shootChargeLength)
            {
                m_action.InitShoot(m_human.m_shootInitSpeed, m_human.m_shootMotionLength, m_human.m_shootTakeOfFrame);
                m_status = CPlayerManager.ePLAYER_STATUS.eSHOOT;
                m_animator.ChangeAnimation("NormalShoot");
            }
            // シュートじゃなくてチャージ時間が一定量以上ならパス
            else if (m_chargeFrame >= m_human.m_passChargeLength)
            {
                m_action.InitPass(m_human.m_passInitSpeed, m_human.m_passMotionLength, m_human.m_passTakeOfFrame);
                m_status = CPlayerManager.ePLAYER_STATUS.ePASS;
            }
            // それ以外の場合はそのまま戻る
            else
            {
                m_animator.ChangeAnimation(m_animator.m_isWait);
                m_status = CPlayerManager.ePLAYER_STATUS.eNONE;
            }

            // 初期化
            InputXBOX360.RTButtonPress(InputXBOX360.P1_XBOX_RT);
            m_chargeFrame = 0;
        }
    }

    //----------------------------------------------------------------------
    // ダッシュとタックルの判定
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/11/14  @Update 2014/11/14  @Author T.Kawashita      
    //----------------------------------------------------------------------
    private void DashTackleDecision()
    {
        // ダッシュが出来る状態になったら(ボールを持っていなかったら)
        if ((m_status == CPlayerManager.ePLAYER_STATUS.eNONE || m_status == CPlayerManager.ePLAYER_STATUS.eDASHCHARGE)
            && m_isBall == false && InputXBOX360.IsGetLTButton(InputXBOX360.P1_XBOX_LT) == true)
        {
            // 通常状態の場合はアニメーション切り替え
            if (m_status == CPlayerManager.ePLAYER_STATUS.eNONE)
            {
                m_status = CPlayerManager.ePLAYER_STATUS.eDASHCHARGE;
                m_animator.ChangeAnimation("DashCharge");
            }

            m_chargeFrame = InputXBOX360.LTButtonPress(InputXBOX360.P1_XBOX_LT);
            
            return;
        }

        // LTボタンが離されたら
        if (m_status == CPlayerManager.ePLAYER_STATUS.eDASHCHARGE && m_isBall == false &&
            InputXBOX360.IsGetLTButton(InputXBOX360.P1_XBOX_LT) == false)
        {
            // チャージ時間が一定量以上ならタックル
            if (m_chargeFrame >= m_human.m_tackleChangeLength)
            {
//                m_action.InitShoot(m_human.m_shootInitSpeed, m_human.m_shootMotionLength, m_human.m_shootTakeOfFrame);
//                m_status = CPlayerManager.ePLAYER_STATUS.eSHOOT;
//                m_animator.ChangeAnimation(m_animator.m_isNormalShoot);
            }
            // タックルじゃなくてチャージ時間が一定量以上ならダッシュ
            else if (m_chargeFrame >= m_human.m_dashChargeLength)
            {
                m_action.InitDash(m_human.m_dashInitSpeed, m_human.m_dashMotionLength, m_human.m_dashDecFrame);
                m_status = CPlayerManager.ePLAYER_STATUS.eDASH;
                m_animator.ChangeAnimation("Dash");
            }
            // それ以外の場合はそのまま戻る
            else
            {
                m_status = CPlayerManager.ePLAYER_STATUS.eNONE;
                m_animator.ChangeAnimation(m_animator.m_isWait);
            }

            // 初期化
            InputXBOX360.LTButtonPress(InputXBOX360.P1_XBOX_LT);
            m_chargeFrame = 0;
        }
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
