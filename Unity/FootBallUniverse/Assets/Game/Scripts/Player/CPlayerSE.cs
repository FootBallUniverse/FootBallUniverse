using UnityEngine;
using System.Collections;

public class CPlayerSE : MonoBehaviour {

    public GameObject m_sePlayerObject;
    public AudioSource m_seAudioSource;

	// Use this for initialization
	void Start () {
        // SEオブジェクトの作成
        m_sePlayerObject = new GameObject("SEObject");
        m_sePlayerObject.transform.parent = this.transform;
        m_seAudioSource = m_sePlayerObject.AddComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //----------------------------------------------------------------------
    // SEの再生
    //----------------------------------------------------------------------
    // @Param	string 鳴らしたいSEファイルの名前		
    // @Return	成功か失敗
    // @Date	2014/10/15  @Update 2014/10/15  @Author T.Kawashita      
    //----------------------------------------------------------------------
    public bool PlaySE(string _audioName)
    {
        // 音楽データロード
        AudioClip audioClip = (AudioClip)Resources.Load("Sound/SE/" + _audioName);

        // SE再生
        m_seAudioSource.PlayOneShot(audioClip);

        return true;
    }

    //----------------------------------------------------------------------
    // SEの停止
    //----------------------------------------------------------------------
    // @Param   none		
    // @Return	bool    成功か失敗
    // @Date	2014/12/6  @Update 2014/12/6  @Author T.Kawashita      
    //----------------------------------------------------------------------
    public bool StopSE()
    {
        m_seAudioSource.Stop();
        return true;
    }
}
