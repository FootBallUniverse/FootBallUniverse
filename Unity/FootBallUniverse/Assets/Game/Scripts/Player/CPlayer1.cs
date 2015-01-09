﻿using UnityEngine;
using System.Collections;

public class CPlayer1 : CPlayer {

    //----------------------------------------------------------------------
    // コンストラクタ
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/10/15  @Update 2014/10/31  @Author T.Kawashita
    // @Update  2014/12/30  一部処理を一括に変更  
    //----------------------------------------------------------------------
    void Start () {

        this.Init();

        // プレイヤーのデータをセット
        CPlayerManager.SetPlayerData(this.m_playerData, CPlayerManager.PLAYER_1);
	
        // 国の情報をセット / 国によってマテリアルを変更
        m_human = CHumanManager.GetWorldInstance(TeamData.teamNationality[0]);
        this.transform.FindChild("polySurface14").GetComponent<CPlayer1Mesh>().ChangeMaterial(TeamData.teamNationality[0]);
        m_gauge.m_teamNo = 1;

        // プレイヤーごとの値をセット
        this.SetData();

        // プレイヤーのアニメーターをセット
        m_animator = this.gameObject.transform.parent.GetComponent<CPlayerAnimator>();

        // カメラをセット
        m_camera = this.gameObject.transform.parent.FindChild("Player1Camera").GetComponent<PlayerCamera>();
        m_trans = this.transform.Find("LookTrans").transform; 
    }

    //----------------------------------------------------------------------
    // 更新
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/10/15  @Update 2014/11/11  @Author T.Kawashita
    //          2015/01/09  @Update 2015/01/09  @Author T.Takeuchi  
    //----------------------------------------------------------------------
    void Update () 
	{
        // ボールを持っている場合位置を先に変更
        if (m_isBall == true)
            this.transform.FindChild("SoccerBall").GetComponent<CSoccerBall>().SetPosition(new Vector3(0.0f, -0.13f, 0.14f));

        m_pos = this.transform.localPosition;

        CGaugeManager.m_p1Gauge = m_gauge.m_gauge;

        switch (m_status)
        {
			case CPlayerManager.ePLAYER_STATUS.eTUTORIAL: PlayerTutorial();                 break;    // チュートリアル中の状態
            case CPlayerManager.ePLAYER_STATUS.eWAIT: PlayerStatusWait();                   break;    // 始めの待機状態
            case CPlayerManager.ePLAYER_STATUS.eCOUNTDOWN: PlayerStatusGameStartWait();     break;    // カウントダウンの状態
            case CPlayerManager.ePLAYER_STATUS.eNONE: PlayerStatusNone();                   break;    // 何もしてない状態
            case CPlayerManager.ePLAYER_STATUS.eDASH: PlayerStatusDash();                   break;    // ダッシュ中
            case CPlayerManager.ePLAYER_STATUS.eTACKLE: PlayerStatusTackle();               break;    // タックル中
            case CPlayerManager.ePLAYER_STATUS.eTACKLEDAMAGE: PlayerStatusTackleDamage();   break;    // タックルのダメージ受け中
            case CPlayerManager.ePLAYER_STATUS.eTACKLESUCCESS: PlayerStatusTackleSuccess(); break;    // タックル成功中
            case CPlayerManager.ePLAYER_STATUS.eSHOOT: PlayerStatusShoot();                 break;    // シュート中
            case CPlayerManager.ePLAYER_STATUS.ePASS: PlayerStatusPass();                   break;    // パス中
            case CPlayerManager.ePLAYER_STATUS.eSHOOTCHARGE:                                          // チャージ中
            case CPlayerManager.ePLAYER_STATUS.eDASHCHARGE: PlayerStatusCharge();           break;    // チャージ中
            case CPlayerManager.ePLAYER_STATUS.eEND:                                        break;    // 終了
            case CPlayerManager.ePLAYER_STATUS.eGOAL: PlayerStatusGoal();                   break;    // ゴールした時は何もさせない
            case CPlayerManager.ePLAYER_STATUS.eOVERRIMIT: PlayerStatusOverRimit();         break;    // オーバーリミット状態
        }

    }

