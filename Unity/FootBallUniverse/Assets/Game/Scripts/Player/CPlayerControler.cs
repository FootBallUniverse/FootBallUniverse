using UnityEngine;
using System.Collections;

//----------------------------------------------------------------------
// プレイヤーのデバッグ用コントローラー
//----------------------------------------------------------------------
// @Date 2014/11/11  @Update 2014/11/11  @Author T.Kawashita      
//----------------------------------------------------------------------
public class CPlayerControler : MonoBehaviour {

    // 現在どのプレイヤーを操作しているかどうかのための定数
    private enum ePLAYER_STATUS
    {
        ePLAYER1,
        ePLAYER2,
        ePLAYER3,
        ePLAYER4
    }

    public GameObject m_player;
    public CPlayer m_playerScript;          // 操作するプレイヤーのスクリプト
    private ePLAYER_STATUS m_playerStatus;  // 現在のプレイヤー

    public float PLAYER_MOVE_SPEED = 0.6f;
    public float PLAYER_ROTATION_SPEED = 0.6f;

    //----------------------------------------------------------------------
    // コンストラクタ
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/11/11  @Update 2014/11/11  @Author T.Kawashita      
    //----------------------------------------------------------------------
    void Start () {

        // 最初はプレイヤー１のスクリプトを取得
        m_playerScript = this.gameObject.transform.parent.GetComponent<CPlayer1>();
        m_player = this.gameObject.transform.parent.gameObject;
        m_playerStatus = ePLAYER_STATUS.ePLAYER1;

	}

    //----------------------------------------------------------------------
    // 更新
    //----------------------------------------------------------------------
    // @Param   none			
    // @Return	none
    // @Date	2014/111/11 @Update 2014/111/12  @Author T.kawashita      
    //----------------------------------------------------------------------
	void Update () 
	{
		switch (m_playerScript.m_status) 
		{
			// 始めの待機状態
			case CPlayerManager.ePLAYER_STATUS.eWAIT:
				break;    

			// カウントダウンの状態
			case CPlayerManager.ePLAYER_STATUS.eCOUNTDOWN: 
				this.Rotation();
				break; 

			// 何もしてない状態
			case CPlayerManager.ePLAYER_STATUS.eNONE:
				ChangePlayer();    
				Move ();
				Rotation();
				SetBall();
				InitDashCharge();
				InitShootCharge();
				ChangeView();
                GaugeAction();
				break;

            // オーバーリミット状態
            case CPlayerManager.ePLAYER_STATUS.eOVERRIMIT:
				ChangePlayer();    
				Move ();
				Rotation();
				SetBall();
				InitDashCharge();
				InitShootCharge();
				ChangeView();               
                
                break;

			// ダッシュ中
			case CPlayerManager.ePLAYER_STATUS.eDASH: 
                break;    

			// タックル中
			case CPlayerManager.ePLAYER_STATUS.eTACKLE: 
				break;

			// タックルのダメージ受け中
			case CPlayerManager.ePLAYER_STATUS.eTACKLEDAMAGE: 
				break;    

			// タックル成功中
			case CPlayerManager.ePLAYER_STATUS.eTACKLESUCCESS: 
				break;

			// シュート中
			case CPlayerManager.ePLAYER_STATUS.eSHOOT: 
				break;

			// パス中
			case CPlayerManager.ePLAYER_STATUS.ePASS:
				break;

			// チャージ中
			case CPlayerManager.ePLAYER_STATUS.eSHOOTCHARGE:                                   
			case CPlayerManager.ePLAYER_STATUS.eDASHCHARGE: 
				
				if( m_playerScript.m_status == CPlayerManager.ePLAYER_STATUS.eDASHCHARGE )
					this.DashCharge();
				if( m_playerScript.m_status == CPlayerManager.ePLAYER_STATUS.eSHOOTCHARGE)
					this.ShootCharge();

				// 回転
				this.Rotation();
				break;

			// 終了
			case CPlayerManager.ePLAYER_STATUS.eEND:
				break;

			// ゴールした時
			case CPlayerManager.ePLAYER_STATUS.eGOAL:
				break;

		}
	}

