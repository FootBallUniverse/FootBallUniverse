using UnityEngine;
using System.Collections;

public class Title_Fade : MonoBehaviour {


    // TweenAlpha用スクリプト
    private TweenAlpha m_tweenAlpha;

	// Use this for initialization
	void Start () {
        m_tweenAlpha = this.gameObject.GetComponent<TweenAlpha>();
        m_tweenAlpha.from = 1;
        m_tweenAlpha.to = 0;

//        m_tweenAlpha.Play(true);

        TweenAlpha.Begin(this.gameObject, 3, 0);

	}
	
	// Update is called once per frame
	void Update () {
        if( Input.GetKeyDown(KeyCode.J) )
        {
            if (m_tweenAlpha.enabled == false)
            {
                m_tweenAlpha.from = 0;
                m_tweenAlpha.to = 1;

                TweenAlpha.Begin(this.gameObject, 3, 1);  
            }
        }

	}
}
