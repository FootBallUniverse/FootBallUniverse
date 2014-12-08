using UnityEngine;
using System.Collections;

public class CFadeIn : MonoBehaviour {

    private TweenAlpha m_tweenAlpha;

	// Use this for initialization
	void Start () {
        // TweenAlphaのスクリプトを取得しておく
        m_tweenAlpha = this.GetComponent<TweenAlpha>();
	    
        // フェードイン用の状態をセット
        m_tweenAlpha.delay = 0;
        m_tweenAlpha.duration = 3.0f;
        m_tweenAlpha.from = 1;
        m_tweenAlpha.to = 0;

        m_tweenAlpha.Play(true);    
	}
	
	// Update is called once per frame
	void Update () {

        // フェードインがおわったら自分自身を削除
        if (m_tweenAlpha.enabled == false)
        {
            GameObject.Destroy(this.gameObject);
        }

	}
}
