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
	void Start (){
		
	}

	void Init()
	{
		// チーム得点・国旗設置
		for (int i = 0; i < 3; i++)
		{
			GameObject.Find("Score" + i).GetComponent<DrawNumber>().number = 3;// = CGameManager.m_isPoint[i];
			switch (TeamData.teamNationality[i])
			{
				case TeamData.TEAM_NATIONALITY.BRASIL:
					GameObject.Find("Flag" + i).GetComponent<UISprite>().spriteName = "BRA";
					break;
				case TeamData.TEAM_NATIONALITY.ENGLAND:
					GameObject.Find("Flag" + i).GetComponent<UISprite>().spriteName = "ENG";
					break;
				case TeamData.TEAM_NATIONALITY.ESPANA:
					GameObject.Find("Flag" + i).GetComponent<UISprite>().spriteName = "ESP";
					break;
				case TeamData.TEAM_NATIONALITY.JAPAN:
					GameObject.Find("Flag" + i).GetComponent<UISprite>().spriteName = "JPN";
					break;
			}
		}
		// プレイヤー得点
		for (int i = 1; i < 5; i++)
		{
			GameObject.Find("Player" + i + "Score").GetComponent<DrawNumber>().number = CGameManager.m_playerPoint[i - 1];
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


	//----------------------------------------------------------------------
	// 更新
	//----------------------------------------------------------------------
	// @Param   none
	// @Return  none
	// @Date    2014/10/29  @Update 2014/10/29  @Author T.Kawashita
	//          2014/11/15  @Update 2014/11/15  @Author T.Takeuchi
	//----------------------------------------------------------------------
	void Update () {

		Init();

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
