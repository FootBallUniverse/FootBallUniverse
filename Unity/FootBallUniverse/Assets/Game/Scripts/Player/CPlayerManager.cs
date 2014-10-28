using UnityEngine;
using System.Collections;

public class CPlayerManager {

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
        ePATH,
        eTACKLEDAMAGE,
        eEND
    };

}
