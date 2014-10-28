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
    // 初期化
    //----------------------------------------------------------------------
    // @Param   _pos    設定したい初期位置			
    // @Return	bool    成功か失敗
    // @Date	2014/10/28  @Update 2014/10/28  @Author T.Kawashita      
    //----------------------------------------------------------------------
    public bool Init(Vector3 _pos)
    {
        m_pos = _pos;
        this.transform.localPosition = m_pos;

        // 速度ベクトル，角速度ベクトルの初期化
        this.rigidbody.velocity = new Vector3(0.0f, 0.0f, 0.0f);
        this.rigidbody.angularVelocity = new Vector3(0.0f, 0.0f, 0.0f);

        return true;
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
