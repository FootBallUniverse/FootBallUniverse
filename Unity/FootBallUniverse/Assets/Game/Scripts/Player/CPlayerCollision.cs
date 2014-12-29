using UnityEngine;
using System.Collections;

public class CPlayerCollision : MonoBehaviour
{

    //----------------------------------------------------------------------
    // コンストラクタ
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/12/1  @Update 2014/12/1  @Author T.Kawashita      
    //----------------------------------------------------------------------
    void Start()
    {

    }

    //----------------------------------------------------------------------
    // 更新
    //----------------------------------------------------------------------
    // @Param   none			
    // @Return	none
    // @Date	2014/12/1  @Update 2014/12/1  @Author T.Kawashita      
    //----------------------------------------------------------------------
    void Update()
    {
    }

    //----------------------------------------------------------------------
    // 当たり判定
    //----------------------------------------------------------------------
    // @Param   Collider    ぶつかったもののGameObject		
    // @Return	none
    // @Other   CallBack
    // @Date	2014/11/28  @Update 2014/11/28  @Author T.Kawashita      
    // @Update  2014/12/29  ボールを取った瞬間の時の処理追加
    //----------------------------------------------------------------------
    void OnTriggerEnter(Collider obj)
    {
        // ボールとぶつかった時の判定
        if (obj.gameObject.tag == "SoccerBall" && this.GetComponent<CPlayer>().m_isBall == false && 
            this.GetComponent<CPlayer>().m_status != CPlayerManager.ePLAYER_STATUS.eTACKLEDAMAGE )
        {
            // 浮いているボールの場合は自分のボールになる
            if (obj.transform.parent == GameObject.Find("BallGameObject").transform)
            {
				CPlayer playerScript = this.GetComponent<CPlayer>();

				// ダッシュチャージ中にボールを取ったらウェイトアニメーションに変更
				if( playerScript.m_status == CPlayerManager.ePLAYER_STATUS.eDASHCHARGE )
				{
					playerScript.m_animator.Wait();
					playerScript.m_status = CPlayerManager.ePLAYER_STATUS.eNONE;
				}

                // ボールの位置をセット
                Vector3 pos = new Vector3(0.0f, -0.13f, 0.14f);
				if(this.gameObject.tag == "RedTeam" )
					obj.GetComponent<CSoccerBall>().SetTrailRed();
				if(this.gameObject.tag == "BlueTeam" )
					obj.GetComponent<CSoccerBall>().SetTrailBlue();
				CGameManager.m_soundPlayer.ChangeSEVolume(1.0f);
				CGameManager.m_soundPlayer.PlaySE("game/boll_totta");

                // プレイヤーのボールに設定
                CPlayerManager.m_soccerBallManager.ChangeOwner(this.transform, pos);
                CSoccerBallManager.m_shootPlayerNo = this.GetComponent<CPlayer>().m_playerData.m_playerNo;
                CSoccerBallManager.m_shootTeamNo = this.GetComponent<CPlayer>().m_playerData.m_teamNo;
                this.gameObject.GetComponent<CPlayer>().m_isBall = true;

                // ボールの判定をトリガーにする
                obj.GetComponent<SphereCollider>().isTrigger = true;

                // ボールを取った後スピードを速くする準備
                playerScript.m_action.InitGetBall(playerScript.m_human.m_getBallAccSpeedDupRate, 
                                                  playerScript.m_human.m_getBallAccDurationFrame, 
                                                  playerScript.m_human.m_getBallAccDecFrame);
                playerScript.m_isGetBall = true;

                // サポーター追加
                int supporter = 0;
                // プレイヤーのステータスがダッシュだったらサポーター増加
                if (playerScript.m_status == CPlayerManager.ePLAYER_STATUS.eDASH)
                    supporter += CSupporterData.m_getBallDashSupporter;

                // 味方が蹴ったボール
                if (CSoccerBallManager.m_shootTeamNo == playerScript.m_playerData.m_teamNo)
                    supporter += CSupporterData.m_getBallPassSupporter;

                supporter += CSupporterData.m_getBallSupporter;
                CSupporterManager.AddSupporter(playerScript.m_playerData.m_teamNo, supporter);
//				playerScript.m_playerSE.PlaySE("game/supoter_up");
            }

                     
       }

        // タックルの当たり判定
        if (this.GetComponent<CPlayer>().m_status == CPlayerManager.ePLAYER_STATUS.eTACKLE &&
            ((obj.gameObject.tag == "RedTeam") || (obj.gameObject.tag == "BlueTeam")))
        {
            CPlayer playerScript = this.GetComponent<CPlayer>();

            // アニメーション変更
            // 相手がタックル中だったらくらいモーションに変更
            if (obj.GetComponent<CPlayer>().m_status == CPlayerManager.ePLAYER_STATUS.eTACKLE)
            {
                this.transform.parent.GetComponent<CPlayerAnimator>().TackleDamage();
                playerScript.m_status = CPlayerManager.ePLAYER_STATUS.eTACKLEDAMAGE;
                playerScript.m_action.InitTackleDamage(playerScript.m_human.m_tackleDamageMotionLength,
                                                       playerScript.m_human.m_tackleDamageInitSpeed,
                                                       playerScript.m_human.m_tackleDamageDecFrame);
            }

            // それ以外の場合は成功モーションに変更
            else
            {
                this.transform.parent.GetComponent<CPlayerAnimator>().TackleSuccess();
                playerScript.m_playerSE.StopSE();
                playerScript.m_playerSE.PlaySE("game/tackle_success");
                playerScript.m_status = CPlayerManager.ePLAYER_STATUS.eTACKLESUCCESS;
                playerScript.m_action.InitTackleSuccess(playerScript.m_human.m_tackleHitMotionLength);
            }

            // 相手をやられモーションに変更
            CPlayer colPlayerScript = obj.GetComponent<CPlayer>();
            obj.transform.LookAt(this.transform);
            obj.transform.parent.GetComponent<CPlayerAnimator>().TackleDamage();
            colPlayerScript.m_status = CPlayerManager.ePLAYER_STATUS.eTACKLEDAMAGE;

            colPlayerScript.m_action.InitTackleDamage(colPlayerScript.m_human.m_tackleDamageMotionLength,
                                                        colPlayerScript.m_human.m_tackleDamageInitSpeed,
                                                        colPlayerScript.m_human.m_tackleDamageDecFrame);

			int supporter = 0;
			// ボールを持っている場合は飛ばす
	        if (colPlayerScript.m_isBall == true)
	        {
	            GameObject soccerBall = obj.transform.FindChild("SoccerBall").gameObject;
	            soccerBall.GetComponent<CSoccerBall>().BlownOff(this.transform);
	            colPlayerScript.m_isBall = false;
					obj.transform.FindChild("SoccerBall").GetComponent<CSoccerBall>().SetTrailYellow();
					obj.transform.FindChild("SoccerBall").parent = GameObject.Find("BallGameObject").transform;
	            supporter += CSupporterData.m_damageTackleOnBallSupporter;
	        }

			if (playerScript.m_playerData.m_teamNo != colPlayerScript.m_playerData.m_teamNo)
			{
				// サポーター追加
                supporter += CSupporterData.m_damageTackleSupporter;
                CSupporterManager.AddSupporter(playerScript.m_playerData.m_teamNo, supporter);
				playerScript.m_playerSE.PlaySE("game/supoter_up");
            }
        }
    }


}