using UnityEngine;
using System.Collections;

public class Result : MonoBehaviour {
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
		GameObject[] panels = new GameObject[5];

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
		panels[0] = GameObject.Find("MainPanel") as GameObject;
		panels[1] = GameObject.Find("SubPanel0") as GameObject;
		panels[2] = GameObject.Find("SubPanel1") as GameObject;
		panels[3] = GameObject.Find("SubPanel2") as GameObject;

		// サポーター数読み込み
		panels[3].transform.FindChild("Suppoter_World").transform.FindChild("NUM_Supporter_World").GetComponent<DrawNumber>().number = (int)TeamData.suppoterByWorld;
		panels[3].transform.FindChild("Suppoter_Team").transform.FindChild("NUM_Supporter_Team0").GetComponent<DrawNumber>().number = (int)TeamData.suppoterByTeam[0];
		panels[3].transform.FindChild("Suppoter_Team").transform.FindChild("NUM_Supporter_Team1").GetComponent<DrawNumber>().number = (int)TeamData.suppoterByTeam[1];

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
		/*
		GameObject logPrefab = Resources.Load("Prefab/Result/logPrefab") as GameObject;
		GameObject[] panels  = new GameObject[3];
		int[] logNo = new int[2] { 0, 0 };

		// TEST_DATA
		TeamData.AddLog(0, 1, 0, true);
		TeamData.AddLog(1, 2, 0, true);
		TeamData.AddLog(2, 3, 1, true);
		TeamData.AddLog(3, 4, 1, true);
		TeamData.AddLog(4, 4, 1, false);


		// パネルデータ読込
		panels[0] = GameObject.Find("MainPanel") as GameObject;
		panels[1] = GameObject.Find("SubPanel0") as GameObject;
		panels[2] = GameObject.Find("SubPanel1") as GameObject;

		for (int i = 0; i < 2; i++)
		{
			// チーム得点・国旗設置
			panels[0].transform.FindChild("Score" + i).GetComponent<DrawNumber>().number = TeamData.teamScore[i];
			panels[1].transform.FindChild("Score" + i).GetComponent<DrawNumber>().number = TeamData.teamScore[i];
			panels[2].transform.FindChild("Score" + i).GetComponent<DrawNumber>().number = TeamData.teamScore[i];

			switch (TeamData.teamNationality[i])
			{
				case TeamData.TEAM_NATIONALITY.BRASIL:
					panels[0].transform.FindChild("Flag" + i).GetComponent<UISprite>().spriteName = "BRA";
					panels[1].transform.FindChild("Flag" + i).GetComponent<UISprite>().spriteName = "BRA";
					panels[2].transform.FindChild("Flag" + i).GetComponent<UISprite>().spriteName = "BRA";
					break;
				case TeamData.TEAM_NATIONALITY.ENGLAND:
					panels[0].transform.FindChild("Flag" + i).GetComponent<UISprite>().spriteName = "ENG";
					panels[1].transform.FindChild("Flag" + i).GetComponent<UISprite>().spriteName = "ENG";
					panels[2].transform.FindChild("Flag" + i).GetComponent<UISprite>().spriteName = "ENG";
					break;
				case TeamData.TEAM_NATIONALITY.ESPANA:
					panels[0].transform.FindChild("Flag" + i).GetComponent<UISprite>().spriteName = "ESP";
					panels[1].transform.FindChild("Flag" + i).GetComponent<UISprite>().spriteName = "ESP";
					panels[2].transform.FindChild("Flag" + i).GetComponent<UISprite>().spriteName = "ESP";
					break;
				case TeamData.TEAM_NATIONALITY.JAPAN:
					panels[0].transform.FindChild("Flag" + i).GetComponent<UISprite>().spriteName = "JPN";
					panels[1].transform.FindChild("Flag" + i).GetComponent<UISprite>().spriteName = "JPN";
					panels[2].transform.FindChild("Flag" + i).GetComponent<UISprite>().spriteName = "JPN";
					break;
			}

			for (int j = 0; j < 2; j++)
			{
				// プレイヤーごとの得点数表示
				//GameObject.Find("Player" + i + "Score").GetComponent<DrawNumber>().number = TeamData.playerScore[i, j];
			}
		}

		// ログ表示
		for (int i = 0; i < TeamData.GetCountLog(); i++)
		{
			GameObject[] logObject = new GameObject[2];
			Vector3 workVec;

			// シュートに成功していなかった場合（飛ばす）
			if (TeamData.GetLogData(i).isGole != true) continue;

			// シュートに成功した場合
			// ログ作成（情報セット）
			logObject[0] = Instantiate(logPrefab) as GameObject;
			logObject[1] = Instantiate(logPrefab) as GameObject;
			// 親オブジェクト指定
			logObject[0].transform.parent = panels[1].transform.FindChild("Logs").transform;
			logObject[1].transform.parent = panels[2].transform.FindChild("Logs").transform;
			//座標
			workVec = new Vector3(-240, -25, 0);
			if (TeamData.GetLogData(i).teamNo == 1) workVec.x *= -1;
			workVec.y -= 30 * logNo[TeamData.GetLogData(i).teamNo];

			logObject[0].transform.localPosition = workVec;
			logObject[1].transform.localPosition = workVec;
			// スケール
			logObject[0].transform.localScale    = new Vector3(26, 26, 26);
			logObject[1].transform.localScale    = new Vector3(26, 26, 26);
			// ログ名変更
			logObject[0].name = "T" + TeamData.GetLogData(i).teamNo + "_No" + logNo[TeamData.GetLogData(i).teamNo];
			logObject[1].name = "T" + TeamData.GetLogData(i).teamNo + "_No" + logNo[TeamData.GetLogData(i).teamNo];
			// テキスト変更
			logObject[0].GetComponent<UILabel>().text = "Player" + TeamData.GetLogData(i).playerNo + "     " + TeamData.GetLogData(i).time + ":" + 0;
			logObject[1].GetComponent<UILabel>().text = "Player" + TeamData.GetLogData(i).playerNo + "     " + TeamData.GetLogData(i).time + ":" + 0;
			// ログNo加算
			logNo[TeamData.GetLogData(i).teamNo]++;
		}
		 */

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
	//----------------------------------------------------------------------
	void Update()
	{

		// デバッグ用スペースキーが押されたら強制的にタイトル画面へ
		if (Input.GetKeyDown(KeyCode.Space) ||
			InputXBOX360.IsGetAllStartButton() )
		{ 
			// ここにタイトル画面に遷移する時のアニメーションを書く
			// 今は強制的に画面を遷移
			Application.LoadLevel("Title");
			Debug.Log("Title画面に遷移");
		}
	}
}

// End of File