using UnityEngine;
using System.Collections;

public class CCpu : MonoBehaviour {
	protected GameObject soccerBallManager;

	//----------------------------------------------------------------------
	// コンストラクタ
	//----------------------------------------------------------------------
	// @Param	none		
	// @Return	none
	// @Date	2014/12/1  @Update 2014/12/1  @Author T.Kawashita      
	//----------------------------------------------------------------------
	void Start () {
		this.soccerBallManager = GameObject.Find("BallGameObject");
	}

	//----------------------------------------------------------------------
	// 更新
	//----------------------------------------------------------------------
	// @Param   none			
	// @Return	none
	// @Date	2014/12/1  @Update 2014/12/1  @Author T.Kawashita      
	//----------------------------------------------------------------------
	void Update () {
	}

	//----------------------------------------------------------------------
	// フレーム最後の更新
	//----------------------------------------------------------------------
	// @Param	none		
	// @Return	none
	// @Date	2014/12/1  @Update 2014/12/1  @Author T.Kawashita      
	//----------------------------------------------------------------------
	void LateUpdate(){
	}

	// 自動移動機能（座標指定）
	void Move(Vector3 targetPosition)
	{
	}
}
