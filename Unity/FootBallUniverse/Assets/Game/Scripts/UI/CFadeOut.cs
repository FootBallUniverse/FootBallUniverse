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
        m_tweenAlpha.from = 1;
        m_tweenAlpha.to = 0;

        m_tweenAlpha.Play(false);

	}
	
	// Update is called once per frame
	void Update () {
        // フェードアウトが終わったら自分自身を削除
        if (m_tweenAlpha.enabled == false)
        {
            GameObject.Destroy(this.GetComponent<CFadeOut>());
        }
	}
}
