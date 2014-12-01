using UnityEngine;
using System.Collections;

public class TEST_BASE : MonoBehaviour {

	AIBase AI;

	// Use this for initialization
	void Start () {
		AI = new AI_GK();
	}
	
	// Update is called once per frame
	void Update () {
		AI.AIUpdate();
	}
}
