using UnityEngine;
using System.Collections;

//----------------------------------------------------
// プレイヤーの基本的なクラス
//----------------------------------------------------

public class CPlayer : MonoBehaviour {

    private Vector3 m_pos;          // 現在位置
    private Vector3 m_old_pos;      // 1F前の位置


    //----------------------------------------------------------------------
    // コンストラクタのようなもの
    //----------------------------------------------------------------------
    // @Param   none			
    // @Return	none
    // @Date	2014/10/14  @Update 2014/10/14  @Author T.Kawashita      
    //----------------------------------------------------------------------
	void Start () {
        m_pos = this.transform.localPosition;
        m_old_pos = m_pos;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
