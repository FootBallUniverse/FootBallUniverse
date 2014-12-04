using UnityEngine;
using System.Collections;

public class Flag_Select2 : MonoBehaviour
{
    public Player_3_Script m_team1;
    public Vector3[] m_FlagPos = new Vector3[5];
    public GameObject m_JPFlag;
    public GameObject m_SPFlag;
    public GameObject m_ENGFlag;
    public GameObject m_BRFlag;
    public GameObject m_Cursor;
    bool[] m_Flag = new bool[4];
    // Use this for initialization
    void Start()
    {
        GameObject m_Team_UI = this.gameObject.transform.parent.gameObject;
        m_SPFlag = this.gameObject.transform.FindChild("flag_0").gameObject;
        m_ENGFlag = this.gameObject.transform.FindChild("flag_1").gameObject;
        m_BRFlag = this.gameObject.transform.FindChild("flag_2").gameObject;
        m_JPFlag = this.gameObject.transform.FindChild("flag_3").gameObject;
        m_Cursor = this.gameObject.transform.FindChild("Cursor").gameObject;

        m_team1 = m_Team_UI.GetComponent<Player_3_Script>();

        m_FlagPos[0] = m_SPFlag.transform.position;
        m_FlagPos[1] = m_ENGFlag.transform.position;
        m_FlagPos[2] = m_BRFlag.transform.position;
        m_FlagPos[3] = m_JPFlag.transform.position;
        m_FlagPos[4] = m_Cursor.transform.position;
        m_Flag[0] =false;
        m_Flag[1] =false;
        m_Flag[2] =false;
        m_Flag[3] =false;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 4; i++)
        {
            if (m_team1.m_Right_RotateFlag == true)
            {
                m_FlagPos[i].x -= 0.01f;
                 
                if (m_FlagPos[i].x <= 3.46f)
                {
                    m_FlagPos[i].x = 4.18f;
                }
            }
            if (m_team1.m_Left_RotateFlag == true)
            {
                m_FlagPos[i].x += 0.01f;

                if (m_FlagPos[i].x >= 4.18f)
                {
                    m_FlagPos[i].x = 3.46f;
                }
            }
            m_SPFlag.transform.position  = m_FlagPos[0];
            m_ENGFlag.transform.position = m_FlagPos[1];
            m_BRFlag.transform.position  = m_FlagPos[2];
            m_JPFlag.transform.position = m_FlagPos[3];
        }   
    }       
}