    //----------------------------------------------------------------------
    // プレイヤーの切り替え
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/11/11  @Update 2014/11/11  @Author T.Kawashita      
    //----------------------------------------------------------------------
    public void ChangePlayer()
    { 
        // 操作する対象を変更する
        // 1P
        if (Input.GetKeyDown(KeyCode.Alpha1) && m_playerStatus != ePLAYER_STATUS.ePLAYER1)
        {
            // 1Pの情報を取得して1Pに切り替える
            m_player = GameObject.Find("P1&P2").transform.FindChild("Player1").transform.FindChild("player").gameObject;

            // コントローラーの位置を変更
            this.ChangeControler(m_player.transform);

            m_playerScript = m_player.GetComponent<CPlayer1>();
            m_playerStatus = ePLAYER_STATUS.ePLAYER1;
        }
        // 2P
        if (Input.GetKeyDown(KeyCode.Alpha2) && m_playerStatus != ePLAYER_STATUS.ePLAYER2)
        {
            // 2Pの情報を取得して2Pに切り替える
            m_player = GameObject.Find("P1&P2").transform.FindChild("Player2").transform.FindChild("player").gameObject;

            this.ChangeControler(m_player.transform);

            m_playerScript = m_player.GetComponent<CPlayer2>();
            m_playerStatus = ePLAYER_STATUS.ePLAYER2;
		}

        // 3P
        if (Input.GetKeyDown(KeyCode.Alpha3) && m_playerStatus != ePLAYER_STATUS.ePLAYER3)
        { 
            // 3Pの情報を取得して3Pに切り替える
            m_player = GameObject.Find("P3&P4").transform.FindChild("Player3").transform.FindChild("player").gameObject;
            this.ChangeControler(m_player.transform);

            m_playerScript = m_player.GetComponent<CPlayer3>();
            m_playerStatus = ePLAYER_STATUS.ePLAYER3;
        }

        // 4P
        if (Input.GetKeyDown(KeyCode.Alpha4) && m_playerStatus != ePLAYER_STATUS.ePLAYER4)
        { 
			// 4Pの情報を取得して4Pに切り替える
			m_player = GameObject.Find("P3&P4").transform.FindChild("Player4").transform.FindChild("player").gameObject;
			this.ChangeControler(m_player.transform);

			m_playerScript = m_player.GetComponent<CPlayer4>();
			m_playerStatus = ePLAYER_STATUS.ePLAYER4;
        
		}
    }

    //----------------------------------------------------------------------
    // コントローラーの切り替え
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/11/27  @Update 2014/11/27  @Author T.Kawashita      
    //----------------------------------------------------------------------
    public void ChangeControler(Transform _parent)
    {
        this.transform.parent = _parent;
    }

    //----------------------------------------------------------------------
    // 移動
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/11/11  @Update 2014/11/11  @Author T.Kawashita      
    //----------------------------------------------------------------------
    public void Move()
    {
        Vector3 speed = new Vector3(0.0f, 0.0f, 0.0f);

        // 前移動
        if (Input.GetKey(KeyCode.W))
            speed.z += PLAYER_MOVE_SPEED;

        // 後移動
        if (Input.GetKey(KeyCode.S))
            speed.z -= PLAYER_MOVE_SPEED;

        // 右移動
        if (Input.GetKey(KeyCode.D))
            speed.x += PLAYER_MOVE_SPEED;

        // 左移動
        if (Input.GetKey(KeyCode.A))
            speed.x -= PLAYER_MOVE_SPEED;

        // 移動関数
        m_playerScript.Move(speed);

    }

