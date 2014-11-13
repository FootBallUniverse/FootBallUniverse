using UnityEngine;
using System.Collections;

public class CPlayerManager {

    // マネージャーのインスタンス
    public static CPlayerManager m_playerManager = new CPlayerManager();

    // マップのオブジェクト
    public GameObject m_mapObject;

    // ボールの管理クラス
    public CSoccerBallManager m_soccerBallManager;

    // カメラのモード
    public enum eCAMERA_STATUS
    {
        eNORMAL,
        eROCKON
    }

    // プレイヤーのステータス
    public enum ePLAYER_STATUS
    {
        eWAIT,
        eNONE,
        eDASH,
        eSHOOTWAIT,
        eSHOOT,
        eSMASHSHOOT,
        eTACKLE,
        eHOLD,
        ePASS,
        eTACKLEDAMAGE,
        eEND
    };

    //----------------------------------------------------------------------
    // コンストラクタ
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/10/31  @Update 2014/10/31  @Author T.Kawashita      
    //----------------------------------------------------------------------
    public CPlayerManager()
    {
        m_mapObject = GameObject.Find("Map").gameObject;
        m_soccerBallManager = GameObject.Find("BallGameObject").GetComponent<CSoccerBallManager>();
    }

    //----------------------------------------------------------------------
    // Playerをマップにセット
    //----------------------------------------------------------------------
    // @Param	GameObject  セットしたいObject
	// @Param   Color       セットしたい色
    // @Return	bool        成功か失敗
    // @Date	2014/10/31  @Update 2014/10/31  @Author T.Kawasita      
    //----------------------------------------------------------------------
    public bool SetMap(GameObject _object, Color _color)
    {
        m_mapObject.GetComponent<Map>().CreateMaker(_object, _color);

        return true;
    }

}
