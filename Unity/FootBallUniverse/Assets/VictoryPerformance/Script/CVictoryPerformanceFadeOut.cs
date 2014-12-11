using UnityEngine;
using System.Collections;

public class CVictoryPerformanceFadeOut : MonoBehaviour {

	private TweenAlpha m_tweenAlpha;

	// Use this for initialization
	void Start () {
		// TweenAlphaのスクリプトを取得しておく
		m_tweenAlpha = this.GetComponent<TweenAlpha>();

		// フェードアウト用の状態をセット
		m_tweenAlpha.delay = 0;
		m_tweenAlpha.duration = 1.0f;
		m_tweenAlpha.from = 0.8f;
		m_tweenAlpha.to = 0;
		
		m_tweenAlpha.Play(false);
	}
	
	// Update is called once per frame
	void Update () {

		// フェードインがおわったら自分自身を削除
		if (m_tweenAlpha.enabled == false)
		{
			GameObject.Destroy(this.GetComponent<CFadeOut>());
		}
	
	}
}
