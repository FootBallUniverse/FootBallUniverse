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
		TUTORIAL4,
		TUTORIAL5,
		FAID_OUT,
		STATE_MAX
	};

	TUTORIAL_STATE state     = TUTORIAL_STATE.WAIT;
	bool[] buttonCheck       = new bool[4];       // ボタンが押されたかどうかチェック
	GameObject[] buttonVewer = new GameObject[4]; // ボタン入力表示用
	GameObject[] bloackOut   = new GameObject[3]; // フェードアウト用
	GameObject[] player      = new GameObject[4]; // プレイヤー情報
	// 音楽ファイル
    CSoundPlayer m_soundPlayer;

	// Use this for initialization
	void Start () {
		// メインゲーム呼び出し
		Application.LoadLevelAdditive("MainGame");

	}

	//----------------------------------------------------------------------
	// チュートリアル画面の初期化
	//----------------------------------------------------------------------
	// @Param   none
	// @Return  none
	// @Date    2014/12/13  @Update 2014/12/13  @Author T.Takeuichi
	//----------------------------------------------------------------------
	void InitTutorial()
	{
        CGameManager.m_nowStatus = CGameManager.eSTATUS.eGAME;
        CGameData.m_isTimer = false;

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
		// フェードアウト用
		this.bloackOut[0] = GameObject.Find("MainPanelFeed").gameObject;
		this.bloackOut[1] = GameObject.Find("SubPanelFeed0").gameObject;
		this.bloackOut[2] = GameObject.Find("SubPanelFeed1").gameObject;
		// 制限時間OFF
		GameObject.Find("P1&P2").transform.FindChild("UI").transform.FindChild("Camera").transform.FindChild("Anchor").transform.FindChild("Panel").transform.FindChild("time_colon").gameObject.SetActive(false);
		GameObject.Find("P1&P2").transform.FindChild("UI").transform.FindChild("Camera").transform.FindChild("Anchor").transform.FindChild("Panel").transform.FindChild("time_sec").gameObject.SetActive(false);
		GameObject.Find("P1&P2").transform.FindChild("UI").transform.FindChild("Camera").transform.FindChild("Anchor").transform.FindChild("Panel").transform.FindChild("time_tensec").gameObject.SetActive(false);
		GameObject.Find("P1&P2").transform.FindChild("UI").transform.FindChild("Camera").transform.FindChild("Anchor").transform.FindChild("Panel").transform.FindChild("time_min").gameObject.SetActive(false);

		GameObject.Find("P3&P4").transform.FindChild("UI").transform.FindChild("Camera").transform.FindChild("Anchor").transform.FindChild("Panel").transform.FindChild("time_colon").gameObject.SetActive(false);
		GameObject.Find("P3&P4").transform.FindChild("UI").transform.FindChild("Camera").transform.FindChild("Anchor").transform.FindChild("Panel").transform.FindChild("time_sec").gameObject.SetActive(false);
		GameObject.Find("P3&P4").transform.FindChild("UI").transform.FindChild("Camera").transform.FindChild("Anchor").transform.FindChild("Panel").transform.FindChild("time_tensec").gameObject.SetActive(false);
		GameObject.Find("P3&P4").transform.FindChild("UI").transform.FindChild("Camera").transform.FindChild("Anchor").transform.FindChild("Panel").transform.FindChild("time_min").gameObject.SetActive(false);

        GameObject.Find("P1&P2").transform.FindChild("UI").transform.FindChild("Camera").transform.FindChild("Anchor").transform.FindChild("Panel").transform.FindChild("BlackOut").gameObject.SetActive(false);
        GameObject.Find("P3&P4").transform.FindChild("UI").transform.FindChild("Camera").transform.FindChild("Anchor").transform.FindChild("Panel").transform.FindChild("BlackOut").gameObject.SetActive(false);

        GameObject.Find("BGMObject").gameObject.SetActive(false);
        GameObject.Find("SEObject").gameObject.SetActive(false);

        m_soundPlayer = new CSoundPlayer();
        m_soundPlayer.PlayBGMFadeIn("tutorial/bgm_01", 0.05f);

		// PlayerMode
		for (int i = 1; i < 5; i++) this.player[i - 1] = GameObject.Find("Player" + i).transform.FindChild("player").gameObject;

        // オブジェクト挿入
		for (int i = 0; i < 3; i++)
			this.guidVewer[i] = GameObject.Find("GuidVewer" + i);

		for (int i = 0; i < 4; i++)
			this.buttonVewer[i] = GameObject.Find("Next_" + i);

		// 最初の画面に合わせて設定
		this.guidVewer[0].SetActive(true);
		this.guidVewer[0].GetComponent<UISprite>().spriteName = "user'sGuide_1";
		this.guidVewer[1].SetActive(false);
		this.guidVewer[2].SetActive(false);

		ReSetButtonCheck();
	}

	//----------------------------------------------------------------------
	// ４人ともボタンを押したかを返す
	//----------------------------------------------------------------------
	// @Param   none
	// @Param   check  true:ボタンがおされた/false:ボタンがおされていない
	// @Return  none
	// @Date    2014/12/13  @Update 2014/12/13  @Author T.Takeuichi
	//----------------------------------------------------------------------
	bool GetButtonCheck()
	{
		for (int i = 0; i < 4; i++)
			if(!this.buttonCheck[i])
				return false;

		return true;
	}

	//----------------------------------------------------------------------
	// ボタンが押されたかのチェックを初期化
	//----------------------------------------------------------------------
	// @Param   none
	// @Return  none
	// @Date    2014/12/13  @Update 2014/12/13  @Author T.Takeuichi
	//----------------------------------------------------------------------
	void ReSetButtonCheck()
	{
		for (int i = 0; i < 4; i++)
			this.buttonCheck[i] = false;
	}

	//----------------------------------------------------------------------
	// ボタンを押せ　を描するかしないかを判断させる
	//----------------------------------------------------------------------
	// @Param   none
	// @Return  none
	// @Date    2014/12/13  @Update 2014/12/13  @Author T.Takeuichi
	//----------------------------------------------------------------------
	void ButtonDraw()
	{
		for (int i = 0; i < 4; i++)
		{
			if (this.buttonCheck[i]) this.buttonVewer[i].SetActive(false);
			else                     this.buttonVewer[i].SetActive(true);
		}
	}



	//----------------------------------------------------------------------
	// 更新
	//----------------------------------------------------------------------
	// @Param	none		
	// @Return	none
	// @Date	2015/01/09  @Update 2015/01/09  @Author T.Takeuchi      
	//----------------------------------------------------------------------
	void Update()
	{
		// Input
        if (Input.GetKeyDown(InputXBOX360.P1_XBOX_START) && this.buttonCheck[0] == false)
        {
            m_soundPlayer.PlaySE("tutorial/button_push");
            this.buttonCheck[0] = true;
        }
        if (Input.GetKeyDown(InputXBOX360.P2_XBOX_START) && this.buttonCheck[1] == false)
        {
            m_soundPlayer.PlaySE("tutorial/button_push");
            this.buttonCheck[1] = true;
        }
        if (Input.GetKeyDown(InputXBOX360.P3_XBOX_START) && this.buttonCheck[2] == false)
        {
            m_soundPlayer.PlaySE("tutorial/button_push");
            this.buttonCheck[2] = true;
        }
        if (Input.GetKeyDown(InputXBOX360.P4_XBOX_START) && this.buttonCheck[3] == false)
        {
            m_soundPlayer.PlaySE("tutorial/button_push");
            this.buttonCheck[3] = true;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            m_soundPlayer.PlaySE("tutorial/button_push");
            this.buttonCheck[0] = this.buttonCheck[1] = true;
        }
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            m_soundPlayer.PlaySE("tutorial/button_push");
            this.buttonCheck[2] = this.buttonCheck[3] = true;
        }



		// 遷移
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
				if ( !this.bloackOut[0].GetComponent<TweenAlpha>().enabled &&
				     !this.bloackOut[1].GetComponent<TweenAlpha>().enabled &&
				     !this.bloackOut[2].GetComponent<TweenAlpha>().enabled )
				{
					ReSetButtonCheck();
					this.state += 1;
				}
				break;


			case TUTORIAL_STATE.TUTORIAL0:
				if (GetButtonCheck())
				{
                    m_soundPlayer.PlaySE("tutorial/tutorial_next");
					ReSetButtonCheck();
					this.state += 1;
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
                    m_soundPlayer.PlaySE("tutorial/tutorial_next");
					ReSetButtonCheck();
					this.state +=1;
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
                    m_soundPlayer.PlaySE("tutorial/tutorial_next");
					ReSetButtonCheck();
					this.state += 1;
				}
				break;


			case TUTORIAL_STATE.TUTORIAL3:
				this.guidVewer[0].SetActive(false);
				this.guidVewer[1].SetActive(true);
				this.guidVewer[1].GetComponent<UISprite>().spriteName = "Instruction06";
				this.guidVewer[2].SetActive(true);
				this.guidVewer[2].GetComponent<UISprite>().spriteName = "Instruction07";

				for (int i = 0; i < 4; i++) player[i].GetComponent<CPlayer>().m_gauge.m_gauge = 210;

				if (GetButtonCheck())
				{
					m_soundPlayer.PlaySE("tutorial/tutorial_next");
					ReSetButtonCheck();
					this.state += 1;
				}
				break;


			case TUTORIAL_STATE.TUTORIAL4:
				this.guidVewer[0].SetActive(true);
				this.guidVewer[0].GetComponent<UISprite>().spriteName = "Instruction08";
				this.guidVewer[1].SetActive(false);
				this.guidVewer[2].SetActive(false);

				for (int i = 0; i < 4; i++) player[i].GetComponent<CPlayer>().m_gauge.m_gauge = 210;

				if (GetButtonCheck())
				{
					ReSetButtonCheck();
					m_soundPlayer.PlaySE("tutorial/tutorial_next");
					for (int i = 0; i < 3; i++)
						this.bloackOut[i].GetComponent<TweenAlpha>().Play(false);
					this.state += 1;
				}
				break;


			case TUTORIAL_STATE.TUTORIAL5:
				this.guidVewer[0].SetActive(true);
				this.guidVewer[0].GetComponent<UISprite>().spriteName = "user'sGuide_6";
				this.guidVewer[1].SetActive(false);
				this.guidVewer[2].SetActive(false);

				for (int i = 0; i < 4; i++) player[i].GetComponent<CPlayer>().m_gauge.m_gauge = 210;

				if (GetButtonCheck())
				{
					m_soundPlayer.PlaySE("tutorial/tutorial_next");
					for (int i = 0; i < 3; i++)
						this.bloackOut[i].GetComponent<TweenAlpha>().Play(false);
					this.state += 1;
				}
				break;


			case TUTORIAL_STATE.FAID_OUT:
				if ( !this.bloackOut[0].GetComponent<TweenAlpha>().enabled &&
					 !this.bloackOut[1].GetComponent<TweenAlpha>().enabled &&
					 !this.bloackOut[2].GetComponent<TweenAlpha>().enabled)
				{
                    TeamData.suppoterByTeam[0] = 0;
                    TeamData.suppoterByTeam[1] = 0;
					Application.LoadLevel("MainGame");
                    
				}
				break;
		}

		ButtonDraw();
	}
}

//End of File
