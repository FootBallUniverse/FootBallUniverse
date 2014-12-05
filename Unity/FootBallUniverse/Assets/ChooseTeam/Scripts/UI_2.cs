using UnityEngine;
using System.Collections;

public class UI_2 : MonoBehaviour
{
    public Player_3_Script m_team2;
    public Vector3[] m_UIPos = new Vector3[4];
    public GameObject m_JP2;
    public GameObject m_SP2;
    public GameObject m_ENG2;
    public GameObject m_BR2;
    // Use this for initialization
    void Start()
    {
        GameObject m_Team_UI = this.gameObject.transform.parent.gameObject;
        m_JP2 = this.gameObject.transform.FindChild("Japan_UI").gameObject;
        m_SP2 = this.gameObject.transform.FindChild("Spain_UI").gameObject;
        m_ENG2 = this.gameObject.transform.FindChild("England_UI").gameObject;
        m_BR2 = this.gameObject.transform.FindChild("Brazil_UI").gameObject;

        m_team2 = m_Team_UI.GetComponent<Player_3_Script>();
        m_UIPos[3] = m_JP2.transform.position;
        m_UIPos[0] = m_SP2.transform.position;
        m_UIPos[1] = m_ENG2.transform.position;
        m_UIPos[2] = m_BR2.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 4; i++)
        {
            if (m_team2.m_Fade_flag_2.m_FadeFlag == 0)
            {
                if (m_team2.m_Right_RotateFlag == true || m_team2.m_Left_RotateFlag == true)
                {
                    m_UIPos[i].y = -2.0f;
                }
                else
                {

                    if (m_team2.m_Country[i].m_Flag == 3)
                    {
                        m_UIPos[i].y = 0.2f;
                        Debug.Log(m_team2.m_Country[i].m_Flag);
                        Debug.Log(i);
                        Debug.Log(m_UIPos[i].y);
                    }
                    else
                    {
                        m_UIPos[i].y = 2.0f;
                    }
                }
            }
            else
            {
                m_UIPos[i].y = 2.0f;
            }
            m_JP2.transform.position = m_UIPos[3];
            m_SP2.transform.position = m_UIPos[0];
            m_ENG2.transform.position = m_UIPos[1];
            m_BR2.transform.position = m_UIPos[2];
        }
    }
}
