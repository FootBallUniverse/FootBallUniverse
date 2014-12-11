using UnityEngine;
using System.Collections;

public class Relay : MonoBehaviour {

    private Player_1_Script m_Team1;
    private Player_3_Script m_Team3;
    private Vector3[] m_Vec1 = new Vector3[4];
    private Vector3[] m_Vec2 = new Vector3[4];
    private Vector3[] m_Relay = new Vector3[2];
    private UILabel m_Label;
    GameObject m_JP1;
    GameObject m_SP1;
    GameObject m_ENG1;
    GameObject m_BR1;
    GameObject m_JP2;
    GameObject m_SP2;
    GameObject m_ENG2;
    GameObject m_BR2;

	// Use this for initialization
	void Start () 
    {
        m_JP1  = this.gameObject.transform.FindChild("Japan_1").gameObject;
        m_SP1  = this.gameObject.transform.FindChild("Spain_1").gameObject;
        m_ENG1 = this.gameObject.transform.FindChild("England_1").gameObject;
        m_BR1  = this.gameObject.transform.FindChild("Brazil_1").gameObject;
        m_JP2  = this.gameObject.transform.FindChild("Japan_2").gameObject;
        m_SP2  = this.gameObject.transform.FindChild("Spain_2").gameObject;
        m_ENG2 = this.gameObject.transform.FindChild("England_2").gameObject;
        m_BR2  = this.gameObject.transform.FindChild("Brazil_2").gameObject;

        GameObject m_Player1 = GameObject.Find("Team1_2");
        GameObject m_Player3 = GameObject.Find("Team3_4");
        m_Team1 = m_Player1.transform.GetComponent<Player_1_Script>();
        m_Team3 = m_Player3.transform.GetComponent<Player_3_Script>();
        m_Label = GameObject.Find("Label(Relay)").GetComponent<UILabel>();
        m_Vec1[3] = m_JP1.transform.position;
        m_Vec1[0] = m_SP1.transform.position;
        m_Vec1[1] = m_ENG1.transform.position;
        m_Vec1[2] = m_BR1.transform.position;
        m_Vec2[3] = m_JP2.transform.position;
        m_Vec2[0] = m_SP2.transform.position;
        m_Vec2[1] = m_ENG2.transform.position;
        m_Vec2[2] = m_BR2.transform.position;
        
	}

	// Update is called once per frame
	void Update () {
        for (int i = 0; i < 4; i++)
        {
            if (m_Team1.m_Country[i].m_Flag == 3)
            {
                m_Vec1[i].y = 0.25f;
            }
            else
            {
                m_Vec1[i].y = 2.0f;
            }
            
            if (m_Team3.m_Country[i].m_Flag == 3)
            {
                m_Vec2[i].y = 0.25f;
            }
            else
            {
                m_Vec2[i].y = 2.0f;
            }

          
            if (m_Team1.m_Fade_flag_1.m_FadeFlag == 2
                && m_Team3.m_Fade_flag_2.m_FadeFlag == 2)
            {
                m_Label.text = "ゲームを開始します。";
            }
            else
            {
                m_Label.text = "チームを選択中です。";
            }
        }

         m_JP1.transform.position = m_Vec1[3];
         m_SP1.transform.position = m_Vec1[0];
         m_ENG1.transform.position = m_Vec1[1];
         m_BR1.transform.position = m_Vec1[2];
         m_JP2.transform.position = m_Vec2[3];
         m_SP2.transform.position = m_Vec2[0];
         m_ENG2.transform.position = m_Vec2[1];
         m_BR2.transform.position = m_Vec2[2];
	}
    
}            
                        
                        
                        