using UnityEngine;
using System.Collections;

public class TweenAlphaSprite : MonoBehaviour {
	public int alphaCount;
	public TWEEN_STATE state = TWEEN_STATE.FADE_OUT_END;
	int nowCount;

	public enum TWEEN_START_SATE
	{
		FADE_IN,
		FADE_OUT
	};

	public enum TWEEN_STATE
	{
		FADE_IN_END,
		FADE_OUT_END,
		FADE_IN_PLAYING,
		FADE_OUT_PLAYING,
		FADE_IN_SOTP,
		FADE_OUT_SOTP
	};

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		Color works = this.renderer.material.color;

		if (this.state == TWEEN_STATE.FADE_OUT_PLAYING || this.state == TWEEN_STATE.FADE_IN_PLAYING)
		{
			// カウント変動
			if (this.state == TWEEN_STATE.FADE_IN_PLAYING) this.nowCount++;
			if (this.state == TWEEN_STATE.FADE_OUT_PLAYING) this.nowCount--;
			// 最大までいっているなら終了
			if (this.nowCount >= this.alphaCount)
			{
				this.state = TWEEN_STATE.FADE_IN_END;
				this.nowCount = this.alphaCount;
			}
			if (this.nowCount <= 0)
			{
				this.state = TWEEN_STATE.FADE_OUT_END;
				this.nowCount = 0;
			}
			// 変化
			works.a = (float)this.nowCount / (float)this.alphaCount;
			this.renderer.material.color = works;
		}
		
	}

	public void SetCout(int newCount)
	{
		Color workColor = this.renderer.material.color;
		float workCount = this.nowCount;

		workCount /= this.alphaCount;
		if (nowCount < 0) return;
	}

	public void TweenStart(TWEEN_START_SATE startState)
	{
		if (startState == TWEEN_START_SATE.FADE_IN)  this.state = TWEEN_STATE.FADE_IN_PLAYING;
		if (startState == TWEEN_START_SATE.FADE_OUT) this.state = TWEEN_STATE.FADE_OUT_PLAYING;
	}

	public void TweenStop()
	{
		if (this.state == TWEEN_STATE.FADE_IN_PLAYING)  this.state  = TWEEN_STATE.FADE_IN_SOTP;
		if (this.state == TWEEN_STATE.FADE_OUT_PLAYING) this.state = TWEEN_STATE.FADE_OUT_SOTP;
	}

	public void SetColor(Color newColor)
	{
		switch (this.state)
		{
			case TWEEN_STATE.FADE_IN_END:
			case TWEEN_STATE.FADE_IN_PLAYING:
			case TWEEN_STATE.FADE_IN_SOTP:
				newColor.a = (this.nowCount / this.alphaCount);
				if (this.nowCount == 0) this.state = TWEEN_STATE.FADE_IN_END;
				break;
			case TWEEN_STATE.FADE_OUT_END:
			case TWEEN_STATE.FADE_OUT_PLAYING:
			case TWEEN_STATE.FADE_OUT_SOTP:
				newColor.a = 1.0f - (this.nowCount / this.alphaCount);
				break;
		}

		this.renderer.material.color = newColor;
	}

	public TWEEN_STATE GetState() { return this.state; }

	}