    //----------------------------------------------------------------------
    // 回転
    //----------------------------------------------------------------------
    // @Param   none			
    // @Return	none
    // @Date	2014/11/12  @Update 2014/11/12  @Author T.Kawashita      
    //----------------------------------------------------------------------
    public void Rotation()
    { 
        Vector2 angle = new Vector2(0.0f,0.0f);

        // 前回転
        if (Input.GetKey(KeyCode.DownArrow))
            angle.y += PLAYER_ROTATION_SPEED;

        // 後回転
        if (Input.GetKey(KeyCode.UpArrow))
            angle.y -= PLAYER_ROTATION_SPEED;

        // 右回転
        if (Input.GetKey(KeyCode.RightArrow))
            angle.x += PLAYER_ROTATION_SPEED;

        // 左回転
        if (Input.GetKey(KeyCode.LeftArrow))
            angle.x -= PLAYER_ROTATION_SPEED;

        // 回転関数
        m_playerScript.Rotation(angle);
    }

	//----------------------------------------------------------------------
	// ダッシュチャージの初期化
	//----------------------------------------------------------------------
	// @Param	none		
	// @Return	none
	// @Date	2014/12/10  @Update 2014/12/10  @Author T.Kawashita       
	//----------------------------------------------------------------------
	public void InitDashCharge()
	{        
		// ダッシュが出来る状態になったら(ボールを持っていなかったら)
		if ((m_playerScript.m_status == CPlayerManager.ePLAYER_STATUS.eNONE || 
             m_playerScript.m_status == CPlayerManager.ePLAYER_STATUS.eOVERRIMIT ||
             m_playerScript.m_status == CPlayerManager.ePLAYER_STATUS.eTUTORIAL) &&
			m_playerScript.m_isBall == false && 
			Input.GetKeyDown (KeyCode.LeftShift) == true &&
			m_playerScript.m_isLtPress == false) 
		{
			m_playerScript.m_status = CPlayerManager.ePLAYER_STATUS.eDASHCHARGE;
			m_playerScript.m_chargeFrame = 0;
			m_playerScript.m_isLtPress = true;
			m_playerScript.m_playerSE.PlaySE("game/charging");
		
		}
	}

	//----------------------------------------------------------------------
	// ダッシュチャージ
	//----------------------------------------------------------------------
	// @Param	none		
	// @Return	none
	// @Date	2014/12/10  @Update 2014/12/10  @Author T.Kawashita       
	//----------------------------------------------------------------------
	public void DashCharge()
	{
		// チャージフレーム取得
		if( Input.GetKey(KeyCode.LeftShift) == true )
			m_playerScript.m_chargeFrame = ShiftKeyPress( ref m_playerScript.m_chargeFrame );
	}

	//----------------------------------------------------------------------
	// シュートチャージの初期化
	//----------------------------------------------------------------------
	// @Param	none		
	// @Return	none
	// @Date	2014/12/10  @Update 2014/12/10  @Author T.Kawashita       
	//----------------------------------------------------------------------
	public void InitShootCharge()
	{
		// シュートかパスが打てる状態になったら(ボールが手持ちにある場合）
		if ((m_playerScript.m_status == CPlayerManager.ePLAYER_STATUS.eNONE ||
             m_playerScript.m_status == CPlayerManager.ePLAYER_STATUS.eOVERRIMIT ||
             m_playerScript.m_status == CPlayerManager.ePLAYER_STATUS.eTUTORIAL )&&
		    m_playerScript.m_isBall == true && 
		    Input.GetKeyDown(KeyCode.Space) && 
		    m_playerScript.m_isRtPress == false )
		{
			m_playerScript.m_status = CPlayerManager.ePLAYER_STATUS.eSHOOTCHARGE;
			m_playerScript.m_chargeFrame = 0;
			m_playerScript.m_isRtPress = true;
			m_playerScript.m_playerSE.PlaySE("game/charging");
			return;
		}	
	}

