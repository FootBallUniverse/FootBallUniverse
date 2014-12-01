using UnityEngine;
using System.Collections;

public class Count : MonoBehaviour {
    public struct COUNT
    {
        public bool m_AnimeFlag;
        public bool m_TimeFlag;
        public float m_FadeTime;                 // 経過時間測定
        public float m_FinishTime;                 // 経過時間測定
        public float m_AnimationTime;            // モーション経過時間
        public float m_FinishAnimation;            // モーション経過時間
    };
    COUNT[] m_Count = new COUNT[2];

	// Use this for initialization
	void Start () 
    {
        for (int i = 0; i < 2; i++)
        {
            m_Count[i].m_AnimeFlag          = false;
            m_Count[i].m_TimeFlag           = false;
            m_Count[i].m_AnimationTime      = 0.0f;
            m_Count[i].m_FadeTime           = 0.0f;
            m_Count[i].m_FinishAnimation    = 0.0f;
            m_Count[i].m_FinishTime         = 0.0f;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        for (int i = 0; i < 2; i++)
        {
            TimeCount(m_Count[i].m_FinishAnimation);
            TimeCount(m_Count[i].m_FinishTime);
        }
	}

    void TimeCount(float m_time)
    {
        
    }
}
