﻿using UnityEngine;
using System.Collections;

public class Player_Script : MonoBehaviour
{
    public struct TEAM_NO
    {
        public GameObject m_Country;    // チームの国名
        public int m_TeamColor;         // チーム色
        public float degree;            // 回転角度
        public float radian;            // 
        public float r;                 // 中心からの
        public float centerx;           // 
        public float centerz;           // 
        public int m_Flag;              // モデルの位置を表す数値
    };
    // 速度
    public TEAM_NO[] m_Country = new TEAM_NO[4];
    public Vector2 SPEED = new Vector2(0.05f, 0.01f);
    Vector3[] Position = new Vector3[4];
    int m_Count = 0;
    bool m_Right_RotateFlag;
    bool m_Left_RotateFlag;
    // Use this for initialization
    void Start()
    {
        // モデルの呼び出し
        m_Country[0].m_Country = transform.Find("Japan").gameObject;
        m_Country[1].m_Country = transform.Find("Spain").gameObject;
        m_Country[2].m_Country = transform.Find("England").gameObject;
        m_Country[3].m_Country = transform.Find("Brazil").gameObject;

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
            m_Country[i].degree = 92.0f * i;
            m_Country[i].r = 0.37f;
            m_Country[i].centerx = 0.0f;
            m_Country[i].centerz = 0.0f;
            m_Country[i].radian = 0.0f;
            m_Country[i].m_Flag = i;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Right_Rotate();
        Left_Rotate();
    }

