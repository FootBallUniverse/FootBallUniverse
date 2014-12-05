using UnityEngine;
using System.Collections;

public class SP_Check_1 : MonoBehaviour
{
    private Player_1_Script m_TeamSP1;
    private Vector3 m_Check_SP1;
    private Fade_1 m_Fade_SP_1;

    // Use this for initialization
    void Start()
    {

        GameObject m_Team_UI = this.gameObject.transform.parent.gameObject;
        GameObject m_Fade_1 = m_Team_UI.transform.FindChild("Fade_In_Out_1").gameObject;
        m_TeamSP1 = m_Team_UI.GetComponent<Player_1_Script>();
        m_Fade_SP_1 = m_Fade_1.GetComponent<Fade_1>();
        m_Check_SP1.x = 0.0f;
        m_Check_SP1.y = 2.0f;
        m_Check_SP1.z = -0.6f;

    }

    // Update is called once per frame
    void Update()
    {
        if (m_Fade_SP_1.m_FadeFlag == 1 && m_TeamSP1.m_Country[0].m_Flag == 3)
        {
            m_Check_SP1.y = 0.3f;
        }
        else
        {
            m_Check_SP1.y = 2.0f;
        }
        this.transform.position = m_Check_SP1;
    }
}
