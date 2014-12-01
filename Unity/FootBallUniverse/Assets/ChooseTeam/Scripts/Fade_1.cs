using UnityEngine;
using System.Collections;

public class Fade_1 : MonoBehaviour
{
    public int m_FadeFlag;

    // TweenAlpha用スクリプト
    public TweenAlpha m_tweenAlpha;

    // Use this for initialization
    void Start()
    {
        m_FadeFlag = 0;

        m_tweenAlpha = this.gameObject.GetComponent<TweenAlpha>();
        m_tweenAlpha.from = 0;
        m_tweenAlpha.to = 0;

         TweenAlpha.Begin(this.gameObject, 1, 0);

    }

    // Update is called once per frame
    void Update()
    {
        if (m_FadeFlag == 0)
        {
            // 何もしない
            // 絶対ニダ
        }
        if (m_FadeFlag == 1)
        {
            this.transform.GetComponent<UISprite>().alpha = 0.75f;
        }

        if (m_FadeFlag == 2)
        {
            if (m_tweenAlpha.enabled == false)
            {
                this.transform.GetComponent<UISprite>().alpha = 0.75f;
            }
        }

        if (m_FadeFlag == 3)
        {
            if (m_tweenAlpha.enabled == false)
            {
                this.transform.GetComponent<UISprite>().alpha = 0.0f;
                m_FadeFlag = 0;
            }
        }
    }
}
