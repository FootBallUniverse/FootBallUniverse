﻿using UnityEngine;
using System.Collections;

public class CCountDown : MonoBehaviour {

    private float m_frame;
    private bool m_isTween;
    private TweenPosition m_tweenPosition;
    private TweenAlpha m_tweenAlpha;

	// Use this for initialization
	void Start () {
        m_frame = 0.0f;
        m_isTween = false;
        m_tweenPosition = this.GetComponent<TweenPosition>();
        m_tweenAlpha = this.GetComponent<TweenAlpha>();

        this.transform.localScale = new Vector3(150.0f, 180.0f, 150.0f);        

        m_tweenAlpha.from = 0.0f;
        m_tweenAlpha.to = 1.0f;
        m_tweenAlpha.duration = 0.25f;
        m_tweenAlpha.delay = 0.0f;  
        m_tweenAlpha.Play(true);

        m_tweenPosition.Play(true);
    }
	
	// Update is called once per frame
	void Update () {
        if (m_tweenPosition.enabled == false)
        {
            // フレームをデルタタイムで取得
            m_frame += Time.deltaTime;

            if (m_frame >= 0.75f && m_isTween == true)
			{
				GameObject.Destroy(this.gameObject);
			}
            // 0.25秒経ったら
            if (m_frame >= 0.25f && m_isTween == false)
            {
                m_tweenAlpha.delay = 0.0f;
                m_tweenAlpha.Play(false);
                m_isTween = true;

				CGameManager.m_soundPlayer.ChangeSEVolume(0.1f);
				CGameManager.m_soundPlayer.PlaySE("game/time_count");

//this.transform.Find("Player1").transform.GetChild("player").transform.GetComponent<CPlayer1>().m_playerSE.PlaySE("game/time_count");
				m_tweenPosition.from = new Vector3(this.transform.localPosition.x, -30.0f, 0.0f);
                m_tweenPosition.to = new Vector3(this.transform.localPosition.x, 30.0f, 0.0f);
                m_tweenPosition.delay = 0.0f;
                m_tweenPosition.Play(false);
            }
        }
	}   

}
