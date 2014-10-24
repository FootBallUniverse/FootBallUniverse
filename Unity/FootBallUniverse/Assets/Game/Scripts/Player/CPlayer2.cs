using UnityEngine;
using System.Collections;

public class CPlayer2 : CPlayer {

	// Use this for initialization
	void Start () {

        this.Init();
        m_pos = this.transform.localPosition;

        m_human = CHumanManager.GetInstance().GetWorldInstance(CHumanManager.eWORLD.eSPAIN);

	}
	
	// Update is called once per frame
	void Update () {

        Vector3 speed = new Vector3(0.0f, 0.0f, 0.0f);
        speed.x = Input.GetAxis(InputXBOX360.P1_XBOX_LEFT_ANALOG_X) * m_human.m_playerMoveSpeed;
        speed.z = Input.GetAxis(InputXBOX360.P1_XBOX_LEFT_ANALOG_Y) * m_human.m_playerMoveSpeed;
        m_action.Move(ref m_pos, speed, this.transform.forward, this.transform.right);

        this.transform.localPosition = m_pos;
	}
}
