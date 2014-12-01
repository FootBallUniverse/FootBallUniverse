using UnityEngine;
using System.Collections;

//----------------------------------------------------------------------
// プレイヤーのアクション
//----------------------------------------------------------------------
// @Date 2014/10/30  @Update 2014/11/11  @Author T.Kawashita      
//----------------------------------------------------------------------
public class CPlayerAction {

    private float m_dashFrame;                  // ダッシュのフレーム
    private int m_dashMotionLength;             // ダッシュ全体のフレーム
    private float m_dashDecSpeed;               // ダッシュの減速量
    private int m_dashDecFrame;                 // ダッシュの減速開始フレーム
    private float m_dashInitSpeed;              // ダッシュの初速度

    private float m_shootFrame;                 // シュートのフレーム
    private int m_shootMotionLength;            // シュート全体の長さ
    private int m_shootTakeOfFrame;             // シュートが足を離れるまでの時間
    private float m_shootInitSpeed;             // シュート速度

    private float m_passFrame;                  // パスのフレーム
    private int m_passMotionLength;             // パス全体の長さ 
    private int m_passTakeOfFrame;              // パスが足を離れるまでの時間
    private float m_passInitSpeed;              // パス速度

    private float m_tackleFrame;                // タックルのフレーム
    private int m_tackleMotionLength;           // タックル全体のフレーム
    private float m_tackleDecSpeed;             // タックルの減速量
    private int m_tackleDecFrame;               // タックルの減速開始フレーム
    private float m_tackleInitSpeed;            // タックルの初速度

    private float m_tackleSuccessFrame;         // タックルの成功フレーム
    private int m_tackleSuccessMotionLength;    // タックルのモーションの長さ

    private float m_tackleDamageFrame;          // タックルダメージのフレーム
    private int m_tackleDamageMotionLength;     // タックルダメージのモーションの長さ
    private float m_tackleDamageInitSpeed;      // タックルダメージの初速度
    private float m_tackleDamageDecSpeed;       // タックルの減速量
    private int m_tackleDamageDecFrame;         // タックルダメージの減速開始時間 

    //----------------------------------------------------------------------
    // コンストラクタ
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/10/14  @Update 2014/10/14  @Author T.Kawashita      
    //----------------------------------------------------------------------
    public CPlayerAction()
    {
        m_dashMotionLength = 0;
        m_dashDecFrame = 0;
        m_dashDecSpeed = 0.0f;
        m_dashFrame = 0;
        m_dashInitSpeed = 0.0f;

        m_shootFrame = 0.0f;
        m_shootMotionLength = 0;
        m_shootTakeOfFrame = 0;
        m_shootInitSpeed = 0.0f;

        m_passFrame = 0.0f;
        m_passMotionLength = 0;
        m_passTakeOfFrame = 0;
        m_passInitSpeed = 0.0f;

        m_tackleInitSpeed = 0.0f;
        m_tackleFrame = 0;
        m_tackleDecSpeed = 0.0f;
        m_tackleDecFrame = 0;
        m_tackleMotionLength = 0;

        m_tackleSuccessFrame = 0.0f;
        m_tackleSuccessMotionLength = 0;

        m_tackleDamageFrame = 0.0f;
        m_tackleDamageMotionLength = 0;
        m_tackleDamageInitSpeed = 0.0f;
        m_tackleDamageDecFrame = 0;
        m_tackleDamageDecSpeed = 0.0f;
    }

    //----------------------------------------------------------------------
    // プレイヤーの移動
    //----------------------------------------------------------------------
    // @Param	_outPos 移動した結果 _speed 移動量	
    // @Param   _forward 前方向ベクトル    _right 横方向ベクトル
    // @Return	Vector3 移動した結果
    // @Date	2014/10/16  @Update 2014/10/16  @Author T.Kawashita      
    //----------------------------------------------------------------------
    public Vector3 Move(ref Vector3 _outPos,Vector3 _speed,Vector3 _forward,Vector3 _right)
    {
        _outPos += _speed.z * _forward + _speed.x * _right;

        return _outPos;
    }

