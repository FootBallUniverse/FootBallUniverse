using UnityEngine;
using System.Collections;

public class Result : MonoBehaviour {
	// リザルト内のみの画面遷移管理用
	enum RESULT_STATE
	{
		ALPHA_IN,
		STAY_FIRST,
		ALPHA_TEAM_SUPPORTER,
		STAY_TEAM_SUPPORTER,
		MOVING_TEAM_SUPPORTER,
		ALPHA_WORLD_SUPPORTER,
		ADDING_WORLD_SUPPORTER,
		STAY_TWO,
		ALPHA_THANK_YOU,
		STAY_LAST,
		ALPHA_OUT,
		WAIT,
		STATE_MAX
	};

	private RESULT_STATE[] state      = new RESULT_STATE[2];
	private GameObject[,] SubPanels   = new GameObject[3,3];
	private GameObject[,] button      = new GameObject[2, 2];
	private int[] works               = new int[2];
	private bool[,] buttonCheck       = new bool[2,2];
	private int[,] suppoterBffByTeam  = new int[2, 2];
	private int[] suppoterBffByWorld  = new int[2];
	public  int AddSuppoterTime;
	private int count;
	public int  countMax = 120;

    public CSoundPlayer m_soundPlayer;

	//----------------------------------------------------------------------
	// コンストラクタ
	//----------------------------------------------------------------------
	// @Param   none
	// @Return  none
	// @Date    2014/10/29  @Update 2014/10/29  @Author T.Kawashita
	//          2014/11/15  @Update 2014/11/15  @Author T.Takeuchi
    //          2014/12/7   @Update 2014/12/7   @Author T.Kawashita
	//----------------------------------------------------------------------
	void Start() 
    {
		Init();

		// music
		m_soundPlayer = new CSoundPlayer();
        m_soundPlayer.PlayBGMFadeIn("result/bgm_01", 0.002f);
    }

