using UnityEngine;
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

        this.transform.localPosition = new Vector3(0.0f, 90.0f, 0.0f);
        this.transform.localScale = new Vector3(200.0f, 230.0f, 230.0f);        

        m_tweenAlpha.from = 0.0f;
        m_tweenAlpha.to = 1.0f;
        m_tweenAlpha.duration = 0.25f;
        m_tweenAlpha.delay = 0.0f;  
        m_tweenAlpha.Play(true);

        m_tweenPosition.from = new Vector3(0.0f, 90.0f, 0.0f);
        m_tweenPosition.to = new Vector3(0.0f, 30.0f, 0.0f);
        m_tweenPosition.duration = 0.25f;
        m_tweenPosition.delay = 0.0f;
        m_tweenPosition.Play(true);
    }
	
	// Update is called once per frame
	void Update () {
        if (m_tweenPosition.enabled == false)
        {
            // フレームをデルタタイムで取得
            m_frame += Time.deltaTime;

            if (m_frame >= 0.75f && m_isTween == true)
                GameObject.Destroy(this.gameObject);

            // 0.5秒経ったら
            if (m_frame >= 0.5f && m_isTween == false)
            {
                m_tweenAlpha.delay = 0.0f;
                m_tweenAlpha.Play(false);
                m_isTween = true;

                m_tweenPosition.from = new Vector3(0.0f, -30.0f, 0.0f);
                m_tweenPosition.to = new Vector3(0.0f, 30.0f, 0.0f);
                m_tweenPosition.delay = 0.0f;
                m_tweenPosition.Play(false);
            }
        }
	}   

}
