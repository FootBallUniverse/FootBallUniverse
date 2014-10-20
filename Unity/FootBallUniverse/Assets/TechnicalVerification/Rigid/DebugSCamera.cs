using UnityEngine;
using System.Collections;

public class DebugSCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		GameObject ball;
		ball= GameObject.Find("Ball");
		transform.localPosition = new Vector3(ball.transform.position.x-0.6f, ball.transform.position.y+0.4f, ball.transform.position.z);
		if (Input.GetKey(KeyCode.F1)) {
			transform.Translate(0, 0, 1);
		}
		if (Input.GetKey(KeyCode.F2)) {
			transform.Translate(0, 0, -1);
		}

	}
}
