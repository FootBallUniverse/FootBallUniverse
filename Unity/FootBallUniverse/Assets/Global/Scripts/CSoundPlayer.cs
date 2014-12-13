using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CSoundPlayer
{
    public const float FADE_SPEED = 0.01f;

    private GameObject m_bgmPlayerObject;  
    private AudioSource m_bgmAudioSource;
    private GameObject m_sePlayerObject;
    private AudioSource m_seAudioSource;
    private CSoundFade m_fade;


    //----------------------------------------------------------------------
    // コンストラクタ
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/10/15  @Update 2014/10/15  @Author T.Kawashita      
    //----------------------------------------------------------------------
    public CSoundPlayer()
    {
        // BGMオブジェクトの作成
        m_bgmPlayerObject = new GameObject("BGMObject");
        m_bgmAudioSource = m_bgmPlayerObject.AddComponent<AudioSource>();

        // フェードの設定
        m_fade = m_bgmPlayerObject.AddComponent<CSoundFade>();
        m_fade.SetAudioSource(m_bgmAudioSource);

        // SEオブジェクトの作成
        m_sePlayerObject = new GameObject("SEObject");
        m_seAudioSource = m_sePlayerObject.AddComponent<AudioSource>();
    }

    //----------------------------------------------------------------------
    // シングルトン実装
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/10/15  @Update 2014/10/15  @Author T.Kawashita      
    //----------------------------------------------------------------------
    private static CSoundPlayer m_soundPlayer; 
    public static CSoundPlayer GetInstance()
    {
        if( m_soundPlayer == null )
        {
            m_soundPlayer = new CSoundPlayer();
        }

        return m_soundPlayer;
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

    //----------------------------------------------------------------------
    // BGMの停止
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	bool    成功か失敗
    // @Date	2014/12/14  @Update 2014/12/14  @Author 2014/12/14      
    //----------------------------------------------------------------------
    public bool StopBGM()
    {
        m_bgmAudioSource.Stop();
        return true;
    }


    //----------------------------------------------------------------------
    // BGMの再生
    //----------------------------------------------------------------------
    // @Param	string 鳴らしたいBGMファイルの名前		
    // @Return	成功か失敗
    // @Date	2014/10/15  @Update 2014/10/15  @Author T.Kawashita      
    //----------------------------------------------------------------------
    public bool PlayBGM(string _audioName)
    {
        // 音楽データロード
        AudioClip audioClip = (AudioClip)Resources.Load("Sound/BGM/" + _audioName);
        m_bgmAudioSource.clip = audioClip;

        // BGM再生
        m_bgmAudioSource.loop = true;
        m_bgmAudioSource.Play();

        return true;
    }

    //----------------------------------------------------------------------
    // 音量の調整
    //----------------------------------------------------------------------
    // @Param	float 調整したい量		
    // @Return	none
    // @Date	2014/10/15  @Update 2014/10/15  @Author T.Kawashita      
    //----------------------------------------------------------------------
    public void ChangeVolume(float _volume)
    {
        m_bgmAudioSource.volume = _volume;
    }

	//----------------------------------------------------------------------
	// 音量の調整
	//----------------------------------------------------------------------
	// @Param	float 調整したい量		
	// @Return	none
	// @Date	2014/10/15  @Update 2014/10/15  @Author T.Kawashita      
	//----------------------------------------------------------------------
	public void ChangeSEVolume(float _volume)
	{
		m_seAudioSource.volume = _volume;
	}

	//----------------------------------------------------------------------
    // 一時停止
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/10/15  @Update 2014/10/15  @Author T.Kawashita      
    //----------------------------------------------------------------------
    public void Pause()
    {
        m_bgmAudioSource.Pause();
    }

    //----------------------------------------------------------------------
    // フェードイン再生
    //----------------------------------------------------------------------
    // @Param	string 再生したいBGMの名前 float フェードインのスピード		
    // @Return	none
    // @Date	2014/10/15  @Update 2014/10/15  @Author T.Kawashita      
    //----------------------------------------------------------------------
    public void PlayBGMFadeIn(string _audioName ,float _fadeSpeed)
    {
        this.PlayBGM(_audioName);
        m_fade.FadeIn(_fadeSpeed);
    }

    //----------------------------------------------------------------------
    // フェードアウト停止
    //----------------------------------------------------------------------
    // @Param	float フェードアウトのスピード		
    // @Return	none
    // @Date	2014/10/15  @Update 2014/10/15  @Author T.Kawashita      
    //----------------------------------------------------------------------
    public void PlayBGMFadeOut(float _fadeSpeed)
    {
        m_fade.FadeOut(_fadeSpeed);
    }
}
