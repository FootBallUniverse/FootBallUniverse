using UnityEngine;
using System.Collections;

//----------------------------------------------------------------------
// プレイヤーのデバッグ用コントローラー
//----------------------------------------------------------------------
// @Date 2014/11/11  @Update 2014/11/11  @Author T.Kawashita      
//----------------------------------------------------------------------
public class CPlayerControler : MonoBehaviour {

    // 現在どのプレイヤーを操作しているかどうかのための定数
    private enum ePLAYER_STATUS
    {
        ePLAYER1,
        ePLAYER2,
        ePLAYER3,
        ePLAYER4
    }

    public GameObject m_player;
    public CPlayer m_playerScript;          // 操作するプレイヤーのスクリプト
    private ePLAYER_STATUS m_playerStatus;  // 現在のプレイヤー

    //----------------------------------------------------------------------
    // コンストラクタ
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/11/11  @Update 2014/11/11  @Author T.Kawashita      
    //----------------------------------------------------------------------
    void Start () {

        // 最初はプレイヤー１のスクリプトを取得
        m_playerScript = this.gameObject.GetComponent<CPlayer1>();
        m_playerStatus = ePLAYER_STATUS.ePLAYER1;
	}

    //----------------------------------------------------------------------
    // 更新
    //----------------------------------------------------------------------
    // @Param   none			
    // @Return	none
    // @Date	2014/111/11 @Update 2014/111/12  @Author T.kawashita      
    //----------------------------------------------------------------------
	void Update () {
        ChangePlayer(); // プレイヤーの切り替え
        Move();         // 移動
        Rotation();     // 回転
        SetBall();      // ボールのセット
	}

    //----------------------------------------------------------------------
    // プレイヤーの切り替え
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/11/11  @Update 2014/11/11  @Author T.Kawashita      
    //----------------------------------------------------------------------
    public void ChangePlayer()
    { 
        // 操作する対象を変更する
        // 1P
        if (Input.GetKeyDown(KeyCode.Alpha1) && m_playerStatus != ePLAYER_STATUS.ePLAYER1)
        {
            

            // 1Pの情報を取得して1Pに切り替える
            m_player = GameObject.Find("P1&P2").transform.FindChild("Player1").transform.FindChild("player").gameObject;
            m_playerScript = gameObject.GetComponent<CPlayer1>();
            m_playerStatus = ePLAYER_STATUS.ePLAYER1;
        }
        // 2P
        if (Input.GetKeyDown(KeyCode.Alpha2) && m_playerStatus != ePLAYER_STATUS.ePLAYER2)
        {
            m_player = GameObject.Find("P1&P2").transform.FindChild("Player2").transform.FindChild("player").gameObject;
            m_playerScript = gameObject.GetComponent<CPlayer2>();
            m_playerStatus = ePLAYER_STATUS.ePLAYER2;
        }

        // 3P

        // 4P


    }

    //----------------------------------------------------------------------
    // 移動
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/11/11  @Update 2014/11/11  @Author T.Kawashita      
    //----------------------------------------------------------------------
    public void Move()
    {
        Vector3 speed = new Vector3(0.0f, 0.0f, 0.0f);

        // 前移動
        if (Input.GetKey(KeyCode.W))
            speed.z += 0.3f;

        // 後移動
        if (Input.GetKey(KeyCode.S))
            speed.z -= 0.3f;

        // 右移動
        if (Input.GetKey(KeyCode.D))
            speed.x += 0.3f;

        // 左移動
        if (Input.GetKey(KeyCode.A))
            speed.x -= 0.3f;

        // 移動関数
        m_playerScript.Move(speed);

    }

    //----------------------------------------------------------------------
    // 回転
    //----------------------------------------------------------------------
    // @Param   none			
    // @Return	none
    // @Date	2014/11/12  @Update 2014/11/12  @Author T.Kawashita      
    //----------------------------------------------------------------------
    public void Rotation()
    { 
        Vector2 angle = new Vector2(0.0f,0.0f);

        // 前回転
        if (Input.GetKey(KeyCode.UpArrow))
            angle.y += 0.6f;

        // 後回転
        if (Input.GetKey(KeyCode.DownArrow))
            angle.y -= 0.6f;

        // 右回転
        if (Input.GetKey(KeyCode.RightArrow))
            angle.x += 0.6f;

        // 左回転
        if (Input.GetKey(KeyCode.LeftArrow))
            angle.x -= 0.6f;

        // 回転関数
        m_playerScript.Rotation(angle);
    }

    //----------------------------------------------------------------------
    // ボールのセット
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/11/11  @Update 2014/11/11  @Author T.Kawashita       
    //----------------------------------------------------------------------
    public void SetBall()
    {
        // Bが押されたらプレイヤーにボールをセット
        if (Input.GetKeyDown(KeyCode.B) && m_playerScript.m_isBall == false)
        {
            // ボールの位置をセット
            Vector3 pos = new Vector3(0.0f,0.05f,0.1f);

            // サッカーボールに誰が現在保持しているかを設定
            CSoccerBallManager.m_shootPlayerNo = m_playerScript.m_playerData.m_id;
            CSoccerBallManager.m_shootTeamNo = m_playerScript.m_playerData.m_teamNo;
            CSoccerBallManager.m_nowPlayer = m_playerScript.m_playerData.m_id;

            // プレイヤーのボールに設定
            CPlayerManager.m_playerManager.m_soccerBallManager.ChangeOwner(this.transform,pos);
            m_playerScript.m_isBall = true;
        }
    }
}
