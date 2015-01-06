using UnityEngine;
using System.Collections;

public class Flag_Select1 : MonoBehaviour
{
    public Player_1_Script m_team1;
    public GameObject[] m_Country = new GameObject[4];
    public Vector3[] m_FlagPos = new Vector3[4];
    public TweenAlpha[] m_Alpha = new TweenAlpha[4];
    public Vector3 m_PlusScale;
    int[] m_Flag = new int[4];
    float m_Center_Pos = 0.0f;
    // Use this for initialization
    void Start()
    {
        GameObject m_Team_UI = this.gameObject.transform.parent.gameObject;
        m_Country[0] = this.gameObject.transform.FindChild("flag_SPA1").gameObject;
        m_Alpha[0] = m_Country[0].GetComponent<TweenAlpha>();
        m_Country[1] = this.gameObject.transform.FindChild("flag_ENG1").gameObject;
        m_Alpha[1] = m_Country[1].GetComponent<TweenAlpha>();
        m_Country[2] = this.gameObject.transform.FindChild("flag_BRA1").gameObject;
        m_Alpha[2] = m_Country[2].GetComponent<TweenAlpha>();
        m_Country[3] = this.gameObject.transform.FindChild("flag_JAP1").gameObject;
        m_Alpha[3] = m_Country[3].GetComponent<TweenAlpha>();

        m_team1 = m_Team_UI.GetComponent<Player_1_Script>();

        m_PlusScale = new Vector3(0.03f,0.03f,0f);
        for (int i = 0; i < 4; i++)
        {
            m_FlagPos[i] = m_Country[i].transform.position;
            m_Flag[i] = m_team1.m_Country[i].m_Flag;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 4; i++)
        {
            Debug.Log("右回転は" + m_team1.m_Left_RotateFlag);
            Debug.Log("左回転は" + m_team1.m_Right_RotateFlag);
            m_Flag[i] = m_team1.m_Country[i].m_Flag;
            if (m_team1.m_Left_RotateFlag == true)
            {
                switch (m_Flag[i])
                {
                    case 0:
                        m_FlagPos[i].x -= 0.01f;
                        break;
                    case 1:
                        m_FlagPos[i].x -= 0.01f;
                        if (m_FlagPos[i].x <= m_Center_Pos + 0.3f)
                        {
                            TweenAlpha.Begin(m_Country[i], 0.1f, 1.0f);
                        }
                        break;
                    case 2:
                        m_FlagPos[i].x -= 0.02f;
                        if (m_FlagPos[i].x <= -0.5f)
                        {
                            m_FlagPos[i].x = 0.49f;
                        }
                        if (m_FlagPos[i].x <= -0.31f)
                        {
                            TweenAlpha.Begin(m_Country[i], 0.05f, 0.0f);
                        }
                        break;
                    case 3:
                        m_FlagPos[i].x -= 0.01f;
                        break;
                }
            }
            if (m_team1.m_Right_RotateFlag == true)
            {
                switch (m_Flag[i])
                {
                    case 0:
                        m_FlagPos[i].x += 0.01f;
                        if (m_FlagPos[i].x >= 0.11f)
                        {
                            TweenAlpha.Begin(m_Country[i], 0.05f, 0.0f);
                        }
                        break;
                    case 1:
                        m_FlagPos[i].x += 0.02f;
                        if (m_FlagPos[i].x >= 0.49f)
                        {
                            m_FlagPos[i].x = -0.5f;
                        }
                        else if(m_FlagPos[i].x <= -0.3f)
                        {
                            TweenAlpha.Begin(m_Country[i], 0.1f, 1.0f);
                        }
                        break;
                    case 2:
                        m_FlagPos[i].x += 0.01f;
                        break;
                    case 3:
                        m_FlagPos[i].x += 0.01f;
                        break;
                }
            }

            if (m_team1.m_Right_RotateFlag == false && m_team1.m_Left_RotateFlag == false)
            {
                if (m_FlagPos[i].x <= -0.04f && m_FlagPos[i].x >= -0.14f)
                {
                    m_FlagPos[i].x = -0.1f;
                }
                if (m_FlagPos[i].x <= 0.14f && m_FlagPos[i].x >= 0.06f)
                {
                    m_FlagPos[i].x = 0.1f;
                }
                if (m_FlagPos[i].x <= 0.34f && m_FlagPos[i].x >= 0.26f)
                {
                    m_FlagPos[i].x = 0.3f;
                }
                if (m_FlagPos[i].x <= -0.26f && m_FlagPos[i].x >= -0.34f)
                {
                    m_FlagPos[i].x = -0.3f;
                }
            }
            m_Country[i].transform.position = m_FlagPos[i];
        }
    }
}