	//----------------------------------------------------------------------
	// 初期化（試合結果を反映）
	//----------------------------------------------------------------------
	// @Param   none
	// @Return  none
	// @Date    2014/11/21  @Update 2014/11/21  @Author T.Takeuchi
	//----------------------------------------------------------------------
	void Init()
	{
		GameObject[] panels = new GameObject[3];

#if false // ドライバ
		TeamData.teamNationality[0] = TeamData.TEAM_NATIONALITY.JAPAN;
		TeamData.teamNationality[1] = TeamData.TEAM_NATIONALITY.ESPANA;

		TeamData.AddLog(0, 1, 0, 1, true);
		TeamData.AddLog(1, 2, 0, 1, true);
		TeamData.AddLog(1, 2, 0, 1, true);
		TeamData.AddLog(1, 2, 0, 1, true);
		TeamData.AddLog(1, 2, 0, 1, true);
		TeamData.AddLog(1, 2, 0, 1, true);
		TeamData.AddLog(1, 2, 0, 1, true);
		TeamData.AddLog(1, 2, 0, 1, true);
		TeamData.AddLog(1, 2, 0, 1, true);
		TeamData.AddLog(1, 2, 0, 1, true);
		TeamData.AddLog(1, 2, 0, 1, true);
		TeamData.AddLog(1, 2, 0, 1, true);
		TeamData.AddLog(2, 3, 1, 0, true);
		TeamData.AddLog(3, 4, 1, 0, true);
		TeamData.AddLog(4, 4, 1, 0, false);

		TeamData.suppoterByTeam[0] = 1234;
		TeamData.suppoterByTeam[1] = 5233;
		TeamData.suppoterByWorld = 100000000;
#endif // ドライバ＿END

		// パネルデータ読込
		panels[0]           = GameObject.Find("MainPanel")  as GameObject;
		panels[1]           = GameObject.Find("SubPanel0")  as GameObject;
		panels[2]           = GameObject.Find("SubPanel1")  as GameObject;
		this.SubPanels[0,0] = GameObject.Find("SubPanel00") as GameObject;
		this.SubPanels[0,1] = GameObject.Find("SubPanel01") as GameObject;
		this.SubPanels[0,2] = GameObject.Find("SubPanel02") as GameObject;
		this.SubPanels[1,0] = GameObject.Find("SubPanel10") as GameObject;
		this.SubPanels[1,1] = GameObject.Find("SubPanel11") as GameObject;
		this.SubPanels[1,2] = GameObject.Find("SubPanel12") as GameObject;

		// ボタンデータ読み込み
		this.button[0, 0] = panels[1].transform.FindChild("Next_0").gameObject;
		this.button[0, 1] = panels[1].transform.FindChild("Next_1").gameObject;
		this.button[1, 0] = panels[2].transform.FindChild("Next_0").gameObject;
		this.button[1, 1] = panels[2].transform.FindChild("Next_1").gameObject;

		for (int j = 0; j < 3; j++)
		{
			for (int i = 0; i < 2; i++)
			{
				// チーム得点
				panels[j].transform.FindChild("Score" + i).GetComponent<DrawNumber>().number = TeamData.GetTeamScore(i);
				if (TeamData.GetTeamScore(i) < 10) panels[j].transform.FindChild("Score" + i).transform.FindChild("num02").transform.localPosition = new Vector3(0.0f, 0.0f);
				else panels[j].transform.FindChild("Score" + i).transform.FindChild("num02").transform.localPosition = new Vector3(0.5f, 0.0f);

				// 国旗国名
				switch (TeamData.teamNationality[i])
				{
					case TeamData.TEAM_NATIONALITY.BRASIL:
						panels[j].transform.FindChild("Flag" + i).GetComponent<UISprite>().spriteName = "BRA_type1";
						panels[j].transform.FindChild("CountryName" + i).GetComponent<UISprite>().spriteName = "name_Brazil_"+i;
						break;
					case TeamData.TEAM_NATIONALITY.ENGLAND:
						panels[j].transform.FindChild("Flag" + i).GetComponent<UISprite>().spriteName = "ENG_type1";
						panels[j].transform.FindChild("CountryName" + i).GetComponent<UISprite>().spriteName = "name_England_" + i;
						break;
					case TeamData.TEAM_NATIONALITY.ESPANA:
						panels[j].transform.FindChild("Flag" + i).GetComponent<UISprite>().spriteName = "ESP_type1";
						panels[j].transform.FindChild("CountryName" + i).GetComponent<UISprite>().spriteName = "name_Spain_" + i;
						break;
					case TeamData.TEAM_NATIONALITY.JAPAN:
						panels[j].transform.FindChild("Flag" + i).GetComponent<UISprite>().spriteName = "JPN_type1";
						panels[j].transform.FindChild("CountryName" + i).GetComponent<UISprite>().spriteName = "name_Japan_" + i;
						break;
				}
			}

			// 結果表示
			switch (TeamData.GetWinTeamNo())
			{
				case 0:
					panels[j].transform.FindChild("VictoryLabel").GetComponent<UILabel>().text = "RED Team\n Victory";
					break;
				case 1:
					panels[j].transform.FindChild("VictoryLabel").GetComponent<UILabel>().text = "Blue Team\n Victory";
					break;
				case 2:
					panels[j].transform.FindChild("VictoryLabel").GetComponent<UILabel>().text = "Draw";
					break;
			}
		}

		// 数値初期化
		for (int i = 0; i < 2; i++)
		{
			this.state[i] = RESULT_STATE.ALPHA_IN;
			this.suppoterBffByWorld[i] = TeamData.suppoterByWorld;
			for (int j = 0; j < 2; j++)
				this.suppoterBffByTeam[i, j] = TeamData.suppoterByTeam[j];
			ReSetButtonCheck(i,true);
		}
		// TeamDataを統合、クリア
		TeamData.suppoterByWorld += (TeamData.suppoterByTeam[0] + TeamData.suppoterByTeam[1]);
		TeamData.suppoterByTeam[0] = TeamData.suppoterByTeam[1] = 0;
		// シュートログをクリア
		TeamData.ClearLog();
	}


