using UnityEngine;
using System.Collections;

public class title : MonoBehaviour {
    
    private int m_StartCount;

    public GameObject entry1;
    public GameObject entry2;
    public GameObject entry3;
    public GameObject entry4;

	// Use this for initialization
    void Start()
    {
        m_StartCount = 0;

        entry1 = this.transform.FindChild("entry_wait_state_1").gameObject;
        entry2 = this.transform.FindChild("entry_wait_state_2").gameObject;
        entry3 = this.transform.FindChild("entry_wait_state_3").gameObject;
        entry4 = this.transform.FindChild("entry_wait_state_4").gameObject;
    
    }

	// Update is called once per frame
	void Update () 
    {
       Entry_1 entry_1 = entry1.GetComponent<Entry_1>();
       Entry_2 entry_2 = entry2.GetComponent<Entry_2>();
       Entry_3 entry_3 = entry3.GetComponent<Entry_3>();
       Entry_4 entry_4 = entry4.GetComponent<Entry_4>();

        if (entry_1.m_inFlag == true &&
            entry_2.m_inFlag == true &&
            entry_3.m_inFlag == true &&
            entry_4.m_inFlag == true)
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
		if(Input.GetKeyDown(KeyCode.Space))
		{
			Application.LoadLevel("ChooseTeam");
		}
	}

    //----------------------------------------------------------------------
    // デバッグ用キー
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/10/29  @Update 2014/10/29  @Author T.Kawashita      
    //----------------------------------------------------------------------
    private void DebugKey()
    {
    }

}
