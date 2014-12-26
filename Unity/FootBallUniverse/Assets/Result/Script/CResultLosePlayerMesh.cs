using UnityEngine;
using System.Collections;

public class CResultLosePlayerMesh : MonoBehaviour {

    //----------------------------------------------------------------------
    // コンストラクタ
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/12/10  @Update 2014/12/10  @Author T.Kawashita    
    //----------------------------------------------------------------------
	void Start () {

		int winTeamNo = TeamData.GetWinTeamNo ();
		
		// 1P側の勝利
		if (winTeamNo == 0) 
		{
			// モデルのマテリアルを変更
			switch (TeamData.teamNationality[0])
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
		
		// 2P側の勝利
		else if (winTeamNo == 1) 
		{
			// モデルのマテリアルを変更
			switch (TeamData.teamNationality[1])
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
	}

    //----------------------------------------------------------------------
    // 更新
    //----------------------------------------------------------------------
    // @Param   none			
    // @Return	none
    // @Date	2014/12/10  @Update 2014/12/10  @Author T.Kawashita      
    //----------------------------------------------------------------------
	void Update () {
	
	}
}
