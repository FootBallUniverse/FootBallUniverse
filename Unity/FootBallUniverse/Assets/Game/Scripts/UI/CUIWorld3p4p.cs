using UnityEngine;
using System.Collections;

public class CUIWorld3p4p : MonoBehaviour {

	//----------------------------------------------------------------------
	// コンストラクタ
	//----------------------------------------------------------------------
	// @Param	none		
	// @Return	none
	// @Date	2014/12/10  @Update 2014/12/10  @Author T.Kawashita      
	//----------------------------------------------------------------------
	void Start () {
		
		switch (TeamData.teamNationality [1]) 
		{
		case TeamData.TEAM_NATIONALITY.JAPAN:
			this.GetComponent<UISprite> ().spriteName = "JPN_type1";
			break;
			
		case TeamData.TEAM_NATIONALITY.ESPANA:
			this.GetComponent<UISprite>().spriteName = "ESP_type1";
			break;
			
		case TeamData.TEAM_NATIONALITY.ENGLAND:
			this.GetComponent<UISprite>().spriteName = "ENG_type1";
			break;
			
		case TeamData.TEAM_NATIONALITY.BRASIL:
			this.GetComponent<UISprite>().spriteName = "BRA_type1";
			break;
		}
	}
	
	//----------------------------------------------------------------------
	// 更新
	//----------------------------------------------------------------------
	// @Param	none		
	// @Return	none
	// @Date	2014/12/10  @Update 2014/12/10  @Author T.Kawashita      
	//----------------------------------------------------------------------
	void Update () {
	
	}
}
