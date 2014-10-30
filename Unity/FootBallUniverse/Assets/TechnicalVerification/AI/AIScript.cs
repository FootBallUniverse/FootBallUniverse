using UnityEngine;
using System.Collections;



public class AIScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

public class PlayerAI
{
	public enum PLAYER_STATE
	{
		TEST0,
		TEST1,
		TEST2,
		TEST3,
		TEST4,
		TEST5
	};

	void Start()
	{
	}

	void Update()
	{
	}
}


public class TeamAI
{
	public int teamNum;
	GameObject haveBallPlayer;
	GameObject nearBallPlayer;
	void Start(){}

	void Update()
	{
		SerchNearBallPlayer(GameObject.Find("SoccerBall") as GameObject);
		SerchNearBallPlayer(GameObject.Find("SoccerBall") as GameObject);
	}

	void SerchNearBallPlayer(GameObject soccerBall)
	{
		for (int i = 0; i < this.teamNum; i++)
		{
			if (true)
			{
				// if (サッカー保持者＝＝チームメンバーだったら）
			}
		}
	}

	void SerchHaveBallPlayer(GameObject soccerBall)
	{
		for (int i = 0; i < this.teamNum; i++)
		{
			if (true)
			{
				// if (サッカー保持者＝＝チームメンバーだったら）
			}
			else
			{
				this.haveBallPlayer = null;
			}
		}
	}
}