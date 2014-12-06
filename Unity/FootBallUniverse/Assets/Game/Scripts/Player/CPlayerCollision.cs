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
    //----------------------------------------------------------------------
    void OnTriggerEnter(Collider obj)
    {
        // ボールとぶつかった時の判定
        if (obj.gameObject.tag == "SoccerBall")
        {
            if (obj.transform.parent == GameObject.Find("BallGameObject").transform)
            {
                // ボールの位置をセット
                Vector3 pos = new Vector3(0.0f, -0.13f, 0.14f);
                obj.gameObject.GetComponent<TrailRenderer>().enabled = false;
            
                // プレイヤーのボールに設定
                CPlayerManager.m_soccerBallManager.ChangeOwner(this.transform, pos);
                CSoccerBallManager.m_shootPlayerNo = this.GetComponent<CPlayer>().m_playerData.m_playerNo;
                CSoccerBallManager.m_shootTeamNo = this.GetComponent<CPlayer>().m_playerData.m_teamNo;


                this.gameObject.GetComponent<CPlayer>().m_isBall = true;

                // ボールの判定をトリガーにする
                obj.GetComponent<SphereCollider>().isTrigger = true;

            }
        }

        // タックルの当たり判定
        if (this.GetComponent<CPlayer>().m_status == CPlayerManager.ePLAYER_STATUS.eTACKLE &&
            (obj.gameObject.tag == "Player") || (obj.gameObject.tag == "cpu") || (obj.gameObject.tag == "GoalKeeper"))
        {
            CPlayer playerScript = this.GetComponent<CPlayer>();
            // アニメーション変更
            this.transform.parent.GetComponent<CPlayerAnimator>().TackleSuccess();
            playerScript.m_status = CPlayerManager.ePLAYER_STATUS.eTACKLESUCCESS;

            // アクション変更
            playerScript.m_action.InitTackleSuccess(playerScript.m_human.m_tackleHitMotionLength);

            CPlayer colPlayerScript = obj.GetComponent<CPlayer>();
            obj.transform.LookAt(this.transform);
            obj.transform.parent.GetComponent<CPlayerAnimator>().TackleDamage();
            colPlayerScript.m_status = CPlayerManager.ePLAYER_STATUS.eTACKLEDAMAGE;

            colPlayerScript.m_action.InitTackleDamage(colPlayerScript.m_human.m_tackleDamageMotionLength,
                                                        colPlayerScript.m_human.m_tackleDamageInitSpeed,
                                                        colPlayerScript.m_human.m_tackleDamageDecFrame);
            // ボールを持っている場合は飛ばす
            if (colPlayerScript.m_isBall == true)
            {
                GameObject soccerBall = obj.transform.FindChild("SoccerBall").gameObject;
                soccerBall.GetComponent<CSoccerBall>().BlownOff(this.transform);
                colPlayerScript.m_isBall = false;
                obj.transform.FindChild("SoccerBall").parent = GameObject.Find("BallGameObject").transform;

            }
        }
    }


}