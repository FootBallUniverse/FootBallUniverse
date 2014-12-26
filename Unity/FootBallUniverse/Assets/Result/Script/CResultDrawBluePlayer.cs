﻿using UnityEngine;
using System.Collections;

public class CResultDrawBluePlayer : MonoBehaviour {

    //----------------------------------------------------------------------
    // コンストラクタ
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/12/10  @Update 2014/12/26  @Author T.Kawashita      
    //----------------------------------------------------------------------
	void Start ()
    {
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
