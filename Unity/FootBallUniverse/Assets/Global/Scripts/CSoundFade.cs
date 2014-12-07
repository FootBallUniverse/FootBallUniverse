using UnityEngine;
using System.Collections;

public class CSoundFade : MonoBehaviour {

    //----------------------------------------------------
    // フェードのステータス
    //----------------------------------------------------
    enum eSOUND_STATUS 
    {
        eFADEIN,
        eFADEOUT,
        eNONE,
    };

    private eSOUND_STATUS m_status;

    private const float FADE_SPEED = 0.001f;    // フェードの調整値

    // フェードスピード
    private float m_fadeSpeed;  // フェードのスピード
    private bool m_isFade;      // フェード中かどうか
    private AudioSource m_audioSource;


    //----------------------------------------------------------------------
    // Start関数より早く走る初期化関数
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/10/15  @Update 2014/10/15  @Author T.Kawashita      
    //----------------------------------------------------------------------
    void Awake () 
    {
        m_status = eSOUND_STATUS.eNONE;
        m_fadeSpeed = FADE_SPEED;
        m_isFade = false;
    }

    //----------------------------------------------------------------------
    // AudioSourceのセット
    //----------------------------------------------------------------------
    // @Param	AudioSource SoundPlayerから送られてきたaudiosource		
    // @Return	none
    // @Date	2014/10/15  @Update 2014/10/15  @Author T.Kawashita      
    //----------------------------------------------------------------------
    public void SetAudioSource(AudioSource _audioSource)
    {
        m_audioSource = _audioSource;
    }

    //----------------------------------------------------------------------
    // フェードインとフェードアウトのアップデート
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/10/15  @Update 2014/10/15  @Author T.Kawashita      
    //----------------------------------------------------------------------
	void Update () 
    {
        // フェードしているかどうか確認
        if( m_isFade == true )
        {
            switch (m_status)
            {
                // フェードイン中
                case eSOUND_STATUS.eFADEIN:
                    if (m_audioSource.volume <= 0.15f)
                    {
                        m_audioSource.volume = m_audioSource.volume + m_fadeSpeed;
                        if (m_audioSource.volume >= 0.15f)
                        {
                            // フェードイン終了
                            m_audioSource.volume = 0.15f;
                            m_isFade = false;
                            m_status = eSOUND_STATUS.eNONE;
                        }
                    }
                    break;


                // フェードアウト中
                case eSOUND_STATUS.eFADEOUT:
                    if (m_audioSource.volume >= 0.0f)
                    {
                        m_audioSource.volume = m_audioSource.volume - m_fadeSpeed;
                        if (m_audioSource.volume <= 0.0f)
                        {
                            // フェードアウト終了
                            m_audioSource.volume = 0.0f;
                            m_isFade = false;
                            m_status = eSOUND_STATUS.eNONE;
                        }
                    }
                    break;

                // 何もしない
                case eSOUND_STATUS.eNONE:
                    break;
            }
        }
	}

    //----------------------------------------------------------------------
    // フェードイン
    //----------------------------------------------------------------------
    // @Param	float フェードインのスピード　引数なしの場合はデフォルト		
    // @Return	none
    // @Date	2014/10/15  @Update 2014/10/15  @Author T.Kawashita      
    //----------------------------------------------------------------------
    public void FadeIn(float _fadeSpeed = FADE_SPEED)
    {
        m_status = eSOUND_STATUS.eFADEIN;
        m_audioSource.volume = 0.0f;        // Volumeをいったん0にする
        m_fadeSpeed = _fadeSpeed;
        m_isFade = true;
    }

    //----------------------------------------------------------------------
    // フェードアウト
    //----------------------------------------------------------------------
    // @Param	float フェードアウトのスピード　引数なしの場合はデフォルト		
    // @Return	none
    // @Date	2014/10/15  @Update 2014/10/15  @Author T.Kawashita      
    //----------------------------------------------------------------------
    public void FadeOut(float _fadeSpeed = FADE_SPEED)
    {
        m_status = eSOUND_STATUS.eFADEOUT;
        m_fadeSpeed = _fadeSpeed;
        m_isFade = true;
    }
}
