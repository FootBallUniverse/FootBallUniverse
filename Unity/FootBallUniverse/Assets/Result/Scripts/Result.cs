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
		GameObject logPrefab = Resources.Load("Prefab/Result/logPrefab") as GameObject;
		GameObject[] panels  = new GameObject[3];

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
		
#if false
		// ログ表示
		for (int i = 0; i < TeamData.logs.Count; i++)
		{
			TeamData.SHOOT_LOG log;
			GameObject logObject;
			int[] logNo = new int[2]{0,0};

			log = (TeamData.SHOOT_LOG)TeamData.logs[i];
			// シュートに成功していなかった場合
			if (log.isGole != true) continue;
			// シュートに成功した場合
			logObject = Instantiate(logPrefab) as GameObject;
			logObject.GetComponent<UILabel>().text = "Player" + log.playerNo + "     " + 0 + ":" + 0;
		}

		// どっちか買ったか判定
		if (CGameManager.m_isPoint[0] > CGameManager.m_isPoint[1])
		{
			// 左チーム勝利
		}
		else if (CGameManager.m_isPoint[0] < CGameManager.m_isPoint[1])
		{
			// 右チーム勝利
		}
		else
		{
			// 引き分け
		}
#endif
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