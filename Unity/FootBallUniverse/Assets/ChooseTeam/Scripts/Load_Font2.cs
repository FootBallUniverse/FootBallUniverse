using UnityEngine;
using System.Collections;

public class Load_Font2 : MonoBehaviour
{
    private Player_3_Script m_PlayerScript;
    private Vector3 m_Loading2;
    private Fade_2 m_Fadeout2;

    // Use this for initialization
    void Start()
    {
        GameObject m_Team_UI = this.gameObject.transform.parent.gameObject;
        GameObject m_Fade_1 = m_Team_UI.transform.FindChild("Fade_In_Out_2").gameObject;
        m_PlayerScript = m_Team_UI.GetComponent<Player_3_Script>();
        m_Fadeout2 = m_Fade_1.GetComponent<Fade_2>();
        m_Loading2.x = 4.0f;
        m_Loading2.y = 0.0f;
        m_Loading2.z = -0.34f;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Fadeout2.m_FadeFlag == 2)
        {
            m_Loading2.y = 0.2f;
        }
        else
        {
            m_Loading2.y = 2.0f;
        }
        this.transform.position = m_Loading2;
    }
}