using UnityEngine;
using System.Collections;

public class DeliveryCamera : MonoBehaviour {
	private GameObject Ball;

	private float cameraLength      = cameraLengthMax / 2;
	private Vector3 HOME_POSITION   = new Vector3(2.0f,1.0f,0.0f);
	private Vector3 oldBallPosition = new Vector3();
	const float cameraLengthMax     = 5.0f;
	const float cameraLengthMin     = 1.0f;

	// Use this for initialization
	void Start () {
		Ball = GameObject.FindWithTag("SoccerBall").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		float ballMovePoint;
//		Debug.Log("old = " + this.oldBallPosition + "  now = " + this.Ball.transform.position);
//		Debug.Log(Vector3.Distance(this.oldBallPosition,this.Ball.transform.position));

		if (Input.GetKey(KeyCode.F1)) this.cameraLength += 0.001f;
		if (Input.GetKey(KeyCode.F2)) this.cameraLength -= 0.001f;

		//test
		switch (CGameManager.m_nowStatus)
		{
			case CGameManager.eSTATUS.eGAME:
				ballMovePoint = Vector3.Distance(this.oldBallPosition,this.Ball.transform.position);
				if      (ballMovePoint >= 0.07f) this.cameraLength += 0.01f;
				else if (ballMovePoint <= 0.02f) this.cameraLength -= 0.01f;
				break;
		}
		//test_end

		CheckLength();
		this.oldBallPosition = this.Ball.transform.position;
	}

	void CheckLength()
	{
		if      (this.cameraLength >= cameraLengthMax) this.cameraLength = cameraLengthMax;
		else if (this.cameraLength <= cameraLengthMin) this.cameraLength = cameraLengthMin;
	}

    void LateUpdate()
    {
		switch (CGameManager.m_nowStatus)
		{
			case CGameManager.eSTATUS.eGAME:
				this.transform.position = Ball.transform.position;
				this.transform.position = new Vector3( this.transform.localPosition.x + (HOME_POSITION.x * cameraLength),
				                                       this.transform.localPosition.y + (HOME_POSITION.y * cameraLength),
				                                       this.transform.localPosition.z);
				this.transform.LookAt(Ball.transform.FindChild("LookTrans"));
				break;
		}

    }
}
