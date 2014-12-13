using UnityEngine;
using System.Collections;

public class TutorialManagerScript : MonoBehaviour {
	GameObject[] guidVewer = new GameObject[3];

	enum TUTORIAL_STATE
	{
		WAIT,
		INIT,
		FAID_IN,
		TUTORIAL0,
		TUTORIAL1,
		TUTORIAL2,
		TUTORIAL3,
		FAID_OUT,
		STATE_MAX
	};

	TUTORIAL_STATE state     = TUTORIAL_STATE.WAIT;
	bool[] buttonCheck       = new bool[4];
	GameObject[] buttonVewer = new GameObject[4];

	// Use this for initialization
	void Start () {
		// メインゲーム呼び出し
		Application.LoadLevelAdditive("MainGame");
	}

	void InitTutorial()
	{
		// 配信用カメラ削除
		GameObject.Find("DeliveryCamera").SetActive(false);
		// チュートリアルに必要のない機能を無効化
		GameObject.Find("goal1_collision").SetActive(false);
		GameObject.Find("goal2_collision").SetActive(false);
		GameObject.Find("goal1_collision2").SetActive(false);
		GameObject.Find("goal2_collision2").SetActive(false);
		// NPCの削除
		GameObject.Find("CPU1").SetActive(false);
		GameObject.Find("CPU2").SetActive(false);
		GameObject.Find("GoalKeeper1").SetActive(false);
		GameObject.Find("GoalKeeper2").SetActive(false);
		// メインUI削除
		GameObject.Find("MainUI").SetActive(false);

		// 制限時間OFF

		// オブジェクト挿入
		for (int i = 0; i < 3; i++)
			this.guidVewer[i] = GameObject.Find("GuidVewer" + i);

		for (int i = 0; i < 4; i++)
			this.buttonVewer[i] = GameObject.Find("Next_" + i);

		ReSetButtonCheck();
	}

	bool GetButtonCheck()
	{
		for (int i = 0; i < 4; i++)
			if(!this.buttonCheck[i])
				return false;

		return true;
	}

	void ReSetButtonCheck()
	{
		for (int i = 0; i < 4; i++)
			this.buttonCheck[i] = false;
	}

	void ButtonDraw()
	{
		for (int i = 0; i < 4; i++)
		{
			if (this.buttonCheck[i]) this.buttonVewer[i].SetActive(false);
			else                     this.buttonVewer[i].SetActive(true);
		}
	}

	// Update is called once per frame
	void Update()
	{
		// Input
		if (Input.GetKeyDown(InputXBOX360.P1_XBOX_A)) this.buttonCheck[0] = true;
		if (Input.GetKeyDown(InputXBOX360.P2_XBOX_A)) this.buttonCheck[1] = true;
		if (Input.GetKeyDown(InputXBOX360.P3_XBOX_A)) this.buttonCheck[2] = true;
		if (Input.GetKeyDown(InputXBOX360.P4_XBOX_A)) this.buttonCheck[3] = true;
		if (Input.GetKeyDown(KeyCode.LeftShift))  this.buttonCheck[0] = this.buttonCheck[1] = true;
		if (Input.GetKeyDown(KeyCode.RightShift)) this.buttonCheck[2] = this.buttonCheck[3] = true;

		switch (this.state)
		{
			case TUTORIAL_STATE.WAIT:
				this.state = TUTORIAL_STATE.INIT;
				return;


			case TUTORIAL_STATE.INIT:
				InitTutorial();
				this.state = TUTORIAL_STATE.FAID_IN;
				break;


			case TUTORIAL_STATE.FAID_IN:
				// 仮
				ReSetButtonCheck();
				this.state = TUTORIAL_STATE.TUTORIAL0;
				break;


			case TUTORIAL_STATE.TUTORIAL0:
				this.guidVewer[0].SetActive(true);
				this.guidVewer[0].GetComponent<UISprite>().spriteName = "user'sGuide_1";
				this.guidVewer[1].SetActive(false);
				this.guidVewer[2].SetActive(false);

				if (GetButtonCheck())
				{
					ReSetButtonCheck();
					this.state = TUTORIAL_STATE.TUTORIAL1;
				}
				break;


			case TUTORIAL_STATE.TUTORIAL1:
				this.guidVewer[0].SetActive(false);
				this.guidVewer[1].SetActive(true);
				this.guidVewer[1].GetComponent<UISprite>().spriteName = "user'sGuide_2";
				this.guidVewer[2].SetActive(true);
				this.guidVewer[2].GetComponent<UISprite>().spriteName = "user'sGuide_3";

				if (GetButtonCheck())
				{
					ReSetButtonCheck();
					this.state = TUTORIAL_STATE.TUTORIAL2;
				}
				break;
			case TUTORIAL_STATE.TUTORIAL2:
				this.guidVewer[0].SetActive(false);
				this.guidVewer[1].SetActive(true);
				this.guidVewer[1].GetComponent<UISprite>().spriteName = "user'sGuide_4";
				this.guidVewer[2].SetActive(true);
				this.guidVewer[2].GetComponent<UISprite>().spriteName = "user'sGuide_5";

				if (GetButtonCheck())
				{
					ReSetButtonCheck();
					this.state = TUTORIAL_STATE.TUTORIAL3;
				}
				break;


			case TUTORIAL_STATE.TUTORIAL3:
				this.guidVewer[0].SetActive(true);
				this.guidVewer[0].GetComponent<UISprite>().spriteName = "user'sGuide_6";
				this.guidVewer[1].SetActive(false);
				this.guidVewer[2].SetActive(false);
				if (GetButtonCheck())
				{
					this.state = TUTORIAL_STATE.FAID_OUT;
				}
				break;


			case TUTORIAL_STATE.FAID_OUT:
				Application.LoadLevel("MainGame");
				break;
		}

		ButtonDraw();
	}
}
