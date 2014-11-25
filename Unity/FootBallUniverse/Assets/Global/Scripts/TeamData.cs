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
	public static ArrayList logs     = new ArrayList();               // ログデータ格納

};

// End of File