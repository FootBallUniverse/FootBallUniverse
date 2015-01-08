using UnityEngine;
using System.Collections;

//----------------------------------------------------------------------
// CSoccerBall
//----------------------------------------------------------------------
// @Info サッカーボール用クラス
// @Date 2014/10/27	@Update 2014/10/27  @Author T.Kawashita      
//----------------------------------------------------------------------
public class CSoccerBall : MonoBehaviour {

    public Vector3 m_pos;           // 位置座標
    public bool m_isPlayer;         // プレイヤーに持たれているかどうか

    //----------------------------------------------------------------------
    // コンストラクタ
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/10/27  @Update 2014/10/27  @Author T.Kawashita      
    //----------------------------------------------------------------------
	void Start () {
        this.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        m_pos = new Vector3();
        m_pos = this.transform.localPosition;
        this.rigidbody.drag = CGameData.m_ballDecRec;           // 空気抵抗をセット
        this.rigidbody.angularDrag = CGameData.m_ballDecRec;    // 反射係数をセット

        m_isPlayer = false;

        this.rigidbody.angularVelocity = new Vector3(Random.value * 10.0f, 0.0f, Random.value * 10.0f);
		SetTrailYellow();
	}

    //----------------------------------------------------------------------
    // 初期化
    //----------------------------------------------------------------------
    // @Param   _pos    設定したい初期位置			
    // @Return	bool    成功か失敗
    // @Date	2014/10/28  @Update 2014/10/28  @Author T.Kawashita      
    //----------------------------------------------------------------------
    public bool Init(Vector3 _pos)
    {
        m_pos = _pos;
        this.transform.localPosition = m_pos;
        this.transform.localRotation = Quaternion.identity;

        m_isPlayer = false;

        // 速度ベクトル，角速度ベクトルの初期化
        this.rigidbody.velocity = new Vector3(0.0f, 0.0f, 0.0f);
        this.rigidbody.angularVelocity = new Vector3(0.0f, 0.0f, 0.0f);

        return true;
    }

    //----------------------------------------------------------------------
    // ゴール後のリスタート
    //----------------------------------------------------------------------
    // @Param	_pos    設定したい初期位置		
    // @Return	bool    成功か失敗
    // @Date	2014/12/6  @Update 2014/12/6  @Author 2014/12/6      
    //----------------------------------------------------------------------
    public bool Restart(Vector3 _pos)
    {
        m_pos = _pos;

        this.transform.parent = GameObject.Find("BallGameObject").transform;       
        this.transform.localPosition = m_pos;
        this.transform.localRotation = Quaternion.identity;

        m_isPlayer = false;

        // 速度ベクトル，角速度ベクトルの初期化
        this.rigidbody.velocity = new Vector3(0.0f, 0.0f, 0.0f);
        this.rigidbody.angularVelocity = new Vector3(0.0f, 0.0f, 0.0f);

        

		SetTrailYellow();

        return true;
    }

    //----------------------------------------------------------------------
    // ゲーム開始時のボールの動き
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/11/21  @Update 2014/11/21  @Author T.Kawashita      
    //----------------------------------------------------------------------
    public void StartGame()
    {
        this.rigidbody.velocity = new Vector3(Random.Range(-1.0f,1.0f) * 5.0f, Random.Range(-1.0f,1.0f) * 5.0f, 0.0f);
    }

    //----------------------------------------------------------------------
    // 更新
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/10/27  @Update 2014/10/27  @Author T.Kawashita      
    //----------------------------------------------------------------------
	void Update () {
        
        // オーバーリミットのシュートが放たれてなおかつイングランドだった場合は加速させていく
        if (CSoccerBallManager.m_isOverRimitShoot == true && CSoccerBallManager.m_team == TeamData.TEAM_NATIONALITY.ENGLAND)
        {
            // 上限値を超えたら元に戻す
            if (this.GetComponent<Rigidbody>().velocity.x >= CGaugeManager.m_englandShootLimit ||
                this.GetComponent<Rigidbody>().velocity.y >= CGaugeManager.m_englandShootLimit ||
                this.GetComponent<Rigidbody>().velocity.z >= CGaugeManager.m_englandShootLimit)
            {
                CSoccerBallManager.m_isOverRimitShoot = false;
                CSoccerBallManager.m_team = TeamData.TEAM_NATIONALITY.NONE;
            }
            else
            {
                this.GetComponent<Rigidbody>().velocity *= CGaugeManager.m_englandAccRate;
            }
        }

	}

    //----------------------------------------------------------------------
    // 位置の変更
    //----------------------------------------------------------------------
    // @Param	Vector3 セットしたい位置座標		
    // @Return	none
    // @Date	2014/10/27  @Update 2014/10/27  @Author T.Kawashita       
    //----------------------------------------------------------------------
    public void SetPosition(Vector3 _pos)
    {
        m_pos = _pos;

        this.transform.localPosition = m_pos;
    }

