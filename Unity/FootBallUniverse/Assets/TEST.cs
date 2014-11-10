using UnityEngine;
using System.Collections;

public class TEST : MonoBehaviour {
	GameObject test;

	// Use this for initialization
	void Start () {
		test = GameObject.Find("Quad") as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.A)) test.GetComponent<TweenAlphaSprite>().TweenStart(TweenAlphaSprite.TWEEN_START_SATE.FADE_IN);
		if (Input.GetKeyDown(KeyCode.S)) test.GetComponent<TweenAlphaSprite>().TweenStart(TweenAlphaSprite.TWEEN_START_SATE.FADE_OUT);
		if (Input.GetKeyDown(KeyCode.D)) test.GetComponent<TweenAlphaSprite>().TweenStop();
	}
}
