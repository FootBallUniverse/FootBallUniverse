using UnityEngine;
using System.Collections;

public class DebugMCamera : MonoBehaviour {

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.F3)) {
			transform.Translate(0, 0, 1);
		}
		if (Input.GetKey(KeyCode.F4)) {
			transform.Translate(0, 0, -1);
		}
	}
}