    //----------------------------------------------------------------------
    // ボールの吹っ飛び
    //----------------------------------------------------------------------
    // @Param   Transform   タックルしたプレイヤー     
    // @Return	none
    // @Date	2014/12/3  @Update 2014/12/3  @Author T.Kawashita     
    //----------------------------------------------------------------------
    public void BlownOff(Transform _player)
    { 
        // プレイヤーの方向に向ける
        this.transform.LookAt(_player);

        float speedX = Random.Range(-2.0f, 2.0f);
        float speedY = Random.Range(-2.0f, 2.0f);
        float speedZ = Random.Range(0.0f, 0.0f);

        m_isPlayer = false;

        Vector3 speed = new Vector3();
        speed = speedX * this.transform.right + speedY * this.transform.up + speedZ * this.transform.forward;

        // 飛ばす
        this.rigidbody.velocity = speed;
    }

    //----------------------------------------------------------------------
    // ボールの当たり判定
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/12/7  @Update 2014/12/7  @Author T.Kawashita     
    // @Update  2014/12/29 ボールを取った瞬間にあがるスピードの準備
    //----------------------------------------------------------------------
    void OnTriggerEnter(Collider obj)
    {
		GameObject player = obj.gameObject;
		CPlayer playerScript = obj.GetComponent<CPlayer> ();
		CapsuleCollider capsuleCollider = obj as CapsuleCollider;

		// プレイヤーとの当たり判定
        // 同じチームの場合は取れない
		if (capsuleCollider != null && playerScript.m_isBall == false && m_isPlayer == true && 
			playerScript.m_status != CPlayerManager.ePLAYER_STATUS.eTACKLEDAMAGE &&
			playerScript.m_status != CPlayerManager.ePLAYER_STATUS.eDASHCHARGE &&
			playerScript.m_status != CPlayerManager.ePLAYER_STATUS.eSHOOTCHARGE &&
            playerScript.m_playerData.m_teamNo != this.transform.parent.GetComponent<CPlayer>().m_playerData.m_teamNo)
        {
			// 現在持っているプレイヤーのステータス変更
			CPlayer ballPlayer = this.transform.parent.GetComponent<CPlayer> ();
			this.transform.parent.transform.parent.GetComponent<CPlayerAnimator> ().TackleDamage ();
			ballPlayer.m_isBall = false;
			ballPlayer.m_status = CPlayerManager.ePLAYER_STATUS.eTACKLEDAMAGE;
			ballPlayer.m_action.InitTackleDamage (ballPlayer.m_human.m_stealDamageLength, 0.0f, ballPlayer.m_human.m_stealDamageLength);
            
			// 当たった方のプレイヤーに持ち主を変更
			// プレイヤーのボールに設定
			Vector3 pos = new Vector3 (0.0f, -0.13f, 0.14f);
			if (obj.gameObject.tag == "RedTeam")
					this.SetTrailRed ();
			if (obj.gameObject.tag == "BlueTeam")
					this.SetTrailBlue ();
					
			CPlayerManager.m_soccerBallManager.ChangeOwner (player.transform, pos);
			CSoccerBallManager.m_shootPlayerNo = playerScript.m_playerData.m_playerNo;
			CSoccerBallManager.m_shootTeamNo = playerScript.m_playerData.m_teamNo;
			playerScript.m_isBall = true;

            // ボールを取った後スピードを速くする準備
            playerScript.m_action.InitGetBall(playerScript.m_human.m_getBallAccSpeedDupRate,
                                              playerScript.m_human.m_getBallAccDurationFrame,
                                              playerScript.m_human.m_getBallAccDecFrame);
            playerScript.m_isGetBall = true;

			// 相手のボールの場合サポーター追加
			int supporter = 0;
			supporter += CSupporterData.m_getBallSupporter;

			if (playerScript.m_playerData.m_teamNo != ballPlayer.m_playerData.m_teamNo)
					supporter += CSupporterData.m_takeBallSupporter;

			if (playerScript.m_status == CPlayerManager.ePLAYER_STATUS.eDASH) {
				if (playerScript.m_playerData.m_teamNo != ballPlayer.m_playerData.m_teamNo)
					supporter += CSupporterData.m_takeBallDashSupporter;
    
				supporter += CSupporterData.m_getBallDashSupporter;
			}
			CSupporterManager.AddSupporter (playerScript.m_playerData.m_teamNo, supporter);
			playerScript.m_playerSE.PlaySE("game/supoter_up");

		}
    }




