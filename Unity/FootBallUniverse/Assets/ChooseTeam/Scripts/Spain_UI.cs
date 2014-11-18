﻿using UnityEngine;
using System.Collections;

public class Spain_UI : MonoBehaviour
{
    public Player_3_Script m_team3;
    public Vector3 m_UI;
    // Use this for initialization
    void Start()
    {
        GameObject m_Team_UI = this.gameObject.transform.parent.gameObject;
        m_team3 = m_Team_UI.GetComponent<Player_3_Script>();
        m_UI.x = 1.28f;
        m_UI.y = 2.0f;
        m_UI.z = -0.31f;

    }

    // Update is called once per frame
    void Update()
    {
        if (m_team3.m_Right_RotateFlag == true || m_team3.m_Left_RotateFlag == true)
        {
            m_UI.y = -2.0f;
        }
        else
        {
            if (m_team3.m_Country[0].m_Flag == 3)
            {
                m_UI.y = -0.05f;
            }
        }
        this.transform.position = m_UI;
    }
}