    //----------------------------------------------------------------------
    // プレイヤーの回転
    //----------------------------------------------------------------------
    // @Param	_outRot 回転角度の結果 _rotX X方向回転角度  _rotY Y方向回転角度
    // @Return	Vector3 回転角度の結果
    // @Date	2014/10/16  @Update 2014/10/16  @Author T.Kawashita      
    //----------------------------------------------------------------------
    public Vector3 Rotation(ref Vector3 _outRot, float _rotX, float _rotY)
    {
        _outRot.y += _rotX;
        _outRot.x += _rotY;

        return _outRot;
    }

    //----------------------------------------------------------------------
    // プレイヤーのダッシュ
    //----------------------------------------------------------------------
    // @Param	_dashPos 現在位置 _forward 前方向ベクトル	
    // @Return	bool　ダッシュが終わったかどうか
    // @Date	2014/10/16  @Update 2014/10/17  @Author T.Kawashita      
    //----------------------------------------------------------------------
    public bool Dash(ref Vector3 _dashPos, Vector3 _forward)
    {
        // フレームをデルタタイムで足していく
        m_dashFrame += Time.deltaTime;

        // 減速開始フレームになった場合はスピードを減速させていく
        if (m_dashFrame >= (float)m_dashDecFrame / 60 )
        {
            m_dashInitSpeed -= m_dashDecSpeed;
        }

        // 前方向に移動させる
        _dashPos += m_dashInitSpeed * _forward;

        // ダッシュ終了
        if (m_dashFrame >= (float)m_dashMotionLength / 60)
            return true;

        return false;
    }

    //----------------------------------------------------------------------
    // プレイヤーのタックル
    //----------------------------------------------------------------------
    // @Param   _tacklePos  現在位置
    // @Param   _forward    前方向ベクトル    		
    // @Return	bool    タックルが終わったかどうか
    // @Date	2014/11/17  @Update 2014/11/17  @Author T.Kawashita      
    //----------------------------------------------------------------------
    public bool Tackle(ref Vector3 _tacklePos, Vector3 _forward)
    {
        // フレームをデルタタイムで足していく
        m_tackleFrame += Time.deltaTime;

        // 減速開始フレームになった場合はスピードを減速させていく
        if (m_dashFrame >= (float)m_dashDecFrame / 60)
        {
            m_dashInitSpeed -= m_dashDecSpeed;
        }

        // 前方向に移動させる
        _tacklePos += m_tackleInitSpeed * _forward;

        // タックル終了
        if (m_tackleFrame >= (float)m_tackleMotionLength / 60)
            return true;

        return false;
    }

    //----------------------------------------------------------------------
    // プレイヤーのタックル成功中
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	bool    タックル成功モーションが終わったかどうか
    // @Date	2014/12/1  @Update 2014/12/1  @Author T.Kawashita      
    //----------------------------------------------------------------------
    public bool TackleSuccess()
    {
        // フレームをデルタタイムで足していく
        m_tackleSuccessFrame += Time.deltaTime;

        // タックル成功モーション終了
        if (m_tackleSuccessFrame >= (float)m_tackleSuccessMotionLength / 60)
            return true;

        return false;
    }

    //----------------------------------------------------------------------
    // プレイヤーのタックルダメージ中
    //----------------------------------------------------------------------
    // @Param	_pos    現在地
	// @Param   _back   反対方向ベクトル	
    // @Return	bool    タックルダメージモーションが終わったかどうか
    // @Date	2014/12/1  @Update 2014/12/1  @Author T.Kawashita      
    //----------------------------------------------------------------------
    public bool TackleDamage(ref Vector3 _pos, Vector3 _back)
    {
        // フレームをデルタタイムで足していく
        m_tackleDamageFrame += Time.deltaTime;

        // 減速開始フレームになった場合はスピードを減速させていく
        if (m_tackleDamageFrame >= (float)m_tackleDamageDecFrame / 60)
        {
            m_tackleDamageInitSpeed -= m_tackleDamageDecSpeed;
            Debug.Log("減速");
        }

        // 反対方向に移動させる
        _pos += m_tackleDamageInitSpeed * _back;

        Debug.Log(_pos);

        // タックルダメージ終了
        if (m_tackleDamageFrame >= (float)m_tackleDamageMotionLength / 60)
            return true;

        return false;
    }

