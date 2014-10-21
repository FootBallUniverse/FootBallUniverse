using UnityEngine;
using System.Collections;

public class test : MonoBehaviour {

	// Use this for initialization
	void Start () {
        HumanData humanData = Resources.Load("Data/HumanData") as HumanData;
        Debug.Log(humanData.param[0].id);
        Debug.Log(humanData.param[0].eng_name);
        Debug.Log(humanData.param[0].jpn_name);
        Debug.Log(humanData.param[0].player_speed);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