    //----------------------------------------------------------------------
    // 物理挙動の更新
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/12/7  @Update 2014/12/7  @Author T.Kaneko      
    //----------------------------------------------------------------------
    void FixedUpdate()
    {
        this.rigidbody.MovePosition(m_pos);
    }

    //----------------------------------------------------------------------
    // フレームの最後の更新
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/11/11  @Update 2014/11/17  @Author T.Kawashita      
    //----------------------------------------------------------------------
    void LateUpdate()
    {
		// アニメーション
        this.Animation();
	
        m_speed = new Vector3(0.0f, 0.0f, 0.0f);    // 最後にスピードを初期化

        // プレイヤーのカメラを更新する
        this.transform.parent.FindChild("Player1Camera").GetComponent<PlayerCamera>().CameraUpdate();

        // 最後に位置をマネージャークラスにセットしておく
        CPlayerManager.m_player1Transform = this.transform;

        // ゲームが終了しているかどうか判定
        this.CheckGamePlay();
    }

    //----------------------------------------------------------------------
    // プレイヤーの通常状態
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/10/28  @Update 2014/11/17  @Author T.Kawashita   
    //----------------------------------------------------------------------
    private void PlayerStatusNone()
    {
        // 移動
        Vector3 speed = new Vector3(Input.GetAxis(InputXBOX360.P1_XBOX_LEFT_ANALOG_X), 0.0f, Input.GetAxis(InputXBOX360.P1_XBOX_LEFT_ANALOG_Y));
        this.Move(speed);

        // 回転
        Vector2 angle = new Vector2(Input.GetAxis(InputXBOX360.P1_XBOX_RIGHT_ANALOG_X), Input.GetAxis(InputXBOX360.P1_XBOX_RIGHT_ANALOG_Y));
        this.Rotation(angle);

    
        this.LTDashTackle();        // ダッシュかタックルの判定
        this.RTShootPass();         // パスかシュートの判定

        this.GaugeAction();         // ゲージのアクション状態

        this.ChangeViewPoint();     // 視点変更
    }

    //----------------------------------------------------------------------
    // プレイヤーの待機中状態
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/11/18  @Update 2014/11/18  @Author T.Kawashita      
    //----------------------------------------------------------------------
    private void PlayerStatusWait()
    {
        // 状態を遷移
        if (CGameManager.m_nowStatus == CGameManager.eSTATUS.eCOUNTDOWN)
        {
            m_status = CPlayerManager.ePLAYER_STATUS.eCOUNTDOWN;
        }

        if (CGameManager.m_nowStatus == CGameManager.eSTATUS.eGAME)
        {
            m_status = CPlayerManager.ePLAYER_STATUS.eNONE;
            m_gauge.m_status = CPlayerGauge.eGAUGESTATUS.eNORMAL;
        }
    }

    //----------------------------------------------------------------------
    // プレイヤーのオーバーリミット状態
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2015/1/6  @Update 2014/1/6  @Author T.Kawashita      
    //----------------------------------------------------------------------
    private void PlayerStatusOverRimit()
    {
        Vector3 speed = new Vector3(Input.GetAxis(InputXBOX360.P1_XBOX_LEFT_ANALOG_X), 0.0f, Input.GetAxis(InputXBOX360.P1_XBOX_LEFT_ANALOG_Y));

        // 国によってオーバーリミットの状態を変える
        switch(TeamData.teamNationality[0])
        {
            case TeamData.TEAM_NATIONALITY.JAPAN:
            break;

            case TeamData.TEAM_NATIONALITY.ENGLAND:
            break;

            case TeamData.TEAM_NATIONALITY.ESPANA:
            break;

            // ブラジル
            case TeamData.TEAM_NATIONALITY.BRASIL:
            speed.x *= CGaugeManager.m_brazilSpeedRate;
            speed.y *= CGaugeManager.m_brazilSpeedRate;
            speed.z *= CGaugeManager.m_brazilSpeedRate;
            break;
        }

        // 移動
        this.Move(speed);

        // 回転
        Vector2 angle = new Vector2(Input.GetAxis(InputXBOX360.P1_XBOX_RIGHT_ANALOG_X), Input.GetAxis(InputXBOX360.P1_XBOX_RIGHT_ANALOG_Y));
        this.Rotation(angle);


        this.LTDashTackle();        // ダッシュかタックルの判定
        this.RTShootPass();         // パスかシュートの判定

        this.ChangeViewPoint();     // 視点変更

        // ゲージが0になったら
        if (m_gauge.m_status == CPlayerGauge.eGAUGESTATUS.eNORMAL)
        {
            m_status = CPlayerManager.ePLAYER_STATUS.eNONE;
            m_isOverRimit = false;

            // 日本の場合は取れる範囲を元に戻す
            if (TeamData.teamNationality[0] == TeamData.TEAM_NATIONALITY.JAPAN)
            {
                // ボールの取れる範囲をセット
                this.GetComponent<SphereCollider>().radius = m_human.m_holdRangeRadius;
            }

            // 終了エフェクト
			this.transform.FindChild("PlayerEffect").transform.GetComponent<CEffect>().OverRimitOff();
			m_playerSE.PlaySE("game/alert_pass");

        }
    }

