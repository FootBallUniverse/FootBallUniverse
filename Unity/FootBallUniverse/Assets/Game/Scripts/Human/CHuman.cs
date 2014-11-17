using UnityEngine;
using System.Collections;

[System.Serializable]
public class CHuman : Object {

    public int m_id;                         // ID
    public string m_name;                    // 名前
    public float m_playerMoveSpeed;          // 移動速度
    public float m_playerMoveSpeedHold;      // ホールド時の移動速度
    public float m_cameraMoveSpeed;          // カメラの移動速度
    public float m_shootInitSpeed;           // シュート初速度
    public float m_shootInitSmashSpeed;      // スマッシュシュートの初速度
    public int m_shootMotionLength;          // シュートモーションの長さ
    public int m_shootMotionLengthSmash;     // スマッシュシュートの長さ
    public int m_shootTakeOfFrame;           // シュートモーションの中でボールが足を離れる時間
    public int m_shootTakeOfFrameSmash;      // スマッシュシュートのモーションの中でボールが足を離れる時間
    public float m_shootRangeRadiusSmash;    // スマッシュシュートが出来るときの距離
    public int m_shootWaitMotionLength;      // シュートモーションの待機時間
    public float m_passInitSpeed;            // パスの初速度
    public int m_passMotionLength;           // パスモーションの長さ
    public int m_passTakeOfFrame;            // パスモーションの中でボールが足を離れる時間
    public float m_holdRangeRadius;          // ボールを取る範囲
    public int m_holdMotionLength;           // ホールドモーションの長さ
    public int m_holdMotionLengthPass;       // パスホールドモーションの長さ
    public float m_dashInitSpeed;            // ダッシュの初速度
    public int m_dashMotionLength;           // ダッシュモーションの長さ
    public int m_dashDecFrame;               // ダッシュが減速し始める時間
    public float m_tackleInitSpeed;          // タックルの初速度
    public int m_tackleMotionLength;         // タックルのモーションの長さ
    public int m_tackleChangeLength;         // タックルの溜め時間
    public int m_tackleDecFrame;             // タックルが減速し始める時間
    public int m_tackleDamegeLength;         // タックルのダメージの長さ
    public int m_shootChargeLength;          // シュートのチャージ時間
    public int m_passChargeLength;           // パスのチャージ時間
    public int m_dashChargeLength;           // ダッシュのチャージ時間
    public float m_cameraMoveSpeedCharging;  // チャージしているときのカメラの移動速度
    public int m_shootChargeLengthMax;       // シュートのチャージの最大時間
    public int m_dashChargeLengthMax;        // ダッシュのチャージの最大時間
    public float m_tackleHitRadius;          // タックルの当たる範囲

    //----------------------------------------------------------------------
    // コンストラクタ
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/10/24  @Update 2014/10/24  @Author T.Kawashita      
    //----------------------------------------------------------------------
    void Start()
    { 
    }

    //----------------------------------------------------------------------
    // 更新
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/10/24  @Update 2014/10/24  @Author T.Kawashita      
    //----------------------------------------------------------------------
    void Update()
    { 
    }

    //----------------------------------------------------------------------
    // 値をセット
    //----------------------------------------------------------------------
    // @Param	string[] セットしたい値が格納されている配列		
    // @Return	none
    // @Date	2014/10/24  @Update 2014/10/24  @Author T.Kawashita      
    //----------------------------------------------------------------------
    public void Set(string[] _value)
    {
        m_id = int.Parse(_value[0]);
        m_name = _value[1];
        m_playerMoveSpeed = float.Parse(_value[2]);
        m_playerMoveSpeedHold = float.Parse(_value[3]);
        m_cameraMoveSpeed = float.Parse(_value[4]);
        m_shootInitSpeed = float.Parse( _value[5]);
        m_shootInitSmashSpeed = float.Parse( _value[6]);
        m_shootMotionLength = int.Parse(_value[7] );
        m_shootMotionLengthSmash = int.Parse(_value[8]);
        m_shootTakeOfFrame = int.Parse(_value[9]);
        m_shootTakeOfFrameSmash = int.Parse( _value[10] );
        m_shootRangeRadiusSmash = float.Parse( _value[11] );
        m_shootWaitMotionLength = int.Parse( _value[12] );
        m_passInitSpeed = float.Parse( _value[13] );
        m_passMotionLength = int.Parse( _value[14] );
        m_passTakeOfFrame = int.Parse( _value[15] );
        m_holdRangeRadius = float.Parse( _value[16] );
        m_holdMotionLength = int.Parse( _value[17] );
        m_holdMotionLengthPass = int.Parse( _value[18] );
        m_dashInitSpeed = float.Parse( _value[19] );
        m_dashMotionLength = int.Parse( _value[20] );
        m_dashDecFrame = int.Parse( _value[21] );
        m_tackleInitSpeed = float.Parse( _value[22] );
        m_tackleMotionLength = int.Parse( _value[23] );
        m_tackleChangeLength = int.Parse(_value[24]);
        m_tackleDecFrame = int.Parse( _value[25] );
        m_tackleDamegeLength = int.Parse( _value[26] );
        m_shootChargeLength = int.Parse(_value[27]);
        m_passChargeLength = int.Parse(_value[28]);
        m_dashChargeLength = int.Parse(_value[29]);
        m_cameraMoveSpeedCharging = float.Parse(_value[30]);
        m_shootChargeLengthMax = int.Parse(_value[31]);
        m_dashChargeLengthMax = int.Parse(_value[32]);
        m_tackleHitRadius = float.Parse(_value[33]);

    }


}
