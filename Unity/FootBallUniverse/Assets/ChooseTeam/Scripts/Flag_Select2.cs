using UnityEngine;
using System.Collections;

public class Flag_Select2 : MonoBehaviour
{
    public Player_3_Script m_team2;
    public GameObject[] m_Country2 = new GameObject[4];
    public Vector3[] m_FlagPos2 = new Vector3[4];
    public TweenAlpha[] m_Alpha2 = new TweenAlpha[4];
    public Vector3 m_PlusScale2;
    float m_Center_Pos2 = 4.0f;
    int[] m_Flag2 = new int[4];
    // Use this for initialization
    void Start()
    {
        GameObject m_Team_UI = this.gameObject.transform.parent.gameObject;
        m_Country2[0] = this.gameObject.transform.FindChild("flag_SPA2").gameObject;
        m_Alpha2[0] = m_Country2[0].GetComponent<TweenAlpha>();
        m_Country2[1] = this.gameObject.transform.FindChild("flag_ENG2").gameObject;
        m_Alpha2[1] = m_Country2[1].GetComponent<TweenAlpha>();
        m_Country2[2] = this.gameObject.transform.FindChild("flag_BRA2").gameObject;
        m_Alpha2[2] = m_Country2[2].GetComponent<TweenAlpha>();
        m_Country2[3] = this.gameObject.transform.FindChild("flag_JAP2").gameObject;
        m_Alpha2[3] = m_Country2[3].GetComponent<TweenAlpha>();

        m_team2 = m_Team_UI.GetComponent<Player_3_Script>();

        m_PlusScale2 = new Vector3(0.03f, 0.03f, 0f);
        for (int i = 0; i < 4; i++)
        {
            m_FlagPos2[i] = m_Country2[i].transform.position;
            m_Flag2[i] = m_team2.m_Country[i].m_Flag;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 4; i++)
        {
            m_Flag2[i] = m_team2.m_Country[i].m_Flag;
            if (m_team2.m_Left_RotateFlag == true)// →押した
            {
                switch (m_Flag2[i])
                {
                    case 0:
                        m_FlagPos2[i].x -= 0.01f;
                        break;

                    case 1:
                        m_FlagPos2[i].x -= 0.02f;
                        if (m_FlagPos2[i].x <= m_Center_Pos2 - 0.5f)
                        {
                            m_FlagPos2[i].x = m_Center_Pos2 + 0.49f;
                        }
                        else if (m_FlagPos2[i].x >= m_Center_Pos2 + 0.29f)
                        {
                            TweenAlpha.Begin(m_Country2[i], 0.1f, 1.0f);
                        }
                        break;

                    case 2:
                        m_FlagPos2[i].x -= 0.01f;
                        if (m_FlagPos2[i].x <= m_Center_Pos2 - 0.11f)
                        {
                            TweenAlpha.Begin(m_Country2[i], 0.05f, 0.0f);
                        }
                        break;

                    case 3:
                        m_FlagPos2[i].x -= 0.01f;
                        break;
                }
            }
            else if (m_team2.m_Right_RotateFlag == true)// ←押した
            {
                switch (m_Flag2[i])
                {
                    case 0: m_FlagPos2[i].x += 0.02f;
                        if (m_FlagPos2[i].x >= m_Center_Pos2 + 0.49f)
                        {
                            m_FlagPos2[i].x = m_Center_Pos2 - 0.5f;
                        }
                        else if (m_FlagPos2[i].x >= m_Center_Pos2 + 0.31f)
                        {
                            TweenAlpha.Begin(m_Country2[i], 0.05f, 0.0f);
                        }
                        break;

                    case 1:
                        m_FlagPos2[i].x += 0.01f;
                        if (m_FlagPos2[i].x <= m_Center_Pos2 - 0.1f)
                        {
                            TweenAlpha.Begin(m_Country2[i], 0.1f, 1.0f);
                        }
                        break;

                    case 2:
                        m_FlagPos2[i].x += 0.01f;
                        break;

                    case 3:
                        m_FlagPos2[i].x += 0.01f;
                        break;
                }
            }

            if (m_team2.m_Right_RotateFlag == false && m_team2.m_Left_RotateFlag == false)
            {
                if (m_FlagPos2[i].x <= m_Center_Pos2 - 0.04f && m_FlagPos2[i].x >= m_Center_Pos2 - 0.14f)
                {
                    m_FlagPos2[i].x = m_Center_Pos2 - 0.1f;
                }
                if (m_FlagPos2[i].x <= m_Center_Pos2 + 0.14f && m_FlagPos2[i].x >= m_Center_Pos2 + 0.06f)
                {
                    m_FlagPos2[i].x = m_Center_Pos2 + 0.1f;
                }
                if (m_FlagPos2[i].x <= m_Center_Pos2 + 0.34f && m_FlagPos2[i].x >= m_Center_Pos2 + 0.26f)
                {
                    m_FlagPos2[i].x = m_Center_Pos2 + 0.3f;
                }
                if (m_FlagPos2[i].x <= m_Center_Pos2 - 0.26f && m_FlagPos2[i].x >= m_Center_Pos2 - 0.34f)
                {
                    m_FlagPos2[i].x = m_Center_Pos2 - 0.3f;
                }
            }
            m_Country2[i].transform.position = m_FlagPos2[i];
        }
    }
}