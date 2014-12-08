using UnityEngine;
using System.Collections;

public class C1P2PGoalStart : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.tag == "Player")
        {
             this.transform.parent.FindChild("goal1_collision").GetComponent<BoxCollider>().isTrigger = false;
        }
        else if (collider.transform.tag == "SoccerBall")
        {
            this.transform.parent.FindChild("goal1_collision").GetComponent<BoxCollider>().isTrigger = true;
        }
    }
}
