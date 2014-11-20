using UnityEngine;
using System.Collections;

public class ENG_UI_2 : MonoBehaviour
{
    public Player_3_Script m_team;
    public Vector3 m_UI;
    // Use this for initialization
    void Start()
    {
        GameObject m_Team_UI = this.gameObject.transform.parent.gameObject;
        m_team = m_Team_UI.GetComponent<Player_3_Script>();
        m_UI.x = 1.28f;
        m_UI.y = 2.0f;
        m_UI.z = -0.31f;

    }

    // Update is called once per frame
    void Update()
    {
        if (m_team.m_Right_RotateFlag == true || m_team.m_Left_RotateFlag == true)
        {
            m_UI.y = -2.0f;
        }
        else
        {
            if (m_team.m_Country[1].m_Flag == 3)
            {
                m_UI.y = -0.05f;
            }
        }
        this.transform.position = m_UI;
    }
}