    void Right_Rotate()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (m_Right_RotateFlag == false)
            {
                m_Right_RotateFlag = true;
            }
        }
        if (m_Right_RotateFlag == true)
        {
            for (int i = 0; i < 4; i++)
            {
                switch (m_Country[i].m_Flag)
                {
                    case 0:
                        m_Country[i].radian = Mathf.PI / 180.0f * m_Country[i].degree;
                        Position[i].x = m_Country[i].centerx + m_Country[i].r * Mathf.Cos(m_Country[i].radian);
                        Position[i].z = m_Country[i].centerz + m_Country[i].r * Mathf.Sin(m_Country[i].radian) / 2;
                        m_Country[i].degree += 5.0f;
                        if (Position[i].x >= 0.31f)
                        {
                            Position[i].x = 0.31f;
                        }
                        else if (Position[i].x <= -0.31f)
                        {
                            Position[i].x = -0.31f;
                        }
                        if (Position[i].z >= 0.31f)
                        {
                            Position[i].z = 0.31f;
                        }
                        else if (Position[i].z <= -0.31f)
                        {
                            Position[i].z = -0.31f;
                        }

                        if (Position[i].x <= 0.02f && Position[i].x >= -0.02f)
                        {
                            Position[i].x = 0.0f;
                        }
                        if (Position[i].z <= 0.02f && Position[i].z >= -0.02f)
                        {
                            Position[i].z = 0.0f;
                        }
                        m_Country[i].m_Country.transform.position = Position[i];

                        break;
                    case 1:
                        m_Country[i].radian = Mathf.PI / 180.0f * m_Country[i].degree;
                        Position[i].x = m_Country[i].centerx + m_Country[i].r * Mathf.Cos(m_Country[i].radian);
                        Position[i].z = m_Country[i].centerz + m_Country[i].r * Mathf.Sin(m_Country[i].radian) / 2;
                        m_Country[i].degree += 5.0f;
                        if (Position[i].x >= 0.31f)
                        {
                            Position[i].x = 0.31f;
                        }
                        else if (Position[i].x <= -0.31f)
                        {
                            Position[i].x = -0.31f;
                        }
                        if (Position[i].z >= 0.31f)
                        {
                            Position[i].z = 0.31f;
                        }
                        else if (Position[i].z <= -0.31f)
                        {
                            Position[i].z = -0.31f;
                        }

                        if (Position[i].x <= 0.02f && Position[i].x >= -0.02f)
                        {
                            Position[i].x = 0.0f;
                        }
                        if (Position[i].z <= 0.02f && Position[i].z >= -0.02f)
                        {
                            Position[i].z = 0.0f;
                        }
                        m_Country[i].m_Country.transform.position = Position[i];

                        break;
                    case 2:
                        m_Country[i].radian = Mathf.PI / 180.0f * m_Country[i].degree;
                        Position[i].x = m_Country[i].centerx + m_Country[i].r * Mathf.Cos(m_Country[i].radian);
                        Position[i].z = m_Country[i].centerz + m_Country[i].r * Mathf.Sin(m_Country[i].radian) / 2;
                        m_Country[i].degree += 5.0f;
                        if (Position[i].x >= 0.31f)
                        {
                            Position[i].x = 0.31f;
                        }
                        else if (Position[i].x <= -0.31f)
                        {
                            Position[i].x = -0.31f;
                        }
                        if (Position[i].z >= 0.31f)
                        {
                            Position[i].z = 0.31f;
                        }
                        else if (Position[i].z <= -0.31f)
                        {
                            Position[i].z = -0.31f;
                        }

                        if (Position[i].x <= 0.02f && Position[i].x >= -0.02f)
                        {
                            Position[i].x = 0.0f;
                        }
                        if (Position[i].z <= 0.02f && Position[i].z >= -0.02f)
                        {
                            Position[i].z = 0.0f;
                        }
                        m_Country[i].m_Country.transform.position = Position[i];

                        break;
                    case 3:
                        m_Country[i].radian = Mathf.PI / 180.0f * m_Country[i].degree;
                        Position[i].x = m_Country[i].centerx + m_Country[i].r * Mathf.Cos(m_Country[i].radian);
                        Position[i].z = m_Country[i].centerz + m_Country[i].r * Mathf.Sin(m_Country[i].radian) / 2;
                        m_Country[i].degree += 5.0f;
                        if (Position[i].x >= 0.31f)
                        {
                            Position[i].x = 0.31f;
                        }
                        else if (Position[i].x <= -0.31f)
                        {
                            Position[i].x = -0.31f;
                        }
                        if (Position[i].z >= 0.31f)
                        {
                            Position[i].z = 0.31f;
                        }
                        else if (Position[i].z <= -0.31f)
                        {
                            Position[i].z = -0.31f;
                        }

                        if (Position[i].x <= 0.02f && Position[i].x >= -0.02f)
                        {
                            Position[i].x = 0.0f;
                        }
                        if (Position[i].z <= 0.02f && Position[i].z >= -0.02f)
                        {
                            Position[i].z = 0.0f;
                        }
                        m_Country[i].m_Country.transform.position = Position[i];

                        break;
                }
            }
            m_Count++;
            if (m_Count >= 18)
            {
                for (int i = 0; i < 4; i++)
                {
                    m_Country[i].m_Flag++;
                    Debug.Log("ヒャッハーーーーー！！" + m_Country[0].m_Flag);
                    Debug.Log("ヒャッハーーーーー！！" + m_Country[0].m_Country.transform.position);
                    Debug.Log("ヒャッハーーーーー！！" + m_Country[0].m_Country.transform.position);
                    if (m_Country[i].m_Flag == 4)
                    {
                        m_Country[i].m_Flag = 0;
                    }
                    if (m_Country[i].m_Flag == 1)
                    {
                        Position[i].x = 0.0f;
                        Position[i].z = 0.31f;
                    }
                    if (m_Country[i].m_Flag == 2)
                    {
                        Position[i].x = -0.31f;
                        Position[i].z = 0.0f;
                    }
                    if (m_Country[i].m_Flag == 3)
                    {
                        Position[i].x = 0.0f;
                        Position[i].z = -0.31f;
                    }
                    if (m_Country[i].m_Flag == 0)
                    {
                        Position[i].x = 0.31f;
                        Position[i].z = 0.0f;
                    }/**/
                    m_Country[i].m_Country.transform.position = Position[i];
                }
                m_Count = 0;
                m_Right_RotateFlag = false;
            }

        }
    }
    void Left_Rotate()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (m_Left_RotateFlag == false)
            {
                m_Left_RotateFlag = true;
            }
        }
        if (m_Left_RotateFlag == true)
        {
            for (int i = 0; i < 4; i++)
            {
                switch (m_Country[i].m_Flag)
                {
                    case 0:
                        m_Country[i].radian = Mathf.PI / 180.0f * m_Country[i].degree;
                        Position[i].x = m_Country[i].centerx + m_Country[i].r * Mathf.Cos(m_Country[i].radian);
                        Position[i].z = m_Country[i].centerz + m_Country[i].r * Mathf.Sin(m_Country[i].radian) / 2;
                        m_Country[i].degree -= 5.0f;
                        if (Position[i].x >= 0.31f)
                        {
                            Position[i].x = 0.31f;
                        }
                        else if (Position[i].x <= -0.31f)
                        {
                            Position[i].x = -0.31f;
                        }
                        if (Position[i].z >= 0.31f)
                        {
                            Position[i].z = 0.31f;
                        }
                        else if (Position[i].z <= -0.31f)
                        {
                            Position[i].z = -0.31f;
                        }

                        if (Position[i].x <= 0.02f && Position[i].x >= -0.02f)
                        {
                            Position[i].x = 0.0f;
                        }
                        if (Position[i].z <= 0.02f && Position[i].z >= -0.02f)
                        {
                            Position[i].z = 0.0f;
                        }
                        m_Country[i].m_Country.transform.position = Position[i];

                        break;
                    case 1:
                        m_Country[i].radian = Mathf.PI / 180.0f * m_Country[i].degree;
                        Position[i].x = m_Country[i].centerx + m_Country[i].r * Mathf.Cos(m_Country[i].radian);
                        Position[i].z = m_Country[i].centerz + m_Country[i].r * Mathf.Sin(m_Country[i].radian) / 2;
                        m_Country[i].degree -= 5.0f;
                        if (Position[i].x >= 0.31f)
                        {
                            Position[i].x = 0.31f;
                        }
                        else if (Position[i].x <= -0.31f)
                        {
                            Position[i].x = -0.31f;
                        }
                        if (Position[i].z >= 0.31f)
                        {
                            Position[i].z = 0.31f;
                        }
                        else if (Position[i].z <= -0.31f)
                        {
                            Position[i].z = -0.31f;
                        }

                        if (Position[i].x <= 0.02f && Position[i].x >= -0.02f)
                        {
                            Position[i].x = 0.0f;
                        }
                        if (Position[i].z <= 0.02f && Position[i].z >= -0.02f)
                        {
                            Position[i].z = 0.0f;
                        }
                        m_Country[i].m_Country.transform.position = Position[i];

                        break;
                    case 2:
                        m_Country[i].radian = Mathf.PI / 180.0f * m_Country[i].degree;
                        Position[i].x = m_Country[i].centerx + m_Country[i].r * Mathf.Cos(m_Country[i].radian);
                        Position[i].z = m_Country[i].centerz + m_Country[i].r * Mathf.Sin(m_Country[i].radian) / 2;
                        m_Country[i].degree -= 5.0f;
                        if (Position[i].x >= 0.31f)
                        {
                            Position[i].x = 0.31f;
                        }
                        else if (Position[i].x <= -0.31f)
                        {
                            Position[i].x = -0.31f;
                        }
                        if (Position[i].z >= 0.31f)
                        {
                            Position[i].z = 0.31f;
                        }
                        else if (Position[i].z <= -0.31f)
                        {
                            Position[i].z = -0.31f;
                        }

                        if (Position[i].x <= 0.02f && Position[i].x >= -0.02f)
                        {
                            Position[i].x = 0.0f;
                        }
                        if (Position[i].z <= 0.02f && Position[i].z >= -0.02f)
                        {
                            Position[i].z = 0.0f;
                        }
                        m_Country[i].m_Country.transform.position = Position[i];

                        break;
                    case 3:
                        m_Country[i].radian = Mathf.PI / 180.0f * m_Country[i].degree;
                        Position[i].x = m_Country[i].centerx + m_Country[i].r * Mathf.Cos(m_Country[i].radian);
                        Position[i].z = m_Country[i].centerz + m_Country[i].r * Mathf.Sin(m_Country[i].radian) / 2;
                        m_Country[i].degree -= 5.0f;
                        if (Position[i].x >= 0.31f)
                        {
                            Position[i].x = 0.31f;
                        }
                        else if (Position[i].x <= -0.31f)
                        {
                            Position[i].x = -0.31f;
                        }
                        if (Position[i].z >= 0.31f)
                        {
                            Position[i].z = 0.31f;
                        }
                        else if (Position[i].z <= -0.31f)
                        {
                            Position[i].z = -0.31f;
                        }

                        if (Position[i].x <= 0.02f && Position[i].x >= -0.02f)
                        {
                            Position[i].x = 0.0f;
                        }
                        if (Position[i].z <= 0.02f && Position[i].z >= -0.02f)
                        {
                            Position[i].z = 0.0f;
                        }
                        m_Country[i].m_Country.transform.position = Position[i];

                        break;
                }
            }
            m_Count++;
            if (m_Count >= 18)
            {
                for (int i = 0; i < 4; i++)
                {
                    m_Country[i].m_Flag--;
                    if (m_Country[i].m_Flag == -1)
                    {
                        m_Country[i].m_Flag = 3;
                    }
                    if (m_Country[i].m_Flag == 1)
                    {
                        Position[i].x = 0.0f;
                        Position[i].z = 0.31f;
                    }
                    if (m_Country[i].m_Flag == 2)
                    {
                        Position[i].x = -0.31f;
                        Position[i].z = 0.0f;
                    }
                    if (m_Country[i].m_Flag == 3)
                    {
                        Position[i].x = 0.0f;
                        Position[i].z = -0.31f;
                    }
                    if (m_Country[i].m_Flag == 0)
                    {
                        Position[i].x = 0.31f;
                        Position[i].z = 0.0f;
                    }/**/
                    m_Country[i].m_Country.transform.position = Position[i];
                }
                m_Count = 0;
                m_Left_RotateFlag = false;
            }

        }
    }
}