using UnityEngine;
using System.Collections;
public struct Guide
{
    public GameObject m_Guide;
    public TweenScale m_GuideScale;
    public UISprite m_Sprite;
    public Vector3 m_GuidePos;
    public TweenAlpha m_Alpha;
}
public class TutorialGuide : MonoBehaviour
{
    Guide[] m_Guide = new Guide[6];
    bool m_Control;
    bool[] m_PushStart = new bool[4];
    int m_PosFlag;
    Vector3 m_BasePos;
    GameObject m_FadeObject;
    FadeOut m_Fade;
    // Use this for initialization
    void Start()
    {
        m_Control = false;
        m_PosFlag = 0;
        for (int i = 0; i < 4; i++)
        {
            m_PushStart[i] = false;
        }
        m_BasePos = new Vector3(60.0f, 0.0f, 0.0f);
        m_Guide[0].m_Guide = this.transform.FindChild("Guide1").gameObject;
        m_Guide[1].m_Guide = this.transform.FindChild("Guide2").gameObject;
        m_Guide[2].m_Guide = this.transform.FindChild("Guide3").gameObject;
        m_Guide[3].m_Guide = this.transform.FindChild("Guide4").gameObject;
        m_Guide[4].m_Guide = this.transform.FindChild("Guide5").gameObject;
        m_Guide[5].m_Guide = this.transform.FindChild("Guide6").gameObject;
        m_FadeObject = this.transform.FindChild("Fade").gameObject;
        for (int i = 0; i < 6; i++)
        {
            m_Guide[i].m_GuideScale = m_Guide[i].m_Guide.gameObject.GetComponent<TweenScale>();
            m_Guide[i].m_Alpha = m_Guide[i].m_Guide.gameObject.GetComponent<TweenAlpha>();
            m_Guide[i].m_GuidePos = m_Guide[i].m_Guide.transform.position;
            m_Guide[i].m_Sprite = m_Guide[i].m_Guide.gameObject.GetComponent<UISprite>();
        }
        m_Fade = m_FadeObject.GetComponent<FadeOut>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (m_PosFlag)
        {
            case 0:
                m_Guide[0].m_GuidePos.x = 0.0f + m_BasePos.x;
                m_Guide[0].m_Sprite.depth = 6;
                m_Guide[0].m_GuideScale.scale = new Vector3(3.375f, 6.0f, 1.0f);
                m_Guide[1].m_GuidePos.x = 0.5f + m_BasePos.x;
                m_Guide[1].m_Sprite.depth = 5;
                m_Guide[1].m_GuideScale.scale = new Vector3(3.09375f, 5.5f, 1.0f);
                m_Guide[2].m_GuidePos.x = 1.0f + m_BasePos.x;
                m_Guide[2].m_Sprite.depth = 4;
                m_Guide[2].m_GuideScale.scale = new Vector3(3.09375f, 5.5f, 1.0f);
                m_Guide[3].m_GuidePos.x = 1.5f + m_BasePos.x;
                m_Guide[3].m_Sprite.depth = 3;
                m_Guide[3].m_GuideScale.scale = new Vector3(3.09375f, 5.5f, 1.0f);
                m_Guide[4].m_GuidePos.x = 2.0f + m_BasePos.x;
                m_Guide[4].m_Sprite.depth = 2;
                m_Guide[4].m_GuideScale.scale = new Vector3(3.09375f, 5.5f, 1.0f);
                m_Guide[5].m_GuidePos.x = 2.5f + m_BasePos.x;
                m_Guide[5].m_Sprite.depth = 1;
                m_Guide[5].m_GuideScale.scale = new Vector3(3.09375f, 5.5f, 1.0f);
                break;
            case 1:
                m_Guide[0].m_GuidePos.x = -0.5f + m_BasePos.x;
                m_Guide[0].m_Sprite.depth = 5;
                m_Guide[0].m_GuideScale.scale = new Vector3(3.09375f, 5.5f, 1.0f);
                m_Guide[1].m_GuidePos.x = 0.0f + m_BasePos.x;
                m_Guide[1].m_Sprite.depth = 6;
                m_Guide[1].m_GuideScale.scale = new Vector3(3.375f, 6.0f, 1.0f);
                m_Guide[2].m_GuidePos.x = 0.5f + m_BasePos.x;
                m_Guide[2].m_Sprite.depth = 5;
                m_Guide[2].m_GuideScale.scale = new Vector3(3.09375f, 5.5f, 1.0f);
                m_Guide[3].m_GuidePos.x = 1.0f + m_BasePos.x;
                m_Guide[3].m_Sprite.depth = 4;
                m_Guide[3].m_GuideScale.scale = new Vector3(3.09375f, 5.5f, 1.0f);
                m_Guide[4].m_GuidePos.x = 1.5f + m_BasePos.x;
                m_Guide[4].m_Sprite.depth = 3;
                m_Guide[4].m_GuideScale.scale = new Vector3(3.09375f, 5.5f, 1.0f);
                m_Guide[5].m_GuidePos.x = 2.0f + m_BasePos.x;
                m_Guide[5].m_Sprite.depth = 2;
                m_Guide[5].m_GuideScale.scale = new Vector3(3.09375f, 5.5f, 1.0f);
                break;
            case 2:
                m_Guide[0].m_GuidePos.x = -1.0f + m_BasePos.x;
                m_Guide[0].m_Sprite.depth = 4;
                m_Guide[0].m_GuideScale.scale = new Vector3(3.09375f, 5.5f, 1.0f);
                m_Guide[1].m_GuidePos.x = -0.5f + m_BasePos.x;
                m_Guide[1].m_Sprite.depth = 5;
                m_Guide[1].m_GuideScale.scale = new Vector3(3.09375f, 5.5f, 1.0f);
                m_Guide[2].m_GuidePos.x = 0.0f + m_BasePos.x;
                m_Guide[2].m_Sprite.depth = 6;
                m_Guide[2].m_GuideScale.scale = new Vector3(3.375f, 6.0f, 1.0f);
                m_Guide[3].m_GuidePos.x = 0.5f + m_BasePos.x;
                m_Guide[3].m_Sprite.depth = 5;
                m_Guide[3].m_GuideScale.scale = new Vector3(3.09375f, 5.5f, 1.0f);
                m_Guide[4].m_GuidePos.x = 1.0f + m_BasePos.x;
                m_Guide[4].m_Sprite.depth = 4;
                m_Guide[4].m_GuideScale.scale = new Vector3(3.09375f, 5.5f, 1.0f);
                m_Guide[5].m_GuidePos.x = 1.5f + m_BasePos.x;
                m_Guide[5].m_Sprite.depth = 3;
                m_Guide[5].m_GuideScale.scale = new Vector3(3.09375f, 5.5f, 1.0f);
                break;
            case 3:
                m_Guide[0].m_GuidePos.x = -1.5f + m_BasePos.x;
                m_Guide[0].m_Sprite.depth = 3;
                m_Guide[0].m_GuideScale.scale = new Vector3(3.09375f, 5.5f, 1.0f);
                m_Guide[1].m_GuidePos.x = -1.0f + m_BasePos.x;
                m_Guide[1].m_Sprite.depth = 4;
                m_Guide[1].m_GuideScale.scale = new Vector3(3.09375f, 5.5f, 1.0f);
                m_Guide[2].m_GuidePos.x = -0.5f + m_BasePos.x;
                m_Guide[2].m_Sprite.depth = 5;
                m_Guide[2].m_GuideScale.scale = new Vector3(3.09375f, 5.5f, 1.0f);
                m_Guide[3].m_GuidePos.x = 0.0f + m_BasePos.x;
                m_Guide[3].m_Sprite.depth = 6;
                m_Guide[3].m_GuideScale.scale = new Vector3(3.375f, 6.0f, 1.0f);
                m_Guide[4].m_GuidePos.x = 0.5f + m_BasePos.x;
                m_Guide[4].m_Sprite.depth = 5;
                m_Guide[4].m_GuideScale.scale = new Vector3(3.09375f, 5.5f, 1.0f);
                m_Guide[5].m_GuidePos.x = 1.0f + m_BasePos.x;
                m_Guide[5].m_Sprite.depth = 4;
                m_Guide[5].m_GuideScale.scale = new Vector3(3.09375f, 5.5f, 1.0f);
                break;
            case 4:
                m_Guide[0].m_GuidePos.x = -2.0f + m_BasePos.x;
                m_Guide[0].m_Sprite.depth = 2;
                m_Guide[0].m_GuideScale.scale = new Vector3(3.09375f, 5.5f, 1.0f);
                m_Guide[1].m_GuidePos.x = -1.5f + m_BasePos.x;
                m_Guide[1].m_Sprite.depth = 3;
                m_Guide[1].m_GuideScale.scale = new Vector3(3.09375f, 5.5f, 1.0f);
                m_Guide[2].m_GuidePos.x = -1.0f + m_BasePos.x;
                m_Guide[2].m_Sprite.depth = 4;
                m_Guide[2].m_GuideScale.scale = new Vector3(3.09375f, 5.5f, 1.0f);
                m_Guide[3].m_GuidePos.x = -0.5f + m_BasePos.x;
                m_Guide[3].m_Sprite.depth = 5;
                m_Guide[3].m_GuideScale.scale = new Vector3(3.09375f, 5.5f, 1.0f);
                m_Guide[4].m_GuidePos.x = 0.0f + m_BasePos.x;
                m_Guide[4].m_Sprite.depth = 6;
                m_Guide[4].m_GuideScale.scale = new Vector3(3.375f, 6.0f, 1.0f);
                m_Guide[5].m_GuidePos.x = 0.5f + m_BasePos.x;
                m_Guide[5].m_Sprite.depth = 5;
                m_Guide[5].m_GuideScale.scale = new Vector3(3.09375f, 5.5f, 1.0f);
                break;
            case 5:
                m_Guide[0].m_GuidePos.x = -2.5f + m_BasePos.x;
                m_Guide[0].m_Sprite.depth = 1;
                m_Guide[0].m_GuideScale.scale = new Vector3(3.09375f, 5.5f, 1.0f);
                m_Guide[1].m_GuidePos.x = -2.0f + m_BasePos.x;
                m_Guide[1].m_Sprite.depth = 2;
                m_Guide[1].m_GuideScale.scale = new Vector3(3.09375f, 5.5f, 1.0f);
                m_Guide[2].m_GuidePos.x = -1.5f + m_BasePos.x;
                m_Guide[2].m_Sprite.depth = 3;
                m_Guide[2].m_GuideScale.scale = new Vector3(3.09375f, 5.5f, 1.0f);
                m_Guide[3].m_GuidePos.x = -1.0f + m_BasePos.x;
                m_Guide[3].m_Sprite.depth = 4;
                m_Guide[3].m_GuideScale.scale = new Vector3(3.09375f, 5.5f, 1.0f);
                m_Guide[4].m_GuidePos.x = -0.5f + m_BasePos.x;
                m_Guide[4].m_Sprite.depth = 5;
                m_Guide[4].m_GuideScale.scale = new Vector3(3.09375f, 5.5f, 1.0f);
                m_Guide[5].m_GuidePos.x = 0.0f + m_BasePos.x;
                m_Guide[5].m_Sprite.depth = 6;
                m_Guide[5].m_GuideScale.scale = new Vector3(3.375f, 6.0f, 1.0f);
                break;
        }
        for (int i = 0; i < 6; i++)
        {
            if (m_Guide[i].m_Sprite.depth == 6)
            {
                m_Guide[i].m_Alpha.alpha = 1.0f;
            }
            else
            {
                m_Guide[i].m_Alpha.alpha = 0.2f;
            }
        }

        if (m_PushStart[0] == true
            && m_PushStart[1] == true
            && m_PushStart[2] == true
            && m_PushStart[3] == true)
        {
            m_PushStart[0] = false;
            m_PushStart[1] = false;
            m_PushStart[2] = false;
            m_PushStart[3] = false;
            if (m_PosFlag < 5)
            {
                m_PosFlag += 1;
            }
            //else if(m_PosFlag >= 5)
            //{
            //    m_Fade.m_FadeFlag = true;
            //}
        }
        // P1がStartボタンを押した
        if (Input.GetKeyDown(InputXBOX360.P1_XBOX_START))
        {
            m_PushStart[0] = true;
        }
        // P2がStartボタンを押した
        if (Input.GetKeyDown(InputXBOX360.P2_XBOX_START))
        {
            m_PushStart[1] = true;
        }
        // P3がStartボタンを押した
        if (Input.GetKeyDown(InputXBOX360.P3_XBOX_START))
        {
            m_PushStart[2] = true;
        }
        // P4がStartボタンを押した
        if (Input.GetKeyDown(InputXBOX360.P4_XBOX_START))
        {
            m_PushStart[3] = true;
        }
        
        for (int i = 0; i < 6; i++)
        {
            m_Guide[i].m_Guide.transform.position = m_Guide[i].m_GuidePos;
        }
    }
}
