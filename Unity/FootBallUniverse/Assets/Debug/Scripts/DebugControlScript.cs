using UnityEngine;
using System.Collections;

public class DebugControlScript : MonoBehaviour {
	public bool isDebugMode;
	// FPS計測用
	public float updateInterval = 1.0f;
	private int   numFlame    = 0;
	private float timeCounter = 0.0f;
	private float timer       = 0.0f;
	private float fps         = 0.0f;
	
	// Use this for initialization
	void Start () {
		this.isDebugMode = true;
		DontDestroyOnLoad(this);
	}
	
	// Update is called once per frame
	void Update () {
		if (this.isDebugMode)
		{
			if (Input.GetKeyDown(KeyCode.F1)) Application.LoadLevel("DebugMenu");
			CountFPS();
		}
	}

	void OnGUI()
	{
		if (this.isDebugMode)
		{
			GUI.Label(new Rect(0.0f, 0.0f, 100.0f, 20.0f), "DEBUG MODE");
			GUI.Label(new Rect(0.0f, 20.0f, 400.0f, 20.0f), "SceneName : " + Application.loadedLevelName.ToString());
			GUI.Label(new Rect(0.0f, 40.0f, 100.0f, 20.0f), "FPS : " + this.fps.ToString());
		}
	}

	//----------------------------------------------------------------------
	// FPSを計測し、メンバに格納
	//----------------------------------------------------------------------
	// @Param   none
	// @Return  計測されたFPS（メンバに直接格納）
	// @Date    2014/10/7/11:30  @Update 2014/10/7/9:30  @Author T.Takeuchi
	//----------------------------------------------------------------------
	void CountFPS()
	{
		this.timeCounter -= Time.deltaTime;
		this.timer += Time.timeScale / Time.deltaTime;
		numFlame++;

		if (this.timeCounter < 0.0f)
		{
			this.fps = this.timer / this.numFlame;
			this.numFlame = 0;
			this.timer = 0;
			this.timeCounter = this.updateInterval;
		}
	}
}

// End of File
