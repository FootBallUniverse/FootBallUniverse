using UnityEngine;
using System.Collections;

public class TutorialManagerScript : MonoBehaviour {
	GameObject[] guidVewer = new GameObject[3];

	// チュートリアル用メッセージ（仮置）
	string[] MainMessage = new string[24]{
		// シーン１
		"やあ、私は君たちのチーム…",
		"代表の監督を努めさせてもらっている",
		"だ。確か君たちは、このスタジアムは初めてだったね？",
		"ここは、宇宙ステーション内に建設された球体スタジアムの中。これから“スペースフットボール”の、４年に１度の祭典…スペースワールドカップが行われる",
		"地上のスタジアムのそれとは、比べ物にならない浮遊感だろう？試合が始まるまで少し時間がある。今のうちに、体を馴らしておこうか",
		"まずは、そうだな… 上を見てくれ",
		"今向かい合っている相手が、君のチームメイトだ。隣にいる、と言ったほうがわかりやすいかもしれないが",
		"試合では、２人で協力して戦ってもらうことになる。今のうちに、連携を深めておいてくれ",
		// シーン２
		"次に、ボールの場所を確認しよう。[1P:左/2P:右]を見てくれ",
		"見ての通り、ボールだ。試合では、そいつを相手チームのゴールに蹴りこむことを目指す",
		"ボールは近づくと勝手に”ホールド”されて、足元についてくる",
		"試しに近くまで移動して、ボールをホールドしてみるんだ",
		"よし、いいぞ。次はボールを蹴ってみようか",
		// シーン３
		"チームメイトに向けて、”パス”を出してみよう。ボールは君が見ている場所に、まっすぐに飛ぶ",
		"いいパスだ。体も動かしながら、何度かボールを蹴り合ってみようか",
		"ボールを蹴る感覚も、だいぶ掴めてきたか？相手チームのキーパーに来てもらった。彼をかいくぐってゴールにシュートしてみよう",
		"やるね！今の感覚は、しっかり覚えておくんだ",
		"次は、相手チームの選手と一緒にウォーミングアップしようと思ったけど…まだ時間がかかるようだ。少し自由にしておいてくれ",
		"次は、相手チームの選手と一緒にウォーミングアップしてみよう。本番と同じ環境で、ボールの奪い合いをしてみよう",
		"相手チームの準備ができたみたいだ。本番と同じ環境で、ボールの奪い合いをしてみよう",
		// シーン４
		"正面に見えるのが、相手チームの選手たちだ。まずは、中央のボールを取って主導権を掴もう",
		"よし、ボールを取ったな。相手はダッシュやタックルを使ってボールをスティールしようとしてくるはずだ。彼らをかわして、ゴールに突っ込め！",
		"取られてしまったな。なら取り返すまでだ！ダッシュやタックルを使って追いつき、ボールをスティールしてやれ！",
		"おっと、いつの間にかこんな時間だ。もうすぐ試合が始まってしまうな…君たちなら、きっと勝てる。最高の試合を見せてくれ！"
		};

	// チュートリアル用サブメッセージ（仮置）
	string[] SubMessage = new string[]{
		"左",
		"右"
	};

	// チュートリアル状態遷移
	enum TUTORIAL_STATE
	{
		// スタート処理
		WAIT = 0,
		INIT,
		FAID_IN,
		// 各自チュートリアル
		SCENE0_Message00,
		SCENE0_Message01,
		SCENE0_Message02,
		SCENE0_Message03,
		SCENE0_Instruction00,
		SCENE0_Play00,
		SCENE0_Message04,
		SCENE0_Message05,
		// 各自チュートリアル
		SCENE1_Message00,
		SCENE1_Instruction00,
		SCENE1_Play00,
		SCENE1_Message01,
		SCENE1_Instruction01,
		SCENE1_Message02,
		SCENE1_Message03,
		SCENE1_Instruction02,
		SCENE1_Play01,
		SCENE1_Play02,
		// シーン４
		SCENE2_Message00,

		// 終了処理
		FAID_OUT,
		TUTORIAL_STATE_MAX
	};

	enum BUTTON_TYPE
	{
		A,
		START,
		NO_VIEW
	};

