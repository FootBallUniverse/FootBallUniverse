using UnityEngine;
using System.Collections;

//----------------------------------------------------
// プレイヤーの動きクラス
// 基本的にプレイヤーのクラスにこのクラスを
// インスタンス化させたものを持たせて動きをさせる
//----------------------------------------------------

public class CActionPlayer {

    private int m_dashWholeFrame;     // ダッシュ全体速度
    private int m_dashDeceFrame;      // 減速開始速度
    private float m_dashSpeed;        // ダッシュのスピード
    private float m_dashDeceSpeed;    // ダッシュの減速量
    private int m_dashFrame;          // ダッシュのフレーム

    //----------------------------------------------------------------------
    // コンストラクタ
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/10/14  @Update 2014/10/14  @Author T.Kawashita      
    //----------------------------------------------------------------------
    public CActionPlayer()
    {
        m_dashWholeFrame = 0;
        m_dashDeceFrame = 0;
        m_dashSpeed = 0.0f;
        m_dashDeceSpeed = 0.0f;
        m_dashFrame = 0;
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
    public Quaternion Rotation(ref Vector3 _outRot, float _rotX, float _rotY)
    {
        _outRot.y += _rotX;
        _outRot.x += _rotY;

        return Quaternion.Euler(_outRot.x, _outRot.y, 0.0f);
        
    }

    //----------------------------------------------------------------------
    // プレイヤーのダッシュ
    //----------------------------------------------------------------------
    // @Param	_dashPos 現在位置 _forward 前方向ベクトル	
    // @Return	bool　ダッシュが終わったかどうか
    // @Date	2014/10/16  @Update 2014/10/17  @Author T.Kawashita      
    //----------------------------------------------------------------------
    public bool Dash(ref Vector3 _dashPos, Vector3 _foarward)
    {
        m_dashFrame ++;
        
        // 減速開始フレームになった場合はスピードを減速させていく
        if (m_dashFrame >= m_dashDeceFrame)
        {
            m_dashSpeed -= m_dashDeceSpeed;
        }

        Debug.Log(m_dashSpeed);

        // 移動させる
        _dashPos += m_dashSpeed * _foarward;

        // ダッシュ終了
        if (m_dashFrame >= m_dashWholeFrame)
            return true;

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


        Debug.Log("dashDeceSpeed:" + m_dashDeceSpeed);

  
    }

}
