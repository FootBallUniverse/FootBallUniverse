using UnityEngine;
using System.Collections;

public class Flag_Scale2 : MonoBehaviour
{
    TweenScale m_TweenScale;
    Vector3 m_Position;
    Vector3 m_MaxScale;
    Vector3 m_MinScale;

    // Use this for initialization
    void Start()
    {
        m_Position = this.gameObject.transform.position;
        m_TweenScale = this.gameObject.GetComponent<TweenScale>();
        m_MaxScale = new Vector3(0.18f, 0.14f, 1.0f);
        m_MinScale = new Vector3(0.11f, 0.09f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.transform.position.x >= 2.5f && this.gameObject.transform.position.x <= 4.01f)
        {
            TweenScale.Begin(this.gameObject, 0.1f, m_MaxScale);
        }
        else
        {
            TweenScale.Begin(this.gameObject, 0.2f, m_MinScale);
        }
    }
}