	struct DEF
	{
		public bool move;
		public bool rotation;
		public bool shoote;
		public bool takkle;
	};

	DEF[] controle = new DEF[4];

	GameObject[] player           = new GameObject[4];
	Vector3[] playerOldPositon    = new Vector3[4];
	Quaternion[] playerOldRotaton = new Quaternion[4];
	GameObject[] keeper           = new GameObject[2];
	TUTORIAL_STATE[] state        = new TUTORIAL_STATE[2];
	BUTTON_TYPE[] buttonType      = new BUTTON_TYPE[2];

	bool[] buttonCheck            = new bool[4];

	GameObject[,] buttonAicon  = new GameObject[4,2];
	GameObject[] buttonVewer  = new GameObject[4];
	GameObject[] bloackOut    = new GameObject[3];
	GameObject[] messageLog   = new GameObject[2];
	GameObject[] guidSubVewer = new GameObject[2];

	CSoundPlayer m_soundPlayer;

	// Use this for initialization
	void Start () {
#if true
		Application.LoadLevel("MainGame");
		return;
#endif
		// メインゲーム呼び出し
		Application.LoadLevelAdditive("MainGame");
		// 状態遷移設定
		this.state[0] = this.state[1] = TUTORIAL_STATE.WAIT;
		ReSetAllButtonCheck();
		// オブジェクト挿入
		for (int i = 0; i < 3; i++)
			this.guidVewer[i] = GameObject.Find("GuidVewer" + i);

		for (int i = 0; i < 2; i++)
			this.guidSubVewer[i] = GameObject.Find("SubPanel"+i).transform.FindChild("GuidVewer").gameObject;

		for (int i = 0; i < 2; i++)
			this.buttonType[i] = BUTTON_TYPE.A;

		for (int i = 0; i < 4; i++)
		{
			this.buttonVewer[i]    = GameObject.Find("Next_" + i);
			this.buttonAicon[i, 0] = GameObject.Find("Next_" + i).transform.FindChild("button_A").gameObject;
			this.buttonAicon[i, 1] = GameObject.Find("Next_" + i).transform.FindChild("button_Start").gameObject;
		}

		// コントロールの初期化
		for(int i = 0; i < 2; i ++)
			for(int j = 0; j < 2; j++)
				this.controle[i].move = this.controle[i].rotation = this.controle[i].shoote = this.controle[i].takkle = false;
	}

	void Controle()
	{
		for (int i = 0; i < 4; i++)
		{
			// 行動を無効化
			if (!this.controle[i].move) player[i].transform.localPosition = this.playerOldPositon[i];
			if (!this.controle[i].rotation) player[i].transform.rotation = this.playerOldRotaton[i];
			if (!this.controle[i].shoote)   player[i].GetComponent<CPlayer>().m_status = CPlayerManager.ePLAYER_STATUS.eNONE;
			if (!this.controle[i].takkle)   player[i].GetComponent<CPlayer>().m_status = CPlayerManager.ePLAYER_STATUS.eNONE;
		}
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
		// メッセージログ
		this.messageLog[0] = GameObject.Find("SubPanel0").gameObject.transform.FindChild("MessageLog").gameObject;
		this.messageLog[1] = GameObject.Find("SubPanel1").gameObject.transform.FindChild("MessageLog").gameObject;
		// プレイヤーセット
		for (int i = 1; i < 5; i++) this.player[i-1] = GameObject.Find("Player" + i).transform.FindChild("player").gameObject;
		this.player[0].transform.Rotate(new Vector3(90.0f, 260.0f, 0.0f));
		this.player[1].transform.Rotate(new Vector3(90.0f,  90.0f, 0.0f));
		this.player[2].transform.Rotate(new Vector3(90.0f,  90.0f, 0.0f));
		this.player[3].transform.Rotate(new Vector3(90.0f, 260.0f, 0.0f));
		for (int i = 0; i < 4; i++)
		{
			this.playerOldPositon[i] = this.player[i].transform.position;
			this.playerOldRotaton[i] = this.player[i].transform.rotation;
		}

		

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

		// 最初の画面に合わせて設定
		this.guidVewer[0].SetActive(true);
		this.guidVewer[0].GetComponent<UISprite>().spriteName = "user'sGuide_1";
		this.guidVewer[1].SetActive(false);
		this.guidVewer[2].SetActive(false);
		this.guidSubVewer[0].SetActive(false);
		this.guidSubVewer[1].SetActive(false);

		ReSetAllButtonCheck();
	}

