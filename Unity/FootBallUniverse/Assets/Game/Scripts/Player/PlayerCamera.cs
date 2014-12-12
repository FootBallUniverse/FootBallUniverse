using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour {

	private GameObject Player;
	private GameObject PCamera;
//	private float distance;
//	private int lockcount;
//	private int locktime;
	public float Mcameraspeed;
	public float Rcameraspeed;
	public float cameranormalspeed;
	public float cameramovelockspeed;
	public float cameralockspeed;
	// Use this for initialization
	void Start () {
		Player = this.transform.parent.FindChild("player").gameObject;
		PCamera = this.transform.parent.FindChild("player").transform.FindChild("PlayerCamera").gameObject;
//		distance = -1.5f;
		Mcameraspeed = 10;	// カメラの追従速度
		Rcameraspeed = 100;	// カメラの回転速度
		cameranormalspeed = 100;	// カメラの通常追従速度
		cameramovelockspeed = 5;	// カメラのロック時移動追従速度
		cameralockspeed = 10;		// カメラのロック時回転追従速度
//		locktime = 10;				// ロック時減速時間
//		lockcount = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

public void CameraUpdate() {
		Vector3 wantedPosition;
		Quaternion targetRotation;

		wantedPosition.x = PCamera.transform.position.x;
		wantedPosition.y = PCamera.transform.position.y;
		wantedPosition.z = PCamera.transform.position.z;
		
//		transform.position = Vector3.Lerp (transform.position, wantedPosition, Mcameraspeed * Time.deltaTime);
		transform.position = PCamera.transform.position;
		transform.rotation = Quaternion.Slerp (transform.rotation, Player.transform.rotation, Rcameraspeed * Time.deltaTime);

	}

	public void ChangeRspeednormal() {
		Rcameraspeed = cameranormalspeed;
		
	}
	
	public void ChangeRspeedlock() {
		Rcameraspeed = cameralockspeed;
	}

	public void ChangeMspeednormal() {
		Mcameraspeed = cameranormalspeed;
		
	}
	
	public void ChangeMspeedlock() {
		Mcameraspeed = cameralockspeed;
	}

}
