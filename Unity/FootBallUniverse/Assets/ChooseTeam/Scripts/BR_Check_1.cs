using UnityEngine;
using System.Collections;

public class BR_Check_1 : MonoBehaviour
{
    private Player_1_Script m_TeamBR_1;
    private Vector3 m_Check_BR1;
    private Fade_1 m_Fade_BR_1;

    // Use this for initialization
    void Start()
    {

        GameObject m_Team_UI = this.gameObject.transform.parent.gameObject;
        GameObject m_Fade_1 = m_Team_UI.transform.FindChild("Fade_In_Out_1").gameObject;
        m_TeamBR_1 = m_Team_UI.GetComponent<Player_1_Script>();
        m_Fade_BR_1 = m_Fade_1.GetComponent<Fade_1>();
        m_Check_BR1.x = 0.0f;
        m_Check_BR1.y = 2.0f;
        m_Check_BR1.z = -0.34f;

    }

    // Update is called once per frame
    void Update()
    {
        if (m_Fade_BR_1.m_FadeFlag == 1 && m_TeamBR_1.m_Country[2].m_Flag == 3)
        {
            m_Check_BR1.y = 0.0f;
        }
        else
        {
            m_Check_BR1.y = 2.0f;
        }
        this.transform.position = m_Check_BR1;
    }
}
