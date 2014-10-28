using UnityEngine;
using System.Collections;

//----------------------------------------------------------------------
// CSoccerBall
//----------------------------------------------------------------------
// @Info サッカーボール用クラス
// @Date 2014/10/27	@Update 2014/10/27  @Author T.Kawashita      
//----------------------------------------------------------------------
public class CSoccerBall : MonoBehaviour {

    private Vector3 m_pos;  // 位置座標

    //----------------------------------------------------------------------
    // コンストラクタ
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/10/27  @Update 2014/10/27  @Author T.Kawashita      
    //----------------------------------------------------------------------
	void Start () {
        m_pos = new Vector3();
        m_pos = this.transform.localPosition;
        this.rigidbody.drag = CGameData.m_ballDecRec;           // 空気抵抗をセット
        this.rigidbody.angularDrag = CGameData.m_ballDecRec;    // 反射係数をセット
    }

    //----------------------------------------------------------------------
    // 更新
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/10/27  @Update 2014/10/27  @Author T.Kawashita      
    //----------------------------------------------------------------------
	void Update () {	    
	}

    //----------------------------------------------------------------------
    // 位置の変更
    //----------------------------------------------------------------------
    // @Param	Vector3 セットしたい位置座標		
    // @Return	none
    // @Date	2014/10/27  @Update 2014/10/27  @Author T.Kawashita       
    //----------------------------------------------------------------------
    public void SetPosition(Vector3 _pos)
    {
        m_pos = _pos;

        this.transform.localPosition = m_pos;
    }

}
