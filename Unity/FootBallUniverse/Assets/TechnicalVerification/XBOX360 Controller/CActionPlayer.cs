using UnityEngine;
using System.Collections;

//----------------------------------------------------
// プレイヤーの動きクラス
// 基本的にプレイヤーのクラスにこのクラスを
// インスタンス化させたものを持たせて動きをさせる
//----------------------------------------------------

public class CActionPlayer {

    //----------------------------------------------------------------------
    // コンストラクタ
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/10/14  @Update 2014/10/14  @Author T.Kawashita      
    //----------------------------------------------------------------------
    public CActionPlayer()
    {
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
    // @Date	2014/10/16  @Update 2014/10/16  @Author 2014/10/16      
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
    // @Param	_dashPos ダッシュした後の位置 _speed スピード
	// @Param   _forward 前方向ベクトル	
    // @Return	Vector3 ダッシュした後の位置
    // @Date	2014/10/16  @Update 2014/10/16  @Author 2014/10/16      
    //----------------------------------------------------------------------
    public Vector3 Dash(ref Vector3 _dashPos, Vector3 _speed, Vector3 _foarward, Vector3 _right)
    {
        _dashPos += _speed.z * _foarward + _speed.x * _right;

        return _dashPos;
    }

}
