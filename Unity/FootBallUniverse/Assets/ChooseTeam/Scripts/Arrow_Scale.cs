using UnityEngine;
using System.Collections;
struct ArrowObject
{
    public TweenScale m_TweenScale;
    public GameObject m_Position;
    public Vector3 m_MaxScale;
    public Vector3 m_MinScale;
    public Vector4 m_NowScale;
}

public class Arrow_Scale : MonoBehaviour
{
    ArrowObject[] m_Arrow = new ArrowObject[2];
    bool m_ScaleFlag;
    // Use this for initialization
    void Start()
    {
        m_Arrow[0].m_Position = this.gameObject.transform.FindChild("Arrow1").gameObject;
        m_Arrow[1].m_Position = this.gameObject.transform.FindChild("Arrow2").gameObject;
        for (int i = 0; i < 2; i++)
        {
            m_Arrow[i].m_NowScale = m_Arrow[i].m_Position.transform.localScale;
            m_Arrow[i].m_TweenScale = m_Arrow[i].m_Position.GetComponent<TweenScale>();
            m_Arrow[i].m_MaxScale = new Vector3(0.04f, 0.1f, 1.0f);
            m_Arrow[i].m_MinScale = new Vector3(0.02f, 0.05f, 1.0f);
        }
        m_ScaleFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        m_Arrow[0].m_NowScale = m_Arrow[0].m_Position.transform.localScale;
        m_Arrow[1].m_NowScale = m_Arrow[1].m_Position.transform.localScale;

        if (m_Arrow[0].m_NowScale.x <= 0.025f && m_ScaleFlag == false)
        {
            TweenScale.Begin(m_Arrow[0].m_Position, 0.0f, m_Arrow[0].m_MaxScale);
            TweenScale.Begin(m_Arrow[1].m_Position, 0.0f, m_Arrow[1].m_MaxScale);
            m_ScaleFlag = true;
        }
        else if (m_Arrow[0].m_NowScale.x >= 0.039f && m_ScaleFlag == true)
        {
            TweenScale.Begin(m_Arrow[0].m_Position, 0.8f, m_Arrow[0].m_MinScale);
            TweenScale.Begin(m_Arrow[1].m_Position, 0.8f, m_Arrow[1].m_MinScale);
            m_ScaleFlag = false;
        }
    }
}
