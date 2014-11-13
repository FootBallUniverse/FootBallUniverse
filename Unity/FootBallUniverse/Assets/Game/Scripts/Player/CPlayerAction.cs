using UnityEngine;
using System.Collections;

//----------------------------------------------------------------------
// プレイヤーのアクション
//----------------------------------------------------------------------
// @Date 2014/10/30  @Update 2014/11/11  @Author T.Kawashita      
//----------------------------------------------------------------------
public class CPlayerAction {

    private int m_dashWholeFrame;     // ダッシュ全体速度
    private int m_dashDeceFrame;      // 減速開始速度
    private float m_dashSpeed;        // ダッシュのスピード
    private float m_dashDeceSpeed;    // ダッシュの減速量
    private int m_dashFrame;          // ダッシュのフレーム

    private float m_shootFrame;       // シュートのフレーム
    private int m_shootMotionLength;  // シュート全体の長さ
    private int m_shootTakeOfFrame;   // シュートが足を離れるまでの時間
    private float m_shootInitSpeed;   // シュート速度

    private float m_passFrame;        // パスのフレーム
    private int m_passMotionLength;   // パス全体の長さ 
    private int m_passTakeOfFrame;    // パスが足を離れるまでの時間
    private float m_passInitSpeed;    // パス速度

    //----------------------------------------------------------------------
    // コンストラクタ
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/10/14  @Update 2014/10/14  @Author T.Kawashita      
    //----------------------------------------------------------------------
    public CPlayerAction()
    {
        m_dashWholeFrame = 0;
        m_dashDeceFrame = 0;
        m_dashSpeed = 0.0f;
        m_dashDeceSpeed = 0.0f;
        m_dashFrame = 0;

        m_shootFrame = 0.0f;
        m_shootMotionLength = 0;
        m_shootTakeOfFrame = 0;
        m_shootInitSpeed = 0.0f;

        m_passFrame = 0.0f;
        m_passMotionLength = 0;
        m_passTakeOfFrame = 0;
        m_passInitSpeed = 0.0f;
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
        m_dashFrame ++;
        
        // 減速開始フレームになった場合はスピードを減速させていく
        if (m_dashFrame >= m_dashDeceFrame)
        {
            m_dashSpeed -= m_dashDeceSpeed;
        }

        // 移動させる
        _dashPos += m_dashSpeed * _forward;

        // ダッシュ終了
        if (m_dashFrame >= m_dashWholeFrame)
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
        if (m_passFrame >= m_passTakeOfFrame / 60 && _isBall == true)
        {
            _player.transform.FindChild("SoccerBall").GetComponent<CSoccerBall>().rigidbody.velocity = _forward * m_passInitSpeed;
            _player.transform.FindChild("SoccerBall").GetComponent<CSoccerBall>().rigidbody.angularVelocity = _forward * 1.0f;
            _isBall = false;
        }

        // パスモーション終わりの時間になった場合はコンポーネントを切り替えて終了
        if (m_passFrame >= (float)m_passMotionLength / 60)
        {
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
            _isBall = false;    // プレイヤーのボールではない状態にする
        }

        // シュートモーション終わりの時間になった場合はコンポーネントを切り替えて終了
        if( m_shootFrame >= (float)m_shootMotionLength / 60 )
        {
            _player.transform.FindChild("SoccerBall").parent = GameObject.Find("BallGameObject").transform;
            return true;
        }

        // シュートモーション終了していない
        return false;
     }

    //----------------------------------------------------------------------
    // プレイヤーのダッシュの初期化
    //----------------------------------------------------------------------
    // @Param	_dashSpeed  ダッシュスピード		
    // @Return	none
    // @Date	2014/10/17  @Update 2014/10/17  @Author T.Kawashita      
    //----------------------------------------------------------------------
    public void InitDash(float _dashSpeed)
    {
        m_dashWholeFrame = 10;
        m_dashDeceFrame = 7;
        m_dashSpeed = _dashSpeed;
        m_dashFrame = 0;

        // ダッシュの減速量計算
        m_dashDeceSpeed = _dashSpeed / (float)( m_dashWholeFrame - m_dashDeceFrame );

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
