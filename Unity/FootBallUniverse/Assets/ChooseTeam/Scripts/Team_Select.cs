using UnityEngine;
using System.Collections;

public class Team_Select : MonoBehaviour {

    Player_1_Script m_Fade_flag_1;
    Player_3_Script m_Fade_flag_2;
    private float m_Count;
	// Use this for initialization
	void Start () {
        m_Count = 0;
        // 必要データの読込み
        GameObject m_TeamData = GameObject.Find("TeamData");

        GameObject m_TeamSelect_1 = transform.FindChild("Team1_2").gameObject;
        GameObject m_TeamSelect_2 = transform.FindChild("Team3_4").gameObject;
        m_Fade_flag_1 = m_TeamSelect_1.GetComponent<Player_1_Script>();
        m_Fade_flag_2 = m_TeamSelect_2.GetComponent<Player_3_Script>();
	}
	
	// Update is called once per frame
	void Update () {
        
        if(m_Fade_flag_1.m_SceneFlag == true &&
            m_Fade_flag_2.m_SceneFlag == true )
        {
            m_Count+= Time.deltaTime;
            Debug.Log(m_Count);
            if (m_Count >= 3.0f)
            {
                if (m_Fade_flag_1.m_Country[0].m_Flag == 3)
                {
                    TeamData.teamNationality[0] = TeamData.TEAM_NATIONALITY.ESPANA;
                }
                else if (m_Fade_flag_1.m_Country[1].m_Flag == 3)
                {
                    TeamData.teamNationality[0] = TeamData.TEAM_NATIONALITY.ENGLAND;
                }
                else if (m_Fade_flag_1.m_Country[2].m_Flag == 3)
                {
                    TeamData.teamNationality[0] = TeamData.TEAM_NATIONALITY.BRASIL;
                }
                else if (m_Fade_flag_1.m_Country[3].m_Flag == 3)
                {
                    TeamData.teamNationality[0] = TeamData.TEAM_NATIONALITY.JAPAN;
                }

                if (m_Fade_flag_2.m_Country[0].m_Flag == 3)
                {
                    TeamData.teamNationality[1] = TeamData.TEAM_NATIONALITY.ESPANA;
                }
                else if (m_Fade_flag_2.m_Country[1].m_Flag == 3)
                {
                    TeamData.teamNationality[1] = TeamData.TEAM_NATIONALITY.ENGLAND;
                }
                else if (m_Fade_flag_2.m_Country[2].m_Flag == 3)
                {
                    TeamData.teamNationality[1] = TeamData.TEAM_NATIONALITY.BRASIL;
                }
                else if (m_Fade_flag_2.m_Country[3].m_Flag == 3)
                {
                    TeamData.teamNationality[1] = TeamData.TEAM_NATIONALITY.JAPAN;
                }
                Application.LoadLevel("Tutorial");
            }
        }
	
	}
}
