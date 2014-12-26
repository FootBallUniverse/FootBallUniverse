using UnityEngine;
using System.Collections;

public class CResultFadeOut : MonoBehaviour {

	private TweenAlpha m_tweenAlpha;

    //----------------------------------------------------------------------
    // constructor
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/12/10  @Update 2014/12/10  @Author T.Kawashita      
    //----------------------------------------------------------------------
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

    //----------------------------------------------------------------------
    // 更新
    //----------------------------------------------------------------------
    // @Param   none			
    // @Return	none
    // @Date	2014/12/10  @Update 2014/12/10  @Author T.Kawashita      
    //----------------------------------------------------------------------
	void Update () {

		// フェードインがおわったら自分自身を削除
		if (m_tweenAlpha.enabled == false)
		{
			GameObject.Destroy(this.GetComponent<CFadeOut>());
		}
	
	}
}