	//----------------------------------------------------------------------
	// シュートチャージ
	//----------------------------------------------------------------------
	// @Param	none		
	// @Return	none
	// @Date	2014/12/10  @Update 2014/12/10  @Author T.Kawashita       
	//----------------------------------------------------------------------
	public void ShootCharge()
	{
		// チャージフレーム取得
		if (Input.GetKey (KeyCode.Space) == true)
			m_playerScript.m_chargeFrame = SpaceKeyPress (ref m_playerScript.m_chargeFrame);
	}

	//----------------------------------------------------------------------
	// 視点変更
	//----------------------------------------------------------------------
	// @Param	none		
	// @Return	none
	// @Date	2014/12/10  @Update 2014/12/10  @Author T.Kawashita       
	//----------------------------------------------------------------------
	public void ChangeView()
	{
		if (Input.GetKeyDown (KeyCode.J) || Input.GetKeyDown (KeyCode.I) || Input.GetKeyDown (KeyCode.L) || Input.GetKeyDown (KeyCode.K)) {
			m_playerScript.m_camera.ChangeRspeedlock ();
		}
		
		// ボールの方向に向ける
		if (Input.GetKey (KeyCode.L) && m_playerScript.m_isBall == false) 
		{
			m_playerScript.m_trans.LookAt (CSoccerBallManager.m_soccerBallTransform);
			this.transform.parent.transform.rotation = Quaternion.Slerp (this.transform.parent.transform.rotation, m_playerScript.m_trans.rotation, m_playerScript.m_camera.Rcameraspeed * Time.deltaTime);
			return;
		}


		switch (m_playerStatus) 
		{
		case ePLAYER_STATUS.ePLAYER1:
			// 2Pの方向に向ける
			if (Input.GetKey (KeyCode.J)) {
				m_playerScript.m_trans.LookAt (CPlayerManager.m_player2Transform);
				this.transform.parent.transform.rotation = Quaternion.Slerp (this.transform.parent.transform.rotation, m_playerScript.m_trans.rotation, m_playerScript.m_camera.Rcameraspeed * Time.deltaTime);
				return;
			}
			
			// 敵ゴールへ向ける
			if (Input.GetKey (KeyCode.I)) {
				m_playerScript.m_trans.LookAt (CStageManager.m_3p4pGoalTransform);
				this.transform.parent.transform.rotation = Quaternion.Slerp (this.transform.parent.transform.rotation, m_playerScript.m_trans.rotation, m_playerScript.m_camera.Rcameraspeed * Time.deltaTime);
				return;
			}

			// 自分のゴールへ向ける
			if (Input.GetKey (KeyCode.K)) {
				m_playerScript.m_trans.LookAt (CStageManager.m_1p2pGoalTransform);
				this.transform.parent.transform.rotation = Quaternion.Slerp (this.transform.parent.transform.rotation, m_playerScript.m_trans.rotation, m_playerScript.m_camera.Rcameraspeed * Time.deltaTime);
				return;
			}
			break;

		case ePLAYER_STATUS.ePLAYER2:

			// 1Pの方向に向ける
			if (Input.GetKey (KeyCode.J)) {
				m_playerScript.m_trans.LookAt (CPlayerManager.m_player1Transform);
				this.transform.parent.transform.rotation = Quaternion.Slerp (this.transform.parent.transform.rotation, m_playerScript.m_trans.rotation, m_playerScript.m_camera.Rcameraspeed * Time.deltaTime);
				return;
			}
			
			// 敵ゴールへ向ける
			if (Input.GetKey (KeyCode.I)) {
				m_playerScript.m_trans.LookAt (CStageManager.m_3p4pGoalTransform);
				this.transform.parent.transform.rotation = Quaternion.Slerp (this.transform.parent.transform.rotation, m_playerScript.m_trans.rotation, m_playerScript.m_camera.Rcameraspeed * Time.deltaTime);
				return;
			}
			
			// 自分のゴールへ向ける
			if (Input.GetKey (KeyCode.K)) {
				m_playerScript.m_trans.LookAt (CStageManager.m_1p2pGoalTransform);
				this.transform.parent.transform.rotation = Quaternion.Slerp (this.transform.parent.transform.rotation, m_playerScript.m_trans.rotation, m_playerScript.m_camera.Rcameraspeed * Time.deltaTime);
				return;
			}
			break;

		case ePLAYER_STATUS.ePLAYER3:
			// 4Pの方向に向ける
			if (Input.GetKey (KeyCode.J)) {
				m_playerScript.m_trans.LookAt (CPlayerManager.m_player4Transform);
				this.transform.parent.transform.rotation = Quaternion.Slerp (this.transform.parent.transform.rotation, m_playerScript.m_trans.rotation, m_playerScript.m_camera.Rcameraspeed * Time.deltaTime);
				return;
			}
			
			// 敵ゴールへ向ける
			if (Input.GetKey (KeyCode.I)) {
				m_playerScript.m_trans.LookAt (CStageManager.m_1p2pGoalTransform);
				this.transform.parent.transform.rotation = Quaternion.Slerp (this.transform.parent.transform.rotation, m_playerScript.m_trans.rotation, m_playerScript.m_camera.Rcameraspeed * Time.deltaTime);
				return;
			}
			
			// 自分のゴールへ向ける
			if (Input.GetKey (KeyCode.K)) {
				m_playerScript.m_trans.LookAt (CStageManager.m_3p4pGoalTransform);
				this.transform.parent.transform.rotation = Quaternion.Slerp (this.transform.parent.transform.rotation, m_playerScript.m_trans.rotation, m_playerScript.m_camera.Rcameraspeed * Time.deltaTime);
				return;
			}
			break;

		case ePLAYER_STATUS.ePLAYER4:
			// 3Pの方向に向ける
			if (Input.GetKey (KeyCode.J)) {
				m_playerScript.m_trans.LookAt (CPlayerManager.m_player3Transform);
				this.transform.parent.transform.rotation = Quaternion.Slerp (this.transform.parent.transform.rotation, m_playerScript.m_trans.rotation, m_playerScript.m_camera.Rcameraspeed * Time.deltaTime);
				return;
			}
			
			// 敵ゴールへ向ける
			if (Input.GetKey (KeyCode.I)) {
				m_playerScript.m_trans.LookAt (CStageManager.m_1p2pGoalTransform);
				this.transform.parent.transform.rotation = Quaternion.Slerp (this.transform.parent.transform.rotation, m_playerScript.m_trans.rotation, m_playerScript.m_camera.Rcameraspeed * Time.deltaTime);
				return;
			}
			
			// 自分のゴールへ向ける
			if (Input.GetKey (KeyCode.K)) {
				m_playerScript.m_trans.LookAt (CStageManager.m_3p4pGoalTransform);
				this.transform.parent.transform.rotation = Quaternion.Slerp (this.transform.parent.transform.rotation, m_playerScript.m_trans.rotation, m_playerScript.m_camera.Rcameraspeed * Time.deltaTime);
				return;
			}
			break;
		}
	}

