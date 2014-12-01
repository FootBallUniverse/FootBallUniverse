using UnityEngine;
using System.Collections;

public class InputXBOX360
{
    //----------------------------------------------------
    // XBOXコントローラーの定数データ
    //----------------------------------------------------
    // @Date    10月8日17:10  @Update 10月9日15:20  
    // @Author  T.Kawashita
    //----------------------------------------------------
    public static KeyCode P1_XBOX_A = KeyCode.Joystick1Button0;                     // Player1 Aボタン
    public static KeyCode P1_XBOX_B = KeyCode.Joystick1Button1;                     // Player1 Bボタン
    public static KeyCode P1_XBOX_X = KeyCode.Joystick1Button2;                     // Player1 Xボタン    
    public static KeyCode P1_XBOX_Y = KeyCode.Joystick1Button3;                     // Player1 Yボタン
    public static KeyCode P1_XBOX_L = KeyCode.Joystick1Button4;                     // Player1 Lボタン
    public static KeyCode P1_XBOX_R = KeyCode.Joystick1Button5;                     // Player1 Rボタン 
    public static KeyCode P1_XBOX_BACK = KeyCode.Joystick1Button6;                  // Player1 BACKボタン
    public static KeyCode P1_XBOX_START = KeyCode.Joystick1Button7;                 // Player1 STARTボタン
    public static KeyCode P1_XBOX_LEFT_ANALOG_PRESS = KeyCode.Joystick1Button8;     // Player1 LeftAnalogPressボタン
    public static KeyCode P1_XBOX_RIGHT_ANALOG_PRESS = KeyCode.Joystick1Button9;    // Player1 RightAnalogPressボタン

    public static KeyCode P2_XBOX_A = KeyCode.Joystick2Button0;                     // Player2 Aボタン
    public static KeyCode P2_XBOX_B = KeyCode.Joystick2Button1;                     // Player2 Bボタン
    public static KeyCode P2_XBOX_X = KeyCode.Joystick2Button2;                     // Player2 Xボタン
    public static KeyCode P2_XBOX_Y = KeyCode.Joystick2Button3;                     // Player2 Yボタン
    public static KeyCode P2_XBOX_L = KeyCode.Joystick2Button4;                     // Player2 Lボタン
    public static KeyCode P2_XBOX_R = KeyCode.Joystick2Button5;                     // Plyaer2 Rボタン
    public static KeyCode P2_XBOX_BACK = KeyCode.Joystick2Button6;                  // Player2 BACKボタン
    public static KeyCode P2_XBOX_START = KeyCode.Joystick2Button7;                 // Player2 STARTボタン
    public static KeyCode P2_XBOX_LEFT_ANALOG_PRESS = KeyCode.Joystick2Button8;     // Player2 LeftAnalogPressボタン
    public static KeyCode P2_XBOX_RIGHT_ANALOG_PRESS = KeyCode.Joystick2Button9;    // Player2 RightAnalogPressボタン

    public static KeyCode P3_XBOX_A = KeyCode.Joystick3Button0;                     // Player3 Aボタン
    public static KeyCode P3_XBOX_B = KeyCode.Joystick3Button1;                     // Player3 Bボタン
    public static KeyCode P3_XBOX_X = KeyCode.Joystick3Button2;                     // Player3 Xボタン
    public static KeyCode P3_XBOX_Y = KeyCode.Joystick3Button3;                     // Player3 Yボタン
    public static KeyCode P3_XBOX_L = KeyCode.Joystick3Button4;                     // Player3 Lボタン
    public static KeyCode P3_XBOX_R = KeyCode.Joystick3Button5;                     // Plyaer3 Rボタン
    public static KeyCode P3_XBOX_BACK = KeyCode.Joystick3Button6;                  // Player3 BACKボタン
    public static KeyCode P3_XBOX_START = KeyCode.Joystick3Button7;                 // Player3 STARTボタン
    public static KeyCode P3_XBOX_LEFT_ANALOG_PRESS = KeyCode.Joystick3Button8;     // Player3 LeftAnalogPressボタン
    public static KeyCode P3_XBOX_RIGHT_ANALOG_PRESS = KeyCode.Joystick3Button9;    // Player3 RightAnalogPressボタン

