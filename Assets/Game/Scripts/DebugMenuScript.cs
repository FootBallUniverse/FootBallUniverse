using UnityEngine;
using System.Collections;

public class DebugMenuScript : MonoBehaviour {
	public GameObject debugControlScriptPrefab;
	private GameObject debugControlScript;

	// Use this for initialization
	void Start () {
		// デバッグ管理用スクリプトが存在しない場合は作成する
		this.debugControlScript = GameObject.Find("DebugControlScript");
		if (this.debugControlScript == null)
		{
			this.debugControlScript = Instantiate(debugControlScriptPrefab) as GameObject;
			this.debugControlScript.name = "DebugControlScript";
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI()
	{
		// シーン移動用
		if (GUI.Button(new Rect(10.0f, 60.0f, 100.0f, 20.0f), "タイトル"))        { Application.LoadLevel("Title");}
		if (GUI.Button(new Rect(10.0f, 80.0f, 100.0f, 20.0f), "待機画面"))        { Application.LoadLevel("WaitPlayer"); }
		if (GUI.Button(new Rect(10.0f, 100.0f, 100.0f, 20.0f), "チーム選択"))     { Application.LoadLevel("ChooseTeam"); }
		if (GUI.Button(new Rect(10.0f, 120.0f, 100.0f, 20.0f), "チュートリアル")) { Application.LoadLevel("Tutorial"); }
		if (GUI.Button(new Rect(10.0f, 140.0f, 100.0f, 20.0f), "ゲーム"))         { Application.LoadLevel("MainGame"); }
		if (GUI.Button(new Rect(10.0f, 160.0f, 100.0f, 20.0f), "リザルト"))       { Application.LoadLevel("Result"); }
		if (GUI.Button(new Rect(10.0f,180.0f, 100.0f, 20.0f), "NoSetting")) { ;}
		if (GUI.Button(new Rect(10.0f,200.0f, 100.0f, 20.0f), "NoSetting")) { ;}
		if (GUI.Button(new Rect(10.0f,220.0f, 100.0f, 20.0f), "NoSetting")) { ;}
		if (GUI.Button(new Rect(10.0f,240.0f, 100.0f, 20.0f), "NoSetting")) { ;}
		if (GUI.Button(new Rect(10.0f,260.0f, 100.0f, 20.0f), "NoSetting")) { ;}

		// デバッグモードチェック
		/*
		this.debugControlScript.AddComponent<DebugControlScript>().isDebugMode =
			GUI.Toggle(new Rect(200.0f, 40.0f, 260.0f, 20.0f), this.debugControlScript.AddComponent<DebugControlScript>().isDebugMode, "DEBUGモード表示");
		 */
	}
}

// End of File