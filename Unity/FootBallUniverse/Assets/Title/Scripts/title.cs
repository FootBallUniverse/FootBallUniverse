using UnityEngine;
using System.Collections;

public class title : MonoBehaviour {

    public int m_StartCount;

    private Entry_1     m_entry1;
    private Entry_2     m_entry2;
    private Entry_3     m_entry3;
    private Entry_4     m_entry4;
    private Title_Fade  m_Fade;
    public  bool        m_SceneFlag;
	// Use this for initialization
    void Start()
    {
        m_StartCount = 0;
        GameObject entry1 = this.transform.FindChild("entry_wait_state_1").gameObject;
        GameObject entry2 = this.transform.FindChild("entry_wait_state_2").gameObject;
        GameObject entry3 = this.transform.FindChild("entry_wait_state_3").gameObject;
        GameObject entry4 = this.transform.FindChild("entry_wait_state_4").gameObject;
        GameObject Fade   = this.transform.FindChild("Title_Fade").gameObject;
        m_entry1 = entry1.GetComponent<Entry_1>();
        m_entry2 = entry2.GetComponent<Entry_2>();
        m_entry3 = entry3.GetComponent<Entry_3>();
        m_entry4 = entry4.GetComponent<Entry_4>();
        m_Fade   = Fade.GetComponent<Title_Fade>();
       

    }

	// Update is called once per frame
	void Update () 
    {
        if (m_entry1.m_inFlag == true &&
            m_entry2.m_inFlag == true &&
            m_entry3.m_inFlag == true &&
            m_entry4.m_inFlag == true)
        {
            m_SceneFlag = true;
        }
        else
        {
            m_SceneFlag = false;
            m_StartCount = 0;
        }
        if (m_SceneFlag == true)
        {
            if (m_StartCount >= 200)
            {
                Application.LoadLevel("ChooseTeam");
            }
            else if (m_StartCount >= 100)
            {
                m_Fade.m_FadeFlag = true;
                
            }
            m_StartCount++;
        }
       
        // スペースキーで強制的に次のシーンに飛ばす
		if(Input.GetKeyDown(KeyCode.Space) ||
           InputXBOX360.IsGetAllStartButton() == true)
		{
			Application.LoadLevel("ChooseTeam");
		}
	}

}