	//----------------------------------------------------------------------
	// チーム２人のボタンを押したかを返す
	//----------------------------------------------------------------------
	// @Param   teamNo チーム番号（０で左、１で右）
	// @Param   check  true:ボタンがおされた/false:ボタンがおされていない
	// @Return  none
	// @Date    2014/12/30  @Update 2014/12/30  @Author T.Takeuichi
	//----------------------------------------------------------------------
	bool GetButtonCheck(int teamNo)
	{
		if (teamNo == 0)
		{
			if (!this.buttonCheck[0]) return false;
			if (!this.buttonCheck[1]) return false;
		}
		else if (teamNo == 1)
		{
			if (!this.buttonCheck[2]) return false;
			if (!this.buttonCheck[3]) return false;
		}
		else return false;

		return true;
	}

	//----------------------------------------------------------------------
	// ４人ともボタンを押したかを返す
	//----------------------------------------------------------------------
	// @Param   none
	// @Param   check  true:ボタンがおされた/false:ボタンがおされていない
	// @Return  none
	// @Date    2014/12/13  @Update 2014/12/13  @Author T.Takeuichi
	//----------------------------------------------------------------------
	bool GetAllButtonCheck()
	{
		for (int i = 0; i < 4; i++)
			if(!this.buttonCheck[i])
				return false;

		return true;
	}

	//----------------------------------------------------------------------
	// チーム２人のボタンが押されたかのチェックを初期化
	//----------------------------------------------------------------------
	// @Param   teamNo チーム番号（０で左、１で右）
	// @Return  none
	// @Date    2014/12/30  @Update 2014/12/30  @Author T.Takeuichi
	//----------------------------------------------------------------------
	void ReSetButtonCheck(int teamNo)
	{
		if (teamNo == 0)
		{
			this.buttonCheck[0] = false;
			this.buttonCheck[1] = false;
		}else if (teamNo == 1){
			this.buttonCheck[2] = false;
			this.buttonCheck[3] = false;
		}
		else return;
	}

	//----------------------------------------------------------------------
	// ４人のボタンが押されたかのチェックを初期化
	//----------------------------------------------------------------------
	// @Param   none
	// @Return  none
	// @Date    2014/12/13  @Update 2014/12/13  @Author T.Takeuichi
	//----------------------------------------------------------------------
	void ReSetAllButtonCheck()
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
			if (this.buttonCheck[i])
			{
				this.buttonVewer[i].SetActive(false);
			}

