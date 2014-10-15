﻿using UnityEngine;
using System.Collections;

public class InputXBOX360 : MonoBehaviour{

    //----------------------------------------------------
    // XBOXコントローラーの定数データ
    //----------------------------------------------------
    // @Date    10月8日17:10  @Update 10月9日15:20  
    // @Author  T.Kawashita
    //----------------------------------------------------
    const KeyCode P1_XBOX_A = KeyCode.Joystick1Button0;                     // Player1 Aボタン
    const KeyCode P1_XBOX_B = KeyCode.Joystick1Button1;                     // Player1 Bボタン
    const KeyCode P1_XBOX_X = KeyCode.Joystick1Button2;                     // Player1 Xボタン    
    const KeyCode P1_XBOX_Y = KeyCode.Joystick1Button3;                     // Player1 Yボタン
    const KeyCode P1_XBOX_L = KeyCode.Joystick1Button4;                     // Player1 Lボタン
    const KeyCode P1_XBOX_R = KeyCode.Joystick1Button5;                     // Player1 Rボタン 
    const KeyCode P1_XBOX_BACK = KeyCode.Joystick1Button6;                  // Player1 BACKボタン
    const KeyCode P1_XBOX_START = KeyCode.Joystick1Button7;                 // Player1 STARTボタン
    const KeyCode P1_XBOX_LEFT_ANALOG_PRESS = KeyCode.Joystick1Button8;     // Player1 LeftAnalogPressボタン
    const KeyCode P1_XBOX_RIGHT_ANALOG_PRESS = KeyCode.Joystick1Button9;    // Player1 RightAnalogPressボタン

    const KeyCode P2_XBOX_A = KeyCode.Joystick2Button0;                     // Player2 Aボタン
    const KeyCode P2_XBOX_B = KeyCode.Joystick2Button1;                     // Player2 Bボタン
    const KeyCode P2_XBOX_X = KeyCode.Joystick2Button2;                     // Player2 Xボタン
    const KeyCode P2_XBOX_Y = KeyCode.Joystick2Button3;                     // Player2 Yボタン
    const KeyCode P2_XBOX_L = KeyCode.Joystick2Button4;                     // Player2 Lボタン
    const KeyCode P2_XBOX_R = KeyCode.Joystick2Button5;                     // Plyaer2 Rボタン
    const KeyCode P2_XBOX_BACK = KeyCode.Joystick2Button6;                  // Player2 BACKボタン
    const KeyCode P2_XBOX_START = KeyCode.Joystick2Button7;                 // Player2 STARTボタン
    const KeyCode P2_XBOX_LEFT_ANALOG_PRESS = KeyCode.Joystick2Button8;     // Player2 LeftAnalogPressボタン
    const KeyCode P2_XBOX_RIGHT_ANALOG_PRESS = KeyCode.Joystick2Button9;    // Player2 RightAnalogPressボタン

    const KeyCode P3_XBOX_A = KeyCode.Joystick3Button0;                     // Player3 Aボタン
    const KeyCode P3_XBOX_B = KeyCode.Joystick3Button1;                     // Player3 Bボタン
    const KeyCode P3_XBOX_X = KeyCode.Joystick3Button2;                     // Player3 Xボタン
    const KeyCode P3_XBOX_Y = KeyCode.Joystick3Button3;                     // Player3 Yボタン
    const KeyCode P3_XBOX_L = KeyCode.Joystick3Button4;                     // Player3 Lボタン
    const KeyCode P3_XBOX_R = KeyCode.Joystick3Button5;                     // Plyaer3 Rボタン
    const KeyCode P3_XBOX_BACK = KeyCode.Joystick3Button6;                  // Player3 BACKボタン
    const KeyCode P3_XBOX_START = KeyCode.Joystick3Button7;                 // Player3 STARTボタン
    const KeyCode P3_XBOX_LEFT_ANALOG_PRESS = KeyCode.Joystick3Button8;     // Player3 LeftAnalogPressボタン
    const KeyCode P3_XBOX_RIGHT_ANALOG_PRESS = KeyCode.Joystick3Button9;    // Player3 RightAnalogPressボタン