    //----------------------------------------------------------------------
    // ボールのセット
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/11/11  @Update 2014/11/11  @Author T.Kawashita       
    //----------------------------------------------------------------------
    public void SetBall()
    {
        // Bが押されたらプレイヤーにボールをセット
        if (Input.GetKeyDown(KeyCode.B) && m_playerScript.m_isBall == false)
        {
            // ボールの位置をセット
            Vector3 pos = new Vector3(0.0f,0.05f,0.1f);

            // サッカーボールに誰が現在保持しているかを設定
            CSoccerBallManager.m_shootPlayerNo = m_playerScript.m_playerData.m_id;
            CSoccerBallManager.m_shootTeamNo = m_playerScript.m_playerData.m_teamNo;

            // プレイヤーのボールに設定
			// 他のプレイヤーがボールを持っている場合の処理
			if( CSoccerBallManager.m_soccerBall.transform.parent.tag == "Player")
				CSoccerBallManager.m_soccerBall.transform.parent.gameObject.GetComponent<CPlayer>().m_isBall = false;

            CPlayerManager.m_soccerBallManager.ChangeOwner(m_player.transform,pos);
            m_playerScript.m_isBall = true;

            // 取った瞬間スピードがあがる処理の追加
            m_playerScript.m_action.InitGetBall(m_playerScript.m_human.m_getBallAccSpeedDupRate,
                                                m_playerScript.m_human.m_getBallAccDurationFrame,
                                                m_playerScript.m_human.m_getBallAccDecFrame);
            m_playerScript.m_isGetBall = true;
        }
    }   

