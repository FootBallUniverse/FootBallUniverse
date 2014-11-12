using UnityEngine;
using System.Collections;

public class Char_Test : MonoBehaviour {

	float degree;
	float r;
	float centerx;
	float centerz;
	float radian;
	
	// Use this for initialization
	void Start () {
		degree = 0.0f;
		r = 0.31f;
		centerx = 0.0f;
		centerz = 0.0f;
		radian = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 Position = transform.position;
		radian = Mathf.PI/180.0f*degree;
		Position.x = centerx+r*Mathf.Cos(radian);
		Position.z = centerz+r*Mathf.Sin(radian)/2;
		degree += 5.0f;
		Debug.Log(degree);
		transform.position = Position;
		if(degree >= 360.0f){
			degree =0.0f;
		}
	
	}
}
