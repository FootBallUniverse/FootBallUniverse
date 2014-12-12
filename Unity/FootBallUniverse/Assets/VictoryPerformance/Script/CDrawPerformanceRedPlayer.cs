using UnityEngine;
using System.Collections;

public class CDrawPerformanceRedPlayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		// モデルのマテリアルを変更
		switch (TeamData.teamNationality[0])
		{
			// 日本
		case TeamData.TEAM_NATIONALITY.JAPAN:
			this.renderer.material = (Material)Instantiate(Resources.Load("Model/Player/Materials/lambert_japan"));
			break;
			
			// ブラジル
		case TeamData.TEAM_NATIONALITY.BRASIL:
			this.renderer.material = (Material)Instantiate(Resources.Load("Model/Player/Materials/lambert_brasil"));
			break;
			
			// イングランド
		case TeamData.TEAM_NATIONALITY.ENGLAND:
			this.renderer.material = (Material)Instantiate(Resources.Load("Model/Player/Materials/lambert_england"));
			break;
			
			// スペイン
		case TeamData.TEAM_NATIONALITY.ESPANA:
			this.renderer.material = (Material)Instantiate(Resources.Load("Model/Player/Materials/lambert_spain"));
			break;
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
