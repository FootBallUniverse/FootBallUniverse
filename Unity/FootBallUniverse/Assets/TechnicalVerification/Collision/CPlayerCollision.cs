using UnityEngine;
using System.Collections;

public class CPlayerCollision : MonoBehaviour {


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag=="SoccerBall")
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

	}

}