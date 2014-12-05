using UnityEngine;
using System.Collections;

public class Flag_Select1 : MonoBehaviour
{
    public Player_1_Script m_team1;
    public Vector3[] m_FlagPos = new Vector3[4];
    public GameObject m_JPFlag;
    public GameObject m_SPFlag;
    public GameObject m_ENGFlag;
    public GameObject m_BRFlag;
    public GameObject m_Cursor;
    int[] m_Flag = new int[4];

    // Use this for initialization
    void Start()
    {
        GameObject m_Team_UI = this.gameObject.transform.parent.gameObject;
        m_SPFlag = this.gameObject.transform.FindChild("flag_SPA1").gameObject;
        m_ENGFlag = this.gameObject.transform.FindChild("flag_ENG1").gameObject;
        m_BRFlag = this.gameObject.transform.FindChild("flag_BRA1").gameObject;
        m_JPFlag = this.gameObject.transform.FindChild("flag_JAP1").gameObject;

        m_team1 = m_Team_UI.GetComponent<Player_1_Script>();

        m_FlagPos[0] = m_SPFlag.transform.position;
        m_FlagPos[1] = m_ENGFlag.transform.position;
        m_FlagPos[2] = m_BRFlag.transform.position;
        m_FlagPos[3] = m_JPFlag.transform.position;

        for (int i = 0; i < 4; i++)
        {

            m_Flag[i] = m_team1.m_Country[i].m_Flag;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 4; i++)
        {
            if (m_team1.m_Right_RotateFlag == true)
            {
                m_FlagPos[i].x -= 0.01f;

                if (m_FlagPos[i].x <= -0.3f)
                {
                    m_FlagPos[i].x = 0.49f;
                }
            }
            if (m_team1.m_Left_RotateFlag == true)
            {
                m_FlagPos[i].x += 0.01f;

                if (m_FlagPos[i].x >= 0.49f)
                {
                    m_FlagPos[i].x = -0.3f;
                }
            }
            if (m_team1.m_Right_RotateFlag == false && m_team1.m_Left_RotateFlag == false)
            {
                if (m_FlagPos[i].x <= 0.04f && m_FlagPos[i].x >= -0.04f)
                {
                    m_FlagPos[i].x = 0.0f;
                }
                if (m_FlagPos[i].x <= 0.24f && m_FlagPos[i].x >= 0.16f)
                {
                    m_FlagPos[i].x = 0.2f;
                }
                if (m_FlagPos[i].x <= 0.44f && m_FlagPos[i].x >= 0.36f)
                {
                    m_FlagPos[i].x = 0.4f;
                }
                if (m_FlagPos[i].x <= -0.16f && m_FlagPos[i].x >= -0.24f)
                {
                    m_FlagPos[i].x = -0.2f;
                }
            }
        }
        m_SPFlag.transform.position = m_FlagPos[0];
        m_ENGFlag.transform.position = m_FlagPos[1];
        m_BRFlag.transform.position = m_FlagPos[2];
        m_JPFlag.transform.position = m_FlagPos[3];
    }
}
