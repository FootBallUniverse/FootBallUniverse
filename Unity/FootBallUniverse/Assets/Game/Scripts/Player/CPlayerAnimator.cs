using UnityEngine;
using System;
using System.Collections;

public class CPlayerAnimator : MonoBehaviour{

    // flag管理
    public Animator m_animator;

    private int m_oldAnimation; // 前回のアニメーションのID

    public int m_isWait;
    public int m_isFrontMove;
    public int m_isBackMove;
    public int m_isRightMove;
    public int m_isLeftMove;

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
        m_isFrontMove = Animator.StringToHash("isFrontMove");
        m_isBackMove = Animator.StringToHash("isBackMove");
        m_isRightMove = Animator.StringToHash("isRightMove");
        m_isLeftMove = Animator.StringToHash("isLeftMove");

        m_oldAnimation = m_isWait;

        m_animator.SetBool(m_isWait, false);
        m_animator.SetBool(m_isFrontMove, false);
        m_animator.SetBool(m_isBackMove, false);
        m_animator.SetBool(m_isRightMove, false);
        m_animator.SetBool(m_isLeftMove, false);
	}

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
            m_oldAnimation = m_isWait;
            // 待機状態に戻したら終了
            return;
        }

        // 絶対値を参照
        float xAbs = Math.Abs(_speed.x);
        float zAbs = Math.Abs(_speed.z);

        // 横移動の方が大きい場合
        if (xAbs >= zAbs)
        {
            // 右移動
            if (_speed.x >= 0.0f && m_oldAnimation != m_isRightMove)
            {
                m_animator.SetBool(m_oldAnimation, false);
                m_animator.SetBool(m_isRightMove, true);
                m_oldAnimation = m_isRightMove;
                return;
            }
            // 左移動
            else if (_speed.x <= 0.0f && m_oldAnimation != m_isLeftMove)
            {
                m_animator.SetBool(m_oldAnimation, false );
                m_animator.SetBool(m_isLeftMove, true);
                m_oldAnimation = m_isLeftMove;
                return;
            }
        }
        // 前後移動の方が大きい場合
        else if (xAbs <= zAbs)
        { 
            // 前移動
            if (_speed.z >= 0.0f && m_oldAnimation != m_isFrontMove)
            {
                m_animator.SetBool(m_oldAnimation, false);
                m_animator.SetBool(m_isFrontMove, true);
                m_oldAnimation = m_isFrontMove;
                return;
            }
            // 後移動
            else if (_speed.z <= 0.0f && m_oldAnimation != m_isBackMove)
            {
                m_animator.SetBool(m_oldAnimation, false);
                m_animator.SetBool(m_isBackMove, true);
                m_oldAnimation = m_isBackMove;
                return;
            }
        }

    }

}
