using UnityEngine;
using System.Collections;

public class Player_3_Script : MonoBehaviour
{
    public struct TEAM_NO
    {
        public GameObject m_Country;            // チームの国名
        public UISprite m_Sprit;                // 
        public int m_TeamColor;                 // チーム色
        public float degree;                    // 回転角度
        public float radian;                    // 
        public float r;                         // 中心からの
        public float centerx;                   // 
        public float centerz;                   // 
        public int m_Flag;                      // モデルの位置を表す数値
        public PlayerAnimator m_PlayerAnimator; // プレイヤーのアニメーター
        public Vector3 m_Scale;                 // すけーる
    };
    // 速度
    public TEAM_NO[] m_Country = new TEAM_NO[4];
    public Vector2 SPEED = new Vector2(0.05f, 0.01f);

    Vector3[] Position = new Vector3[4];
    int m_Count = 0;
    public bool m_Right_RotateFlag;
    public bool m_Left_RotateFlag;
    public bool m_SceneFlag;
    public Fade_2 m_Fade_flag_2;

    private float m_CenterPos;
    
    // Use this for initialization
    void Start()
    {

        GameObject m_TeamData = GameObject.Find("TeamData");
        m_SceneFlag = false;
        m_CenterPos = 4.0f;
        // モデルの呼び出し
        m_Country[0].m_Country = transform.Find("Spain_2").gameObject;
        m_Country[1].m_Country = transform.Find("England_2").gameObject;
        m_Country[2].m_Country = transform.Find("Brazil_2").gameObject;
        m_Country[3].m_Country = transform.Find("Japan_2").gameObject;
        GameObject m_Fade_2 = transform.FindChild("Fade_In_Out_2").gameObject;

        m_Country[0].m_Sprit = m_Country[0].m_Country.transform.FindChild("flag_0").GetComponent<UISprite>();
        m_Country[1].m_Sprit = m_Country[1].m_Country.transform.FindChild("flag_1").GetComponent<UISprite>();
        m_Country[2].m_Sprit = m_Country[2].m_Country.transform.FindChild("flag_2").GetComponent<UISprite>();
        m_Country[3].m_Sprit = m_Country[3].m_Country.transform.FindChild("flag_3").GetComponent<UISprite>();
        m_Fade_flag_2 = m_Fade_2.GetComponent<Fade_2>();

       // 位置計算用の変数に代入
       Position[0] = m_Country[0].m_Country.transform.position;
       Position[1] = m_Country[1].m_Country.transform.position;
       Position[2] = m_Country[2].m_Country.transform.position;
       Position[3] = m_Country[3].m_Country.transform.position;
       // カウント、回転フラグの初期化
       m_Count = 0;
       m_Right_RotateFlag = false;
       m_Left_RotateFlag = false;

       // 初期値の設定
       for (int i = 0; i < 4; i++)
       {
           m_Country[i].m_TeamColor = 0;
           m_Country[i].degree = 90.0f * i;
           m_Country[i].r = 0.21f;
           m_Country[i].centerx = m_CenterPos - 0.21f;
           m_Country[i].centerz = 0.0f;
           m_Country[i].radian = 0.0f;
           m_Country[i].m_Flag = i;
           m_Country[i].m_PlayerAnimator = m_Country[i].m_Country.GetComponent<PlayerAnimator>();
           m_Country[i].m_Scale = new Vector3(1, 1, 1);
       }
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            // 右回転フラグと左回転フラグがFALSEのときだけTRUEにする
            if (m_Right_RotateFlag == false && m_Left_RotateFlag == false)
            {
                m_Right_RotateFlag = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // 右回転フラグと左回転フラグがFALSEのときだけTRUEにする
            if (m_Left_RotateFlag == false && m_Right_RotateFlag == false)
            {
                m_Left_RotateFlag = true;
            }
        }
        
        if (Input.GetKeyUp(KeyCode.RightShift)     // シフトが押されたか
            && m_Fade_flag_2.m_FadeFlag == 0       // フェードアウトしているか
            && m_Right_RotateFlag == false         // 右回転しているか
            && m_Left_RotateFlag == false)         // 左回転しているか)
        {
            m_Fade_flag_2.m_FadeFlag = 1;
        }
        else if (Input.GetKeyUp(KeyCode.RightShift) && m_Fade_flag_2.m_FadeFlag == 1)
        {
            m_SceneFlag = true;
            m_Fade_flag_2.m_FadeFlag = 2;
        }
        else if (Input.GetKeyUp(KeyCode.RightControl) && m_Fade_flag_2.m_FadeFlag == 1)
        {
            m_Fade_flag_2.m_FadeFlag = 3;
        }

        if (m_Left_RotateFlag == true)
        {
            for (int i = 0; i < 4; i++)
            {
                if (m_Country[i].m_Flag == 2)
                {
                    m_Country[i].m_Country.transform.localScale = m_Country[i].m_Scale;
                    if (m_Country[i].m_Scale.x <= 1.5f)
                    {
                        m_Country[i].m_Scale.x += 0.05f;
                    }
                    if (m_Country[i].m_Scale.y <= 1.5f)
                    {
                        m_Country[i].m_Scale.y += 0.05f;
                    }
                }
                else
                {
                    m_Country[i].m_Country.transform.localScale = m_Country[i].m_Scale;
                    if (m_Country[i].m_Scale.x >= 1.0f)
                    {
                        m_Country[i].m_Scale.x -= 0.05f;
                    }
                    if (m_Country[i].m_Scale.y >= 1.0f)
                    {
                        m_Country[i].m_Scale.y -= 0.05f;
                    }
                }
            }
        }
        if (m_Right_RotateFlag == true)
        {
            for (int i = 0; i < 4; i++)
            {
                if (m_Country[i].m_Flag == 0)
                {
                    m_Country[i].m_Country.transform.localScale = m_Country[i].m_Scale;
                    if (m_Country[i].m_Scale.x <= 1.5f)
                    {
                        m_Country[i].m_Scale.x += 0.05f;
                    }
                    if (m_Country[i].m_Scale.y <= 1.5f)
                    {
                        m_Country[i].m_Scale.y += 0.05f;
                    }
                }
                else
                {
                    m_Country[i].m_Country.transform.localScale = m_Country[i].m_Scale;
                    if (m_Country[i].m_Scale.x >= 1.0f)
                    {
                        m_Country[i].m_Scale.x -= 0.05f;
                    }
                    if (m_Country[i].m_Scale.y >= 1.0f)
                    {
                        m_Country[i].m_Scale.y -= 0.05f;
                    }
                }
            }
        }
        if (m_Fade_flag_2.m_FadeFlag == 0)
        {
            // 右回転処理
            Right_Rotate();
            // 左回転処理
            Left_Rotate();
        }
        else
        {
            m_Right_RotateFlag = false;
            m_Left_RotateFlag = false;
        }
    }
    //=========================================================================================//
    // みぎ回転処理                                                                              //
    //=========================================================================================//
    void Left_Rotate()
    {

        // 右回転フラグがTRUEの時、90°右回転してフラグをFALSEにする
        if (m_Left_RotateFlag == true)
        {

            for (int i = 0; i < 4; i++)
            {
                m_Country[i].radian = Mathf.PI / 180.0f * m_Country[i].degree;
                Position[i].x = m_Country[i].centerx + m_Country[i].r * Mathf.Cos(m_Country[i].radian);
                Position[i].z = m_Country[i].centerz + m_Country[i].r * Mathf.Sin(m_Country[i].radian);
                m_Country[i].degree += 5.0f;

                m_Country[i].m_Country.transform.position = Position[i];

                // 前に来た国のスプライトのデプスのみ変更
                if (m_Country[i].m_Flag == 2)
                    m_Country[i].m_Sprit.depth = 6;
                else
                    m_Country[i].m_Sprit.depth = 2;

            }
            m_Count++;

            if (m_Count >= 18)
            {
                for (int i = 0; i < 4; i++)
                {
                    m_Country[i].m_Flag++;

                    // フラグが4以上になった場合0にする
                    if (m_Country[i].m_Flag >= 4)
                    {
                        m_Country[i].m_Flag = 0;
                    }

                    if (Position[i].x <= 3.62f)
                    {
                        Position[i].x = 3.58f;
                    }
                    else if (Position[i].x >= 3.97)
                    {
                        Position[i].x = 4.0f;
                    }
                    else
                    {
                        Position[i].x = 3.79f;
                    }

                    if (Position[i].z >= 0.18)
                    {
                        Position[i].z = 0.21f;
                    }
                    else if (Position[i].z <= -0.18)
                    {
                        Position[i].z = -0.21f;
                    }
                    else
                    {
                        Position[i].z = 0.0f;
                    }

                    if (Position[i].x == 4.0f && Position[i].z == -0.21f)
                    {
                        // m_Country[i].m_PlayerAnimator.ChangeAnimation(m_Country[i].m_PlayerAnimator.m_isKickCharge);
                        m_Country[i].m_PlayerAnimator.ChangeAnimation(m_Country[i].m_PlayerAnimator.m_isDashCharge);
                        Debug.Log("センターのモデルのフラグは" + m_Country[i].m_Flag);
                    }
                    else
                    {
                        m_Country[i].m_PlayerAnimator.ChangeAnimation(m_Country[i].m_PlayerAnimator.m_isWait);
                    }
                    m_Country[i].m_Country.transform.position = Position[i];
                }

                m_Count = 0;
                m_Left_RotateFlag = false;

            }

        }
    }

