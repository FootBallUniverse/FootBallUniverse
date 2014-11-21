﻿using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;

public class TeamData
{
	// 国籍表
	public enum TEAM_NATIONALITY{
		JAPAN,
		ESPANA,
		ENGLAND,
		BRASIL,
		NATIONALITY_MAX
	};

	// ゴールログ
	public struct SHOOT_LOG
	{
		public int  time;     // ゴールした時間
		public int  playerNo; // プレイヤーNo
		public int  teamNo;   // プレイヤーのチームNo
		public bool isGole;   // ゴールに成功したか
	};

	// シングルトン用
	private static TeamData teamData = new TeamData();

	// 各種データ（デバッグ時エラー防止のため初期値が入っています）
	public static TEAM_NATIONALITY[] teamNationality = new TEAM_NATIONALITY[2] {TEAM_NATIONALITY.JAPAN,TEAM_NATIONALITY.BRASIL}; // 国籍
	public static int[] teamScore    = new int[2]{10,11};             // チームスコア (削除予定）
	public static int[,] playerScore = new int[2,2]{{10,11},{12,13}}; // プレイヤー別スコア（削除予定）
	private static ArrayList logs    = new ArrayList();               // ログデータ格納（カプセル化）

	//================以下、シュートログ操作メソッド================
	//----------------------------------------------------------------------
	// シュートログデータを追加する
	//----------------------------------------------------------------------
	// @Param   time     シュートした時間
	// @Param   playerNo プレイヤーNo
	// @Param   teamNo   チームNo
	// @Param   isGole   ゴールしたかどうか(true 成功した / false 失敗した（シュートを止められた）
	// @Return  none
	// @Date    2014/11/21  @Update 2014/11/21  @Author T.Takeuchi
	//----------------------------------------------------------------------
	public void AddLog(int time,int playerNo,int teamNo,bool isGole)
	{
		// 入力ログが規定値かチェック
		// 未実装

		// ログデータを作成しリストに挿入
		SHOOT_LOG newData = new SHOOT_LOG();
		newData.time      = time;
		newData.playerNo  = playerNo;
		newData.teamNo    = teamNo;
		newData.isGole    = isGole;
		logs.Add(newData);
	}

	//----------------------------------------------------------------------
	// プレイヤー別スコア出力
	//----------------------------------------------------------------------
	// @Param   playerNo プレイヤーNo
	// @Return  int      そのプレイヤーが得点した点数
	// @Date    2014/11/21  @Update 2014/11/21  @Author T.Takeuchi
	//----------------------------------------------------------------------
	public int GetPlayerScore(int playerNo)
	{
		SHOOT_LOG logData;
		int score = 0;
		for (int i = 0; i < logs.Count; i++)
		{
			logData = (SHOOT_LOG)logs[i];
			if (logData.playerNo == playerNo &&
			    logData.isGole   == true)
			{
				score++;
			}
		}

		return score;
	}

	//----------------------------------------------------------------------
	// チーム点数を取得
	//----------------------------------------------------------------------
	// @Param   teamNo   チームNo
	// @Return  int      チームが取得した得点
	// @Date    2014/11/21  @Update 2014/11/21  @Author T.Takeuchi
	//----------------------------------------------------------------------
	public int GetTeamScore(int teamNo)
	{
		SHOOT_LOG logData;
		int score = 0;
		for (int i = 0; i < logs.Count; i++)
		{
			logData = (SHOOT_LOG)logs[i];
			if (logData.teamNo == teamNo &&
				logData.isGole == true)
			{
				score++;
			}
		}

		return score;
	}

	//----------------------------------------------------------------------
	// 勝利したチーム番号を取得
	//----------------------------------------------------------------------
	// @Return  勝利チーム番号（０か１    ３だと引き分け)
	// @Date    2014/11/21  @Update 2014/11/21  @Author T.Takeuchi
	//----------------------------------------------------------------------
	public int GetWinTeamNo()
	{
		int[] teamScore = new int[2] {GetTeamScore(0),GetTeamScore(1)};

		if (teamScore[0] == teamScore[1])     return 3;
		else if (teamScore[0] > teamScore[1]) return 0;
		else                                  return 1;
	}

	//----------------------------------------------------------------------
	// その他関数
	//----------------------------------------------------------------------
	// @Date    2014/11/21  @Update 2014/11/21  @Author T.Takeuchi
	//----------------------------------------------------------------------
	public SHOOT_LOG GetLogData(int logNo) { return (SHOOT_LOG)logs[logNo]; }  // シュートログデータ（引数要素目）を取得
	public void ClearLog(){ logs.Clear();}                                     // シュートログをクリア
	public int GetCountLog(){ return logs.Count;}                              // シュートログ要素数を取得
};

// End of File