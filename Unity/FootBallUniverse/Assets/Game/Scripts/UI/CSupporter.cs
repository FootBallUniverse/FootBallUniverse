using UnityEngine;
using System.Collections;

public class CSupporter : MonoBehaviour {

	public Vector3 m_pos;		// 自分の位置
	public TweenPosition m_tweenPos;
	public TweenAlpha m_tweenAlpha;

	// Use this for initialization
	void Awake () {
		// 現在の位置保存
		m_pos = this.transform.localPosition;

		m_tweenPos = this.transform.GetComponent<TweenPosition> ();
		m_tweenAlpha = this.transform.GetComponent<TweenAlpha> ();

		m_tweenPos.from = m_pos;
		m_tweenPos.to = new Vector3 (m_pos.x, m_pos.y + 30.0f, m_pos.z);

		m_tweenAlpha.from = 0;
		m_tweenAlpha.to = 1;


	}
	
	// Update is called once per frame
	void Update () {
		if (this.transform.GetComponent<TweenPosition> ().enabled == false && 
			this.transform.GetComponent<TweenAlpha> ().enabled == false) 
		{
			GameObject.Destroy (this.gameObject);
		}
	}

	public void Init()
	{
		// 移動の前の位置変更
		this.transform.localPosition = m_pos;
	}

	// サポーターの表示スタート
	public void StartSupporterDraw(string _str)
	{
		this.Init ();

		ChangeLabel(_str);

		m_tweenPos.Play (true);
		m_tweenAlpha.Play (true);
	
	}
	
	// ラベルの内容変更
	public void ChangeLabel(string _str)
	{
		this.GetComponent<UILabel> ().text = _str;
	}
}
