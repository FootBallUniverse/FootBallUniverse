using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;

public class TeamData
{
	public enum TEAM_NATIONALITY{
		JAPAN,
		ESPANA,
		ENGLAND,
		BRASIL,
		NATIONALITY_MAX
	};

	// どの選手　いつゴールしたか
	public struct GOAL_LOG
	{
		int time;
		int playerNo;
	};

	// シングルトン用
	private static TeamData teamData = new TeamData();

	public static TEAM_NATIONALITY[] teamNationality = new TEAM_NATIONALITY[2];  // 国籍
	public static int[] teamScore = new int[2];                                  // スコア
	//public static var goalLog = new ArrayList;                                                   // 
};
