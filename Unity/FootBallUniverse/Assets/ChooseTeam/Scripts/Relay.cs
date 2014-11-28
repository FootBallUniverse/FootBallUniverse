using UnityEngine;
using System.Collections;

public class Relay : MonoBehaviour {

    private Player_1_Script m_Team1;
    private Player_3_Script m_Team3;
    private Relay_JP1 m_PosJP1;
    private Relay_SP1 m_PosSP1;
    private Relay_ENG1 m_PosENG1;
    private Relay_BR1 m_PosBR1;
    private Relay_JP2 m_PosJP2;
    private Relay_SP2 m_PosSP2;
    private Relay_ENG2 m_PosENG2;
    private Relay_BR2 m_PosBR2;
    private Vector3[] m_Vec1 = new Vector3[4];
    private Vector3[] m_Vec2 = new Vector3[4];
    private Vector3[] m_Relay = new Vector3[2];
    private GameObject m_Relay1;
    private GameObject m_Relay2;


	// Use this for initialization
	void Start () 
    {
        GameObject m_JP1  = this.gameObject.transform.FindChild("Japan_1").gameObject;
        GameObject m_SP1  = this.gameObject.transform.FindChild("Spain_1").gameObject;
        GameObject m_ENG1 = this.gameObject.transform.FindChild("England_1").gameObject;
        GameObject m_BR1  = this.gameObject.transform.FindChild("Brazil_1").gameObject;
        GameObject m_JP2  = this.gameObject.transform.FindChild("Japan_2").gameObject;
        GameObject m_SP2  = this.gameObject.transform.FindChild("Spain_2").gameObject;
        GameObject m_ENG2 = this.gameObject.transform.FindChild("England_2").gameObject;
        GameObject m_BR2  = this.gameObject.transform.FindChild("Brazil_2").gameObject;

        m_Relay1 = this.gameObject.transform.FindChild("Relay_1").gameObject;
        m_Relay2 = this.gameObject.transform.FindChild("Relay_2").gameObject;

        GameObject m_Player1 = GameObject.Find("Team1_2");
        GameObject m_Player3 = GameObject.Find("Team3_4");
        m_Team1 = m_Player1.transform.GetComponent<Player_1_Script>();
        m_Team3 = m_Player3.transform.GetComponent<Player_3_Script>();
        m_PosJP1  = m_JP1.GetComponent<Relay_JP1>();
        m_PosSP1  = m_SP1.GetComponent<Relay_SP1>();
        m_PosENG1 = m_ENG1.GetComponent<Relay_ENG1>();
        m_PosBR1  = m_BR1.GetComponent<Relay_BR1>(); 
        m_PosJP2  = m_JP2.GetComponent<Relay_JP2>(); 
        m_PosSP2  = m_SP2.GetComponent<Relay_SP2>(); 
        m_PosENG2 = m_ENG2.GetComponent<Relay_ENG2>();
        m_PosBR2  = m_BR2.GetComponent<Relay_BR2>();

        m_Vec1[3] = m_PosJP1.transform.position;
        m_Vec1[0] = m_PosSP1.transform.position;
        m_Vec1[1] = m_PosENG1.transform.position;
        m_Vec1[2] = m_PosBR1.transform.position;
        m_Vec2[3] = m_PosJP2.transform.position;
        m_Vec2[0] = m_PosSP2.transform.position;
        m_Vec2[1] = m_PosENG2.transform.position;
        m_Vec2[2] = m_PosBR2.transform.position;
        m_Relay[0] = m_Relay1.transform.position;
        m_Relay[1] = m_Relay2.transform.position;
        m_Relay[0].y = 2.0f;
        m_Relay[1].y = 2.0f;
	}

	// Update is called once per frame
	void Update () {
        for (int i = 0; i < 4; i++)
        {
            if (m_Team1.m_Country[i].m_Flag == 3)
            {
                m_Vec1[i].y = 0.2f;
            }
            else
            {
                m_Vec1[i].y = 2.0f;
            }
            
            if (m_Team3.m_Country[i].m_Flag == 3)
            {
                m_Vec2[i].y = 0.2f;
            }
            else
            {
                m_Vec2[i].y = 2.0f;
            }

            // 
            if (m_Team1.m_Fade_flag_1.m_FadeFlag == 2)
            {
                m_Relay[0].y = 0.18f;
            }
            else
            {
                m_Relay[0].y = 2.0f;
            }
            if (m_Team3.m_Fade_flag_2.m_FadeFlag == 2)
            {
                m_Relay[1].y = 0.18f;
            }
            else
            {
                m_Relay[1].y = 2.0f;
            }
        }

        m_PosJP1.transform.position  = m_Vec1[3];
        m_PosSP1.transform.position  = m_Vec1[0];
        m_PosENG1.transform.position = m_Vec1[1];
        m_PosBR1.transform.position  = m_Vec1[2];
        m_PosJP2.transform.position  = m_Vec2[3];
        m_PosSP2.transform.position  = m_Vec2[0];
        m_PosENG2.transform.position = m_Vec2[1];
        m_PosBR2.transform.position  = m_Vec2[2];
        m_Relay1.transform.position = m_Relay[0];
        m_Relay2.transform.position = m_Relay[1];
	}
    
}            
                        
                        
                        