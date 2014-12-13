using UnityEngine;
using System.Collections;

public class CPlayer3 : CPlayer {
	
	//----------------------------------------------------------------------
	// コンストラクタ
	//----------------------------------------------------------------------
	// @Param	none		
	// @Return	none
	// @Date	2014/10/15  @Update 2014/10/31  @Author T.Kawashita      
	//----------------------------------------------------------------------
	void Start () {
		
		this.Init();
		
		// プレイヤーのデータをセット
		CPlayerManager.SetPlayerData(this.m_playerData, CPlayerManager.PLAYER_3);
		
		// 国の情報をセット / 国によってマテリアルを変更
		m_human = CHumanManager.GetWorldInstance(TeamData.teamNationality[0]);
		this.transform.FindChild("polySurface14").GetComponent<CPlayer3Mesh>().ChangeMaterial(TeamData.teamNationality[0]);
		
		// プレイヤーごとの値をセット
		this.SetData();
		
		// プレイヤーの情報をマップにセット
//       Color color = Color.red;
//        CPlayerManager.m_playerManager.SetMap(this.gameObject, color);
		
		// プレイヤーのアニメーターをセット
		m_animator = this.gameObject.transform.parent.GetComponent<CPlayerAnimator>();
		
		// カメラをセット
		m_camera = this.gameObject.transform.parent.FindChild("Player3Camera").GetComponent<PlayerCamera>();
		m_trans = this.transform.Find("LookTrans").transform; 
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
		
		if (m_isBall == true)
			this.transform.FindChild("SoccerBall").GetComponent<CSoccerBall>().SetPosition(new Vector3(0.0f, -0.13f, 0.14f));
		
		m_pos = this.transform.localPosition;   
		
		switch (m_status)
		{
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
		this.transform.parent.FindChild("Player3Camera").GetComponent<PlayerCamera>().CameraUpdate();
		
		// 最後に位置をマネージャークラスにセットしておく
		CPlayerManager.m_player3Transform = this.transform;
		
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
		Vector3 speed = new Vector3(Input.GetAxis(InputXBOX360.P3_XBOX_LEFT_ANALOG_X), 0.0f, Input.GetAxis(InputXBOX360.P3_XBOX_LEFT_ANALOG_Y));
		this.Move(speed);
		
		// 回転
		Vector2 angle = new Vector2(Input.GetAxis(InputXBOX360.P3_XBOX_RIGHT_ANALOG_X), Input.GetAxis(InputXBOX360.P3_XBOX_RIGHT_ANALOG_Y));
		this.Rotation(angle);
		
		this.LTDashTackle();        // ダッシュかタックルの判定
		this.RTShootPass();         // パスかシュートの判定
		
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
		Vector2 angle = new Vector2(Input.GetAxis(InputXBOX360.P3_XBOX_RIGHT_ANALOG_X), Input.GetAxis(InputXBOX360.P3_XBOX_RIGHT_ANALOG_Y));
		this.Rotation(angle);
		
		// 状態を遷移
		if (CGameManager.m_nowStatus == CGameManager.eSTATUS.eGAME)
		{
			m_status = CPlayerManager.ePLAYER_STATUS.eNONE;
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
			m_status = CPlayerManager.ePLAYER_STATUS.eNONE;
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
			m_status = CPlayerManager.ePLAYER_STATUS.eNONE;
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
				m_status = CPlayerManager.ePLAYER_STATUS.eNONE;
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
				m_status = CPlayerManager.ePLAYER_STATUS.eNONE;
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
			m_status = CPlayerManager.ePLAYER_STATUS.eNONE;
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
			m_status = CPlayerManager.ePLAYER_STATUS.eNONE;
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
		Vector2 angle = new Vector2(Input.GetAxisRaw(InputXBOX360.P3_XBOX_RIGHT_ANALOG_X), Input.GetAxisRaw(InputXBOX360.P3_XBOX_RIGHT_ANALOG_Y));
		this.Rotation(angle);
		
		// シュートチャージ中ならシュートの処理のみ
		if( m_status == CPlayerManager.ePLAYER_STATUS.eSHOOTCHARGE )
			this.ShootHold();               // シュートホールド状態
		
		// ダッシュチャージ中ならダッシュの処理のみ
		if (m_status == CPlayerManager.ePLAYER_STATUS.eDASHCHARGE)
			this.DashHold();                // ダッシュホールド状態
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
		if (m_status == CPlayerManager.ePLAYER_STATUS.eNONE ||
		    m_status == CPlayerManager.ePLAYER_STATUS.eCOUNTDOWN)
		{
			angle.y = _angle.x * m_human.m_cameraMoveSpeed;
			angle.x = _angle.y * m_human.m_cameraMoveSpeed;
		}
		else if (m_status == CPlayerManager.ePLAYER_STATUS.eSHOOTCHARGE ||
		         m_status == CPlayerManager.ePLAYER_STATUS.eDASHCHARGE)
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
		// シュート中
		if (m_status == CPlayerManager.ePLAYER_STATUS.eSHOOT)
			return m_action.Shoot(this.gameObject, this.transform.forward, ref m_isBall);
		
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
		if (m_status == CPlayerManager.ePLAYER_STATUS.ePASS)
			return m_action.Pass(this.gameObject, this.transform.forward, ref m_isBall);
		
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
		if (m_status == CPlayerManager.ePLAYER_STATUS.eNONE &&
		    m_isBall == true && 
		    InputXBOX360.IsGetRTButton(InputXBOX360.P3_XBOX_RT) == true && 
		    m_isRtPress == false )
		{
			m_status = CPlayerManager.ePLAYER_STATUS.eSHOOTCHARGE;
			m_chargeFrame = 0;
			m_isRtPress = true;
			m_playerSE.PlaySE("game/charging");
			return;
		}
		
		// 離されたら押しっぱなしフラグOFF
		else if (InputXBOX360.IsGetRTButton(InputXBOX360.P3_XBOX_RT) == false &&
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
		if( InputXBOX360.IsGetRTButton(InputXBOX360.P3_XBOX_RT) )
			m_chargeFrame = InputXBOX360.RTButtonPress(InputXBOX360.P3_XBOX_RT,ref m_chargeFrame);
		
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
			m_status = CPlayerManager.ePLAYER_STATUS.eNONE;
			m_animator.ChangeAnimation(m_animator.m_isWait);
			m_playerSE.StopSE();
			m_isSE = false;
			return;
		}
		
		// RTボタンが離されたらシュートかチャージ
		if(m_status == CPlayerManager.ePLAYER_STATUS.eSHOOTCHARGE && 
		   m_isBall == true &&
		   InputXBOX360.IsGetRTButton(InputXBOX360.P3_XBOX_RT) == false &&
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
			m_chargeFrame = InputXBOX360.RTButtonPress(InputXBOX360.P3_XBOX_RT,ref m_chargeFrame);
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
		if (m_status == CPlayerManager.ePLAYER_STATUS.eNONE &&
		    m_isBall == false && 
		    InputXBOX360.IsGetLTButton(InputXBOX360.P3_XBOX_LT) == true &&
		    m_isLtPress == false)
		{
			m_status = CPlayerManager.ePLAYER_STATUS.eDASHCHARGE;
			m_chargeFrame = 0;
			m_isLtPress = true;
			m_playerSE.PlaySE("game/charging");
			return;
		}
		
		else if( InputXBOX360.IsGetLTButton(InputXBOX360.P3_XBOX_LT ) == false &&
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
		if( InputXBOX360.IsGetRTButton(InputXBOX360.P3_XBOX_LT))
			m_chargeFrame = InputXBOX360.LTButtonPress(InputXBOX360.P3_XBOX_LT, ref m_chargeFrame);
		
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
			m_status  = CPlayerManager.ePLAYER_STATUS.eNONE;
			m_animator.Wait();
			m_playerSE.StopSE();
			m_isSE = false;
			return;
		}
		
		// LTボタンが離されたら
		if (m_status == CPlayerManager.ePLAYER_STATUS.eDASHCHARGE &&
		    m_isBall == false &&
		    InputXBOX360.IsGetLTButton(InputXBOX360.P3_XBOX_LT) == false &&
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
			m_chargeFrame = InputXBOX360.LTButtonPress(InputXBOX360.P3_XBOX_LT, ref m_chargeFrame);
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

                // LTボタンが押されたら敵の視点に変更
  //              if (Input.GetKeyDown(InputXBOX360.P3_XBOX_L))
  //              {
  //                  m_viewPointStatus = CPlayerManager.eVIEW_POINT_STATUS.eENEMY;
  //                  return;
  //              }

                if (Input.GetKeyDown(InputXBOX360.P3_XBOX_X) || Input.GetKeyDown(InputXBOX360.P3_XBOX_Y) || Input.GetKeyDown(InputXBOX360.P3_XBOX_B) || Input.GetKeyDown(InputXBOX360.P3_XBOX_A))
                {
					m_playerSE.PlaySE("game/rockon");
					m_camera.ChangeRspeedlock();
                }

                // 味方プレイヤーに向ける
                if (Input.GetKey(InputXBOX360.P3_XBOX_X))
                {
                    m_trans.LookAt(CPlayerManager.m_player4Transform);
                    this.transform.rotation = Quaternion.Slerp(this.transform.rotation, m_trans.rotation, m_camera.Rcameraspeed * Time.deltaTime);
                    return;
                }

                // 敵ゴールに向ける
                if (Input.GetKey(InputXBOX360.P3_XBOX_Y))
                {
                    m_trans.LookAt(CStageManager.m_1p2pGoalTransform);
                    this.transform.rotation = Quaternion.Slerp(this.transform.rotation, m_trans.rotation, m_camera.Rcameraspeed * Time.deltaTime);
                    return;
                }

                // ボールの方向に向ける
                if (Input.GetKey(InputXBOX360.P3_XBOX_B) && m_isBall == false)
                {
                    m_trans.LookAt(CSoccerBallManager.m_soccerBallTransform);
                    this.transform.rotation = Quaternion.Slerp(this.transform.rotation, m_trans.rotation, m_camera.Rcameraspeed * Time.deltaTime);
                    return;
                }

                // 味方キーパーの方に向ける
                if (Input.GetKey(InputXBOX360.P3_XBOX_A))
                {
                    m_trans.LookAt(CStageManager.m_3p4pGoalTransform);
                    this.transform.rotation = Quaternion.Slerp(this.transform.rotation, m_trans.rotation, m_camera.Rcameraspeed * Time.deltaTime);
                    return;
                }
                break;

			/*
            case CPlayerManager.eVIEW_POINT_STATUS.eENEMY:
                // RTボタンが押されたら味方の視点に変更
                if (Input.GetKeyDown(InputXBOX360.P3_XBOX_R))
                {
                    m_viewPointStatus = CPlayerManager.eVIEW_POINT_STATUS.ePLAYER;
                    return;
                }

                if (Input.GetKeyDown(InputXBOX360.P3_XBOX_X) || Input.GetKeyDown(InputXBOX360.P3_XBOX_Y) || Input.GetKeyDown(InputXBOX360.P3_XBOX_B) || Input.GetKeyDown(InputXBOX360.P3_XBOX_A))
                {
                    m_camera.ChangeRspeedlock();
                }

                // 1Pの方向に向ける
                if (Input.GetKey(InputXBOX360.P3_XBOX_X))
                {
                    m_trans.LookAt(CPlayerManager.m_player1Transform);
                    this.transform.rotation = Quaternion.Slerp(this.transform.rotation, m_trans.rotation, m_camera.Rcameraspeed * Time.deltaTime);
                    return;
                }

                // 2Pの方向に向ける
                if (Input.GetKey(InputXBOX360.P3_XBOX_Y))
                {
                    m_trans.LookAt(CPlayerManager.m_player2Transform);
                    this.transform.rotation = Quaternion.Slerp(this.transform.rotation, m_trans.rotation, m_camera.Rcameraspeed * Time.deltaTime);
                    return;
                }

                // 敵のＡＩの方向に向ける
                if (Input.GetKey(InputXBOX360.P3_XBOX_B))
                {
                    m_trans.LookAt(CCpuManager.m_cpuManager.m_cpuP1P2);
                    this.transform.rotation = Quaternion.Slerp(this.transform.rotation, m_trans.rotation, m_camera.Rcameraspeed * Time.deltaTime);
                    return;
                }

                // キーパーの方向に向ける
                if (Input.GetKey(InputXBOX360.P3_XBOX_A))
                {
                    m_trans.LookAt(CCpuManager.m_cpuManager.m_cpuP1P2Keeper);
                    this.transform.rotation = Quaternion.Slerp(this.transform.rotation, m_trans.rotation, m_camera.Rcameraspeed * Time.deltaTime);
                    return;
                }
                break;
*/
        }
    }

}