			else
			{
				this.buttonVewer[i].SetActive(true);


				switch (this.buttonType[i / 2])
				{
					case BUTTON_TYPE.A:
						if (this.buttonType[i/2] == BUTTON_TYPE.A)     this.buttonAicon[i, 0].SetActive(true);
						if (this.buttonType[i/2] == BUTTON_TYPE.START) this.buttonAicon[i, 1].SetActive(false);
						break;
					case BUTTON_TYPE.START:
						if (this.buttonType[i/2] == BUTTON_TYPE.A)     this.buttonAicon[i, 0].SetActive(false);
						if (this.buttonType[i/2] == BUTTON_TYPE.START) this.buttonAicon[i, 1].SetActive(true);
						break;
					case BUTTON_TYPE.NO_VIEW:
						if (this.buttonType[i/2] == BUTTON_TYPE.A)     this.buttonAicon[i, 0].SetActive(false);
						if (this.buttonType[i/2] == BUTTON_TYPE.START) this.buttonAicon[i, 1].SetActive(false);
						break;
				}
			}
		}
	}

	//----------------------------------------------------------------------
	// ４人のボタンを強制的にチェック
	//----------------------------------------------------------------------
	// @Param   none
	// @Return  none
	// @Date    2014/12/20  @Update 2014/12/20  @Author T.Takeuichi
	//----------------------------------------------------------------------
	void SetActiveAllButtonCheck()
	{
		for (int i = 0; i < 4; i++)
			this.buttonCheck[i] = true;
	}

	//----------------------------------------------------------------------
	// チーム２人のボタンを強制的にチェック
	//----------------------------------------------------------------------
	// @Param   teamNo チーム番号（０で左、１で右）
	// @Return  none
	// @Date    2014/12/20  @Update 2014/12/20  @Author T.Takeuichi
	//----------------------------------------------------------------------
	void SetActiveButtonCheck(int teamNo)
	{
		if (teamNo == 0)
		{
			this.buttonCheck[0] = true;
			this.buttonCheck[1] = true;
		}else if (teamNo == 1){
			this.buttonCheck[2] = true;
			this.buttonCheck[3] = true;
		}
		else return;
	}

	//----------------------------------------------------------------------
	// ボタンが押されたらチェックを入れる（Ａボタン）
	//----------------------------------------------------------------------
	// @Param   none
	// @Return  none
	// @Date    2014/12/30  @Update 2014/12/30  @Author T.Takeuichi
	//----------------------------------------------------------------------
	void SetButton_A()
	{
		if (Input.GetKeyDown(InputXBOX360.P1_XBOX_A) && this.buttonCheck[0] == false)
		{
			m_soundPlayer.PlaySE("tutorial/button_push");
			this.buttonCheck[0] = true;
		}
		if (Input.GetKeyDown(InputXBOX360.P2_XBOX_A) && this.buttonCheck[1] == false)
		{
			m_soundPlayer.PlaySE("tutorial/button_push");
			this.buttonCheck[1] = true;
		}
		if (Input.GetKeyDown(InputXBOX360.P3_XBOX_A) && this.buttonCheck[2] == false)
		{
			m_soundPlayer.PlaySE("tutorial/button_push");
			this.buttonCheck[2] = true;
		}
		if (Input.GetKeyDown(InputXBOX360.P4_XBOX_A) && this.buttonCheck[3] == false)
		{
			m_soundPlayer.PlaySE("tutorial/button_push");
			this.buttonCheck[3] = true;
		}
		// DEBUG用（ＰＣ）
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
	}

	//----------------------------------------------------------------------
	// ボタンが押されたらチェックを入れる（STARTボタン）
	//----------------------------------------------------------------------
	// @Param   none
	// @Return  none
	// @Date    2014/12/30  @Update 2014/12/30  @Author T.Takeuichi
	//----------------------------------------------------------------------
	void SetButton_START()
	{
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
		// DEBUG用（ＰＣ）
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
	}



	// Update is called once per frame
	void Update()
	{

		Debug.Log(this.state[0] + "  /  " +this.state[1]);
		string message;

		if(player[0] != null)Controle();

		for (int i = 0; i < 2; i++)
		{
			switch (this.state[i])
			{
				case TUTORIAL_STATE.WAIT: this.state[i] = TUTORIAL_STATE.INIT; break;
				case TUTORIAL_STATE.INIT:
					if (i == 0) InitTutorial();
					this.state[i] = TUTORIAL_STATE.FAID_IN;
					break;

				case TUTORIAL_STATE.FAID_IN:
					if ( !this.bloackOut[0].GetComponent<TweenAlpha>().enabled &&
				         !this.bloackOut[1].GetComponent<TweenAlpha>().enabled &&
				         !this.bloackOut[2].GetComponent<TweenAlpha>().enabled )
					{
						this.state[i] = TUTORIAL_STATE.SCENE0_Message00;
						break;
					}
					break;

				case TUTORIAL_STATE.SCENE0_Message00:
					message = this.MainMessage[0] + TeamData.GetTeamNationalityName(TeamData.teamNationality[i]) + this.MainMessage[1] + "○○" + this.MainMessage[2];
					this.messageLog[i].GetComponent<UILabel>().text = message;
					SetButton_A();
					break;

				case TUTORIAL_STATE.SCENE0_Message01:
					message = this.MainMessage[3];
					this.messageLog[i].GetComponent<UILabel>().text = message;
					SetButton_A();
					break;

				case TUTORIAL_STATE.SCENE0_Message02:
					message = this.MainMessage[4];
					this.messageLog[i].GetComponent<UILabel>().text = message;
					SetButton_A();
					break;

				case TUTORIAL_STATE.SCENE0_Message03:
					message = this.MainMessage[5];
					this.messageLog[i].GetComponent<UILabel>().text = message;
					SetButton_A();
					break;

				case TUTORIAL_STATE.SCENE0_Instruction00:
					this.messageLog[i].SetActive(false);
					this.guidSubVewer[i].SetActive(true);
					SetButton_START();
					break;

				case TUTORIAL_STATE.SCENE0_Play00:
					this.messageLog[i].SetActive(false);
					this.guidSubVewer[i].SetActive(false);
					this.controle[i*2].rotation     = true;
					this.controle[i * 2 + 1].rotation = true;


						//test
						//testEnd

					// test
					SetButton_A();
					// endTest
					break;

				case TUTORIAL_STATE.SCENE0_Message04:
					this.messageLog[i].SetActive(true);
					this.guidSubVewer[i].SetActive(false);
					this.controle[i*2].rotation     = false;
					this.controle[i*2 + 1].rotation = false;
					message = this.MainMessage[6];
					this.messageLog[i].GetComponent<UILabel>().text = message;
					SetButton_A();
					break;

				case TUTORIAL_STATE.SCENE0_Message05:
					message = this.MainMessage[7];
					this.messageLog[i].GetComponent<UILabel>().text = message;
					SetButton_A();
					break;

				case TUTORIAL_STATE.SCENE1_Message00:
					message = this.MainMessage[8];
					this.messageLog[i].GetComponent<UILabel>().text = message;
					SetButton_A();
					break;

				case TUTORIAL_STATE.SCENE1_Instruction00:
					this.messageLog[i].SetActive(false);
					this.guidSubVewer[i].SetActive(true);
					SetButton_START();
					break;

				case TUTORIAL_STATE.SCENE1_Play00:
					this.messageLog[i].SetActive(false);
					this.guidSubVewer[i].SetActive(false);
					this.controle[i*2].rotation     = true;
					this.controle[i*2 + 1].rotation = true;
					// test
					SetButton_A();
					// endTest
					break;

				case TUTORIAL_STATE.SCENE1_Message01:
					message = this.MainMessage[9];
					this.messageLog[i].GetComponent<UILabel>().text = message;
					this.controle[i*2].rotation     = false;
					this.controle[i*2 + 1].rotation = false;
					SetButton_A();
					break;

				case TUTORIAL_STATE.SCENE1_Instruction01:
					this.messageLog[i].SetActive(false);
					this.guidSubVewer[i].SetActive(true);
					SetButton_START();
					break;

				case TUTORIAL_STATE.SCENE1_Message02:
					this.messageLog[i].SetActive(true);
					this.guidSubVewer[i].SetActive(false);
					message = this.MainMessage[10];
					this.messageLog[i].GetComponent<UILabel>().text = message;
					SetButton_A();
					break;

				case TUTORIAL_STATE.SCENE1_Message03:
					message = this.MainMessage[11];
					this.messageLog[i].GetComponent<UILabel>().text = message;
					SetButton_A();
					break;

				case TUTORIAL_STATE.SCENE1_Instruction02:
					this.messageLog[i].SetActive(false);
					this.guidSubVewer[i].SetActive(true);
					SetButton_START();
					break;

				case TUTORIAL_STATE.SCENE1_Play01:
					this.messageLog[i].SetActive(false);
					this.guidSubVewer[i].SetActive(false);

					// test
					SetButton_A();
					// endTest
					break;

				case TUTORIAL_STATE.SCENE1_Play02:

					// test
					SetButton_A();
					// endTest
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
		}

		ButtonDraw();
		NextState();

		if (player[0] != null)
		{
			for (int i = 0; i < 4; i++)
			{
				this.playerOldPositon[i] = this.player[i].transform.position;
				this.playerOldRotaton[i] = this.player[i].transform.rotation;
			}
		}
	}

	void NextState()
	{
		// 次へ遷移
		if (GetAllButtonCheck())
		{
			// 次へ遷移
			if (GetAllButtonCheck())
			{
				ReSetAllButtonCheck();
				this.state[0] += 1;
				this.state[1] += 1;
			}
		}
	}
}