    const KeyCode P4_XBOX_A = KeyCode.Joystick4Button0;                     // Player4 Aボタン
    const KeyCode P4_XBOX_B = KeyCode.Joystick4Button1;                     // Player4 Bボタン
    const KeyCode P4_XBOX_X = KeyCode.Joystick4Button2;                     // Player4 Xボタン
    const KeyCode P4_XBOX_Y = KeyCode.Joystick4Button3;                     // Player4 Yボタン
    const KeyCode P4_XBOX_L = KeyCode.Joystick4Button4;                     // Player4Lボタン
    const KeyCode P4_XBOX_R = KeyCode.Joystick4Button5;                     // Plyaer4 Rボタン
    const KeyCode P4_XBOX_BACK = KeyCode.Joystick4Button6;                  // Player4 BACKボタン
    const KeyCode P4_XBOX_START = KeyCode.Joystick4Button7;                 // Player4 STARTボタン
    const KeyCode P4_XBOX_LEFT_ANALOG_PRESS = KeyCode.Joystick4Button8;     // Player4 LeftAnalogPressボタン
    const KeyCode P4_XBOX_RIGHT_ANALOG_PRESS = KeyCode.Joystick4Button9;    // Player4 RightAnalogPressボタン

    public static string P1_XBOX_RIGHT_ANALOG_X = "P1_RIGHT_ANALOG_X";              // Player1 右アナログスティックのX方向
    public static string P1_XBOX_RIGHT_ANALOG_Y = "P1_RIGHT_ANALOG_Y";              // Player1 右アナログスティックのY方向
    public static string P1_XBOX_LEFT_ANALOG_X = "P1_LEFT_ANALOG_X";                // Player1 左アナログスティックのX方向
    public static string P1_XBOX_LEFT_ANALOG_Y = "P1_LEFT_ANALOG_Y";                // Player1 左アナログスティックのY方向
    public static string P1_XBOX_DPAD_X = "P1_DPAD_X";                              // Player1 DPADのX方向  
    public static string P1_XBOX_DPAD_Y = "P1_DPAD_Y";                              // Player1 DPADのY方向
    public static string P1_XBOX_RTLT = "P1_RTLT";                                  // Player1 RTLTボタン

    public static string P2_XBOX_RIGHT_ANALOG_X = "P2_RIGHT_ANALOG_X";              // Player2 右アナログスティックのX方向
    public static string P2_XBOX_RIGHT_ANALOG_Y = "P2_RIGHT_ANALOG_Y";              // Player2 右アナログスティックのY方向
    public static string P2_XBOX_LEFT_ANALOG_X = "P2_LEFT_ANALOG_X";                // Player2 左アナログスティックのX方向
    public static string P2_XBOX_LEFT_ANALOG_Y = "P2_LEFT_ANALOG_Y";                // Player2 左アナログスティックのY方向
    public static string P2_XBOX_DPAD_X = "P2_DPAD_X";                              // Player2 DPADのX方向  
    public static string P2_XBOX_DPAD_Y = "P2_DPAD_Y";                              // Player2 DPADのY方向
    public static string P2_XBOX_RTLT = "P2_RTLT";                                  // Player2 RTLTボタン

    public static string P3_XBOX_RIGHT_ANALOG_X = "P3_RIGHT_ANALOG_X";              // Player3 右アナログスティックのX方向
    public static string P3_XBOX_RIGHT_ANALOG_Y = "P3_RIGHT_ANALOG_Y";              // Player3 右アナログスティックのY方向
    public static string P3_XBOX_LEFT_ANALOG_X = "P3_LEFT_ANALOG_X";                // Player3 左アナログスティックのX方向
    public static string P3_XBOX_LEFT_ANALOG_Y = "P3_LEFT_ANALOG_Y";                // Player3 左アナログスティックのY方向
    public static string P3_XBOX_DPAD_X = "P3_DPAD_X";                              // Player3 DPADのX方向  
    public static string P3_XBOX_DPAD_Y = "P3_DPAD_Y";                              // Player3 DPADのY方向
    public static string P3_XBOX_RTLT = "P3_RTLT";                                  // Player3 RTLTボタン

    public static string P4_XBOX_RIGHT_ANALOG_X = "P4_RIGHT_ANALOG_X";              // Player4 右アナログスティックのX方向
    public static string P4_XBOX_RIGHT_ANALOG_Y = "P4_RIGHT_ANALOG_Y";              // Player4 右アナログスティックのY方向
    public static string P4_XBOX_LEFT_ANALOG_X = "P4_LEFT_ANALOG_X";                // Player4 左アナログスティックのX方向
    public static string P4_XBOX_LEFT_ANALOG_Y = "P4_LEFT_ANALOG_Y";                // Player4 左アナログスティックのY方向
    public static string P4_XBOX_DPAD_X = "P4_DPAD_X";                              // Player4 DPADのX方向  
    public static string P4_XBOX_DPAD_Y = "P4_DPAD_Y";                              // Player4 DPADのY方向
    public static string P4_XBOX_RT = "P4_RTLT";                                    // Player4 RTLTボタン

    private float x;
    private float speed = 0.05f;
    const float thumbstickDeadZone = 0.3f;
    GameObject unitychan;
    GameObject camera;

