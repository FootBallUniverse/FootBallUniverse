using UnityEngine;
using System.Collections;

public class title : MonoBehaviour {
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Entry_1.up_count >= 10 && Entry_2.up_count >= 10 && Entry_3.up_count >= 10 && Entry_4.up_count >= 10 )
		{
			Application.LoadLevel("ChooseTeam");
		}
		if(Input.GetKeyDown(KeyCode.Space))
		{
			Application.LoadLevel("ChooseTeam");
		}
	}
}
