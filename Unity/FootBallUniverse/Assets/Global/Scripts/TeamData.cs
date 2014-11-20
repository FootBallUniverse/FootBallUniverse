using UnityEngine;
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
	public struct GOAL_LOG
	{
		public int time;     // ゴールした時間
		public int playerNo; // プレイヤーNo
		public bool isScore; // ゴールに成功したか
	};

	// シングルトン用
	private static TeamData teamData = new TeamData();

	// 各種データ（デバッグ時エラー防止のため初期値が入っています）
	public static TEAM_NATIONALITY[] teamNationality = new TEAM_NATIONALITY[2] {TEAM_NATIONALITY.JAPAN,TEAM_NATIONALITY.BRASIL}; // 国籍
	public static int[] teamScore    = new int[2]{10,11};                                                                        // チームスコア (仮）
	public static int[,] playerScore = new int[2,2]{{10,11},{12,13}};                                                            // プレイヤー別スコア（仮）
	private static ArrayList logs    = new ArrayList();                                                                          // ログデータ格納（カプセル化）

	public void AddLog(int time,int playerNo,bool isScore)
	{
		GOAL_LOG newData = new GOAL_LOG();
		newData.time     = time;
		newData.playerNo = playerNo;
		newData.isScore  = isScore;
		logs.Add(newData);
	}
	public GOAL_LOG GetLogData(int logNo) { return (GOAL_LOG)logs[logNo]; }
	public void ClearLog(){ logs.Clear();}
	public int GetCountLog(){ return logs.Count;}
};

// End of File