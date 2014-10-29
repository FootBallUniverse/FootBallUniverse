using UnityEngine;
using System.Collections;

public class test : MonoBehaviour {

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {

        if (InputXBOX360.IsGetRTButton(InputXBOX360.P1_XBOX_RTLT) == true)
        {
            Debug.Log("RTが押された！");
        }

        if (InputXBOX360.IsGetLTButton(InputXBOX360.P1_XBOX_RTLT) == true)
            Debug.Log("LTが押された！");
	}
}
