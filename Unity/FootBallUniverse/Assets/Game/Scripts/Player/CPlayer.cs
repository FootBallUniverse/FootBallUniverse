using UnityEngine;
using System.Collections;

//----------------------------------------------------
// プレイヤーの基本的なクラス
// 基底クラス
//----------------------------------------------------
public class CPlayer : MonoBehaviour {

    public CPlayerManager.ePLAYER_STATUS m_status;
    public CPlayerManager.eVIEW_POINT_STATUS m_viewPointStatus;
    
    public Vector3 m_pos;        // 位置座標
    public Vector3 m_speed;      // 移動量
    public Vector3 m_angle;      // 回転角度

    public CPlayerData m_playerData;        // プレイヤーのデータ

    public CPlayerAction m_action;          // プレイヤーのアクション
    public CPlayerAnimator m_animator;      // プレイヤーのアニメーション
    public CHuman m_human;                  // プレイヤーの国のインスタンス
    public CPlayerSE m_playerSE;            // プレイヤーのSE

    public int m_chargeFrame;               // チャージ時のフレーム数
    public bool m_isRtPress;                // RTボタンが押され続けているか
    public bool m_isLtPress;                // LTボタンが押され続けているか
    public bool m_isBall;                   // ボールを持っているかどうか

    // カメラのコンポーネント
    public PlayerCamera m_camera;
    public Transform m_trans;

    //----------------------------------------------------------------------
    // コンストラクタ
    //----------------------------------------------------------------------
    // @Param   none			
    // @Return	none
    // @Date	2014/10/14  @Update 2014/10/14  @Author T.Kawashita      
    //----------------------------------------------------------------------
	void Start () {
        m_pos = new Vector3();
        m_speed = new Vector3();
        m_angle = new Vector3();
        m_status = CPlayerManager.ePLAYER_STATUS.eNONE;
        m_viewPointStatus = CPlayerManager.eVIEW_POINT_STATUS.ePLAYER;

        m_human = new CHuman();
        m_playerData = new CPlayerData();
        m_action = new CPlayerAction();

        m_chargeFrame = 0;
        m_isRtPress = false;
        m_isLtPress = false;
        m_isBall = false;
    }

    //----------------------------------------------------------------------
    // 初期化
    //----------------------------------------------------------------------
    // @Param   none			
    // @Return	bool    成功か失敗
    // @Date	2014/10/24  @Update 2014/10/24  @Author T.Kawashita      
    //----------------------------------------------------------------------
    protected bool Init()
    {
        m_pos = new Vector3(0.0f,0.0f,0.0f);
        m_speed = new Vector3(0.0f,0.0f,0.0f);
        m_angle = new Vector3(0.0f,0.0f,0.0f);
        m_status = CPlayerManager.ePLAYER_STATUS.eWAIT;
        m_viewPointStatus = CPlayerManager.eVIEW_POINT_STATUS.ePLAYER;

        m_human = new CHuman();
        m_playerData = new CPlayerData();
        m_action = new CPlayerAction();

        m_chargeFrame = 0;
        m_isRtPress = false;
        m_isLtPress = false;
        m_isBall = false;

        m_playerSE = this.transform.GetComponent<CPlayerSE>();

        return true;
    }

    //----------------------------------------------------------------------
    // ゴールした後のリスタート
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/11/25  @Update 2014/11/25  @Author T.Kawashita      
    //----------------------------------------------------------------------
    protected bool Restart()
    {
        // 位置と回転をセットしなおす
        m_pos = new Vector3(m_playerData.m_xPos, m_playerData.m_yPos, m_playerData.m_zPos);
        m_angle = new Vector3(0.0f, 0.0f, 0.0f);
        this.transform.localRotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);

        // 状態を変更
        this.m_status = CPlayerManager.ePLAYER_STATUS.eCOUNTDOWN;
        this.m_viewPointStatus = CPlayerManager.eVIEW_POINT_STATUS.ePLAYER;

        // アニメーションを元に戻す
        this.m_speed = new Vector3(0.0f, 0.0f, 0.0f);

        // その他変数初期化
        m_chargeFrame = 0;
        m_isBall = false;
        m_isLtPress = false;
        m_isRtPress = false;

        return true;
    }

    //----------------------------------------------------------------------
    // 更新
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/10/28  @Update 2014/10/28  @Author T.Kawashita      
    //----------------------------------------------------------------------
	void Update () {	
	}

    //----------------------------------------------------------------------
    // ゲームがプレイ中かどうか判定
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/10/28  @Update 2014/11/21  @Author T.Kawashita      
    //----------------------------------------------------------------------
    protected void CheckGamePlay()
    { 
        // ゲーム終了かどうか判定
        if (CGameManager.m_isGamePlay == false)
            m_status = CPlayerManager.ePLAYER_STATUS.eEND;  // 終了していたらステータス変更
    
        // ゴールを決めたかどうか判定
        if (CGameManager.m_nowStatus == CGameManager.eSTATUS.eGOALPERFOMANCE)
            m_status = CPlayerManager.ePLAYER_STATUS.eGOAL; // ゴール状態に遷移
    }

    //----------------------------------------------------------------------
    // 移動(仮想関数)
    //----------------------------------------------------------------------
    // @Param	Vector3     移動量
    // @Return	none
    // @Date	2014/11/11  @Update 2014/11/11  @Author T.Kawashita      
    //----------------------------------------------------------------------
    public virtual void Move(Vector3 _speed)
    {
    }

    //----------------------------------------------------------------------
    // 回転(仮想関数)
    //----------------------------------------------------------------------
    // @Param	Vector2     回転量		
    // @Return	none
    // @Date	2014/11/12  @Update 2014/11/12  @Author T.Kawashita      
    //----------------------------------------------------------------------
    public virtual void Rotation(Vector2 _angle)
    { 
    }

    //----------------------------------------------------------------------
    // データをプレイヤーにセット
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/11/21  @Update 2014/11/21  @Author T.Kawashita      
    //----------------------------------------------------------------------
    protected void SetData()
    {
        // 位置をセット
        this.transform.localPosition = new Vector3(m_playerData.m_xPos, m_playerData.m_yPos, m_playerData.m_zPos);

        // ボールの取れる範囲をセット
        this.GetComponent<SphereCollider>().radius = m_human.m_holdRangeRadius;
    }

    //----------------------------------------------------------------------
    // 視点を変更
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/11/30  @Update 2014/11/30  @Author T.Kawashita      
    //----------------------------------------------------------------------
    public virtual void ChangeViewPoint()
    {
    }

}
