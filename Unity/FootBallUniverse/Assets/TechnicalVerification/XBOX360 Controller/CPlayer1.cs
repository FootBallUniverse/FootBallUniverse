using UnityEngine;
using System.Collections;

public class CPlayer1 : CPlayer {

    // プレイヤーの動きクラス
    private CActionPlayer m_action;

    const float DASH_SPEED = 1.0f;
    private Vector3 m_speed;

	// Use this for initialization
	void Start () {

        m_pos = this.transform.localPosition;
        m_pos = m_old_pos;
        m_angle = new Vector3(0.0f, 0.0f);

        m_action = new CActionPlayer();
	}
	
	// Update is called once per frame
	void Update () {
        
        this.Move();
        this.Rotation();
        if (Input.GetKeyDown(InputXBOX360.P1_XBOX_LEFT_ANALOG_PRESS))
        {
            this.Dash();
        }
    }

    //----------------------------------------------------------------------
    // プレイヤーの移動
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/10/16  @Update 2014/10/16  @Author T.Kawashita      
    //----------------------------------------------------------------------
    private void Move()
    {
        Vector3 speed = new Vector3(0.0f, 0.0f, 0.0f);
        speed.x = Input.GetAxis(InputXBOX360.P1_XBOX_LEFT_ANALOG_X) * 0.2f;
        speed.y = Input.GetAxis(InputXBOX360.P1_XBOX_LEFT_ANALOG_Y) * 0.2f;
        this.transform.localPosition = m_action.Move(ref m_pos, speed, this.transform.forward, this.transform.right);
    }

    //----------------------------------------------------------------------
    // プレイヤーの回転
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/10/16  @Update 2014/10/16   @Author T.Kawashita      
    //----------------------------------------------------------------------
    private void Rotation()
    {
        Vector2 angle;
        Quaternion q;
        angle.x = Input.GetAxis(InputXBOX360.P1_XBOX_RIGHT_ANALOG_X) * 5;
        angle.y = Input.GetAxis(InputXBOX360.P1_XBOX_RIGHT_ANALOG_Y) * 5;
        this.transform.localRotation = m_action.Rotation(ref m_angle, angle.x, angle.y);
    }

    //----------------------------------------------------------------------
    // プレイヤーのダッシュ
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/10/16  @Update 2014/10/16  @Author T.Kawashita     
    //----------------------------------------------------------------------
    private void Dash()
    {

     //   this.transform.localPosition = m_action.Dash(ref m_pos, m_speed,this.transform.forward);
    }

}
