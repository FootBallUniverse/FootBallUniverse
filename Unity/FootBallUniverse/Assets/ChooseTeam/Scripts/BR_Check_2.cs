using UnityEngine;
using System.Collections;

public class BR_Check_2 : MonoBehaviour
{
    private Player_3_Script m_TeamBR_2;
    private Vector3 m_Check_BR2;
    private Fade_2 m_Fade_BR_2;

    // Use this for initialization
    void Start()
    {

        GameObject m_Team_UI = this.gameObject.transform.parent.gameObject;
        GameObject m_Fade_2 = m_Team_UI.transform.FindChild("Fade_In_Out_2").gameObject;
        m_TeamBR_2 = m_Team_UI.GetComponent<Player_3_Script>();
        m_Fade_BR_2 = m_Fade_2.GetComponent<Fade_2>();
        m_Check_BR2.x = 1.28f;
        m_Check_BR2.y = 2.0f;
        m_Check_BR2.z = -0.34f;

    }

    // Update is called once per frame
    void Update()
    {
        if (m_Fade_BR_2.m_FadeFlag == 1 && m_TeamBR_2.m_Country[2].m_Flag == 3)
        {
            m_Check_BR2.y = 0.0f;
        }
        else
        {
            m_Check_BR2.y = 2.0f;
        }
        this.transform.position = m_Check_BR2;
    }
}