    //----------------------------------------------------------------------
    // プレイヤーのカウントダウン中の状態
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/11/18  @Update 2014/11/18  @Author T.Kawashita      
    //----------------------------------------------------------------------
    private void PlayerStatusGameStartWait()
    {
        // 回転
        Vector2 angle = new Vector2(Input.GetAxis(InputXBOX360.P1_XBOX_RIGHT_ANALOG_X), Input.GetAxis(InputXBOX360.P1_XBOX_RIGHT_ANALOG_Y));
        this.Rotation(angle);

        // 状態を遷移
        if (CGameManager.m_nowStatus == CGameManager.eSTATUS.eGAME)
        {
            m_status = CPlayerManager.ePLAYER_STATUS.eNONE;
            m_gauge.m_status = CPlayerGauge.eGAUGESTATUS.eNORMAL;
        }
    }

    //----------------------------------------------------------------------
    // プレイヤーのゴール中の状態
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/11/25  @Update 2014/11/25  @Author T.Kawashita      
    //----------------------------------------------------------------------
    private void PlayerStatusGoal()
    {
        // フェードインする状態になったら位置を初期化
        if (CGameManager.m_nowStatus == CGameManager.eSTATUS.eRESTART)
        {
            this.Restart();
        }
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
        {
            m_animator.Wait();
            m_status = m_oldStatus;
        } 
    }

    //----------------------------------------------------------------------
    // プレイヤーがタックル中の状態
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/11/17  @Update 2014/11/17  @Author T.Kawashita      
    //----------------------------------------------------------------------
    private void PlayerStatusTackle()
    {
        // タックル状態が終わったらプレイヤーのステータス変更
        if (this.Tackle () == true)
		{
			m_animator.Wait();	
			m_status = m_oldStatus;
		}
	}

    //----------------------------------------------------------------------
    // プレイヤーがタックル成功中の状態
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/12/1  @Update 2014/12/1  @Author T.Kawashita      
    //----------------------------------------------------------------------
    private void PlayerStatusTackleSuccess()
    {
        // タックル成功状態が終わったらプレイヤーのステータス変更
        if (m_status == CPlayerManager.ePLAYER_STATUS.eTACKLESUCCESS)
        {
            if (m_action.TackleSuccess() == true)
            {
                m_animator.Wait();
                m_status = m_oldStatus;
            }
        }
    }