    void Start()
    {
        x = 0.0f;
        unitychan = GameObject.Find("unitychan");
        camera = GameObject.Find("Main Camera");
        camera.transform.localPosition = new Vector3(0.0f, 1.0f, -2.0f);
    }

    void Update()
    {
        GetButton();
    }

    //----------------------------------------------------------------------
    // ボタンの取得
    //----------------------------------------------------------------------
    // @Param   なし	
    // @Return	なし
    // @Date	10月8日15:30  @Update 10月8日15:30  @Author T.Kawashita       
    //----------------------------------------------------------------------
    public void GetButton()
    {
        if (Input.GetKeyDown(P1_XBOX_A))
            Debug.Log("1P Aが押された");

        if (Input.GetKeyDown(P1_XBOX_B))
            Debug.Log("1P Bが押された");

        if (Input.GetKeyDown(P1_XBOX_X))
            Debug.Log("1P Xが押された");

        if (Input.GetKeyDown(P1_XBOX_Y))
            Debug.Log("1P Yが押された");

        if (Input.GetKeyDown(P1_XBOX_R))
            Debug.Log("1P Rが押された");

        if (Input.GetKeyDown(P1_XBOX_L))
            Debug.Log("1P Lが押された");

        if (Input.GetKeyDown(P1_XBOX_START))
            Debug.Log("1P STARTが押された");

        if (Input.GetKeyDown(P1_XBOX_BACK))
            Debug.Log("1P BACKが押された");

        if (Input.GetKeyDown(P2_XBOX_A))
            Debug.Log("2P Aが押された");

        if( Input.GetKeyDown(P2_XBOX_B))
            Debug.Log("2P Bが押された");

        if (Input.GetKeyDown(P1_XBOX_RIGHT_ANALOG_PRESS))
            Debug.Log("1P RightAnalogが押された");

        if (Input.GetKeyDown(P1_XBOX_LEFT_ANALOG_PRESS))
            Debug.Log("1P LeftAnalogが押された");

        if (Input.GetKeyDown(P2_XBOX_RIGHT_ANALOG_PRESS))
            Debug.Log("2P RightAnalogが押された");

        Vector2 dInp, leftInp;
        dInp.x = Input.GetAxis(P1_XBOX_RIGHT_ANALOG_X);
        dInp.y = Input.GetAxis(P1_XBOX_RIGHT_ANALOG_Y);

        Debug.Log(dInp.x);
        dInp.x = dInp.x * 10 / 180;

        leftInp.x = Input.GetAxis(P1_XBOX_LEFT_ANALOG_X);
        leftInp.y = Input.GetAxis(P1_XBOX_LEFT_ANALOG_Y);

 //       unitychan.transform.localRotation = Quaternion.AngleAxis(dInp.x, Vector3.up);
 //       camera.transform.localPosition = new Vector3(leftInp.x * 10, leftInp.y * 10 + 1.0f, -2.0f);

        float ltrt;
        ltrt = Input.GetAxis(P1_XBOX_RTLT);
        Debug.Log(ltrt);

        /*
        if (Input.GetAxisRaw(XBOX_RIGHT_ANALOG_X) > thumbstickDeadZone || Input.GetAxisRaw(XBOX_RIGHT_ANALOG_X) < -thumbstickDeadZone)
        {
            float axisInput = Input.GetAxisRaw(XBOX_RIGHT_ANALOG_X);
            Debug.Log(axisInput);
        }

        if (Input.GetAxisRaw(XBOX_RIGHT_ANALOG_Y) > thumbstickDeadZone || Input.GetAxisRaw(XBOX_RIGHT_ANALOG_Y) < -thumbstickDeadZone)
        {
            float axisInput = Input.GetAxisRaw(XBOX_RIGHT_ANALOG_Y);
            Debug.Log(axisInput);
        }

        if (Input.GetAxisRaw(XBOX_LEFT_ANALOG_X) > thumbstickDeadZone || Input.GetAxisRaw(XBOX_LEFT_ANALOG_X) < -thumbstickDeadZone)
        {
            float axisInput = Input.GetAxisRaw(XBOX_LEFT_ANALOG_X);
            Debug.Log(axisInput);
        }

        if (Input.GetAxisRaw(XBOX_LEFT_ANALOG_Y) > thumbstickDeadZone || Input.GetAxisRaw(XBOX_LEFT_ANALOG_Y) < -thumbstickDeadZone)
        {
            float axisInput = Input.GetAxisRaw(XBOX_LEFT_ANALOG_Y);
            Debug.Log(axisInput);
        }
         * */

    }

}
