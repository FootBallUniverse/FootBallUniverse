using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject.Find("Map").GetComponent<Map>().CreateMaker(GameObject.Find("SoccerBall").gameObject,new Color(1,0,0,1));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
