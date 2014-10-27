using UnityEngine;
using System.Collections;

public class DebugBall : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		var speed = 10;

		// カメラ操作
		if (Input.GetKey(KeyCode.LeftArrow)) {
//			Debug.Log ("LeftArrow");
			rigidbody.AddForce(Vector3.forward * speed, ForceMode.Force);
		}
		
		if (Input.GetKey(KeyCode.RightArrow)) {
//			Debug.Log ("RightArrow");
			rigidbody.AddForce(Vector3.back * speed, ForceMode.Force);
		}
	
		if (Input.GetKey(KeyCode.UpArrow)) {
//			Debug.Log ("UpArrow");
			rigidbody.AddForce(Vector3.right * speed, ForceMode.Force);
		}

		if (Input.GetKey(KeyCode.DownArrow)) {
//			Debug.Log ("DownArrow");
			rigidbody.AddForce(Vector3.left * speed, ForceMode.Force);
		}

		if (Input.GetKey(KeyCode.Z)) {
//			Debug.Log ("Z");
			rigidbody.AddForce(Vector3.up * speed, ForceMode.Force);
		}

		if (Input.GetKey(KeyCode.X)) {
//			Debug.Log ("X");
			rigidbody.AddForce(Vector3.down * speed, ForceMode.Force);
		}

		// ボール位置リセット
		if (Input.GetKeyDown(KeyCode.R)) {
			Debug.Log ("InputKey");
			transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
			rigidbody.velocity = Vector3.zero;
			rigidbody.angularVelocity = Vector3.zero;
		}

	}
}