    //----------------------------------------------------------------------
    // プレイヤーのパス
    //----------------------------------------------------------------------
    // @Param	_player     プレイヤーのゲームオブジェクト
	// @Param   _forward    プレイヤーの前方向ベクトル
	// @Param   _isBall     プレイヤーがボールを持っているかどうか
    // @Return	bool        パスが終わったかどうか
    // @Date	2014/11/13  @Update 2014/11/13  @Author T.Kawashita      
    //----------------------------------------------------------------------
    public bool Pass(GameObject _player, Vector3 _forward, ref bool _isBall)
    {
        // 60Fたったかどうか計算
        m_passFrame += Time.deltaTime;

        // パス状態に切り替わった場合はスクリプトの中身を変更
        if (m_passFrame >= (float)m_passTakeOfFrame / 60 && _isBall == true)
        {
            _player.transform.FindChild("SoccerBall").GetComponent<CSoccerBall>().rigidbody.velocity = _forward * m_passInitSpeed;
            _player.transform.FindChild("SoccerBall").GetComponent<CSoccerBall>().rigidbody.angularVelocity = _forward * 1.0f;
            _player.transform.FindChild("SoccerBall").GetComponent<SphereCollider>().isTrigger = false;
            _isBall = false;
        }

        // パスモーション終わりの時間になった場合はコンポーネントを切り替えて終了
        if (m_passFrame >= (float)m_passMotionLength / 60)
        {
            CSoccerBallManager.m_nowPlayer = CSoccerBallManager.eNONE;
            _player.transform.FindChild("SoccerBall").parent = GameObject.Find("BallGameObject").transform;
            return true;
        }

        return false;
    }

    //----------------------------------------------------------------------
    // プレイヤーのシュート
    //----------------------------------------------------------------------
    // @Param	_player     プレイヤーのゲームオブジェクト
	// @Param   _forward    プレイヤーの前方向ベクトル
	// @Param   _isBall     プレイヤーがボールを持っているかどうか
    // @Return	bool    シュートが終わったかどうか
    // @Date	2014/10/27  @Update 2014/10/28  @Author T.Kawashita      
    //----------------------------------------------------------------------
    public bool Shoot(GameObject _player,Vector3 _forward,ref bool _isBall)
    {
        // 60Fたったかどうか計算
        m_shootFrame += Time.deltaTime;

        // シュート状態に切り替わった場合はスクリプトの中身を変更
        if (m_shootFrame >= (float)m_shootTakeOfFrame / 60 && _isBall == true)
        {
            _player.transform.FindChild("SoccerBall").GetComponent<CSoccerBall>().rigidbody.velocity = _forward * m_shootInitSpeed;
            _player.transform.FindChild("SoccerBall").GetComponent<CSoccerBall>().rigidbody.angularVelocity = _forward * 1.0f;
            _player.transform.FindChild("SoccerBall").GetComponent<SphereCollider>().isTrigger = false;
            _isBall = false;    // プレイヤーのボールではない状態にする
        }

        // シュートモーション終わりの時間になった場合はコンポーネントを切り替えて終了
        if( m_shootFrame >= (float)m_shootMotionLength / 60 )
        {
            CSoccerBallManager.m_nowPlayer = CSoccerBallManager.eNONE;
            _player.transform.FindChild("SoccerBall").parent = GameObject.Find("BallGameObject").transform;
            return true;
        }

        // シュートモーション終了していない
        return false;
     }

    //----------------------------------------------------------------------
    // プレイヤーのダッシュの初期化
    //----------------------------------------------------------------------
    // @Param	float    ダッシュの初速度
	// @Param   int      ダッシュのモーションの長さ      
    // @Param   int      ダッシュが減速し始める時間
    // @Return	none
    // @Date	2014/10/17  @Update 2014/11/14  @Author T.Kawashita      
    //----------------------------------------------------------------------
    public void InitDash(float _dashInitSpeed, int _dashMotionLength, int _dashDecFrame)
    {
        m_dashMotionLength = _dashMotionLength;
        m_dashDecFrame = _dashDecFrame;
        m_dashInitSpeed = _dashInitSpeed;
        m_dashFrame = 0.0f;

        // ダッシュの減速量計算
        m_dashDecSpeed = _dashInitSpeed / (float)( m_dashMotionLength - m_dashDecFrame );

    }

