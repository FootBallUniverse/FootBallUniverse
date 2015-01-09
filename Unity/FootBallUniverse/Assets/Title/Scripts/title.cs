using UnityEngine;
using System.Collections;

public class title : MonoBehaviour {

    public float m_StartCount;

    public Entry_1     m_entry1;
    public Entry_2     m_entry2;
    public Entry_3     m_entry3;
    public Entry_4     m_entry4;
    public Title_Fade  m_Fade;
    public  bool        m_SceneFlag;

    public CSoundPlayer m_soundPlayer;
    public SEPlay m_SE;

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
       
        // 音楽用ゲームオブジェクト作成
        m_soundPlayer = new CSoundPlayer();
        m_SE = this.gameObject.GetComponent<SEPlay>();
        m_soundPlayer.PlayBGMFadeIn("title/bgm_01", 0.1f);
        m_SE.VolumeSE(0.1f);

		// スコアロード
		TeamData.Load();
    }

	// Update is called once per frame
	void Update () 
    {
        if (m_entry1.m_inFlag == true &&
            m_entry2.m_inFlag == true &&
            m_entry3.m_inFlag == true &&
            m_entry4.m_inFlag == true &&
            m_Fade.m_FadeFlag == false )
        {
            m_SceneFlag = true;
            m_soundPlayer.PlayBGMFadeOut(0.003f);    
        }

        if (m_StartCount >= 0.1f)
        {
            if (m_StartCount < 0.12f)
            {
                m_SE.VolumeSE(0.1f);
                m_SE.PlaySE("title/goTeamSelect");
            }
            if (m_Fade.m_tweenAlpha.enabled == false)
            {
                Application.LoadLevel("ChooseTeam");
            }
        }
        
        if (m_SceneFlag == true)
        {
            m_Fade.m_FadeFlag = true;
            m_StartCount += Time.deltaTime;
        }

        // スペースキーで強制的に次のシーンに飛ばす
		if(Input.GetKeyDown(KeyCode.Space))
		{
            m_SceneFlag = true;
		}
	}

}
