﻿using UnityEngine;
using System.Collections;

public class GoalA : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void OnCollisionEnter(Collision collision)
	{
			if (collision.gameObject == GameObject.Find ("SoccerBall")) {
					Debug.Log ("GoalA");
			}
	}
}