    //----------------------------------------------------------------------
    // プレイヤーがタックルダメージ受け中の状態
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/12/1  @Update 2014/12/1  @Author T.Kawashita      
    //----------------------------------------------------------------------
    private void PlayerStatusTackleDamage()
    { 
        // タックルダメージ受け中状態が終わったらプレイヤーのステータス変更
        if (m_status == CPlayerManager.ePLAYER_STATUS.eTACKLEDAMAGE)
        {
            if (m_action.TackleDamage(ref m_pos,-this.transform.forward) == true)
            {
                m_animator.Wait();
                m_status = m_oldStatus;
            }
        }
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
		if (this.Shoot () == true) 
		{
			m_animator.Wait();
			m_status = m_oldStatus;
		}
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
        if (this.Pass () == true) 
		{
			m_animator.Wait();
			m_status = m_oldStatus;
		}
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
            this.ShootHold();               // シュートホールド状態

        // ダッシュチャージ中ならダッシュの処理のみ
        if (m_status == CPlayerManager.ePLAYER_STATUS.eDASHCHARGE)
            this.DashHold();                // ダッシュホールド状態
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
    // プレイヤーのタックル関連処理
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/11/17  @Update 2014/11/17  @Author T.Kawashita      
    //----------------------------------------------------------------------
    private bool Tackle()
    {
        if (m_status == CPlayerManager.ePLAYER_STATUS.eTACKLE)
            return m_action.Tackle(ref m_pos, this.transform.forward);

        return false;
    }

    //----------------------------------------------------------------------
    // プレイヤーのシュート
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	bool    シュート状態が終わったかどうか
    // @Date	2014/10/27  @Update 2014/11/13  @Author T.Kawashita      
    //----------------------------------------------------------------------
    private bool Shoot()
    {        
        // 普通のシュート中
        if (m_status == CPlayerManager.ePLAYER_STATUS.eSHOOT && m_isOverRimit == false)
            return m_action.Shoot(this.gameObject, this.transform.forward, ref m_isBall);

        // OverRimitのシュート
        if (m_status == CPlayerManager.ePLAYER_STATUS.eSHOOT && m_isOverRimit == true)
            return m_action.OverRimitShoot(this.gameObject, this.transform.forward, ref m_isBall, TeamData.teamNationality[0]);

        return false;
    }

    //----------------------------------------------------------------------
    // プレイヤーのパス
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	bool    パス状態が終わったかどうか
    // @Date	2014/11/13  @Update 2014/11/13  @Author T.Kawashita      
    //----------------------------------------------------------------------
    private bool Pass()
    {
        // パス中
        if (m_status == CPlayerManager.ePLAYER_STATUS.ePASS && m_isOverRimit == false)
            return m_action.Pass(this.gameObject, this.transform.forward, ref m_isBall);

        // OverRimitのパス
        if (m_status == CPlayerManager.ePLAYER_STATUS.ePASS && m_isOverRimit == true)
            return m_action.OverRimitPass(this.gameObject, this.transform.forward, ref m_isBall, TeamData.teamNationality[0]);


        return false;
    }

    //----------------------------------------------------------------------
    // RTボタンが押された時
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/11/13  @Update 2014/11/13  @Author T.Kawashita      
    //----------------------------------------------------------------------
    private void RTShootPass()
    {
        // シュートかパスが打てる状態になったら(ボールが手持ちにある場合）
        if ((m_status == CPlayerManager.ePLAYER_STATUS.eNONE ||
             m_status == CPlayerManager.ePLAYER_STATUS.eOVERRIMIT ||
			 m_status == CPlayerManager.ePLAYER_STATUS.eTUTORIAL) &&
             m_isBall == true && 
             InputXBOX360.IsGetRTButton(InputXBOX360.P1_XBOX_RT) == true && 
             m_isRtPress == false )
        {
            m_oldStatus = m_status;
            m_status = CPlayerManager.ePLAYER_STATUS.eSHOOTCHARGE;
            m_chargeFrame = 0;
            m_isRtPress = true;
            m_playerSE.PlaySE("game/charging");
            return;
        }

        // 離されたら押しっぱなしフラグOFF
        else if (InputXBOX360.IsGetRTButton(InputXBOX360.P1_XBOX_RT) == false &&
		         Input.GetKey(KeyCode.Space) == false)
        {
            m_isRtPress = false;
        }
    }

    //----------------------------------------------------------------------
    // シュートのホールド待機状態
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/11/17  @Update 2014/11/17  @Author T.Kawashita      
    //----------------------------------------------------------------------
    private void ShootHold()
    {
        // 押されている間はチャージのフレーム取得
        if( InputXBOX360.IsGetRTButton(InputXBOX360.P1_XBOX_RT) )
            m_chargeFrame = InputXBOX360.RTButtonPress(InputXBOX360.P1_XBOX_RT,ref m_chargeFrame);

        if (m_chargeFrame >= m_human.m_tackleChangeLength && m_isSE == false)
        {
            m_playerSE.PlaySE("game/charge_finish");
            m_isSE = true;
        }

        // チャージ時間が一定量以上ならシュートホールド状態終了
        if (m_status == CPlayerManager.ePLAYER_STATUS.eSHOOTCHARGE && 
            m_isBall == true && 
            m_chargeFrame >= m_human.m_shootChargeLengthMax)
        {
            m_status = m_oldStatus;
            m_animator.ChangeAnimation(m_animator.m_isWait);
            m_playerSE.StopSE();
            m_isSE = false;
            return;
        }
        
        // RTボタンが離されたらシュートかチャージ
        if(m_status == CPlayerManager.ePLAYER_STATUS.eSHOOTCHARGE && 
            m_isBall == true &&
            InputXBOX360.IsGetRTButton(InputXBOX360.P1_XBOX_RT) == false &&
		    Input.GetKey(KeyCode.Space) == false)
        {
            // チャージ時間が一定量以上ならシュート
            if (m_chargeFrame >= m_human.m_shootChargeLength)
            {
                m_action.InitShoot(m_human.m_shootInitSpeed, m_human.m_shootMotionLength, m_human.m_shootTakeOfFrame);
                m_status = CPlayerManager.ePLAYER_STATUS.eSHOOT;
                m_playerSE.StopSE();
                m_playerSE.PlaySE("game/kick_shoot");
            }
            // シュートじゃなくてチャージ時間が一定量以上ならパス
            else if (m_chargeFrame >= m_human.m_passChargeLength)
            {
                m_action.InitPass(m_human.m_passInitSpeed, m_human.m_passMotionLength, m_human.m_passTakeOfFrame);
                m_status = CPlayerManager.ePLAYER_STATUS.ePASS;
                m_playerSE.StopSE();
                m_playerSE.PlaySE("game/kick_pass");
            }

            // 初期化
            m_isSE = false;
            m_chargeFrame = InputXBOX360.RTButtonPress(InputXBOX360.P1_XBOX_RT,ref m_chargeFrame);
        }
    }

    //----------------------------------------------------------------------
    // LTボタンが押された時
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/11/14  @Update 2014/11/14  @Author T.Kawashita      
    //----------------------------------------------------------------------
    private void LTDashTackle()
    {
        // ダッシュが出来る状態になったら(ボールを持っていなかったら)
        if ((m_status == CPlayerManager.ePLAYER_STATUS.eNONE || 
            m_status == CPlayerManager.ePLAYER_STATUS.eOVERRIMIT ||
			m_status == CPlayerManager.ePLAYER_STATUS.eTUTORIAL) &&
             m_isBall == false && 
             InputXBOX360.IsGetLTButton(InputXBOX360.P1_XBOX_LT) == true &&
             m_isLtPress == false)
        {
            m_oldStatus = m_status;
            m_status = CPlayerManager.ePLAYER_STATUS.eDASHCHARGE;
            m_chargeFrame = 0;
            m_isLtPress = true;
			m_playerSE.PlaySE("game/charging");
            return;
        }

        else if( InputXBOX360.IsGetLTButton(InputXBOX360.P1_XBOX_LT ) == false &&
		         Input.GetKey(KeyCode.LeftShift) == false)
        {
            m_isLtPress = false;
        }
    }

    //----------------------------------------------------------------------
    // ダッシュのホールド待機状態
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/11/17  @Update 2014/11/17  @Author T.Kawashita      
    //----------------------------------------------------------------------
    private void DashHold()
    {
		// チャージフレーム取得
        if( InputXBOX360.IsGetRTButton(InputXBOX360.P1_XBOX_LT))
            m_chargeFrame = InputXBOX360.LTButtonPress(InputXBOX360.P1_XBOX_LT, ref m_chargeFrame);

        if (m_chargeFrame >= m_human.m_tackleChangeLength && m_isSE == false)
        {
            m_playerSE.PlaySE("game/charge_finish");
            m_isSE = true;
        }

        // チャージ時間が一定量以上になったらチャージ状態終了
        if( m_status == CPlayerManager.ePLAYER_STATUS.eDASHCHARGE &&
            m_isBall == false &&
            m_chargeFrame >= m_human.m_dashChargeLengthMax )
        {
            m_status  = m_oldStatus;
			m_animator.Wait();
			m_playerSE.StopSE();
            m_isSE = false;
            return;
        }

        // LTボタンが離されたら
        if (m_status == CPlayerManager.ePLAYER_STATUS.eDASHCHARGE &&
            m_isBall == false &&
            InputXBOX360.IsGetLTButton(InputXBOX360.P1_XBOX_LT) == false &&
		    Input.GetKey (KeyCode.LeftShift) == false)
        {
            m_camera.ChangeMspeedlock();
            // チャージ時間が一定量以上ならタックル
            if (m_chargeFrame >= m_human.m_tackleChangeLength)
            {
                m_action.InitTackle(m_human.m_tackleInitSpeed, m_human.m_tackleMotionLength, m_human.m_tackleDecFrame);
                m_status = CPlayerManager.ePLAYER_STATUS.eTACKLE;
				m_playerSE.StopSE();
				m_playerSE.PlaySE("game/tackle_go");
            }
            // タックルじゃなくてチャージ時間が一定量以上ならダッシュ
            else if (m_chargeFrame >= m_human.m_dashChargeLength)
            {
                m_action.InitDash(m_human.m_dashInitSpeed, m_human.m_dashMotionLength, m_human.m_dashDecFrame);
                m_status = CPlayerManager.ePLAYER_STATUS.eDASH;
				m_playerSE.StopSE();
				m_playerSE.PlaySE("game/dash");
			}

            // 初期化
            m_isSE = false;
            m_chargeFrame = InputXBOX360.LTButtonPress(InputXBOX360.P1_XBOX_LT, ref m_chargeFrame);
        }
    }

    //----------------------------------------------------------------------
    // アニメーション
    //----------------------------------------------------------------------
    // @Param	none
    // @Return	none
    // @Date	2014/11/11  @Update 2014/11/11  @Author T.Kawashita
    // @Update  2014/11/26  タックルモーション追加  
    //----------------------------------------------------------------------
    private void Animation()
    {
        switch (m_status)
        {
            case CPlayerManager.ePLAYER_STATUS.eNONE:
            case CPlayerManager.ePLAYER_STATUS.eWAIT:
            case CPlayerManager.ePLAYER_STATUS.eCOUNTDOWN:
            case CPlayerManager.ePLAYER_STATUS.eOVERRIMIT:
			case CPlayerManager.ePLAYER_STATUS.eTUTORIAL:
                m_animator.Move(m_speed); break;
            case CPlayerManager.ePLAYER_STATUS.eSHOOTCHARGE: 
                m_animator.ShootCharge(); break;
            case CPlayerManager.ePLAYER_STATUS.ePASS:
                m_animator.Pass(); break;
            case CPlayerManager.ePLAYER_STATUS.eSHOOT:
                m_animator.Shoot(); break;
            case CPlayerManager.ePLAYER_STATUS.eDASHCHARGE:
                m_animator.DashCharge(); break;
            case CPlayerManager.ePLAYER_STATUS.eDASH:
                m_animator.Dash(); break;
            case CPlayerManager.ePLAYER_STATUS.eTACKLE:
                m_animator.Tackle(); break;

        }
    }

    //----------------------------------------------------------------------
    // 視点変更
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/11/30  @Update 2014/11/30  @Author T.Kawashita      
    //----------------------------------------------------------------------
    public override void ChangeViewPoint()
    {
        switch (m_viewPointStatus) 
        {
			case CPlayerManager.eVIEW_POINT_STATUS.ePLAYER:
			if (Input.GetKeyDown (InputXBOX360.P1_XBOX_X) || Input.GetKeyDown (InputXBOX360.P1_XBOX_Y) || Input.GetKeyDown (InputXBOX360.P1_XBOX_B) || Input.GetKeyDown (InputXBOX360.P1_XBOX_A))
            {
                m_playerSE.PlaySE("game/rockon");
				m_camera.ChangeRspeedlock ();
			}

            // ボールの方向に向ける
			if (Input.GetKey (InputXBOX360.P1_XBOX_B) && m_isBall == false) 
			{
				m_trans.LookAt (CSoccerBallManager.m_soccerBallTransform);
				this.transform.rotation = Quaternion.Slerp (this.transform.rotation, m_trans.rotation, m_camera.Rcameraspeed * Time.deltaTime);
				return;
			}

            // 2Pの方向に向ける
			if (Input.GetKey (InputXBOX360.P1_XBOX_X)) {
					m_trans.LookAt (CPlayerManager.m_player2Transform);
					this.transform.rotation = Quaternion.Slerp (this.transform.rotation, m_trans.rotation, m_camera.Rcameraspeed * Time.deltaTime);
					return;
			}

            // 敵ゴールへ向ける
			if (Input.GetKey (InputXBOX360.P1_XBOX_Y)) {
					m_trans.LookAt (CStageManager.m_3p4pGoalTransform);
					this.transform.rotation = Quaternion.Slerp (this.transform.rotation, m_trans.rotation, m_camera.Rcameraspeed * Time.deltaTime);
					return;
			}

            // 自分のゴールへ向ける
			if (Input.GetKey (InputXBOX360.P1_XBOX_A)) {
					m_trans.LookAt (CStageManager.m_1p2pGoalTransform);
					this.transform.rotation = Quaternion.Slerp (this.transform.rotation, m_trans.rotation, m_camera.Rcameraspeed * Time.deltaTime);
					return;
			}
			break;
        }
    }

    //----------------------------------------------------------------------
    // ゲージ解放アクション
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2015/1/3  @Update 2015/1/3  @Author T.Kawashita      
    //----------------------------------------------------------------------
    public void GaugeAction()
    {
        // 右アナログスティックが押し込まれたら
        if (Input.GetKeyDown(InputXBOX360.P1_XBOX_RIGHT_ANALOG_PRESS))
        {
            if (m_gauge.GaugeDecrement() != 0)
            {
                // ここにエフェクトの開始とかを入れる
				this.transform.FindChild("PlayerEffect").transform.GetComponent<CEffect>().OverRimitOn();
				m_playerSE.PlaySE("game/kick_smash_echor");
				m_status = CPlayerManager.ePLAYER_STATUS.eOVERRIMIT;
                m_isOverRimit = true;

                // 日本の場合はボールを取れる範囲を増やす
                if (TeamData.teamNationality[0] == TeamData.TEAM_NATIONALITY.JAPAN)
                {
                    // ボールの取れる範囲を変更
                    this.GetComponent<SphereCollider>().radius = CGaugeManager.m_japanHoldRadius;
					this.transform.FindChild("PlayerEffect").transform.GetComponent<CEffect>().effectOverRimit.particleSystem.startSize = CGaugeManager.m_japanHoldRadius * 50;

                }
            }
        }

    }


	//----------------------------------------------------------------------
	// チュートリアル用アクション
	//----------------------------------------------------------------------
	// @Param	none		
	// @Return	none
	// @Date	2015/1/9  @Update 2015/1/9  @Author T.Takeuchi    
	//----------------------------------------------------------------------
	public void PlayerTutorial()
	{
		Vector3 speed = new Vector3(0.0f,0.0f,0.0f);
		Vector2 angle = new Vector2(0.0f,0.0f);

		// 移動
		if (this.m_controlePermission.move_x) speed.x = Input.GetAxis(InputXBOX360.P1_XBOX_LEFT_ANALOG_X);
		if (this.m_controlePermission.move_z) speed.z = Input.GetAxis(InputXBOX360.P1_XBOX_LEFT_ANALOG_Y);
		this.Move(speed);

		// 回転
		if (this.m_controlePermission.rotate_x) angle.x = Input.GetAxis(InputXBOX360.P1_XBOX_RIGHT_ANALOG_X);
		if (this.m_controlePermission.rotate_y) angle.y = Input.GetAxis(InputXBOX360.P1_XBOX_RIGHT_ANALOG_Y);
		this.Rotation(angle);

		if (this.m_controlePermission.charge) this.LTDashTackle();       // ダッシュかタックルの判定
		if (this.m_controlePermission.shoote) this.RTShootPass();        // パスかシュートの判定
		if (this.m_controlePermission.rockOn) this.ChangeViewPoint();    // 視点変更
		if (this.m_controlePermission.gauge)  this.GaugeAction();         // ゲージのアクション状態
	}
}
