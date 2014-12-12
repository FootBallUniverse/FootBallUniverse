using UnityEngine;
using System.Collections;

public class CDrawPerformanceBluePlayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		switch (TeamData.teamNationality[1])
		{
			// 日本
		case TeamData.TEAM_NATIONALITY.JAPAN:
			this.renderer.material = (Material)Instantiate(Resources.Load("Model/Player/Materials/lambert_japan2"));
			break;
			
			// ブラジル
		case TeamData.TEAM_NATIONALITY.BRASIL:
			this.renderer.material = (Material)Instantiate(Resources.Load("Model/Player/Materials/lambert_brasil2"));
			break;
			
			// イングランド
		case TeamData.TEAM_NATIONALITY.ENGLAND:
			this.renderer.material = (Material)Instantiate(Resources.Load("Model/Player/Materials/lambert_england2"));
			break;
			
			// スペイン
		case TeamData.TEAM_NATIONALITY.ESPANA:
			this.renderer.material = (Material)Instantiate(Resources.Load("Model/Player/Materials/lambert_spain2"));
			break;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
