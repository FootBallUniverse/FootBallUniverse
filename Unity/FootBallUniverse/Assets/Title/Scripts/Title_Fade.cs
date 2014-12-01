﻿using UnityEngine;
using System.Collections;

public class Title_Fade : MonoBehaviour {

    public bool m_FadeFlag;

    // TweenAlpha用スクリプト
    public TweenAlpha m_tweenAlpha;

	// Use this for initialization
	void Start () {
        m_FadeFlag = false;

        m_tweenAlpha = this.gameObject.GetComponent<TweenAlpha>();
        m_tweenAlpha.from = 1;
        m_tweenAlpha.to = 0;

//        m_tweenAlpha.Play(true);

        TweenAlpha.Begin(this.gameObject, 1, 0);

	}
	
	// Update is called once per frame
	void Update () 
    {
        if(m_FadeFlag == true)
        {
            if (m_tweenAlpha.enabled == false)
            {
                m_tweenAlpha.from = 0;
                m_tweenAlpha.to = 1;

                TweenAlpha.Begin(this.gameObject, 1, 1);  
            }
        }

	}
}
