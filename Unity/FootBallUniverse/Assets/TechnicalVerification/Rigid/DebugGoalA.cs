using UnityEngine;
using System.Collections;

public class DebugGoalA : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void OnCollisionEnter(Collision collision)
	{
		GameObject ps;
		ps = GameObject.Find("Particle SystemA");
		Debug.Log ("GoalA");
		ps.particleSystem.Play();
	}
}
