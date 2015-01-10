#if false
using UnityEngine;
using System.Collections;

public class TutorialManagerScript : MonoBehaviour {
	GameObject[] guidVewer = new GameObject[3];

	// チュートリアル用メッセージ（仮置）
	string[] MainMessage = new string[24]{
		// シーン１
/*0*/		"やあ、私は君たちのチーム…",
/*1*/		"代表の監督を努めさせてもらっている",
/*2*/		"者だ。\n確か君たちは、このスタジアムは初めてだったね？",
/*3*/		"ここは、宇宙ステーション内に建設された球体スタジアムの中。\nこれから“スペースフットボール”の、４年に１度の祭典…スペースワールドカップが行われる",
/*4*/		"地上のスタジアムのそれとは、比べ物にならない浮遊感だろう？\n試合が始まるまで少し時間がある。今のうちに、体を馴らしておこうか",
/*5*/		"まずは、そうだな… 上を見てくれ",
/*6*/		"今向かい合っている相手が、君のチームメイトだ。\n隣にいる、と言ったほうがわかりやすいかもしれないが",
/*7*/		"試合では、２人で協力して戦ってもらうことになる。\n今のうちに、連携を深めておいてくれ",
		// シーン２
/*8*/		"次に、ボールの場所を確認しよう。[1P:左/2P:右]を見てくれ",
/*9*/		"見ての通り、ボールだ。試合では、そいつを相手チームのゴールに蹴りこむことを目指す",
/*10*/		"ボールは近づくと勝手に”ホールド”されて、足元についてくる",
/*11*/		"正面に見えるのが、相手チームの選手たちだ。\n相手より先に中央のボールを取って、主導権を掴もう",

/*12*/		"よし、ボールを取ったな。相手はボールをスティールしようとしてくるはずだ。\n彼らをかわして、ゴールにボールをシュートしろ！",
/*13*/		"ボールを取られてしまったな。なら取り返すんだ！\nダッシュやタックルを使って追いつき、ボールをスティールしてやれ！",

/*14*/		"おっと、いつの間にかこんな時間だ。もうすぐ試合が始まってしまうな…君たちなら、きっと勝てる。最高の試合を見せてくれ！",


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
		// シーン１
		SCENE0_Message00,
		SCENE0_Message01,
		SCENE0_Message02,
		SCENE0_Message03,
		SCENE0_Instruction00,
		SCENE0_Play00,
		SCENE0_Message04,
		SCENE0_Message05,
		// シーン２
		SCENE1_Message00,
		SCENE1_Instruction00,
		SCENE1_Play00,
		SCENE1_Message01,
		SCENE1_Instruction01,
		SCENE1_Message02,
		SCENE1_Message03,
		SCENE1_Instruction02,
		SCENE1_Play01,
		SCENE1_Message04,
		SCENE1_Instruction03,
		SCENE1_Play02,
		SCENE1_Instruction04,
		SCENE1_Instruction05,
		SCENE1_Instruction06,
		SCENE1_Message05,
		SCENE1_Play03,

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


	GameObject[] player           = new GameObject[4];
	GameObject[] keeper           = new GameObject[2];
	TUTORIAL_STATE[] state        = new TUTORIAL_STATE[2];
	BUTTON_TYPE buttonType;

	bool[] buttonCheck            = new bool[4];

	GameObject[,] buttonAicon  = new GameObject[4,2];
	GameObject[] buttonVewer  = new GameObject[4];
	GameObject[] bloackOut    = new GameObject[3];
	GameObject[] messageLog   = new GameObject[2];
	GameObject[] guidSubVewer = new GameObject[2];
	GameObject ball;

	int takeBallTeamNo = 2;
	int oldTakeBallTeamNo = 2;

	int time = 0;

	CSoundPlayer m_soundPlayer;

	// Use this for initialization
	void Start () {
#if false
		Application.LoadLevel("MainGame");
		return;
#endif
		// メインゲーム呼び出し
		Application.LoadLevelAdditive("MainGame");
		// 状態遷移設定
		this.state[0] = this.state[1] = TUTORIAL_STATE.WAIT;
		ReSetAllButtonCheck();
		// オブジェクト挿入
		//for (int i = 0; i < 3; i++)
		//	this.guidVewer[i] = GameObject.Find("GuidVewer" + i);

		for (int i = 0; i < 2; i++)
			this.guidSubVewer[i] = GameObject.Find("SubPanel"+i).transform.FindChild("GuidVewer").gameObject;

		this.buttonType = BUTTON_TYPE.A;

		for (int i = 0; i < 4; i++)
		{
			this.buttonVewer[i]    = GameObject.Find("Next_" + i);
			this.buttonAicon[i, 0] = GameObject.Find("Next_" + i).transform.FindChild("button_A").gameObject;
			this.buttonAicon[i, 1] = GameObject.Find("Next_" + i).transform.FindChild("button_Start").gameObject;
		}
	}


	//----------------------------------------------------------------------
	// 目標のオブジェクトがいる方向に向いているか判断
	//----------------------------------------------------------------------
	// @Param   obj1    基準にしたいオブジェクト
	//          obj2    目標のオブジェクト
	//          _degree 許容範囲内角度
	// @Return  bool  範囲内:true   範囲外:false
	// @Date    2014/12/13  @Update 2014/12/13  @Author T.Takeuichi
	//----------------------------------------------------------------------
	bool GetCheck2ObjectDegree(GameObject obj1,GameObject obj2,float _degree)
	{
		Vector3 vec = obj2.transform.position - obj1.transform.position;
		float[] length = new float[2];
		// 長さを求める
		length[0] = Vector3.Distance(Vector3.zero, obj1.transform.forward);
		length[1] = Vector3.Distance(Vector3.zero, vec);

		// なす角度を求める
		float cos = Mathf.Acos(Vector3.Dot(obj1.transform.forward,vec) / (length[0] * length[1]));
		float degree = cos * 180 / Mathf.PI;

		if (degree <= _degree) return true;
		return false;
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
			this.player[i].GetComponent<CPlayer>().m_status = CPlayerManager.ePLAYER_STATUS.eTUTORIAL;
			this.player[i].GetComponent<CPlayer>().m_controlePermission.move_x   = false;
			this.player[i].GetComponent<CPlayer>().m_controlePermission.move_z   = false;
			this.player[i].GetComponent<CPlayer>().m_controlePermission.rotate_x = false;
			this.player[i].GetComponent<CPlayer>().m_controlePermission.rotate_y = false;
			this.player[i].GetComponent<CPlayer>().m_controlePermission.rockOn   = false;
			this.player[i].GetComponent<CPlayer>().m_controlePermission.shoote   = false;
			this.player[i].GetComponent<CPlayer>().m_controlePermission.gauge    = false;
			this.player[i].GetComponent<CPlayer>().m_controlePermission.charge   = false;
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

		// ボールオブジェクト取得
		this.ball = GameObject.Find("SoccerBall").gameObject;

		// 最初の画面に合わせて設定
		//this.guidVewer[0].SetActive(true);
		//this.guidVewer[0].GetComponent<UISprite>().spriteName = "user'sGuide_1";
		//this.guidVewer[1].SetActive(false);
		//this.guidVewer[2].SetActive(false);
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


				switch (this.buttonType)
				{
					case BUTTON_TYPE.A:
						this.buttonAicon[i, 0].SetActive(true);
						this.buttonAicon[i, 1].SetActive(false);
						break;
					case BUTTON_TYPE.START:
						 this.buttonAicon[i, 0].SetActive(false);
						 this.buttonAicon[i, 1].SetActive(true);
						break;
					case BUTTON_TYPE.NO_VIEW:
						this.buttonAicon[i, 0].SetActive(false);
						this.buttonAicon[i, 1].SetActive(false);
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
	}

	//----------------------------------------------------------------------
	// デバッグ用ボタン（シフト）
	//----------------------------------------------------------------------
	// @Param   none
	// @Return  none
	// @Date    2015/01/09  @Update 2015/01/09  @Author T.Takeuichi
	//----------------------------------------------------------------------
	void SetButton_Debug_SHIFT()
	{
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
		string message;

		if (Input.GetKeyDown(KeyCode.Space)) Application.LoadLevel("MainGame"); 

		for (int i = 0; i < 2; i++)
		{
			switch (this.state[i])
			{
				case TUTORIAL_STATE.WAIT: this.state[i] = TUTORIAL_STATE.INIT; break;
				case TUTORIAL_STATE.INIT:
					if (i == 0) InitTutorial();
					this.state[i] = TUTORIAL_STATE.FAID_IN;
					message = this.MainMessage[0] + TeamData.GetTeamNationalityName(TeamData.teamNationality[i]) + this.MainMessage[1] + this.MainMessage[2];
					this.messageLog[i].transform.FindChild("logs").GetComponent<UILabel>().text = message;
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
					message = this.MainMessage[0] + TeamData.GetTeamNationalityName(TeamData.teamNationality[i]) + this.MainMessage[1] + this.MainMessage[2];
					this.messageLog[i].transform.FindChild("logs").GetComponent<UILabel>().text = message;
					SetButton_A();
					break;

				case TUTORIAL_STATE.SCENE0_Message01:
					message = this.MainMessage[3];
					this.messageLog[i].transform.FindChild("logs").GetComponent<UILabel>().text = message;
					SetButton_A();
					break;

				case TUTORIAL_STATE.SCENE0_Message02:
					message = this.MainMessage[4];
					this.messageLog[i].transform.FindChild("logs").GetComponent<UILabel>().text = message;
					SetButton_A();
					break;

				case TUTORIAL_STATE.SCENE0_Message03:
					message = this.MainMessage[5];
					this.messageLog[i].transform.FindChild("logs").GetComponent<UILabel>().text = message;
					SetButton_A();
					break;

				case TUTORIAL_STATE.SCENE0_Instruction00:
					this.buttonType = BUTTON_TYPE.START;
					this.messageLog[i].SetActive(false);
					this.guidSubVewer[i].SetActive(true);
					this.guidSubVewer[i].GetComponent<UISprite>().spriteName = "Instruction01";
					SetButton_START();
					break;

				case TUTORIAL_STATE.SCENE0_Play00:
					this.buttonType = BUTTON_TYPE.NO_VIEW;
					this.messageLog[i].SetActive(false);
					this.guidSubVewer[i].SetActive(false);
					this.player[i*2].GetComponent<CPlayer>().m_controlePermission.rotate_y = true;
					this.player[i * 2 + 1].GetComponent<CPlayer>().m_controlePermission.rotate_y = true;
					
					// お互いがむいたらチェック
					if (GetCheck2ObjectDegree(this.player[i * 2], this.player[i * 2 + 1], 30))
					{
						this.buttonCheck[i * 2] = true;
						this.player[i * 2].GetComponent<CPlayer>().m_controlePermission.rotate_y = false;
					}
					if (GetCheck2ObjectDegree(this.player[i * 2 + 1], this.player[i * 2], 30))
					{
						this.buttonCheck[i * 2 + 1] = true;
						this.player[i * 2+1].GetComponent<CPlayer>().m_controlePermission.rotate_y = false;
					}

					break;

				case TUTORIAL_STATE.SCENE0_Message04:
					this.buttonType = BUTTON_TYPE.A;
					this.messageLog[i].SetActive(true);
					this.guidSubVewer[i].SetActive(false);
					message = this.MainMessage[6];
					this.messageLog[i].transform.FindChild("logs").GetComponent<UILabel>().text = message;
					SetButton_A();
					break;

				case TUTORIAL_STATE.SCENE0_Message05:
					message = this.MainMessage[7];
					this.messageLog[i].transform.FindChild("logs").GetComponent<UILabel>().text = message;
					SetButton_A();
					break;

				case TUTORIAL_STATE.SCENE1_Message00:
					message = this.MainMessage[8];
					this.messageLog[i].transform.FindChild("logs").GetComponent<UILabel>().text = message;
					SetButton_A();
					break;

				case TUTORIAL_STATE.SCENE1_Instruction00:
					this.buttonType = BUTTON_TYPE.START;
					this.messageLog[i].SetActive(false);
					this.guidSubVewer[i].SetActive(true);
					//this.guidSubVewer[i].GetComponent<UISprite>().spriteName = "Instruction02";
					SetButton_START();
					break;

				case TUTORIAL_STATE.SCENE1_Play00:
					this.buttonType = BUTTON_TYPE.NO_VIEW;
					this.messageLog[i].SetActive(false);
					this.guidSubVewer[i].SetActive(false);
					this.player[i*2].GetComponent<CPlayer>().m_controlePermission.rotate_x = true;
					this.player[i * 2 + 1].GetComponent<CPlayer>().m_controlePermission.rotate_x = true;
					this.player[i*2].GetComponent<CPlayer>().m_controlePermission.rotate_y = true;
					this.player[i * 2 + 1].GetComponent<CPlayer>().m_controlePermission.rotate_y = true;
					
					// 全員がボールを見たらチェック
					if (GetCheck2ObjectDegree(this.player[i * 2], this.ball, 10))
					{
						this.buttonCheck[i * 2] = true;
						this.player[i * 2].GetComponent<CPlayer>().m_controlePermission.rotate_x = false;
						this.player[i * 2].GetComponent<CPlayer>().m_controlePermission.rotate_y = false;
					}
					if (GetCheck2ObjectDegree(this.player[i * 2 + 1], this.ball, 10))
					{
						this.buttonCheck[i * 2 + 1] = true;
						this.player[i * 2 + 1].GetComponent<CPlayer>().m_controlePermission.rotate_x = false;
						this.player[i * 2 + 1].GetComponent<CPlayer>().m_controlePermission.rotate_y = false;
					}
					break;

				case TUTORIAL_STATE.SCENE1_Message01:
					this.buttonType = BUTTON_TYPE.A;
					this.messageLog[i].SetActive(true);
					this.guidSubVewer[i].SetActive(false);
					message = this.MainMessage[9];
					this.messageLog[i].transform.FindChild("logs").GetComponent<UILabel>().text = message;
					SetButton_A();
					break;

				case TUTORIAL_STATE.SCENE1_Instruction01:
					this.buttonType = BUTTON_TYPE.START;
					this.messageLog[i].SetActive(false);
					this.guidSubVewer[i].SetActive(true);
					this.guidSubVewer[i].GetComponent<UISprite>().spriteName = "Instruction02";
					SetButton_START();
					break;

				case TUTORIAL_STATE.SCENE1_Message02:
					this.buttonType = BUTTON_TYPE.START;
					this.messageLog[i].SetActive(true);
					this.guidSubVewer[i].SetActive(false);
					message = this.MainMessage[10];
					this.messageLog[i].transform.FindChild("logs").GetComponent<UILabel>().text = message;
					SetButton_A();
					break;

				case TUTORIAL_STATE.SCENE1_Message03:
					message = this.MainMessage[11];
					this.messageLog[i].transform.FindChild("logs").GetComponent<UILabel>().text = message;
					SetButton_A();
					break;

				case TUTORIAL_STATE.SCENE1_Instruction02:
					this.buttonType = BUTTON_TYPE.START;
					this.messageLog[i].SetActive(false);
					this.guidSubVewer[i].SetActive(true);
					this.guidSubVewer[i].GetComponent<UISprite>().spriteName = "Instruction03";
					SetButton_START();
					break;

				case TUTORIAL_STATE.SCENE1_Play01:
					this.buttonType = BUTTON_TYPE.NO_VIEW;
					this.messageLog[i].SetActive(false);
					this.guidSubVewer[i].SetActive(false);
					this.player[i * 2].GetComponent<CPlayer>().m_controlePermission.move_x   = true;
					this.player[i * 2].GetComponent<CPlayer>().m_controlePermission.move_z   = true;
					this.player[i * 2].GetComponent<CPlayer>().m_controlePermission.rotate_x = true;
					this.player[i * 2].GetComponent<CPlayer>().m_controlePermission.rotate_y = true;

					this.player[i * 2 + 1].GetComponent<CPlayer>().m_controlePermission.move_x   = true;
					this.player[i * 2 + 1].GetComponent<CPlayer>().m_controlePermission.move_z   = true;
					this.player[i * 2 + 1].GetComponent<CPlayer>().m_controlePermission.rotate_x = true;
					this.player[i * 2 + 1].GetComponent<CPlayer>().m_controlePermission.rotate_y = true;

					// ボールをとる
					if (this.ball.GetComponent<CSoccerBall>().m_isPlayer)
					{
						if (this.player[i * 2].GetComponent<CPlayer>().m_isGetBall == true || this.player[i * 2 + 1].GetComponent<CPlayer>().m_isGetBall == true) this.takeBallTeamNo = i;
						this.buttonCheck[i * 2] = true;
						this.buttonCheck[i * 2+1] = true;

						this.player[i * 2].GetComponent<CPlayer>().m_controlePermission.move_x   = false;
						this.player[i * 2].GetComponent<CPlayer>().m_controlePermission.move_z   = false;
						this.player[i * 2].GetComponent<CPlayer>().m_controlePermission.rotate_x = false;
						this.player[i * 2].GetComponent<CPlayer>().m_controlePermission.rotate_y = false;

						this.player[i * 2 + 1].GetComponent<CPlayer>().m_controlePermission.move_x   = false;
						this.player[i * 2 + 1].GetComponent<CPlayer>().m_controlePermission.move_z   = false;
						this.player[i * 2 + 1].GetComponent<CPlayer>().m_controlePermission.rotate_x = false;
						this.player[i * 2 + 1].GetComponent<CPlayer>().m_controlePermission.rotate_y = false;
					}
					break;

				case TUTORIAL_STATE.SCENE1_Message04:
					this.messageLog[i].SetActive(true);
					this.guidSubVewer[i].SetActive(false);

					if(this.takeBallTeamNo == i)
						 message = this.MainMessage[12];
					else message = this.MainMessage[13];

					this.messageLog[i].transform.FindChild("logs").GetComponent<UILabel>().text = message;
					SetButton_A();
					break;

				case TUTORIAL_STATE.SCENE1_Instruction03:
					this.messageLog[i].SetActive(false);
					this.guidSubVewer[i].SetActive(true);
					this.guidSubVewer[i].GetComponent<UISprite>().spriteName = "Instruction04";
					SetButton_START();
					break;

				case TUTORIAL_STATE.SCENE1_Play02:
					this.messageLog[i].SetActive(false);
					this.guidSubVewer[i].SetActive(false);
					this.player[i * 2].GetComponent<CPlayer>().m_controlePermission.move_x   = true;
					this.player[i * 2].GetComponent<CPlayer>().m_controlePermission.move_z   = true;
					this.player[i * 2].GetComponent<CPlayer>().m_controlePermission.rotate_x = true;
					this.player[i * 2].GetComponent<CPlayer>().m_controlePermission.rotate_y = true;
					this.player[i * 2].GetComponent<CPlayer>().m_controlePermission.shoote   = true;
					this.player[i * 2].GetComponent<CPlayer>().m_controlePermission.charge   = true;

					this.player[i * 2 + 1].GetComponent<CPlayer>().m_controlePermission.move_x   = true;
					this.player[i * 2 + 1].GetComponent<CPlayer>().m_controlePermission.move_z   = true;
					this.player[i * 2 + 1].GetComponent<CPlayer>().m_controlePermission.rotate_x = true;
					this.player[i * 2 + 1].GetComponent<CPlayer>().m_controlePermission.rotate_y = true;
					this.player[i * 2 + 1].GetComponent<CPlayer>().m_controlePermission.shoote   = true;
					this.player[i * 2 + 1].GetComponent<CPlayer>().m_controlePermission.charge   = true;

					if (this.player[i * 2].GetComponent<CPlayer>().m_isGetBall == true || this.player[i * 2 + 1].GetComponent<CPlayer>().m_isGetBall == true)
						if (this.takeBallTeamNo != i)
							for (int j = 0; j < 4; j++)
							{
								this.player[j].GetComponent<CPlayer>().m_controlePermission.move_x   = false;
								this.player[j].GetComponent<CPlayer>().m_controlePermission.move_z   = false;
								this.player[j].GetComponent<CPlayer>().m_controlePermission.rotate_x = false;
								this.player[j].GetComponent<CPlayer>().m_controlePermission.rotate_y = false;
								this.player[j].GetComponent<CPlayer>().m_controlePermission.shoote   = false;
								this.player[j].GetComponent<CPlayer>().m_controlePermission.charge   = false;
								this.buttonCheck[j] = true;
							}
					break;

				case TUTORIAL_STATE.SCENE1_Instruction04:
					this.messageLog[i].SetActive(false);
					this.guidSubVewer[i].SetActive(true);
					this.guidSubVewer[i].GetComponent<UISprite>().spriteName = "Instruction05";
					SetButton_START();
					break;

				case TUTORIAL_STATE.SCENE1_Instruction05:
					this.messageLog[i].SetActive(false);
					this.guidSubVewer[i].SetActive(true);
					this.guidSubVewer[i].GetComponent<UISprite>().spriteName = "Instruction06";
					SetButton_START();
					break;

				case TUTORIAL_STATE.SCENE1_Instruction06:
					this.messageLog[i].SetActive(false);
					this.guidSubVewer[i].SetActive(true);
					this.guidSubVewer[i].GetComponent<UISprite>().spriteName = "Instruction07";
					SetButton_START();
					break;

				case TUTORIAL_STATE.SCENE1_Play03:
					this.messageLog[i].SetActive(false);
					this.guidSubVewer[i].SetActive(false);

					this.player[i * 2].GetComponent<CPlayer>().m_controlePermission.move_x   = true;
					this.player[i * 2].GetComponent<CPlayer>().m_controlePermission.move_z   = true;
					this.player[i * 2].GetComponent<CPlayer>().m_controlePermission.rotate_x = true;
					this.player[i * 2].GetComponent<CPlayer>().m_controlePermission.rotate_y = true;
					this.player[i * 2].GetComponent<CPlayer>().m_controlePermission.shoote   = true;
					this.player[i * 2].GetComponent<CPlayer>().m_controlePermission.charge   = true;

					this.player[i * 2 + 1].GetComponent<CPlayer>().m_controlePermission.move_x   = true;
					this.player[i * 2 + 1].GetComponent<CPlayer>().m_controlePermission.move_z   = true;
					this.player[i * 2 + 1].GetComponent<CPlayer>().m_controlePermission.rotate_x = true;
					this.player[i * 2 + 1].GetComponent<CPlayer>().m_controlePermission.rotate_y = true;
					this.player[i * 2 + 1].GetComponent<CPlayer>().m_controlePermission.shoote   = true;
					this.player[i * 2 + 1].GetComponent<CPlayer>().m_controlePermission.charge   = true;

					this.time++;

					if (this.time >= 900)
					{
						for (int j = 0; j < 4; j++)
						{
							this.buttonCheck[j] = true;
							this.player[j].GetComponent<CPlayer>().m_controlePermission.move_x   = false;
							this.player[j].GetComponent<CPlayer>().m_controlePermission.move_z   = false;
							this.player[j].GetComponent<CPlayer>().m_controlePermission.rotate_x = false;
							this.player[j].GetComponent<CPlayer>().m_controlePermission.rotate_y = false;
							this.player[j].GetComponent<CPlayer>().m_controlePermission.shoote   = false;
							this.player[j].GetComponent<CPlayer>().m_controlePermission.charge   = false;
						}
					}


					break;

				case TUTORIAL_STATE.SCENE1_Message05:
					message = this.MainMessage[14];
					this.messageLog[i].transform.FindChild("logs").GetComponent<UILabel>().text = message;
					SetButton_A();
					break;

				//============================================================
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
		SetButton_Debug_SHIFT();
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

#else
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
	bool[] buttonCheck       = new bool[4];
	GameObject[] buttonVewer = new GameObject[4];
	GameObject[] bloackOut   = new GameObject[3];
	GameObject[] player = new GameObject[4];

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

	// Update is called once per frame
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
				// 仮

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
#endif
