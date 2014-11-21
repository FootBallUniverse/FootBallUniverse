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
		// チーム得点・国旗設置
		for (int i = 0; i < 2; i++)
		{
			GameObject.Find("MainPanel").transform.FindChild("Score" + i).GetComponent<DrawNumber>().number = TeamData.teamScore[i];
			GameObject.Find("SubPanel0").transform.FindChild("Score" + i).GetComponent<DrawNumber>().number = TeamData.teamScore[i];
			//GameObject.Find("SubPanel1").transform.FindChild("Score" + i).GetComponent<DrawNumber>().number = TeamData.teamScore[i];

			switch (TeamData.teamNationality[i])
			{
				case TeamData.TEAM_NATIONALITY.BRASIL:
					GameObject.Find("MainPanel").transform.FindChild("Flag" + i).GetComponent<UISprite>().spriteName = "BRA";
					GameObject.Find("SubPanel0").transform.FindChild("Flag" + i).GetComponent<UISprite>().spriteName = "BRA";
					//GameObject.Find("SubPanel1").transform.FindChild("Flag" + i).GetComponent<UISprite>().spriteName = "BRA";
					break;
				case TeamData.TEAM_NATIONALITY.ENGLAND:
					GameObject.Find("MainPanel").transform.FindChild("Flag" + i).GetComponent<UISprite>().spriteName = "ENG";
					GameObject.Find("SubPanel0").transform.FindChild("Flag" + i).GetComponent<UISprite>().spriteName = "ENG";
					//GameObject.Find("SubPanel1").transform.FindChild("Flag" + i).GetComponent<UISprite>().spriteName = "ENG";
					break;
				case TeamData.TEAM_NATIONALITY.ESPANA:
					GameObject.Find("MainPanel").transform.FindChild("Flag" + i).GetComponent<UISprite>().spriteName = "ESP";
					GameObject.Find("SubPanel0").transform.FindChild("Flag" + i).GetComponent<UISprite>().spriteName = "ESP";
					//GameObject.Find("SubPanel1").transform.FindChild("Flag" + i).GetComponent<UISprite>().spriteName = "ESP";
					break;
				case TeamData.TEAM_NATIONALITY.JAPAN:
					GameObject.Find("MainPanel").transform.FindChild("Flag" + i).GetComponent<UISprite>().spriteName = "JPN";
					GameObject.Find("SubPanel0").transform.FindChild("Flag" + i).GetComponent<UISprite>().spriteName = "JPN";
					//GameObject.Find("SubPanel1").transform.FindChild("Flag" + i).GetComponent<UISprite>().spriteName = "JPN";
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
		/*
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
		 * */
	}


	//----------------------------------------------------------------------
	// 更新
	//----------------------------------------------------------------------
	// @Param   none
	// @Return  none
	// @Date    2014/10/29  @Update 2014/10/29  @Author T.Kawashita
	//          2014/11/15  @Update 2014/11/15  @Author T.Takeuchi
	//----------------------------------------------------------------------
	void Update () {

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