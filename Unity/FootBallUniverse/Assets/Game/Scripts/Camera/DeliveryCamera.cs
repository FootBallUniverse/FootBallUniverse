using UnityEngine;
using System.Collections;

public class DeliveryCamera : MonoBehaviour {
	private GameObject Ball;
	private GameObject[] keeperScript = new GameObject[2];

	enum DeliveryCameraState
	{
		START,
		FOLLOW_BALL,
		VIEW_GOAL,
		REPLAY,
		STATE_MAX
	};

	private DeliveryCameraState state = DeliveryCameraState.FOLLOW_BALL;

	private float cameraLength      = cameraLengthMax / 2;
	private Vector3 HOME_POSITION   = new Vector3(2.0f,1.0f,0.0f);
	private Vector3 oldBallPosition = new Vector3();
	const float cameraLengthMax     = 4.0f;
	const float cameraLengthMin     = 1.0f;

	int lookGoalNo;

	Vector3 lookPosition;
	Vector3 cameraPosition;

	private int countFlame;

	// Use this for initialization
	void Start () {
		Ball = GameObject.FindWithTag("SoccerBall").gameObject;
		for (int i = 0,j = 1; i < 2; i++,j++)
			this.keeperScript[i] = GameObject.Find("GoalKeeper" + j).transform.FindChild("cpu").gameObject;
	}

	// Update is called once per frame
	void Update()
	{
#if false
		if (Input.GetKey(KeyCode.F1)) this.cameraLength += 0.001f;
		if (Input.GetKey(KeyCode.F2)) this.cameraLength -= 0.001f;
#endif

		// 状態遷移
		for (int i = 0; i < 2; i++)
		{
			if (this.keeperScript[i].GetComponent<CGoalKeeper>().GetGKState() == CGoalKeeper.GK_State.CAT)
			{
				this.state = DeliveryCameraState.VIEW_GOAL;
				this.lookGoalNo = i;
			}
		}

		// 遷移後の処理
		switch (this.state)
		{
			case DeliveryCameraState.START: break;
			case DeliveryCameraState.FOLLOW_BALL: FollowBall(); break;
			case DeliveryCameraState.VIEW_GOAL: ViewGole(); break;
			case DeliveryCameraState.REPLAY: break;
		}
	}

	void CheckLength()
	{
		if      (this.cameraLength >= cameraLengthMax) this.cameraLength = cameraLengthMax;
		else if (this.cameraLength <= cameraLengthMin) this.cameraLength = cameraLengthMin;
	}

	void LateUpdate()
	{
		switch (this.state)
		{
			case DeliveryCameraState.START: break;
			case DeliveryCameraState.FOLLOW_BALL: FollowBallLate(); break;
			case DeliveryCameraState.VIEW_GOAL: ViewGoleLate(); break;
			case DeliveryCameraState.REPLAY: break;
		}
	}

	void FollowBall()
	{
		float ballMovePoint;
		ballMovePoint = Vector3.Distance(this.oldBallPosition, this.Ball.transform.position);
		if (this.Ball.GetComponent<CSoccerBall>().m_isPlayer)
		{
			if (ballMovePoint >= 0.07f) this.cameraLength += 0.01f;
			else if (ballMovePoint <= 0.02f) this.cameraLength -= 0.01f;
		}
		else this.cameraLength += 0.01f;

		CheckLength();
		this.oldBallPosition = this.Ball.transform.position;
	}

	void FollowBallLate()
	{
		this.transform.position = Ball.transform.position;
		this.transform.position = new Vector3( this.transform.localPosition.x + (HOME_POSITION.x * cameraLength),
		                                       this.transform.localPosition.y + (HOME_POSITION.y * cameraLength),
		                                       this.transform.localPosition.z);
		this.transform.LookAt(Ball.transform.FindChild("LookTrans"));
	}

	void ViewShote()
	{
	}

	void ViewShoteLate()
	{
	}

	void ViewGole()
	{

	}

	void ViewGoleLate()
	{
		//int i = this.lookGoalNo + 1;

		/*
		this.transform.position = Ball.transform.position;
		this.transform.rotation = Ball.rigidbody.rotation;

		this.transform.localPosition = new Vector3( this.transform.localPosition.x,
		                                            this.transform.localPosition.y,
		                                            this.transform.localPosition.z - 2.0f);
		*/

		this.transform.LookAt(this.keeperScript[this.lookGoalNo].GetComponent<CGoalKeeper>().GetHomePosition());

		if (this.keeperScript[this.lookGoalNo].GetComponent<CGoalKeeper>().GetGKState() != CGoalKeeper.GK_State.CAT)
		{
			this.countFlame++;
			if (this.countFlame > 60)
			{
				this.countFlame = 0;
				this.state = DeliveryCameraState.FOLLOW_BALL;
			}
		}
		
	}
}