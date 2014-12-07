using UnityEngine;
using System.Collections;

public class CGoalKeeper : CCpu {
	protected enum GK_State
	{
		STAY,
		ON_ALERT,
		TAKE_BALL,
		CAT,
		PASS,
		BACK_HOME,
		GK_STATE_MAX
	};

	protected GK_State gkState = GK_State.STAY;

	protected Vector3 HOME_POSITION = new Vector3(0.0f, 0.0f, 25.0f);

	protected const float ARAT_SPACE = 8.0f;
	protected const float TAKE_BALL_SPACE = 5.0f;


	protected void CGoalKeeperInit()
	{
		this.Init();

		// プレイヤーのデータをセット
		CPlayerManager.SetPlayerData(this.m_playerData, CPlayerManager.AI_2);
		this.SetData();
		m_pos = this.transform.localPosition;

		// 国の情報をセット / 国によってマテリアルを変更
		m_human = CHumanManager.GetWorldInstance(TeamData.teamNationality[0]);
		this.transform.FindChild("polySurface14").GetComponent<CGoalKeeper1Mesh>().ChangeMaterial(TeamData.teamNationality[0]);

		Debug.Log(this.m_human.m_passInitSpeed);

		// サッカーボールの情報を取得
		this.soccerBallObject = GameObject.Find("SoccerBall");

		// プレイヤーのアニメーターをセット
		m_animator = this.gameObject.transform.parent.GetComponent<CPlayerAnimator>();

		// 向きをセット
		this.transform.LookAt(new Vector3(0.0f, 0.0f, 0.0f));
	}

	protected void CGoalKeeperUpdate()
	{
	}
}
