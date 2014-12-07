using UnityEngine;
using System.Collections;

public class Arrow_Scale : MonoBehaviour
{
    TweenScale m_TweenScale;
    Vector3 m_Position;
    Vector3 m_MaxScale;
    Vector3 m_MinScale;
    Vector4 m_NowScale;
    bool m_ScaleFlag;
    // Use this for initialization
    void Start()
    {
        m_Position = this.gameObject.transform.position;
        m_NowScale = this.gameObject.transform.localScale;
        m_TweenScale = this.gameObject.GetComponent<TweenScale>();
        m_MaxScale = new Vector3(0.04f, 0.1f, 1.0f);
        m_MinScale = new Vector3(0.02f, 0.05f, 1.0f);
        m_ScaleFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        m_NowScale = this.gameObject.transform.localScale;

        if (m_NowScale.x <= 0.021f && m_ScaleFlag == false)
        {
            TweenScale.Begin(this.gameObject, 0.0f, m_MaxScale);
            m_ScaleFlag = true;
        }
        else if (m_NowScale.x >= 0.039f && m_ScaleFlag == true)
        {
            TweenScale.Begin(this.gameObject, 0.8f, m_MinScale);
            m_ScaleFlag = false;
        }
    }
}