    public static KeyCode P4_XBOX_A = KeyCode.Joystick4Button0;                     // Player4 Aボタン
    public static KeyCode P4_XBOX_B = KeyCode.Joystick4Button1;                     // Player4 Bボタン
    public static KeyCode P4_XBOX_X = KeyCode.Joystick4Button2;                     // Player4 Xボタン
    public static KeyCode P4_XBOX_Y = KeyCode.Joystick4Button3;                     // Player4 Yボタン
    public static KeyCode P4_XBOX_L = KeyCode.Joystick4Button4;                     // Player4Lボタン
    public static KeyCode P4_XBOX_R = KeyCode.Joystick4Button5;                     // Plyaer4 Rボタン
    public static KeyCode P4_XBOX_BACK = KeyCode.Joystick4Button6;                  // Player4 BACKボタン
    public static KeyCode P4_XBOX_START = KeyCode.Joystick4Button7;                 // Player4 STARTボタン
    public static KeyCode P4_XBOX_LEFT_ANALOG_PRESS = KeyCode.Joystick4Button8;     // Player4 LeftAnalogPressボタン
    public static KeyCode P4_XBOX_RIGHT_ANALOG_PRESS = KeyCode.Joystick4Button9;    // Player4 RightAnalogPressボタン

    public static string P1_XBOX_RIGHT_ANALOG_X = "P1_RIGHT_ANALOG_X";              // Player1 右アナログスティックのX方向
    public static string P1_XBOX_RIGHT_ANALOG_Y = "P1_RIGHT_ANALOG_Y";              // Player1 右アナログスティックのY方向
    public static string P1_XBOX_LEFT_ANALOG_X = "P1_LEFT_ANALOG_X";                // Player1 左アナログスティックのX方向
    public static string P1_XBOX_LEFT_ANALOG_Y = "P1_LEFT_ANALOG_Y";                // Player1 左アナログスティックのY方向
    public static string P1_XBOX_DPAD_X = "P1_DPAD_X";                              // Player1 DPADのX方向  
    public static string P1_XBOX_DPAD_Y = "P1_DPAD_Y";                              // Player1 DPADのY方向
    public static string P1_XBOX_RT = "P1_RT";                                      // Player1 RTボタン
    public static string P1_XBOX_LT = "P1_LT";                                      // Player1 LTボタン

    public static string P2_XBOX_RIGHT_ANALOG_X = "P2_RIGHT_ANALOG_X";              // Player2 右アナログスティックのX方向
    public static string P2_XBOX_RIGHT_ANALOG_Y = "P2_RIGHT_ANALOG_Y";              // Player2 右アナログスティックのY方向
    public static string P2_XBOX_LEFT_ANALOG_X = "P2_LEFT_ANALOG_X";                // Player2 左アナログスティックのX方向
    public static string P2_XBOX_LEFT_ANALOG_Y = "P2_LEFT_ANALOG_Y";                // Player2 左アナログスティックのY方向
    public static string P2_XBOX_DPAD_X = "P2_DPAD_X";                              // Player2 DPADのX方向  
    public static string P2_XBOX_DPAD_Y = "P2_DPAD_Y";                              // Player2 DPADのY方向
    public static string P2_XBOX_RT = "P2_RT";                                      // Player2 RTボタン
    public static string P2_XBOX_LT = "P2_LT";                                      // Player2 LTボタン

    public static string P3_XBOX_RIGHT_ANALOG_X = "P3_RIGHT_ANALOG_X";              // Player3 右アナログスティックのX方向
    public static string P3_XBOX_RIGHT_ANALOG_Y = "P3_RIGHT_ANALOG_Y";              // Player3 右アナログスティックのY方向
    public static string P3_XBOX_LEFT_ANALOG_X = "P3_LEFT_ANALOG_X";                // Player3 左アナログスティックのX方向
    public static string P3_XBOX_LEFT_ANALOG_Y = "P3_LEFT_ANALOG_Y";                // Player3 左アナログスティックのY方向
    public static string P3_XBOX_DPAD_X = "P3_DPAD_X";                              // Player3 DPADのX方向  
    public static string P3_XBOX_DPAD_Y = "P3_DPAD_Y";                              // Player3 DPADのY方向
    public static string P3_XBOX_RT = "P3_RT";                                      // Player3 RTボタン
    public static string P3_XBOX_LT = "P3_LT";                                      // Player3 LTボタン

    public static string P4_XBOX_RIGHT_ANALOG_X = "P4_RIGHT_ANALOG_X";              // Player4 右アナログスティックのX方向
    public static string P4_XBOX_RIGHT_ANALOG_Y = "P4_RIGHT_ANALOG_Y";              // Player4 右アナログスティックのY方向
    public static string P4_XBOX_LEFT_ANALOG_X = "P4_LEFT_ANALOG_X";                // Player4 左アナログスティックのX方向
    public static string P4_XBOX_LEFT_ANALOG_Y = "P4_LEFT_ANALOG_Y";                // Player4 左アナログスティックのY方向
    public static string P4_XBOX_DPAD_X = "P4_DPAD_X";                              // Player4 DPADのX方向  
    public static string P4_XBOX_DPAD_Y = "P4_DPAD_Y";                              // Player4 DPADのY方向
    public static string P4_XBOX_RT = "P4_RT";                                      // Player4 RTボタン
    public static string P4_XBOX_LT = "P4_LT";                                      // Player4 LTボタン

