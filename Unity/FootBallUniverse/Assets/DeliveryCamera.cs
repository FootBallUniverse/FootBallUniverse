using UnityEngine;
using System.Collections;

public class DeliveryCamera : MonoBehaviour {
	private GameObject Ball;

	// Use this for initialization
	void Start () {
        Ball = GameObject.FindWithTag("SoccerBall").gameObject;	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void LateUpdate()
    {
        
        this.transform.position = Ball.transform.position;
        this.transform.position = new Vector3(this.transform.localPosition.x + 2,this.transform.localPosition.y + 1,this.transform.localPosition.z);
        this.transform.LookAt(Ball.transform.FindChild("LookTrans"));

    }
}