	//----------------------------------------------------------------------
	// SPACEキーが押され続けているかどうかを判定
	//----------------------------------------------------------------------
	// @Param   int     フレーム数
	// @Return	int     どれぐらいの時間押されているか(frame)
	// @Date	2014/12/10  @Update 2014/12/10  @Author T.Kawashita      
	//----------------------------------------------------------------------
	private int SpaceKeyPress(ref int _frame)
	{
		if (Input.GetKey(KeyCode.Space) == true)
		{
			_frame += (int)(Time.deltaTime * 90);
			return _frame;
		}
		else
		{
			_frame = 0;
			return 0;
		} 
	}

	//----------------------------------------------------------------------
	// SHIFTキーが押され続けているかどうかを判定
	//----------------------------------------------------------------------
	// @Param   int     フレーム数
	// @Return	int     どれぐらいの時間押されているか(frame)
	// @Date	2014/12/10  @Update 2014/12/10  @Author T.Kawashita      
	//----------------------------------------------------------------------
	private int ShiftKeyPress(ref int _frame)
	{
		if (Input.GetKey (KeyCode.LeftShift) == true) {
			_frame += (int)(Time.deltaTime * 90);
			return _frame;
		} 
		else
		{
			_frame = 0;
			return 0;
		}

	}


    //----------------------------------------------------------------------
    // Gキーが押されたらゲージ解放
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2015/1/16  @Update 2015/1/16  @Author T.Kawashita     
    //----------------------------------------------------------------------
    private void GaugeAction()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (m_playerScript.m_gauge.GaugeDecrement() != 0)
            {
                m_player.transform.FindChild("PlayerEffect").transform.GetComponent<CEffect>().OverRimitOn();
                m_playerScript.m_playerSE.PlaySE("game/kick_smash_echor");
                m_playerScript.m_status = CPlayerManager.ePLAYER_STATUS.eOVERRIMIT;
                m_playerScript.m_isOverRimit = true;

                // 日本の場合はボールをとれる範囲を増やす
                switch (m_playerStatus)
                {
                    case ePLAYER_STATUS.ePLAYER1:
                    case ePLAYER_STATUS.ePLAYER2:
                        if (TeamData.teamNationality[0] == TeamData.TEAM_NATIONALITY.JAPAN)
                        {
                            // ボールの取れる範囲を変更
                            this.GetComponent<SphereCollider>().radius = CGaugeManager.m_japanHoldRadius;
                            this.transform.FindChild("PlayerEffect").transform.GetComponent<CEffect>().effectOverRimit.particleSystem.startSize = CGaugeManager.m_japanHoldRadius * 50;
                        }
                        break;

                    case ePLAYER_STATUS.ePLAYER3:
                    case ePLAYER_STATUS.ePLAYER4:
                        if (TeamData.teamNationality[1] == TeamData.TEAM_NATIONALITY.JAPAN)
                        {
                            // ボールの撮れる範囲を変更
                            this.GetComponent<SphereCollider>().radius = CGaugeManager.m_japanHoldRadius;
                            this.transform.FindChild("PlayerEffect").transform.GetComponent<CEffect>().effectOverRimit.particleSystem.startSize = CGaugeManager.m_japanHoldRadius * 50;
                        }
                        break;
                }
            }
        }
    }
}
