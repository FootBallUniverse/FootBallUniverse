using UnityEngine;
using System.Collections;

public class CFadeOut : MonoBehaviour {

    private TweenAlpha m_tweenAlpha;

	// Use this for initialization
	void Start () {
        // TweenAlphaのスクリプトを取得しておく
        m_tweenAlpha = this.GetComponent<TweenAlpha>();

        // フェードアウト用の状態をセット
        m_tweenAlpha.delay = 0;
        m_tweenAlpha.duration = 1.5f;
        m_tweenAlpha.from = 0;
        m_tweenAlpha.to = 1;

        m_tweenAlpha.Play(true);
	}
	
	// Update is called once per frame
	void Update () {
	}
}
