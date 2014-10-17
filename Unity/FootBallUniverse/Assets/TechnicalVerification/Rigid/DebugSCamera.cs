using UnityEngine;
using System.Collections;

public class DebugSCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
		public var ball = GameObject.Find("Ball");
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (ball.Rigidbody.velocity.x, ball.Rigidbody.velocity.y, 0);
		if (Input.GetKey(KeyCode.F1)) {
			transform.Translate(0, 0, 1);
		}
		if (Input.GetKey(KeyCode.F2)) {
			transform.Translate(0, 0, -1);
		}

	}
}
