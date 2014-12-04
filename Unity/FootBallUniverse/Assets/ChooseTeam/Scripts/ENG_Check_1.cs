﻿using UnityEngine;
using System.Collections;

public class ENG_Check_1 : MonoBehaviour
{
    private Player_1_Script m_TeamENG_2;
    private Vector3 m_Check_ENG2;
    private Fade_1 m_Fade_ENG_2;

    // Use this for initialization
    void Start()
    {

        GameObject m_Team_UI = this.gameObject.transform.parent.gameObject;
        GameObject m_Fade_1 = m_Team_UI.transform.FindChild("Fade_In_Out_1").gameObject;
        m_TeamENG_2 = m_Team_UI.GetComponent<Player_1_Script>();
        m_Fade_ENG_2 = m_Fade_1.GetComponent<Fade_1>();
        m_Check_ENG2.x = 0.0f;
        m_Check_ENG2.y = 2.0f;
        m_Check_ENG2.z = -0.6f;

    }

    // Update is called once per frame
    void Update()
    {
        if (m_Fade_ENG_2.m_FadeFlag == 1 && m_TeamENG_2.m_Country[1].m_Flag == 3)
        {
            m_Check_ENG2.y = 0.3f;
        }
        else
        {
            m_Check_ENG2.y = 2.0f;
        }
        this.transform.position = m_Check_ENG2;
    }
}

