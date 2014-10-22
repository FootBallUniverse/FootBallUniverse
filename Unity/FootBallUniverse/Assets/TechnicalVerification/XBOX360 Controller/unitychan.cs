using UnityEngine;
using System.Collections;

public class unitychan : MonoBehaviour {

    private float minAngle = 0.0f;
    private float maxAngle = 360.0f;
    private float yAngle = 0.0f;
    private float xAngle = 0.0f;
    private float zAngle = 0.0f;


    private Vector3 m_pos;
    private Vector3 m_pos_old;

	// Use this for initialization
	void Start () {
        m_pos = this.transform.localPosition;
        m_pos_old = m_pos;
	}
	
	// Update is called once per frame
	void Update () {
        
        yAngle += Input.GetAxis(InputXBOX360.P1_XBOX_RIGHT_ANALOG_X) * 10;
        xAngle += Input.GetAxis(InputXBOX360.P1_XBOX_RIGHT_ANALOG_Y) * 10;

        Quaternion q = Quaternion.Euler(xAngle,yAngle,0.0f);
        transform.rotation = q;
        

        m_pos_old += (Input.GetAxis(InputXBOX360.P1_XBOX_LEFT_ANALOG_Y) * 0.2f) * transform.forward;

        this.transform.position = m_pos_old;
    }
}
