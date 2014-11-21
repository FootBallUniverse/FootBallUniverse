using UnityEngine;
using System.Collections;

public class Fade_1 : MonoBehaviour
{

    public int m_FadeFlag;

    // TweenAlpha用スクリプト
    private TweenAlpha m_tweenAlpha;

    // Use this for initialization
    void Start()
    {
        m_FadeFlag = 0;

        m_tweenAlpha = this.gameObject.GetComponent<TweenAlpha>();
        m_tweenAlpha.from = 1;
        m_tweenAlpha.to = 0;

        //        m_tweenAlpha.Play(true);

        TweenAlpha.Begin(this.gameObject, 1, 0);

    }

    // Update is called once per frame
    void Update()
    {
        if (m_FadeFlag == 0)
        {
            if (m_tweenAlpha.enabled == false)
            {
                m_tweenAlpha = this.gameObject.GetComponent<TweenAlpha>();
                m_tweenAlpha.from = 1;
                m_tweenAlpha.to = 0;

                //        m_tweenAlpha.Play(true);

                TweenAlpha.Begin(this.gameObject, 1, 0);
            }
        }
        //if( Input.GetKeyDown(KeyCode.J) )
        if (m_FadeFlag == 1)
        {
            if (m_tweenAlpha.enabled == false)
            {
                m_tweenAlpha.from = 0;
                m_tweenAlpha.to = 1;

                TweenAlpha.Begin(this.gameObject, 0.1f,0.5f);
            }
        }

        //if( Input.GetKeyDown(KeyCode.J) )
        if (m_FadeFlag == 2)
        {
            if (m_tweenAlpha.enabled == false)
            {
                m_tweenAlpha.from = 0.5f;
                m_tweenAlpha.to = 1;

                TweenAlpha.Begin(this.gameObject, 1, 1);
            }
        }

    }
}
