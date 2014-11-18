﻿using UnityEngine;
using System.Collections;

public class CKickOff : MonoBehaviour {

    private float m_frame;
    private bool m_isTween;
    private TweenAlpha m_tweenAlpha;
    private TweenScale m_tweenScale;

	// Use this for initialization
	void Start () {

        m_frame = 0.0f;
        m_isTween = false;
        m_tweenAlpha = this.GetComponent<TweenAlpha>();
        m_tweenScale = this.GetComponent<TweenScale>();

        this.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        this.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);

        m_tweenAlpha.from = 0.0f;
        m_tweenAlpha.to = 1.0f;
        m_tweenAlpha.duration = 0.25f;
        m_tweenAlpha.delay = 0.0f;
        m_tweenAlpha.Play(true);

        m_tweenScale.from = new Vector3(0.0f, 0.0f, 0.0f);
        m_tweenScale.to = new Vector3(1000.0f, 300.0f, 0.0f);
        m_tweenScale.duration = 1.0f;
        m_tweenScale.delay = 0.0f;
        m_tweenScale.Play(true);
	}
	
	// Update is called once per frame
	void Update () {

        if (m_tweenAlpha.enabled == false)
        {
            m_frame += Time.deltaTime;

            if (m_frame >= 0.75f && m_isTween == true)
                GameObject.Destroy(this.gameObject);

            if (m_frame >= 0.5f && m_isTween == false )
            {
                m_tweenAlpha.Play(false);
                m_isTween = true;
            }
        }
	}
}
