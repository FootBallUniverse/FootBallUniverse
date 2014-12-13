using UnityEngine;
using System.Collections;

public class FadeOut : MonoBehaviour
{

    private float m_FadeCount;
    private bool m_FadeFlag;
    // TweenAlpha用スクリプト
    private TweenAlpha m_tweenAlpha;

    // Use this for initialization
    void Start()
    {
        m_FadeFlag = false;
        m_FadeCount = 0;

        m_tweenAlpha = this.gameObject.GetComponent<TweenAlpha>();
        m_tweenAlpha.from = 1;
        m_tweenAlpha.to = 0;

        //        m_tweenAlpha.Play(true);

        TweenAlpha.Begin(this.gameObject, 1, 0);

    }

    // Update is called once per frame
    void Update()
    {
        /* if (m_FadeFlag == 0)
         {
             // 何もしない
             // 絶対ニダ
         }*/
        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_FadeFlag = true;
            TweenAlpha.Begin(this.gameObject, 1, 1);
        }
        if (m_FadeFlag == true)
        {
            m_FadeCount += Time.deltaTime;
            if (m_FadeCount >= 1.0f)
            {
                Application.LoadLevel("Tutorial");
            }
        }
    }
}