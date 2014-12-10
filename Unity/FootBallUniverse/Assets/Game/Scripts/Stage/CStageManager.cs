using UnityEngine;
using System.Collections;

public class CStageManager : MonoBehaviour {

	public static Transform m_1p2pGoalTransform;
	public static Transform m_3p4pGoalTransform;

	//----------------------------------------------------------------------
	// コンストラクタ
	//----------------------------------------------------------------------
	// @Param   none		
	// @Return	none
	// @Date	2014/12/9  @Update 2014/12/9  @Author T.Kawashita      
	//----------------------------------------------------------------------
	void Start () {
		m_1p2pGoalTransform = this.transform.FindChild ("goal1_collision").transform;
		m_3p4pGoalTransform = this.transform.FindChild ("goal2_collision").transform;
	}

	//----------------------------------------------------------------------
	// アップデート
	//----------------------------------------------------------------------
	// @Param   none		
	// @Return	none
	// @Date	2014/12/9  @Update 2014/12/9  @Author T.Kawashita      
	//----------------------------------------------------------------------
	void Update () {
	}

}