    //=========================================================================================//
    // ひだり回転処理                                                                              //
    //=========================================================================================//
    void Right_Rotate()
    {

        if (m_Right_RotateFlag == true)
        {

            for (int i = 0; i < 4; i++)
            {
                m_Country[i].radian = Mathf.PI / 180.0f * m_Country[i].degree;
                Position[i].x = m_Country[i].centerx + m_Country[i].r * Mathf.Cos(m_Country[i].radian);
                Position[i].z = m_Country[i].centerz + m_Country[i].r * Mathf.Sin(m_Country[i].radian);
                m_Country[i].degree -= 5.0f;

                m_Country[i].m_Country.transform.position = Position[i];

                // 前に来た国のスプライトのデプスのみ変更
                if (m_Country[i].m_Flag == 0)
                    m_Country[i].m_Sprit.depth = 6;
                else
                    m_Country[i].m_Sprit.depth = 2;

            }
            m_Count++;
            if (m_Count >= 18)
            {
                for (int i = 0; i < 4; i++)
                {
                    m_Country[i].m_Flag--;

                    // フラグが-1以下になった場合0にする
                    if (m_Country[i].m_Flag <= -1)
                    {
                        m_Country[i].m_Flag = 3;
                    }

                    if (Position[i].x <= 3.62f)
                    {
                        Position[i].x = 3.58f;
                    }
                    else if (Position[i].x >= 3.97)
                    {
                        Position[i].x = 4.0f;
                    }
                    else
                    {
                        Position[i].x = 3.79f;
                    }

                    if (Position[i].z >= 0.18)
                    {
                        Position[i].z = 0.21f;
                    }
                    else if (Position[i].z <= -0.18)
                    {
                        Position[i].z = -0.21f;
                    }
                    else
                    {
                        Position[i].z = 0.0f;
                    }

                    if (Position[i].x == 4.0f && Position[i].z == -0.21f)
                    {
                        // m_Country[i].m_PlayerAnimator.ChangeAnimation(m_Country[i].m_PlayerAnimator.m_isKickCharge);
                        m_Country[i].m_PlayerAnimator.ChangeAnimation(m_Country[i].m_PlayerAnimator.m_isDashCharge);
                        Debug.Log("センターのモデルのフラグは" + m_Country[i].m_Flag);
                    }
                    else
                    {
                        m_Country[i].m_PlayerAnimator.ChangeAnimation(m_Country[i].m_PlayerAnimator.m_isWait);
                    }

                    m_Country[i].m_Country.transform.position = Position[i];
                    // 前に来た国のスプライトのデプスのみ変更
                    /* if (m_Country[i].m_Flag == 0)
                     {
                         m_Country[i].m_Country.transform.FindChild("flag_" + i).gameObject.GetComponent<UISprite>().depth = 2;
                     }*/
                    //  Debug.Log(m_Country[i].m_Country.transform.FindChild("flag_" + i).gameObject.GetComponent<UISprite>().depth);
                }
                m_Count = 0;
                m_Right_RotateFlag = false;
            }
        }
    }
}

