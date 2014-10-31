using UnityEngine;
using System.Collections;

public class DebugSCamera : MonoBehaviour {

	private bool MCstate;

	// Use this for initialization
	void Start () {
		MCstate = false;
	}
	
	// Update is called once per frame
	void Update () {
		GameObject ball;
		GameObject player;
		ball= GameObject.Find("SoccerBall");
		player= GameObject.Find("Character001");
		if(MCstate) transform.position = new Vector3(ball.transform.position.x-5.0f, ball.transform.position.y+3.0f, ball.transform.position.z);
		else transform.position = new Vector3(player.transform.position.x-5.0f, player.transform.position.y+3.0f, player.transform.position.z);

		if (Input.GetKeyDown(KeyCode.Space)) {
			if(MCstate)MCstate = false;
			else MCstate = true;
		}

		if (Input.GetKey(KeyCode.F1)) {
			transform.Translate(0, 0, 1);
		}
		if (Input.GetKey(KeyCode.F2)) {
			transform.Translate(0, 0, -1);
		}

	}
}
