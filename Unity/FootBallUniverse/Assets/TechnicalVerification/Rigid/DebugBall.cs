using UnityEngine;
using System.Collections;

public class DebugBall : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		var speed = 100;

		// カメラ操作
		if (Input.GetKey(KeyCode.LeftArrow)) {
			rigidbody.AddForce(Vector3.left * speed, ForceMode.Force);
		}
		
		if (Input.GetKey(KeyCode.RightArrow)) {
			rigidbody.AddForce(Vector3.right * speed, ForceMode.Force);
		}
	
		if (Input.GetKey(KeyCode.UpArrow)) {
			rigidbody.AddForce(Vector3.forward * speed, ForceMode.Force);
		}

		if (Input.GetKey(KeyCode.DownArrow)) {
			rigidbody.AddForce(Vector3.back * speed, ForceMode.Force);
		}

		if (Input.GetKey(KeyCode.LeftShift)) {
			rigidbody.AddForce(Vector3.up * speed, ForceMode.Force);
		}

		if (Input.GetKey(KeyCode.LeftControl)) {
			rigidbody.AddForce(Vector3.down * speed, ForceMode.Force);
		}

		// 固定カメラ
		if (Input.GetKeyDown(KeyCode.Space)) {

		}

	}
}
