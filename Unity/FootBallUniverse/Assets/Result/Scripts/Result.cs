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
		STAY_LAST,
		ALPHA_OUT,
		STATE_MAX
	};

	private RESULT_STATE state     = RESULT_STATE.ALPHA_IN;
	private GameObject[] SubPanels = new GameObject[4];
	private int[] works            = new int[2];
	public  int AddSuppoterTime;

	//----------------------------------------------------------------------
	// コンストラクタ
	//----------------------------------------------------------------------
	// @Param   none
	// @Return  none
	// @Date    2014/10/29  @Update 2014/10/29  @Author T.Kawashita
	//          2014/11/15  @Update 2014/11/15  @Author T.Takeuchi
	//----------------------------------------------------------------------
	void Start() { Init(); }

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

		// ドライバ
		TeamData.teamNationality[0] = TeamData.TEAM_NATIONALITY.JAPAN;
		TeamData.teamNationality[1] = TeamData.TEAM_NATIONALITY.ESPANA;

		TeamData.AddLog(0, 1, 0, true);
		TeamData.AddLog(1, 2, 0, true);
		TeamData.AddLog(1, 2, 0, true);
		TeamData.AddLog(1, 2, 0, true);
		TeamData.AddLog(1, 2, 0, true);
		TeamData.AddLog(1, 2, 0, true);
		TeamData.AddLog(1, 2, 0, true);
		TeamData.AddLog(1, 2, 0, true);
		TeamData.AddLog(1, 2, 0, true);
		TeamData.AddLog(1, 2, 0, true);
		TeamData.AddLog(1, 2, 0, true);
		TeamData.AddLog(1, 2, 0, true);
		TeamData.AddLog(2, 3, 1, true);
		TeamData.AddLog(3, 4, 1, true);
		TeamData.AddLog(4, 4, 1, false);

		TeamData.suppoterByTeam[0] = 1234;
		TeamData.suppoterByTeam[1] = 5233;
		TeamData.suppoterByWorld = 100000000;
		// ドライバ＿END

		// パネルデータ読込
		panels[0]    = GameObject.Find("MainPanel") as GameObject;
		panels[1]    = GameObject.Find("SubPanel0") as GameObject;
		panels[2]    = GameObject.Find("SubPanel1") as GameObject;
		SubPanels[0] = GameObject.Find("SubPanel00") as GameObject;
		SubPanels[1] = GameObject.Find("SubPanel01") as GameObject;
		SubPanels[2] = GameObject.Find("SubPanel10") as GameObject;
		SubPanels[3] = GameObject.Find("SubPanel11") as GameObject;


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
						panels[j].transform.FindChild("CountryName" + i).GetComponent<UILabel>().text = "ブラジル";
						break;
					case TeamData.TEAM_NATIONALITY.ENGLAND:
						panels[j].transform.FindChild("Flag" + i).GetComponent<UISprite>().spriteName = "ENG_type1";
						panels[j].transform.FindChild("CountryName" + i).GetComponent<UILabel>().text = "イングランド";
						break;
					case TeamData.TEAM_NATIONALITY.ESPANA:
						panels[j].transform.FindChild("Flag" + i).GetComponent<UISprite>().spriteName = "ESP_type1";
						panels[j].transform.FindChild("CountryName" + i).GetComponent<UILabel>().text = "スペイン";
						break;
					case TeamData.TEAM_NATIONALITY.JAPAN:
						panels[j].transform.FindChild("Flag" + i).GetComponent<UISprite>().spriteName = "JPN_type1";
						panels[j].transform.FindChild("CountryName" + i).GetComponent<UILabel>().text = "日本";
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
					panels[j].transform.FindChild("VictoryLabel").GetComponent<UILabel>().text = "Lose...";
					break;
			}
		}

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
		this.SubPanels[0].transform.FindChild("NUM_Supporter_World").GetComponent<DrawNumber>().number = TeamData.suppoterByWorld;
		this.SubPanels[1].transform.FindChild("NUM_Supporter_Team0").GetComponent<DrawNumber>().number = TeamData.suppoterByTeam[0];
		this.SubPanels[1].transform.FindChild("NUM_Supporter_Team1").GetComponent<DrawNumber>().number = TeamData.suppoterByTeam[1];
		this.SubPanels[2].transform.FindChild("NUM_Supporter_World").GetComponent<DrawNumber>().number = TeamData.suppoterByWorld;
		this.SubPanels[3].transform.FindChild("NUM_Supporter_Team0").GetComponent<DrawNumber>().number = TeamData.suppoterByTeam[0];
		this.SubPanels[3].transform.FindChild("NUM_Supporter_Team1").GetComponent<DrawNumber>().number = TeamData.suppoterByTeam[1];

		switch (this.state)
		{
			case RESULT_STATE.ALPHA_IN:
				if (GameObject.Find("FeedPanel").GetComponent<TweenAlpha>().enabled == false)
				{
					this.state = RESULT_STATE.STAY_FIRST;
				}
				break;
			case RESULT_STATE.STAY_FIRST:
				if (Input.GetKeyDown(KeyCode.Space))
				{
					this.SubPanels[1].GetComponent<TweenAlpha>().enabled = true;
					this.SubPanels[3].GetComponent<TweenAlpha>().enabled = true;
					this.state = RESULT_STATE.ALPHA_TEAM_SUPPORTER;
				}
				break;

			case RESULT_STATE.ALPHA_TEAM_SUPPORTER:
				if (this.SubPanels[1].GetComponent<TweenAlpha>().enabled == false)
				{
					this.state = RESULT_STATE.STAY_TEAM_SUPPORTER;
				}
				break;

			case RESULT_STATE.STAY_TEAM_SUPPORTER:
				if (Input.GetKeyDown(KeyCode.Space))
				{
					this.SubPanels[1].GetComponent<TweenPosition>().enabled = true;
					this.SubPanels[3].GetComponent<TweenPosition>().enabled = true;
					this.state = RESULT_STATE.MOVING_TEAM_SUPPORTER;
				}
				break;

			case RESULT_STATE.MOVING_TEAM_SUPPORTER:
				if (this.SubPanels[1].GetComponent<TweenPosition>().enabled == false)
				{
					this.SubPanels[0].GetComponent<TweenAlpha>().enabled = true;
					this.SubPanels[2].GetComponent<TweenAlpha>().enabled = true;
					this.state = RESULT_STATE.ALPHA_WORLD_SUPPORTER;
				}
				break;

			case RESULT_STATE.ALPHA_WORLD_SUPPORTER:
				if (this.SubPanels[0].GetComponent<TweenAlpha>().enabled == false)
				{
					this.state = RESULT_STATE.ADDING_WORLD_SUPPORTER;
					this.SubPanels[0].GetComponent<TweenScale>().Play(true);
					this.SubPanels[2].GetComponent<TweenScale>().Play(true);
					works[0] = TeamData.suppoterByTeam[0] / this.AddSuppoterTime;
					works[1] = TeamData.suppoterByTeam[1] / this.AddSuppoterTime;
				}
				break;

			case RESULT_STATE.ADDING_WORLD_SUPPORTER:
				if (TeamData.suppoterByTeam[0] == 0 && TeamData.suppoterByTeam[1] == 0)
				{
					this.state = RESULT_STATE.STAY_LAST;
					this.SubPanels[0].GetComponent<TweenScale>().enabled = false;
					this.SubPanels[2].GetComponent<TweenScale>().enabled = false;
				}
				else
				{
					for (int i = 0; i < 2; i++)
					{
						if (TeamData.suppoterByTeam[i] >= works[i])
						{
							TeamData.suppoterByTeam[i] -= works[i];
							TeamData.suppoterByWorld   += works[i];
						}
						else if (TeamData.suppoterByTeam[i] != 0)
						{
							TeamData.suppoterByTeam[i] --;
							TeamData.suppoterByWorld   ++;
						}
					}
				}
				break;

			case RESULT_STATE.STAY_LAST:
				if (Input.GetKeyDown(KeyCode.Space))
				{
					GameObject.Find("FeedPanel").GetComponent<TweenAlpha>().Play(false);
					this.state = RESULT_STATE.ALPHA_OUT;
				}
				break;

			case RESULT_STATE.ALPHA_OUT:
				if (GameObject.Find("FeedPanel").GetComponent<TweenAlpha>().enabled == false)
				{
					Application.LoadLevel("Title");
					Debug.Log("Title画面に遷移");
				}
				break;
		}

#if false
		// デバッグ用スペースキーが押されたら強制的にタイトル画面へ
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
}

// End of File