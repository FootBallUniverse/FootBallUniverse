using UnityEngine;
using System.Collections;

public class Load_Font1 : MonoBehaviour
{
    private Player_1_Script m_PlayerScript;
    private Vector3 m_Loading1;
    private Fade_1 m_Fadeout1;

    // Use this for initialization
    void Start()
    {
        GameObject m_Team_UI = this.gameObject.transform.parent.gameObject;
        GameObject m_Fade_1 = m_Team_UI.transform.FindChild("Fade_In_Out_1").gameObject;
        m_PlayerScript = m_Team_UI.GetComponent<Player_1_Script>();
        m_Fadeout1 = m_Fade_1.GetComponent<Fade_1>();
        m_Loading1.x = 0.0f;
        m_Loading1.y = 2.0f;
        m_Loading1.z = -0.34f;

    }

    // Update is called once per frame
    void Update()
    {
        if (m_Fadeout1.m_FadeFlag == 2)
        {
            m_Loading1.y = 0.3f;
        }
        else
        {
            m_Loading1.y = 2.0f;
        }
        this.transform.position = m_Loading1;
    }
}