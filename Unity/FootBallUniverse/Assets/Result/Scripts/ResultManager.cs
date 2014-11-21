using UnityEngine;
using System.Collections;

public class ResultManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Init();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Init()
	{
		// チーム得点・国旗設置
		for (int i = 0; i < 2; i++)
		{
			GameObject.Find("MainPanel").transform.FindChild("Score" + i).GetComponent<DrawNumber>().number = TeamData.teamScore[i];
			GameObject.Find("SubPanel0").transform.FindChild("Score" + i).GetComponent<DrawNumber>().number = TeamData.teamScore[i];
			GameObject.Find("SubPanel1").transform.FindChild("Score" + i).GetComponent<DrawNumber>().number = TeamData.teamScore[i];
			switch (TeamData.teamNationality[i])
			{
				case TeamData.TEAM_NATIONALITY.BRASIL:
					GameObject.Find("MainPanel").transform.FindChild("Flag" + i).GetComponent<UISprite>().spriteName = "BRA";
					GameObject.Find("SubPanel0").transform.FindChild("Flag" + i).GetComponent<UISprite>().spriteName = "BRA";
					GameObject.Find("SubPanel1").transform.FindChild("Flag" + i).GetComponent<UISprite>().spriteName = "BRA";
					break;
				case TeamData.TEAM_NATIONALITY.ENGLAND:
					GameObject.Find("MainPanel").transform.FindChild("Flag" + i).GetComponent<UISprite>().spriteName = "ENG";
					GameObject.Find("SubPanel0").transform.FindChild("Flag" + i).GetComponent<UISprite>().spriteName = "ENG";
					GameObject.Find("SubPanel1").transform.FindChild("Flag" + i).GetComponent<UISprite>().spriteName = "ENG";
					break;
				case TeamData.TEAM_NATIONALITY.ESPANA:
					GameObject.Find("MainPanel").transform.FindChild("Flag" + i).GetComponent<UISprite>().spriteName = "ESP";
					GameObject.Find("SubPanel0").transform.FindChild("Flag" + i).GetComponent<UISprite>().spriteName = "ESP";
					GameObject.Find("SubPanel1").transform.FindChild("Flag" + i).GetComponent<UISprite>().spriteName = "ESP";
					break;
				case TeamData.TEAM_NATIONALITY.JAPAN:
					GameObject.Find("MainPanel").transform.FindChild("Flag" + i).GetComponent<UISprite>().spriteName = "JPN";
					GameObject.Find("SubPanel0").transform.FindChild("Flag" + i).GetComponent<UISprite>().spriteName = "JPN";
					GameObject.Find("SubPanel1").transform.FindChild("Flag" + i).GetComponent<UISprite>().spriteName = "JPN";
					break;
			}
		}


		// プレイヤー得点
		for (int i = 0; i < 2; i++)
		{
			for (int j = 0; j < 2; j++)
			{
				//GameObject.Find("Player" + i + "Score").GetComponent<DrawNumber>().number = TeamData.playerScore[i, j];
			}
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
	}
}
