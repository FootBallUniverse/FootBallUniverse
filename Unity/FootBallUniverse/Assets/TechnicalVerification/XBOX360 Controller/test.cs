using UnityEngine;
using System.Collections;

public class test : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Entity_HumanData entry_humandata = Resources.Load("Data/HumanData") as Entity_HumanData;
        Debug.Log( entry_humandata.param[0].id );
        Debug.Log(entry_humandata.param[0].jpn_name);
        Debug.Log(entry_humandata.param[0].eng_name);
        Debug.Log(entry_humandata.param[0].player_speed);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
