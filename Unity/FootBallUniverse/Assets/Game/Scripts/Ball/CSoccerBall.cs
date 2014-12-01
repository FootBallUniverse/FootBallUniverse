using UnityEngine;
using System.Collections;

//----------------------------------------------------------------------
// CSoccerBall
//----------------------------------------------------------------------
// @Info サッカーボール用クラス
// @Date 2014/10/27	@Update 2014/10/27  @Author T.Kawashita      
//----------------------------------------------------------------------
public class CSoccerBall : MonoBehaviour {

    public Vector3 m_pos;           // 位置座標

    //----------------------------------------------------------------------
    // コンストラクタ
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/10/27  @Update 2014/10/27  @Author T.Kawashita      
    //----------------------------------------------------------------------
	void Start () {
        this.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        m_pos = new Vector3();
        m_pos = this.transform.localPosition;
        this.rigidbody.drag = CGameData.m_ballDecRec;           // 空気抵抗をセット
        this.rigidbody.angularDrag = CGameData.m_ballDecRec;    // 反射係数をセット

        this.rigidbody.angularVelocity = new Vector3(Random.value * 10.0f, 0.0f, Random.value * 10.0f);
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
        this.transform.localRotation = Quaternion.identity;

        // 速度ベクトル，角速度ベクトルの初期化
        this.rigidbody.velocity = new Vector3(0.0f, 0.0f, 0.0f);
        this.rigidbody.angularVelocity = new Vector3(0.0f, 0.0f, 0.0f);

        return true;
    }

    //----------------------------------------------------------------------
    // ゲーム開始時のボールの動き
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/11/21  @Update 2014/11/21  @Author T.Kawashita      
    //----------------------------------------------------------------------
    public void StartGame()
    {
        this.rigidbody.velocity = new Vector3(Random.Range(-1.0f,1.0f) * 3.0f, Random.Range(-1.0f,1.0f) * 3.0f, 0.0f);
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
