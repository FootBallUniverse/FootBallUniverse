using UnityEngine;
using System.Collections;

public class CEffect : MonoBehaviour {
	// 親のプレイヤー用
	CPlayer cplayer;
	GameObject rightFoot;
	// 子のエフェクト用スクリプト
	GameObject effectTackle;
	GameObject effectTackleDamage;
	GameObject effectDash;
	GameObject effectCharge;
	GameObject effectChargeMax;
	GameObject effectShoot;
	public GameObject effectOverRimit;
	GameObject effectGaugeUp;
	GameObject effectJapan;

	// Use this for initialization
	void Start()
	{
		// 親情報取得
		this.cplayer = this.transform.parent.gameObject.GetComponent<CPlayer>();
		this.rightFoot = this.transform.parent.transform.FindChild("group1").transform.FindChild("FOOT_R").gameObject;
		this.effectOverRimit    = this.transform.FindChild("Effect_OverRimit").gameObject;
		// エフェクト情報取得
		if (this.transform.parent.gameObject.layer == 8 || this.transform.parent.gameObject.layer == 9)
		{
			this.effectTackle = this.transform.FindChild("Effect_Tackle0").gameObject;
			this.transform.FindChild("Effect_Tackle1").gameObject.SetActive(false);
			this.effectChargeMax = this.transform.FindChild("Effect_ChargeMax0").gameObject;
			this.transform.FindChild("Effect_ChargeMax1").gameObject.SetActive(false);
//			if(this.transform.parent.GetComponent<CPlayer>().m_playerData.m_teamNo == 0)
//			{
//				this.effectOverRimit = this.transform.FindChild("Effect_Japan0").gameObject;
//				this.transform.FindChild("Effect_Japan1").gameObject.SetActive(false);
//			}
		}else{
			this.effectTackle = this.transform.FindChild("Effect_Tackle1").gameObject;
			this.transform.FindChild("Effect_Tackle0").gameObject.SetActive(false);
			this.effectChargeMax = this.transform.FindChild("Effect_ChargeMax1").gameObject;
			this.transform.FindChild("Effect_ChargeMax0").gameObject.SetActive(false);
//			if(this.transform.parent.GetComponent<CPlayer>().m_playerData.m_teamNo == 0)
//			{
//				this.effectOverRimit = this.transform.FindChild("Effect_Japan1").gameObject;
//				this.transform.FindChild("Effect_Japan0").gameObject.SetActive(false);
//			}
		}

		this.effectTackleDamage = this.transform.FindChild("Effect_TackleDamage").gameObject;
		this.effectDash         = this.transform.FindChild("Effect_Dash").gameObject;
		this.effectCharge       = this.transform.FindChild("Effect_Charge").gameObject;
		this.effectShoot        = this.transform.FindChild("Effect_Shoot").gameObject;
		this.effectGaugeUp      = this.transform.FindChild("Effect_GaugeUp").gameObject;

		// 色指定
		if (this.transform.parent.gameObject.layer == 8 || this.transform.parent.gameObject.layer == 9)
		{
			// 1P&2P
			this.effectCharge.particleSystem.startColor       = Color.red;
			this.effectShoot.particleSystem.startColor        = Color.red;
		}else{
			// 3P&4P
			this.effectCharge.particleSystem.startColor       = Color.blue;
			this.effectShoot.particleSystem.startColor        = Color.blue;
		}

		AllReSet();
	}

	void AllReSet()
	{
		this.effectTackle.SetActive(false);
		this.effectTackleDamage.SetActive(false);
		this.effectDash.SetActive(false);
		this.effectCharge.SetActive(false);
		this.effectChargeMax.SetActive(false);
		this.effectShoot.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		switch (this.cplayer.m_status)
		{
			case CPlayerManager.ePLAYER_STATUS.eDASH: PlayerStatusDash(); break;                   // ダッシュ中
			case CPlayerManager.ePLAYER_STATUS.eTACKLE: PlayerStatusTackle(); break;               // タックル中
			case CPlayerManager.ePLAYER_STATUS.eTACKLESUCCESS: PlayerStatusTackleSuccess(); break; // タックル成功中
			case CPlayerManager.ePLAYER_STATUS.eTACKLEDAMAGE: PlayerStatusTackleDamage(); break;   // タックル被ダメージ中
			case CPlayerManager.ePLAYER_STATUS.eSHOOT: PlayerStatusShoot(); break;                 // シュート中
			case CPlayerManager.ePLAYER_STATUS.ePASS: PlayerStatusPass(); break;                   // パス中
			case CPlayerManager.ePLAYER_STATUS.eSHOOTCHARGE: PlayerStatusShootCharge(); break;     // チャージ中
			case CPlayerManager.ePLAYER_STATUS.eDASHCHARGE: PlayerStatusCharge(); break;           // チャージ中
			case CPlayerManager.ePLAYER_STATUS.eEND: break;                                        // 終了
			default: AllReSet();break;                                                             // それ以外（エフェクトをOFF)
		}
	}

	void PlayerStatusDash()
	{
		this.effectDash.SetActive(true);
	}
	void PlayerStatusTackle()
	{
		this.effectTackle.SetActive(true);
		this.effectTackleDamage.SetActive(false);
		this.effectCharge.SetActive(false);
	}
	void PlayerStatusTackleSuccess()
	{
		this.effectTackle.SetActive(true);
		this.effectTackleDamage.SetActive(true);
	}
	void PlayerStatusTackleDamage()
	{
		this.effectTackleDamage.SetActive(true);
	}
	void PlayerStatusShoot()
	{
		// 右足セット
		this.effectShoot.transform.position = this.rightFoot.transform.position;
		this.effectShoot.transform.rotation = this.rightFoot.transform.rotation;

		this.effectCharge.SetActive(false);
	}


	void PlayerStatusPass()
	{
		this.effectCharge.SetActive(false);
	}

	void PlayerStatusShootCharge()
	{
		// 右足セット
		this.effectShoot.transform.position = this.rightFoot.transform.position;
		this.effectShoot.transform.rotation = this.rightFoot.transform.rotation;

		if(this.cplayer.m_chargeFrame == 60)
			this.effectCharge.SetActive(true);
		if (this.cplayer.m_chargeFrame >= 120)
		{
			this.effectChargeMax.SetActive(true);
			this.effectShoot.SetActive(true);
		}
	}
	void PlayerStatusCharge()
	{
		// ブースターにセット
		//this.effectShoot.transform.position = this.rightFoot.transform.position;
		//this.effectShoot.transform.rotation = this.rightFoot.transform.rotation;

		if (this.cplayer.m_chargeFrame >= 60)
			this.effectCharge.SetActive(true);
		//if (this.cplayer.m_chargeFrame >= 120)
		//	this.effectShoot.SetActive(true);
	}
	public void OverRimitOn()
	{
		this.effectOverRimit.SetActive(true);
	}
	public void OverRimitOff()
	{
		this.effectOverRimit.SetActive(false);
	}
	public void PlayerGaugeUp()
	{
		this.effectGaugeUp.particleSystem.Play();
	}
}
