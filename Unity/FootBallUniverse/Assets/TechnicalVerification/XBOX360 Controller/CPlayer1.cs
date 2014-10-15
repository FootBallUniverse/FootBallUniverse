using UnityEngine;
using System.Collections;

public class CPlayer1 : CPlayer {

	// Use this for initialization
	void Start () {

        m_pos = this.transform.localPosition;
        m_pos = m_old_pos;
        m_angle = new Vector2(0.0f, 0.0f);
	}
	
	// Update is called once per frame
	void Update () {

        m_angle.y += Input.GetAxis(InputXBOX360.P1_XBOX_RIGHT_ANALOG_X) * 10;
        m_angle.x += Input.GetAxis(InputXBOX360.P1_XBOX_RIGHT_ANALOG_Y) * 10;

        this.transform.localRotation = Quaternion.Euler(m_angle.x, m_angle.y, 0.0f);

        m_pos += (Input.GetAxis(InputXBOX360.P1_XBOX_LEFT_ANALOG_Y) * 0.2f) * transform.forward;

        this.transform.localPosition = m_pos;
    }
}