	void OnCollisionEnter(Collision col){
		this.transform.FindChild("ShootLine").particleSystem.Stop();
		this.transform.FindChild("ShootLine").particleSystem.Clear();
		if(col.gameObject.tag == "Stage")
		{
			CGameManager.m_soundPlayer.ChangeSEVolume(0.9f);
			CGameManager.m_soundPlayer.PlaySE("game/ball_to_wall");
			this.SetTrailYellow();
		}
	}
	//----------------------------------------------------------------------
	// トレイルの色替え赤
	//----------------------------------------------------------------------
	// @Param   none     
	// @Return	none
	// @Date	2014/12/8  @Update 2014/12/8  @Author T.Kaneko     
	//----------------------------------------------------------------------
	public void SetTrailRed()
	{
		this.transform.FindChild("ShootLine").particleSystem.Play();
		this.transform.FindChild("ShootLine").particleSystem.Clear();
		this.transform.FindChild("ShootLine").GetComponent<ParticleSystem> ().startColor = new Color (255, 0, 0);
		this.transform.FindChild ("TrailBlue").gameObject.SetActive (false);
		this.transform.FindChild ("TrailBlue").GetComponent<TrailRenderer> ().time = -1;
		this.transform.FindChild ("TrailYellow").gameObject.SetActive (false);
		this.transform.FindChild ("TrailYellow").GetComponent<TrailRenderer> ().time = -1;
		this.transform.FindChild ("TrailRed").gameObject.SetActive (true);
		this.transform.FindChild ("TrailRed").GetComponent<TrailRenderer> ().time = 3;
	}
	
	//----------------------------------------------------------------------
	// トレイルの色替え青
	//----------------------------------------------------------------------
	// @Param   none     
	// @Return	none
	// @Date	2014/12/8  @Update 2014/12/8  @Author T.Kaneko     
	//----------------------------------------------------------------------
	public void SetTrailBlue()
	{ 
		this.transform.FindChild("ShootLine").particleSystem.Play();
		this.transform.FindChild("ShootLine").particleSystem.Clear();
		this.transform.FindChild("ShootLine").GetComponent<ParticleSystem> ().startColor = new Color (0, 0, 255);
		this.transform.FindChild ("TrailRed").gameObject.SetActive (false);
		this.transform.FindChild ("TrailRed").GetComponent<TrailRenderer> ().time = -1;
		this.transform.FindChild ("TrailYellow").gameObject.SetActive (false);
		this.transform.FindChild ("TrailYellow").GetComponent<TrailRenderer> ().time = -1;
		this.transform.FindChild ("TrailBlue").gameObject.SetActive (true);
		this.transform.FindChild ("TrailBlue").GetComponent<TrailRenderer> ().time = 3;
	}
	
	//----------------------------------------------------------------------
	// トレイルの色替え白
	//----------------------------------------------------------------------
	// @Param   none     
	// @Return	none
	// @Date	2014/12/8  @Update 2014/12/8  @Author T.Kaneko     
	//----------------------------------------------------------------------
	public void SetTrailYellow()
	{ 
		// ボールの軌道パーティクル処理
		this.transform.FindChild("ShootLine").particleSystem.Stop();
		this.transform.FindChild("ShootLine").particleSystem.Clear();
		this.transform.FindChild("ShootLine").GetComponent<ParticleSystem> ().startColor = new Color (255, 255, 255);
		this.transform.FindChild ("TrailRed").gameObject.SetActive (false);
		this.transform.FindChild ("TrailRed").GetComponent<TrailRenderer> ().time = -1;
		this.transform.FindChild ("TrailBlue").gameObject.SetActive (false);
		this.transform.FindChild ("TrailBlue").GetComponent<TrailRenderer> ().time = -1;
		this.transform.FindChild ("TrailYellow").gameObject.SetActive (true);
		this.transform.FindChild ("TrailYellow").GetComponent<TrailRenderer> ().time = 3;
	}
	//----------------------------------------------------------------------
	// トレイルの表示を消す
	//----------------------------------------------------------------------
	// @Param   none     
	// @Return	none
	// @Date	2014/12/14  @Update 2014/12/14  @Author T.Kaneko     
	//----------------------------------------------------------------------
	public void StopTrail()
	{ 
		this.transform.FindChild("ShootLine").particleSystem.Stop();
		this.transform.FindChild("ShootLine").particleSystem.Clear();
		this.transform.FindChild ("TrailRed").gameObject.SetActive (false);
		this.transform.FindChild ("TrailRed").GetComponent<TrailRenderer> ().time = -1;
		this.transform.FindChild ("TrailBlue").gameObject.SetActive (false);
		this.transform.FindChild ("TrailBlue").GetComponent<TrailRenderer> ().time = -1;
		this.transform.FindChild ("TrailYellow").gameObject.SetActive (false);
		this.transform.FindChild ("TrailYellow").GetComponent<TrailRenderer> ().time = -1;

	}
}
