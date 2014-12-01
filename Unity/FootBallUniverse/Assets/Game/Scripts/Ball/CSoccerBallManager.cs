using UnityEngine;
using System.Collections;

public class CSoccerBallManager : MonoBehaviour {

    // サッカーボールのインスタンス
    public GameObject m_soccerBall;
    public bool m_isStartGame;

    // サッカーボールの情報
    public static int m_shootPlayerNo;         // シュート（パス）したプレイヤーの番号
    public static int m_shootTeamNo;           // シュートしたチームの番号

    // 低数値
    public const int eTEAM_1 = 0;
    public const int eTEAM_2 = 1;
    public const int eTEAM_NONE = 2;

    public const int ePLAYER_1 = 0;
    public const int ePLAYER_2 = 1;
    public const int ePLAYER_3 = 2;
    public const int ePLAYER_4 = 3;
    public const int eAI = 4;
    public const int eNONE = 5;

    //----------------------------------------------------------------------
    // コンストラクタ
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/11/11  @Update 2014/11/11  @Author T.Kawashita      
    //----------------------------------------------------------------------
	void Start () {

        // ゲームがスタートしているかどうか
        m_isStartGame = false;

        // サッカーボールの情報を初期化
        m_shootPlayerNo = eNONE;
        m_shootTeamNo = eTEAM_NONE;

        // サッカーボールをセット
        m_soccerBall = this.gameObject.transform.FindChild("SoccerBall").gameObject;
	}

    //----------------------------------------------------------------------
    // 更新
    //----------------------------------------------------------------------
    // @Param   none			
    // @Return	none
    // @Date	2014/11/11  @Update 2014/11/11  @Author T.Kawashita      
    //----------------------------------------------------------------------
	void Update () {
        
        // ゲームスタートしたら
        if (CGameManager.m_nowStatus == CGameManager.eSTATUS.eGAME && m_isStartGame == false)
        {
            m_soccerBall.GetComponent<CSoccerBall>().StartGame();
            m_isStartGame = true;
        }

        // ブラックアウトしてリスタートする場合
        if (CGameManager.m_nowStatus == CGameManager.eSTATUS.eRESTART && m_isStartGame == true)
        {
            m_soccerBall.GetComponent<CSoccerBall>().Init(new Vector3(0.0f, 0.0f, 0.0f));
            m_isStartGame = false;
        }

        // ゲーム終了
        if (CGameManager.m_nowStatus == CGameManager.eSTATUS.eEND)
        {
            m_soccerBall.GetComponent<CSoccerBall>().Init(this.transform.localPosition);
        }
	}

    //----------------------------------------------------------------------
    // サッカーボールの持ち主を変更
    //----------------------------------------------------------------------
    // @Param	Transform   親
	// @Param   Vector3     設定したい位置	
    // @Return	bool        成功か失敗
    // @Date	2014/11/11  @Update 2014/11/11  @Author T.Kawashita       
    //----------------------------------------------------------------------
    public bool ChangeOwner(Transform _parent,Vector3 _pos)
    {
        // サッカーボール自体をプレイヤーの親にする
        m_soccerBall.transform.parent = _parent; 

        // サッカーボールの位置変更
        m_soccerBall.GetComponent<CSoccerBall>().Init(_pos);

        return true;
    }

}
