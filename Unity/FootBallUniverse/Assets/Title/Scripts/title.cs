using UnityEngine;
using System.Collections;

public class title : MonoBehaviour {
    
    private int m_StartCount;

    public Entry_1 m_entry1;
    public Entry_2 m_entry2;
    public Entry_3 m_entry3;
    public Entry_4 m_entry4;

	// Use this for initialization
    void Start()
    {
        m_StartCount = 0;

        GameObject entry1 = this.transform.FindChild("entry_wait_state_1").gameObject;
        GameObject entry2 = this.transform.FindChild("entry_wait_state_2").gameObject;
        GameObject entry3 = this.transform.FindChild("entry_wait_state_3").gameObject;
        GameObject entry4 = this.transform.FindChild("entry_wait_state_4").gameObject;

        m_entry1 = entry1.GetComponent<Entry_1>();
        m_entry2 = entry2.GetComponent<Entry_2>();
        m_entry3 = entry3.GetComponent<Entry_3>();
        m_entry4 = entry4.GetComponent<Entry_4>();

    }

	// Update is called once per frame
	void Update () 
    {

        if (m_entry1.m_inFlag == true &&
            m_entry2.m_inFlag == true &&
            m_entry3.m_inFlag == true &&
            m_entry4.m_inFlag == true)
        {
            if (m_StartCount >= 30)
            {
                Application.LoadLevel("ChooseTeam");
            }
            m_StartCount++;
        }
        else
        {
            m_StartCount = 0;
        }
       
        // スペースキーで強制的に次のシーンに飛ばす
		if(Input.GetKeyDown(KeyCode.Space) ||
           InputXBOX360.IsGetAllStartButton() == true)
		{
			Application.LoadLevel("ChooseTeam");
		}
	}

}
