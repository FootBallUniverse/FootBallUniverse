using UnityEngine;
using System.Collections;

public class Player_1_Script : MonoBehaviour {

	  public struct TEAM_NO
    {
        public GameObject m_Country;    // チームの国名
        public UISprite m_Sprit;        // 
        public int m_TeamColor;         // チーム色
        public float degree;            // 回転角度
        public float radian;            // 
        public float r;                 // 中心からの
        public float centerx;           // 
        public float centerz;           // 
        public int m_Flag;              // モデルの位置を表す数値
    };
    
    // 速度
    public Vector2 SPEED = new Vector2(0.05f, 0.01f);

    public TEAM_NO[] m_Country = new TEAM_NO[4];
    Vector3[] Position = new Vector3[4];
    public bool m_Right_RotateFlag;
    public bool m_Left_RotateFlag;
    int m_Count = 0;

    Fade_1 m_Fade_flag;
    // Use this for initialization
    void Start()
    {
        // 必要データの読込み
        GameObject m_TeamData = GameObject.Find("TeamData");
        
        // モデルの呼び出し
        m_Country[0].m_Country = transform.Find("Spain_1").gameObject;
        m_Country[1].m_Country = transform.Find("England_1").gameObject;
        m_Country[2].m_Country = transform.Find("Brazil_1").gameObject;
        m_Country[3].m_Country = transform.Find("Japan_1").gameObject;
        GameObject m_Fade = transform.FindChild("Fade").gameObject;

        m_Country[0].m_Sprit = m_Country[0].m_Country.transform.FindChild("flag_0").GetComponent<UISprite>();
        m_Country[1].m_Sprit = m_Country[1].m_Country.transform.FindChild("flag_1").GetComponent<UISprite>();
        m_Country[2].m_Sprit = m_Country[2].m_Country.transform.FindChild("flag_2").GetComponent<UISprite>();
        m_Country[3].m_Sprit = m_Country[3].m_Country.transform.FindChild("flag_3").GetComponent<UISprite>();       
        m_Fade_flag = m_Fade.GetComponent<Fade_1>();

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
           m_Country[i].r = 0.31f;
           m_Country[i].centerx = 0.0f;
           m_Country[i].centerz = 0.0f;
           m_Country[i].radian = 0.0f;
           m_Country[i].m_Flag = i;

       }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            // 右回転フラグと左回転フラグがFALSEのときだけTRUEにする
            if (m_Right_RotateFlag == false && m_Left_RotateFlag == false)
            {
                m_Right_RotateFlag = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            // 右回転フラグと左回転フラグがFALSEのときだけTRUEにする
            if (m_Left_RotateFlag == false && m_Right_RotateFlag == false)
            {
                m_Left_RotateFlag = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            m_Fade_flag.m_FadeFlag += 1;
            if (m_Fade_flag.m_FadeFlag > 2)
            {
                m_Fade_flag.m_FadeFlag = 0;
            }
            //Application.LoadLevel("Title");
        }
        // 右回転処理
        Right_Rotate();
        // 左回転処理
        Left_Rotate();

    }

    //=========================================================================================//
    // みぎ回転処理                                                                              //
    //=========================================================================================//
    void Right_Rotate()
    {
        
        // 右回転フラグがTRUEの時、90°右回転してフラグをFALSEにする
        if (m_Right_RotateFlag == true)
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
                    m_Country[i].m_Sprit.depth = 4;

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

                    if (Position[i].x <= -0.25)
                    {
                        Position[i].x = -0.31f;
                    }
                    else if (Position[i].x >= 0.25)
                    {
                        Position[i].x = 0.31f;
                    }
                    else
                    {
                        Position[i].x = 0.0f;
                    }

                    if (Position[i].z >= 0.25)
                    {
                        Position[i].z = 0.31f;
                    }
                    else if (Position[i].z <= -0.25)
                    {
                        Position[i].z = -0.31f;
                    }
                    else
                    {
                        Position[i].z = 0.0f;
                    }

                    if (Position[i].x == 0.0f && Position[i].z == -0.31f)
                    {
                        Debug.Log("センターのモデルのフラグは" + m_Country[i].m_Flag);
                    }
                    m_Country[i].m_Country.transform.position = Position[i];
                }
                
                m_Count = 0;
                m_Right_RotateFlag = false;
                
            }

        }
    }

    //=========================================================================================//
    // ひだり回転処理                                                                              //
    //=========================================================================================//
    void Left_Rotate()
    {

        if (m_Left_RotateFlag == true)
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
                    m_Country[i].m_Sprit.depth = 4;

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

                    if (Position[i].x <= -0.25)
                    {
                        Position[i].x = -0.31f;
                    }
                    else if (Position[i].x >= 0.25)
                    {
                        Position[i].x = 0.31f;
                    }
                    else
                    {
                        Position[i].x = 0.0f;
                    }

                    if (Position[i].z >= 0.25)
                    {
                        Position[i].z = 0.31f;
                    }
                    else if (Position[i].z <= -0.25)
                    {
                        Position[i].z = -0.31f;
                    }
                    else
                    {
                        Position[i].z = 0.0f;
                    }

                    if (Position[i].x == 0.0f && Position[i].z == -0.31f)
                    {
                        Debug.Log("センターのモデルのフラグは" + m_Country[i].m_Flag);
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
                m_Left_RotateFlag = false;
            }
        }
    }
}

