using UnityEngine;
using System.Collections;
public struct Guide4
{
    public GameObject m_Guide;
    public TweenScale m_GuideScale;
    public UISprite m_Sprite;
    public Vector3 m_GuidePos;
    public TweenAlpha m_Alpha;
}
public class GuideSelect4 : MonoBehaviour
{
    Guide4[] m_Guide = new Guide4[6];
    bool m_RightControl;
    bool m_LeftControl;
    int m_PosFlag;
    Vector3 m_BasePos;

	// Use this for initialization
	void Start () 
    {
        m_RightControl = false;
        m_LeftControl = false;
        m_PosFlag = 0;
        m_BasePos = new Vector3(80.0f, 0.0f, 0.0f);
        m_Guide[0].m_Guide = this.transform.FindChild("Guide1").gameObject;
        m_Guide[1].m_Guide = this.transform.FindChild("Guide2").gameObject;
        m_Guide[2].m_Guide = this.transform.FindChild("Guide3").gameObject;
        m_Guide[3].m_Guide = this.transform.FindChild("Guide4").gameObject;
        m_Guide[4].m_Guide = this.transform.FindChild("Guide5").gameObject;
        m_Guide[5].m_Guide = this.transform.FindChild("Guide6").gameObject;
        for (int i = 0; i < 6; i++)
        {
            m_Guide[i].m_GuideScale = m_Guide[i].m_Guide.gameObject.GetComponent<TweenScale>();
            m_Guide[i].m_Alpha = m_Guide[i].m_Guide.gameObject.GetComponent<TweenAlpha>();
            m_Guide[i].m_GuidePos = m_Guide[i].m_Guide.transform.position;
            m_Guide[i].m_Sprite = m_Guide[i].m_Guide.gameObject.GetComponent<UISprite>();
        }
	}

