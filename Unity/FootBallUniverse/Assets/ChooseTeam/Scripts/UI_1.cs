using UnityEngine;
using System.Collections;

public class UI_1 : MonoBehaviour
{
    public Player_1_Script m_team1;
    public Vector3[] m_UIPos = new Vector3[4];
    public GameObject m_JP1;
    public GameObject m_SP1;
    public GameObject m_ENG1;
    public GameObject m_BR1;
    // Use this for initialization
    void Start()
    {
        GameObject m_Team_UI = this.gameObject.transform.parent.gameObject;
        m_JP1  = this.gameObject.transform.FindChild("Japan_UI").gameObject;
        m_SP1 = this.gameObject.transform.FindChild("Spain_UI").gameObject;
        m_ENG1 = this.gameObject.transform.FindChild("England_UI").gameObject;
        m_BR1 = this.gameObject.transform.FindChild("Brazil_UI").gameObject;

        m_team1 = m_Team_UI.GetComponent<Player_1_Script>();
        m_UIPos[3] = m_JP1.transform.position;
        m_UIPos[0] = m_SP1.transform.position; 
        m_UIPos[1] = m_ENG1.transform.position;
        m_UIPos[2] = m_BR1.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 4; i++)
        {
            if (m_team1.m_Right_RotateFlag == true || m_team1.m_Left_RotateFlag == true)
            {
                m_UIPos[i].y = -2.0f;
            }
            else
            {
                
                if (m_team1.m_Country[i].m_Flag == 3)
                {
                    m_UIPos[i].y = 0.2f;
                    Debug.Log(m_team1.m_Country[i].m_Flag);
                    Debug.Log(i);
                    Debug.Log(m_UIPos[i].y);
                }
                else
                {
                    m_UIPos[i].y = 2.0f;
                }
            }
            m_JP1.transform.position = m_UIPos[3];
            m_SP1.transform.position = m_UIPos[0];
            m_ENG1.transform.position = m_UIPos[1];
            m_BR1.transform.position = m_UIPos[2];
        }
    }
}
