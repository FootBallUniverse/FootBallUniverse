using UnityEngine;
using System;
using System.Collections;

public class PlayerAnimator : MonoBehaviour
{

    public Animator m_animator;

    public int m_oldAnimation; // 前回のアニメーションのID

    // flag管理
    public int m_isWait;
    public int m_isShoot;
    public int m_isDashCharge;

    //----------------------------------------------------------------------
    // コンストラクタ
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/11/10  @Update 2014/11/10  @Author T.Kawashita      
    //----------------------------------------------------------------------
    void Start()
    {
        m_animator = this.gameObject.GetComponent<Animator>();

        m_isWait = Animator.StringToHash("isWait");
        m_isShoot = Animator.StringToHash("isShoot");
        m_isDashCharge = Animator.StringToHash("isDashCharge");

        m_oldAnimation = m_isWait;

        m_animator.SetBool(m_isWait, false);
        m_animator.SetBool(m_isShoot, false);
    }
    /*
        //----------------------------------------------------------------------
        // 移動アニメーション
        //----------------------------------------------------------------------
        // @Param	Vector3     移動速度		
        // @Return	none
        // @Date	2014/11/10  @Update 2014/11/10  @Author T.Kawashita      
        //----------------------------------------------------------------------
        public void Move(Vector3 _speed)
        {
            // 待機状態判定
            if (_speed.x == 0.0f && _speed.z == 0.0f && m_oldAnimation == m_isWait)
                return;

            // 待機状態に戻す
            if (_speed.x == 0.0f && _speed.z == 0.0f && m_oldAnimation != m_isWait)
            {
                m_animator.SetBool(m_oldAnimation,false);
                m_animator.SetBool(m_isWait, true);
                m_oldAnimation = m_isWait; 
                // 待機状態に戻したら終了
                return;
            }

        }
        */

    //----------------------------------------------------------------------
    // シュートモーション
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/11/17  @Update 2014/11/17  @Author T.Kawashita      
    //----------------------------------------------------------------------
    public void Shoot()
    {
        this.ChangeAnimation(m_isShoot);
    }

    //----------------------------------------------------------------------
    // アニメーションの変更
    //----------------------------------------------------------------------
    // @Param	int    アニメーションのハッシュID		
    // @Return	none
    // @Date	2014/11/15  @Update 2014/11/15  @Author T.Kawashita      
    //----------------------------------------------------------------------
    public void ChangeAnimation(int _animationHash)
    {
        m_animator.SetBool(m_oldAnimation, false);
        m_animator.SetBool(_animationHash, true);
        m_oldAnimation = _animationHash;
    }
}