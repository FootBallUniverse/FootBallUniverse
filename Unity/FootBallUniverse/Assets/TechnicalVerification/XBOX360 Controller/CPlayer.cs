using UnityEngine;
using System.Collections;

//----------------------------------------------------
// プレイヤーの基本的なクラス
// 基底クラス
//----------------------------------------------------

public class CPlayer : MonoBehaviour {

    protected Vector3 m_pos;        // 位置座標
    protected Vector3 m_old_pos;    // 前回座標
    protected Vector2 m_angle;      // 回転角度

    //----------------------------------------------------------------------
    // コンストラクタのようなもの
    //----------------------------------------------------------------------
    // @Param   none			
    // @Return	none
    // @Date	2014/10/14  @Update 2014/10/14  @Author T.Kawashita      
    //----------------------------------------------------------------------
	void Start () {
        m_pos = new Vector3();
        m_old_pos = new Vector3();
        m_angle = new Vector2();
	}
	
	// Update is called once per frame
	void Update () {	
	}
}
