using UnityEngine;
using System.Collections;

public class CEffect : MonoBehaviour {
	// 親のプレイヤー用
	CPlayer cplayer;
	GameObject rightFoot;
	// 子のエフェクト用スクリプト
	GameObject effectTackle;
	GameObject effectTackleDamage;
	GameObject effectTackleAttack;
	GameObject effectDash;
	GameObject effectCharge;
	GameObject effectChargeMax;
	GameObject effectShoot;

	// Use this for initialization
	void Start()
	{
		// 親情報取得
		this.cplayer = this.transform.parent.gameObject.GetComponent<CPlayer>();
		this.rightFoot = this.transform.parent.transform.FindChild("group1").transform.FindChild("FOOT_R").gameObject;
		// エフェクト情報取得
		this.effectTackle       = this.transform.FindChild("Effect_Tackle").gameObject;
		this.effectTackleDamage = this.transform.FindChild("Effect_TackleDamage").gameObject;
		this.effectTackleAttack = this.transform.FindChild("Effect_TackleAttack").gameObject;
		this.effectDash         = this.transform.FindChild("Effect_Dash").gameObject;
		this.effectCharge       = this.transform.FindChild("Effect_Charge").gameObject;
		this.effectChargeMax    = this.transform.FindChild("Effect_ChargeMax").gameObject;
		this.effectShoot        = this.transform.FindChild("Effect_Shoot").gameObject;
		AllReSet();
	}

	void AllReSet()
	{
		this.effectTackle.SetActive(false);
		this.effectTackleDamage.SetActive(false);
		this.effectTackleAttack.SetActive(false);
		this.effectDash.SetActive(false);
		this.effectCharge.SetActive(false);
		this.effectChargeMax.SetActive(false);
		this.effectShoot.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log(this.cplayer.m_status);
		// 右足セット
		this.effectShoot.transform.position = this.rightFoot.transform.position;
		this.effectShoot.transform.rotation = this.rightFoot.transform.rotation;

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
			case CPlayerManager.ePLAYER_STATUS.eGOAL: PlayerStatusGoal(); break;                   // ゴールした時は何もさせない
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
	}
	void PlayerStatusTackleSuccess()
	{
		/*
		this.effectTackle.SetActive(false);
		this.effectTackleDamage.SetActive(false);
		this.effectTackleAttack.SetActive(false);
		this.effectTackleDash.SetActive(false);
		this.effectTackleCharge.SetActive(false);
		this.effectTackleChargeMax.SetActive(false);
		this.effectTackleShoot.SetActive(false);
		 */
	}
	void PlayerStatusTackleDamage()
	{
		this.effectTackleDamage.SetActive(true);
	}
	void PlayerStatusShoot()
	{
		this.effectShoot.SetActive(true);
	}
	void PlayerStatusPass()
	{
		/*
		this.effectTackle.SetActive(false);
		this.effectTackleDamage.SetActive(false);
		this.effectTackleAttack.SetActive(false);
		this.effectTackleDash.SetActive(false);
		this.effectTackleCharge.SetActive(false);
		this.effectTackleChargeMax.SetActive(false);
		this.effectTackleShoot.SetActive(false);
		 */
	}
	void PlayerStatusShootCharge()
	{
		/*
		this.effectTackle.SetActive(false);
		this.effectTackleDamage.SetActive(false);
		this.effectTackleAttack.SetActive(false);
		this.effectTackleDash.SetActive(false);
		this.effectTackleCharge.SetActive(false);
		this.effectTackleChargeMax.SetActive(false);
		this.effectTackleShoot.SetActive(false);
		 */
	}
	void PlayerStatusCharge()
	{
		/*
		this.effectTackle.SetActive(false);
		this.effectTackleDamage.SetActive(false);
		this.effectTackleAttack.SetActive(false);
		this.effectTackleDash.SetActive(false);
		this.effectTackleCharge.SetActive(false);
		this.effectTackleChargeMax.SetActive(false);
		this.effectTackleShoot.SetActive(false);
		 */
	}
	void PlayerStatusGoal()
	{
		/*
		this.effectTackle.SetActive(false);
		this.effectTackleDamage.SetActive(false);
		this.effectTackleAttack.SetActive(false);
		this.effectTackleDash.SetActive(false);
		this.effectTackleCharge.SetActive(false);
		this.effectTackleChargeMax.SetActive(false);
		this.effectTackleShoot.SetActive(false);
		 */
	}
}
