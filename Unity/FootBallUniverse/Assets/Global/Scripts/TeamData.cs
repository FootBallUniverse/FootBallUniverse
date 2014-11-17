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

	// シングルトン用
	private static TeamData teamData = new TeamData();

	public static TEAM_NATIONALITY teamNationalityRight;
	public static TEAM_NATIONALITY teamNationalityLeft;
};
