using UnityEngine;
using System.Collections;

public class Fade_3 : MonoBehaviour
{

    public int m_FadeFlag;
    Player_1_Script m_Team1;
    Player_3_Script m_Team3;
    // TweenAlpha用スクリプト
    private TweenAlpha m_tweenAlpha;

    // Use this for initialization
    void Start()
    {
        m_FadeFlag = 0;
        GameObject m_Player1 = GameObject.Find("Team1_2");
        GameObject m_Player3 = GameObject.Find("Team3_4");
        m_Team1 = m_Player1.transform.GetComponent<Player_1_Script>();
        m_Team3 = m_Player3.transform.GetComponent<Player_3_Script>();
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
        //if( Input.GetKeyDown(KeyCode.J) )
        if (m_Team1.m_Fade_flag_1.m_FadeFlag == 2 && m_Team3.m_Fade_flag_2.m_FadeFlag == 2)
        {
            TweenAlpha.Begin(this.gameObject, 0.5f, 1);
        }

       /* //if( Input.GetKeyDown(KeyCode.J) )
        if (m_FadeFlag == 2)
        {
            if (m_tweenAlpha.enabled == false)
            {
                // TweenAlpha.Begin(this.gameObject, 1, 1);
            }
        }

        if (m_FadeFlag == 3)
        {
            if (m_tweenAlpha.enabled == false)
            {
                this.transform.GetComponent<UISprite>().alpha = 0.0f;
                m_FadeFlag = 0;
            }
        }*/

    }
}