    public static float m_rtPress = 0;
    public static float m_ltPress = 0;

//***************************************************************************************************
//                                          メソッド
//                              @Author T.Kawashita @Date 2014/10/29
//***************************************************************************************************

    //----------------------------------------------------------------------
    // RTボタンが押されたかどうかを判定
    //----------------------------------------------------------------------
    // @Param	string  コントローラーの番号定数値		
    // @Return	bool    押されたか押されていないか
    // @Date	2014/10/29  @Update 2014/10/29  @Author T.Kawashita      
    //----------------------------------------------------------------------
    public static bool IsGetRTButton(string _rtKeyName)
    {
        if (Input.GetAxisRaw(_rtKeyName) >= 1.0f)
        {
            // 押された
            return true;
        }

        // 押されていない
        return false;
    }

    //----------------------------------------------------------------------
    // LTボタンが押されたかどうかを判定
    //----------------------------------------------------------------------
    // @Param	string  コントローラーの番号定数値		
    // @Return	bool    押されたか押されていないか
    // @Date	2014/10/29  @Update 2014/10/29  @Author T.Kawashita      
    //----------------------------------------------------------------------
    public static bool IsGetLTButton(string _ltKeyName)
    {
        if (Input.GetAxisRaw(_ltKeyName) >= 1.0f)
        {
            // 押された
            return true;
        }

        // 押されていない
        return false;

    }

    //----------------------------------------------------------------------
    // RTボタンが押され続けているかどうかを判定
    //----------------------------------------------------------------------
    // @Param	string  コントローラーの番号低数値		
    // @Return	int     どれぐらいの時間押されているか(frame)
    // @Date	2014/11/13  @Update 2014/11/13  @Author T.Kawashita      
    //----------------------------------------------------------------------
    public static int RTButtonPress(string _rtKeyName)
    {
        if (IsGetRTButton(_rtKeyName) == true)
        {
            m_rtPress += Time.deltaTime * 60;
            int frame = (int)m_rtPress;
            return frame;
        }
        else
        {
            m_rtPress = 0.0f;
            return 0;
        }
    }

    //----------------------------------------------------------------------
    // LTボタンが押され続けているかどうかを判定
    //----------------------------------------------------------------------
    // @Param	string  コントローラーの番号低数値		
    // @Return	int     どれぐらいの時間押されているか(frame)
    // @Date	2014/11/13  @Update 2014/11/13  @Author T.Kawashita      
    //----------------------------------------------------------------------
    public static int LTButtonPress(string _ltKeyName)
    {
        if (IsGetRTButton(_ltKeyName) == true)
        {
            m_ltPress += Time.deltaTime * 60;
            int frame = (int)m_ltPress;
            return frame;
        }
        else
        {
            m_ltPress = 0.0f;
            return 0;
        } 
    }

    //----------------------------------------------------------------------
    // RTとLTボタンの初期化
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/11/17  @Update 2014/11/17  @Author T.Kawashita      
    //----------------------------------------------------------------------
    public static void InitRTLT()
    {
        m_rtPress = 0;
        m_ltPress = 0;
    }

    //----------------------------------------------------------------------
    // スタートボタンが押されたかどうかを判定
    // (全コントローラー）
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	bool    押されたかどうか
    // @Date	2014/10/29  @Update 2014/10/29  @Author T.Kawashita      
    //----------------------------------------------------------------------
    public static bool IsGetAllStartButton()
    {
        if (Input.GetKeyDown(P1_XBOX_START) ||
            Input.GetKeyDown(P2_XBOX_START) ||
            Input.GetKeyDown(P3_XBOX_START) ||
            Input.GetKeyDown(P4_XBOX_START))
        {
            // 押された
            return true;
        }

        // 押されていない
        return false;
    }

    //----------------------------------------------------------------------
    // セレクトボタンが押されたかどうかを判定
    // (全コントローラー)
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	bool    押されたかどうか
    // @Date	2014/10/29  @Update 2014/10/29  @Author T.Kawashita      
    //----------------------------------------------------------------------
    public static bool IsGetAllSelectButton()
    {
        if (Input.GetKeyDown(P1_XBOX_BACK)||
            Input.GetKeyDown(P2_XBOX_BACK)||
            Input.GetKeyDown(P3_XBOX_BACK)||
            Input.GetKeyDown(P4_XBOX_BACK))
        {
            // 押された
            return true;
        }

        // 押されていない
        return false;
    }


}
