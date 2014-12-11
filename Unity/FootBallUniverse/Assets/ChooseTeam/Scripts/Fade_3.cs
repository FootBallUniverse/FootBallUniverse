using UnityEngine;
using System.Collections;

public class Fade_3 : MonoBehaviour
{

    public int m_FadeFlag;
    private float m_Count;
    Player_1_Script m_Team1;
    Player_3_Script m_Team3;

    UISprite m_Sprite;
    // TweenAlpha用スクリプト
    private TweenAlpha m_tweenAlpha;

    // Use this for initialization
    void Start()
    {
        m_FadeFlag = 0;
        m_Count = 0.0f;
        GameObject m_Player1 = GameObject.Find("Team1_2");
        GameObject m_Player3 = GameObject.Find("Team3_4");
        m_Team1 = m_Player1.transform.GetComponent<Player_1_Script>();
        m_Team3 = m_Player3.transform.GetComponent<Player_3_Script>();
        m_tweenAlpha = this.gameObject.GetComponent<TweenAlpha>();
        m_tweenAlpha.from = 1;
        m_tweenAlpha.to = 0;

        //        m_tweenAlpha.Play(true);
        TweenAlpha.Begin(this.gameObject, 2, 0);
        m_Sprite = this.gameObject.GetComponent<UISprite>();
    }

    // Update is called once per frame
    void Update()
    {

        if (m_Team1.m_Fade_flag_1.m_FadeFlag == 2 && m_Team3.m_Fade_flag_2.m_FadeFlag == 2)
        {
            m_Sprite.depth = 10;
            m_Count += Time.deltaTime;
            if (m_Count >= 2.0f)
            {
                TweenAlpha.Begin(this.gameObject, 0.5f, 1);
            }
        }
        else
        {
            m_Sprite.depth = 15;
        }
    }
}