	//----------------------------------------------------------------------
	// 更新
	//----------------------------------------------------------------------
	// @Param   none
	// @Return  none
	// @Date    2014/10/29  @Update 2014/10/29  @Author T.Kawashita
	//          2014/11/15  @Update 2014/11/15  @Author T.Takeuchi
	//          2014/11/28  @Update 2014/11/28  @Author T.Takeuichi
	//----------------------------------------------------------------------
	void Update()
	{
		// 表示
		this.SubPanels[0,0].transform.FindChild("NUM_Supporter_World").GetComponent<DrawNumber>().number = this.suppoterBffByWorld[0];
		this.SubPanels[0,1].transform.FindChild("NUM_Supporter_Team0").GetComponent<DrawNumber>().number = this.suppoterBffByTeam[0,0];
		this.SubPanels[0,1].transform.FindChild("NUM_Supporter_Team1").GetComponent<DrawNumber>().number = this.suppoterBffByTeam[0,1];
		this.SubPanels[1,0].transform.FindChild("NUM_Supporter_World").GetComponent<DrawNumber>().number = this.suppoterBffByWorld[1];
		this.SubPanels[1,1].transform.FindChild("NUM_Supporter_Team0").GetComponent<DrawNumber>().number = this.suppoterBffByTeam[1,0];
		this.SubPanels[1,1].transform.FindChild("NUM_Supporter_Team1").GetComponent<DrawNumber>().number = this.suppoterBffByTeam[1,1];

		// ボタンチェック
		if(Input.GetKeyDown(InputXBOX360.P1_XBOX_A)) this.buttonCheck[0,0] = true;
		if(Input.GetKeyDown(InputXBOX360.P2_XBOX_A)) this.buttonCheck[0,1] = true;
		if(Input.GetKeyDown(InputXBOX360.P3_XBOX_A)) this.buttonCheck[1,0] = true;
		if(Input.GetKeyDown(InputXBOX360.P4_XBOX_A)) this.buttonCheck[1,1] = true;
		if(Input.GetKeyDown(KeyCode.LeftShift))      this.buttonCheck[0,0] = this.buttonCheck[0,1] = true;
		if(Input.GetKeyDown(KeyCode.RightShift))     this.buttonCheck[1,0] = this.buttonCheck[1,1] = true;

		// ボタン表示
		for (int i = 0; i < 2; i++)
			for (int j = 0; j < 2; j++)
			{
				if (this.buttonCheck[i, j]) this.button[i, j].SetActive(false);
				else                        this.button[i, j].SetActive(true);
			}
		// 遷移（左右画面共有）
		switch (this.state[0])
		{
			// 最初のフェードイン
			case RESULT_STATE.ALPHA_IN:
				if (GameObject.Find("FeedPanel").GetComponent<TweenAlpha>().enabled == false)
				{
					this.state[0] = RESULT_STATE.STAY_FIRST;
					this.state[1] = RESULT_STATE.STAY_FIRST;
					ReSetButtonCheck(0, false);
					ReSetButtonCheck(1, false);
				}
				break;

			// 最終待機
			case RESULT_STATE.STAY_LAST:
				if (this.state[0] == this.state[1])
				{
					this.state[0] = RESULT_STATE.WAIT;
					this.state[1] = RESULT_STATE.WAIT;
					this.count    = 0;
				}
				break;

			// 
			case RESULT_STATE.WAIT:
				this.count++;

				if (this.count > this.countMax)
				{
					this.state[0] = RESULT_STATE.ALPHA_OUT;
					this.state[1] = RESULT_STATE.ALPHA_OUT;
					GameObject.Find("FeedPanel").GetComponent<TweenAlpha>().Play(false);
					m_soundPlayer.PlayBGMFadeOut(0.004f);
				}
				break;

			// フェードアウト
			case RESULT_STATE.ALPHA_OUT:
				if (GameObject.Find("FeedPanel").GetComponent<TweenAlpha>().enabled == false)
					Application.LoadLevel("Title");
				break;
		}

		// 遷移（左右画面別々）
		for (int i = 0; i < 2; i++)
		{
			switch (this.state[i])
			{
				// 待機その１
				case RESULT_STATE.STAY_FIRST:
					if (this.buttonCheck[i,0] == true && this.buttonCheck[i,1] == true)
					{
						this.SubPanels[i,1].GetComponent<TweenAlpha>().enabled = true;
						this.state[i] = RESULT_STATE.ALPHA_TEAM_SUPPORTER;
					}
					break;

				// チームサポーターがフェードイン
				case RESULT_STATE.ALPHA_TEAM_SUPPORTER:
					if (this.SubPanels[i,1].GetComponent<TweenAlpha>().enabled == false)
					{
						this.state[i] = RESULT_STATE.STAY_TEAM_SUPPORTER;
						ReSetButtonCheck(i, false);
					}
					break;

				// チームサポーターが表示状態で待機
				case RESULT_STATE.STAY_TEAM_SUPPORTER:
					if (this.buttonCheck[i,0] == true && this.buttonCheck[i,1] == true)
					{
						this.SubPanels[i,1].GetComponent<TweenPosition>().enabled = true;
						this.state[i] = RESULT_STATE.MOVING_TEAM_SUPPORTER;
					}
					break;

				// チームサポーターが上へ移動
				case RESULT_STATE.MOVING_TEAM_SUPPORTER:
					if (this.SubPanels[i,1].GetComponent<TweenPosition>().enabled == false)
					{
						this.SubPanels[i,0].GetComponent<TweenAlpha>().enabled = true;
						this.state[i] = RESULT_STATE.ALPHA_WORLD_SUPPORTER;
					}
					break;

				// 全サポーターがフェードイン
				case RESULT_STATE.ALPHA_WORLD_SUPPORTER:
					if (this.SubPanels[0,0].GetComponent<TweenAlpha>().enabled == false)
					{
						this.state[i] = RESULT_STATE.ADDING_WORLD_SUPPORTER;
						this.SubPanels[i,0].GetComponent<TweenScale>().Play(true);
						works[0] = this.suppoterBffByTeam[i,0] / this.AddSuppoterTime;
						works[1] = this.suppoterBffByTeam[i,1] / this.AddSuppoterTime;
					}
					break;

				// 全サポーターに加算
				case RESULT_STATE.ADDING_WORLD_SUPPORTER:
					if (this.suppoterBffByTeam[i,0] == 0 && this.suppoterBffByTeam[i,1] == 0)
					{
						this.state[i] = RESULT_STATE.STAY_TWO;
						ReSetButtonCheck(i, false);
						this.SubPanels[i,0].GetComponent<TweenScale>().enabled = false;
					}else{
						for (int j = 0; j < 2; j++)
						{
							if (this.suppoterBffByTeam[i,j] >= works[j])
							{
								this.suppoterBffByTeam[i,j] -= works[j];
								this.suppoterBffByWorld[i] += works[j];
							}else if (this.suppoterBffByTeam[i,j] != 0){
								this.suppoterBffByTeam[i,j]--;
								this.suppoterBffByWorld[i]++;
							}
						}
					}
					break;

				// 全サポーターが加算終了し、待機中
				case RESULT_STATE.STAY_TWO:
					if (this.buttonCheck[i,0] == true && this.buttonCheck[i,1] == true)
					{
						this.state[i] = RESULT_STATE.ALPHA_THANK_YOU;
						this.SubPanels[i,2].GetComponent<TweenAlpha>().enabled = true;
					}
					break;

				// Thank you for Playing!! がフェードイン
				case RESULT_STATE.ALPHA_THANK_YOU:
					if (this.SubPanels[i,2].GetComponent<TweenAlpha>().enabled == false)
						this.state[i] = RESULT_STATE.STAY_LAST;
					break;
			}
		}

#if false // デバッグ用スペースキーが押されたら強制的にタイトル画面へ
		if (Input.GetKeyDown(KeyCode.Space) ||
			InputXBOX360.IsGetAllStartButton() )
		{ 
			// ここにタイトル画面に遷移する時のアニメーションを書く
			// 今は強制的に画面を遷移
			Application.LoadLevel("Title");
			Debug.Log("Title画面に遷移");
		}
#endif
	}


	//----------------------------------------------------------------------
	// ボタンチェックを初期化
	//----------------------------------------------------------------------
	// @Param   no     0:左画面/1:右画面
	// @Param   check  true:ボタンがおされた/false:ボタンがおされていない
	// @Return  none
	// @Date    2014/12/08  @Update 2014/12/08  @Author T.Takeuichi
	//----------------------------------------------------------------------
	void ReSetButtonCheck(int no,bool check)
	{
		for(int i = 0; i < 2; i++) this.buttonCheck[no,i] = check;
	}
}

// End of File