    //----------------------------------------------------------------------
    // プレイヤーのタックルの初期化
    //----------------------------------------------------------------------
    // @Param	_tackleSpeed        タックルスピード
	// @Param   _tackleMotionLength タックル全体のフレーム
	// @Param   _tackleDecFrame     減速までの時間
    // @Return	none
    // @Date	2014/11/14  @Update 2014/11/14  @Author T.Kawashita      
    //----------------------------------------------------------------------
    public void InitTackle(float _tackleInitSpeed, int _tackleMotionLength, int _tackleDecFrame)
    {
        m_tackleMotionLength = _tackleMotionLength;
        m_tackleDecFrame = _tackleDecFrame;
        m_tackleInitSpeed = _tackleInitSpeed;
        m_tackleFrame = 0.0f;

        // タックルの減速量計算
        m_tackleDecSpeed = _tackleInitSpeed / (float)( m_tackleMotionLength - m_tackleDecFrame );
    }

    //----------------------------------------------------------------------
    // プレイヤーのタックル成功の初期化
    //----------------------------------------------------------------------
    // @Param	_tackleSuccessMotionLength  タックルの成功モーションの長さ		
    // @Return	none
    // @Date	2014/12/1  @Update 2014/12/1  @Author T.Kawashita      
    //----------------------------------------------------------------------
    public void InitTackleSuccess(int _tackleSuccessMotionLength)
    {
        m_tackleSuccessFrame = 0.0f;
        m_tackleSuccessMotionLength = _tackleSuccessMotionLength;
    }

    //----------------------------------------------------------------------
    // プレイヤーのタックルダメージの初期化
    //----------------------------------------------------------------------
    // @Param	_tackleDamageMotionLength   タックルダメージモーションの長さ
	// @Param   _tackleDamageInitSpeed      タックルダメージの初速度
	// @Param   _tackleDamageDecFrame       タックルダメージの減速開始フレーム
    // @Return	none
    // @Date	2014/12/1  @Update 2014/12/1  @Author T.Kawashita      
    //----------------------------------------------------------------------
    public void InitTackleDamage(int _tackleDamageMotionLength, float _tackleDamageInitSpeed, int _tackleDamageDecFrame)
    {
        m_tackleDamageFrame = 0.0f;
        m_tackleDamageMotionLength = _tackleDamageMotionLength;
        m_tackleDamageInitSpeed = _tackleDamageInitSpeed;
        m_tackleDamageDecFrame = _tackleDamageDecFrame;

        // タックルダメージの減速量計算
        m_tackleDecSpeed = _tackleDamageInitSpeed / (float)(m_tackleDamageMotionLength - m_tackleDamageDecFrame);
 
    }

    //----------------------------------------------------------------------
    // プレイヤーのシュートの初期化
    //----------------------------------------------------------------------
    // @Param	_initShootSpeed     シュートの初速度
	// @Param   _shootMotionLength  シュートの全体フレーム
    // @Param   _shootTakeOfFrame
    // @Return	none
    // @Date	2014/10/27  @Update 2014/10/27  @Author T.Kawashita      
    //----------------------------------------------------------------------
    public void InitShoot(float _initShootSpeed, int _shootMotionLength, int _shootTakeOfFrame)
    {
        m_shootFrame = 0.0f;
        m_shootInitSpeed = _initShootSpeed;
        m_shootMotionLength = _shootMotionLength;
        m_shootTakeOfFrame = _shootTakeOfFrame;
    }

    //----------------------------------------------------------------------
    // プレイヤーのパスの初期化
    //----------------------------------------------------------------------
    // @Param	_initPassSpeed      パスの初速度
	// @Param   _passMotionLength   パスの全体フレーム
	// @Paramm  _passTakeOfFrame    どのタイミングで足を離れるか
    // @Return	none
    // @Date	2014/11/13  @Update 2014/11/13  @Author T.Kawashita      
    //----------------------------------------------------------------------
    public void InitPass(float _initPassSpeed, int _passMotionLength, int _passTakeOfFrame)
    {
        m_passFrame = 0.0f;
        m_passInitSpeed = _initPassSpeed;
        m_passMotionLength = _passMotionLength;
        m_passTakeOfFrame = _passTakeOfFrame;
    }
}
