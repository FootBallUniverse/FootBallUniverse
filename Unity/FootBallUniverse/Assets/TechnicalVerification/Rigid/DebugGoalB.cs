using UnityEngine;
using System.Collections;

public class DebugGoalB : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	private void OnCollisionEnter(Collision collision)
	{
		GameObject ps;
		ps = GameObject.Find("Particle SystemB");
		Debug.Log ("GoalB");
		ps.particleSystem.Play();
	}
}
