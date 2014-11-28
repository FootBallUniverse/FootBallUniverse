using UnityEngine;
using System.Collections;

public class CPlayerCollision : MonoBehaviour {


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //----------------------------------------------------------------------
    // 当たり判定
    //----------------------------------------------------------------------
    // @Param   Collider    ぶつかったもののGameObject		
    // @Return	none
    // @Other   CallBack
    // @Date	2014/11/28  @Update 2014/11/28  @Author T.Kawashita      
    //----------------------------------------------------------------------
	void OnTriggerEnter(Collider other)
	{
        // ボールとぶつかった時の判定
		if(other.gameObject.tag == "SoccerBall" )
		{
			if(other.transform.parent == GameObject.Find("BallGameObject").transform)
			{
				// ボールの位置をセット
				Vector3 pos = new Vector3(0.0f,0.05f,0.1f);
				
				// プレイヤーのボールに設定
				CPlayerManager.m_playerManager.m_soccerBallManager.ChangeOwner(this.transform,pos);

				this.gameObject.GetComponent<CPlayer>().m_isBall = true;

			}
		}

        // タックルの当たり判定
        if( this.GetComponent<CPlayer>().m_status == CPlayerManager.ePLAYER_STATUS.eTACKLE )
        {
            
        }

	}

}