    // Update is called once per frame
    void Update()
    {
        switch (m_PosFlag)
        {
            case 0:
                if (m_Guide[0].m_GuidePos.x >= -1.0f)
                {
                    for (int i = 5; i > -1; i--)
                    {
                        m_Guide[i].m_GuidePos.x -= 0.1f;
                    }
                }
                else
                {
                    m_RightControl = false;
                }
                m_Guide[0].m_Sprite.depth = 6;
                m_Guide[0].m_GuideScale.scale = new Vector3(3.375f, 6.0f, 1.0f);

                m_Guide[1].m_Sprite.depth = 5;
                m_Guide[1].m_GuideScale.scale = new Vector3(3.09375f, 5.5f, 1.0f);

                m_Guide[2].m_Sprite.depth = 4;
                m_Guide[2].m_GuideScale.scale = new Vector3(3.09375f, 5.5f, 1.0f);

                m_Guide[3].m_Sprite.depth = 3;
                m_Guide[3].m_GuideScale.scale = new Vector3(3.09375f, 5.5f, 1.0f);

                m_Guide[4].m_Sprite.depth = 2;
                m_Guide[4].m_GuideScale.scale = new Vector3(3.09375f, 5.5f, 1.0f);

                m_Guide[5].m_Sprite.depth = 1;
                m_Guide[5].m_GuideScale.scale = new Vector3(3.09375f, 5.5f, 1.0f);
                break;
            case 1:
                if (m_Guide[0].m_GuidePos.x >= -2.0f)
                {
                    for (int i = 5; i > -1; i--)
                    {
                        m_Guide[i].m_GuidePos.x -= 0.1f;
                    }
                }
                else
                {
                    m_RightControl = false;
                }

                m_Guide[0].m_Sprite.depth = 5;
                m_Guide[0].m_GuideScale.scale = new Vector3(3.09375f, 5.5f, 1.0f);

                m_Guide[1].m_Sprite.depth = 6;
                m_Guide[1].m_GuideScale.scale = new Vector3(3.375f, 6.0f, 1.0f);

                m_Guide[2].m_Sprite.depth = 5;
                m_Guide[2].m_GuideScale.scale = new Vector3(3.09375f, 5.5f, 1.0f);

                m_Guide[3].m_Sprite.depth = 4;
                m_Guide[3].m_GuideScale.scale = new Vector3(3.09375f, 5.5f, 1.0f);

                m_Guide[4].m_Sprite.depth = 3;
                m_Guide[4].m_GuideScale.scale = new Vector3(3.09375f, 5.5f, 1.0f);

                m_Guide[5].m_Sprite.depth = 2;
                m_Guide[5].m_GuideScale.scale = new Vector3(3.09375f, 5.5f, 1.0f);
                break;

            case 2:
                if (m_Guide[0].m_GuidePos.x >= -3.0f)
                {
                    for (int i = 5; i > -1; i--)
                    {
                        m_Guide[i].m_GuidePos.x -= 0.1f;
                    }
                }
                else
                {
                    m_RightControl = false;
                }

                m_Guide[0].m_Sprite.depth = 4;
                m_Guide[0].m_GuideScale.scale = new Vector3(3.09375f, 5.5f, 1.0f);

                m_Guide[1].m_Sprite.depth = 5;
                m_Guide[1].m_GuideScale.scale = new Vector3(3.09375f, 5.5f, 1.0f);

                m_Guide[2].m_Sprite.depth = 6;
                m_Guide[2].m_GuideScale.scale = new Vector3(3.375f, 6.0f, 1.0f);

                m_Guide[3].m_Sprite.depth = 5;
                m_Guide[3].m_GuideScale.scale = new Vector3(3.09375f, 5.5f, 1.0f);

                m_Guide[4].m_Sprite.depth = 4;
                m_Guide[4].m_GuideScale.scale = new Vector3(3.09375f, 5.5f, 1.0f);

                m_Guide[5].m_Sprite.depth = 3;
                m_Guide[5].m_GuideScale.scale = new Vector3(3.09375f, 5.5f, 1.0f);
                break;

            case 3:
                if (m_Guide[0].m_GuidePos.x >= -4.0f)
                {
                    for (int i = 5; i > -1; i--)
                    {
                        m_Guide[i].m_GuidePos.x -= 0.1f;
                    }
                }
                else
                {
                    m_RightControl = false;
                }

                m_Guide[0].m_Sprite.depth = 3;
                m_Guide[0].m_GuideScale.scale = new Vector3(3.09375f, 5.5f, 1.0f);

                m_Guide[1].m_Sprite.depth = 4;
                m_Guide[1].m_GuideScale.scale = new Vector3(3.09375f, 5.5f, 1.0f);

                m_Guide[2].m_Sprite.depth = 5;
                m_Guide[2].m_GuideScale.scale = new Vector3(3.09375f, 5.5f, 1.0f);

                m_Guide[3].m_Sprite.depth = 6;
                m_Guide[3].m_GuideScale.scale = new Vector3(3.375f, 6.0f, 1.0f);

                m_Guide[4].m_Sprite.depth = 5;
                m_Guide[4].m_GuideScale.scale = new Vector3(3.09375f, 5.5f, 1.0f);

                m_Guide[5].m_Sprite.depth = 4;
                m_Guide[5].m_GuideScale.scale = new Vector3(3.09375f, 5.5f, 1.0f);
                break;

            case 4:
                if (m_Guide[0].m_GuidePos.x >= -5.0f)
                {
                    for (int i = 5; i > -1; i--)
                    {
                        m_Guide[i].m_GuidePos.x -= 0.1f;
                    }
                }
                else
                {
                    m_RightControl = false;
                }
                m_Guide[0].m_Sprite.depth = 2;
                m_Guide[0].m_GuideScale.scale = new Vector3(3.09375f, 5.5f, 1.0f);

                m_Guide[1].m_Sprite.depth = 3;
                m_Guide[1].m_GuideScale.scale = new Vector3(3.09375f, 5.5f, 1.0f);

                m_Guide[2].m_Sprite.depth = 4;
                m_Guide[2].m_GuideScale.scale = new Vector3(3.09375f, 5.5f, 1.0f);

                m_Guide[3].m_Sprite.depth = 5;
                m_Guide[3].m_GuideScale.scale = new Vector3(3.09375f, 5.5f, 1.0f);

                m_Guide[4].m_Sprite.depth = 6;
                m_Guide[4].m_GuideScale.scale = new Vector3(3.375f, 6.0f, 1.0f);

                m_Guide[5].m_Sprite.depth = 5;
                m_Guide[5].m_GuideScale.scale = new Vector3(3.09375f, 5.5f, 1.0f);
                break;

            case 5:
                m_Guide[0].m_Sprite.depth = 1;
                m_Guide[0].m_GuideScale.scale = new Vector3(3.09375f, 5.5f, 1.0f);

                m_Guide[1].m_Sprite.depth = 2;
                m_Guide[1].m_GuideScale.scale = new Vector3(3.09375f, 5.5f, 1.0f);

                m_Guide[2].m_Sprite.depth = 3;
                m_Guide[2].m_GuideScale.scale = new Vector3(3.09375f, 5.5f, 1.0f);

                m_Guide[3].m_Sprite.depth = 4;
                m_Guide[3].m_GuideScale.scale = new Vector3(3.09375f, 5.5f, 1.0f);

                m_Guide[4].m_Sprite.depth = 5;
                m_Guide[4].m_GuideScale.scale = new Vector3(3.09375f, 5.5f, 1.0f);

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
        // LSを右に倒した
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (m_PosFlag < 5)
            {
                m_PosFlag += 1;
            }
        }
        // LSを左に倒した
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (m_PosFlag > 0)
            {
                m_PosFlag -= 1;
            }
        }


        // LSを右に倒した
        if (Input.GetAxis(InputXBOX360.P4_XBOX_LEFT_ANALOG_X) >= 1.0f && m_RightControl == false)
        {
            if (m_PosFlag < 5)
            {
                m_PosFlag += 1;
                m_RightControl = true;
            }

        }
        // LSを左に倒した
        else if (Input.GetAxis(InputXBOX360.P4_XBOX_LEFT_ANALOG_X) <= -1.0f && m_RightControl == false)
        {
            if (m_PosFlag > 0)
            {
                m_PosFlag -= 1;
                m_RightControl = true;
            }
        }
       /* else if (Input.GetAxis(InputXBOX360.P4_XBOX_LEFT_ANALOG_X) >= -0.3f && Input.GetAxis(InputXBOX360.P4_XBOX_LEFT_ANALOG_X) <= 0.3f)
        {
            m_RightControl = false;
        }*/
        for (int i = 0; i < 6; i++)
        {
            m_Guide[i].m_Guide.transform.position = m_Guide[i].m_GuidePos;
        }
    }
}
