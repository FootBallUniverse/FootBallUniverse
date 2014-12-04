﻿using UnityEngine;
using System.Collections;

public class Player_1_Script : MonoBehaviour
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

    // 構造体
    public TEAM_NO[] m_Country = new TEAM_NO[4];

    // スクリプトの読み込み用
    public Player_3_Script m_Player3;
    public Fade_1 m_Fade_flag_1;
    public Fade_2 m_Fade_flag_2;

    // オブジェクト読込み用
    public GameObject m_Fade_2;

    // ラベル読込み用
    public UILabel m_Label;

    // 座標取得用
    public Vector3[] Position = new Vector3[4];

    // 速度
    private Vector2 SPEED = new Vector2(0.05f, 0.01f);

    // 変数
    public bool m_Right_RotateFlag;
    public bool m_Left_RotateFlag;
    public bool m_SceneFlag;
    private int m_Count = 0;

    //================================================================================================
    //      初期処理
    //================================================================================================
    // Use this for initialization
    void Start()
    {
        // 必要データの読込み
        GameObject m_TeamData = GameObject.Find("TeamData");

        // モデルの読込み
        GameObject m_Fade_1 = transform.FindChild("Fade_In_Out_1").gameObject;
        GameObject m_Object3 = GameObject.Find("Team_Select/Team3_4");
        m_Country[0].m_Country = transform.FindChild("Spain_1").gameObject;
        m_Country[1].m_Country = transform.FindChild("England_1").gameObject;
        m_Country[2].m_Country = transform.FindChild("Brazil_1").gameObject;
        m_Country[3].m_Country = transform.FindChild("Japan_1").gameObject;
        m_Fade_2 = m_Object3.transform.FindChild("Fade_In_Out_2").gameObject;

        // チーム決定後に表示されるラベルの読み込み
        m_Label = GameObject.Find("Label(Wait2)").GetComponent<UILabel>();

        //フェードアウト処理スクリプトの読み込み
        m_Fade_flag_1 = m_Fade_1.GetComponent<Fade_1>();
        m_Fade_flag_2 = m_Fade_2.GetComponent<Fade_2>();
        // m_Player3 = m_Player3.GetComponent<Player_3_Script>();

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
            m_Country[i].m_TeamColor = 0;        // チームの色の変更用フラグ(現在未実装の為不要)
            m_Country[i].degree = 90.0f * i;     // 回転角度
            m_Country[i].r = 0.21f;              // 回転の半径
            m_Country[i].centerx = 3.79f;        // 中心軸のＸ座標
            m_Country[i].centerz = 0.0f;         // 中心軸のＺ座標
            m_Country[i].radian = 0.0f;          // ラジアン
            m_Country[i].m_Flag = i;             // どのモデルがセンターにいるかの確認用フラグ
            m_Country[i].m_PlayerAnimator = m_Country[i].m_Country.GetComponent<PlayerAnimator>();   // モデルのモーション用

        }
    }

    //================================================================================================
    //      モデル回転・拡縮、アニメーション変更、フェードイン処理
    //================================================================================================
    // Update is called once per frame
    void Update()
    {
        //================================================================================================
        //      回転フラグ、フェードインフラグ処理
        //================================================================================================
        // Dを押したとき
        if (Input.GetKeyDown(KeyCode.A))
        {
            // 右回転フラグと左回転フラグがFALSEのときだけTRUEにする
            if (m_Right_RotateFlag == false && m_Left_RotateFlag == false)
                m_Right_RotateFlag = true;      // 右回転のフラグをtrueにする
        }
        // Aを押したとき
        else if (Input.GetKeyDown(KeyCode.D))
        {
            // 右回転フラグと左回転フラグがFALSEのときだけTRUEにする
            if (m_Left_RotateFlag == false && m_Right_RotateFlag == false)
                m_Left_RotateFlag = true;    // 左回転のフラグをtrueにする
        }
        // Shiftが押されたら遷移
        if (Input.GetKeyDown(KeyCode.Space)
            && m_Fade_flag_1.m_FadeFlag <= 2)        // フェードアウトしているか
        {
            // フェードインのフラグを1に変更
            m_Fade_flag_1.m_FadeFlag = 1;
            m_Fade_flag_1.m_FadeFlag = 2;
        }

        // Shiftが押されたら遷移
        if (Input.GetKeyDown(KeyCode.LeftShift)
            && m_Fade_flag_1.m_FadeFlag == 0        // フェードアウトしているか
            && m_Right_RotateFlag == false         // 右回転しているか
            && m_Left_RotateFlag == false)         // 左回転しているか
        {
            // フェードインのフラグを1に変更
            m_Fade_flag_1.m_FadeFlag = 1;
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift) && m_Fade_flag_1.m_FadeFlag == 1)
        {
            // シーン変更フラグをtrueにしてフェードインのフラグを2に変更
            m_SceneFlag = true;
            m_Fade_flag_1.m_FadeFlag = 2;

        }
        else if (Input.GetKeyDown(KeyCode.LeftControl) && m_Fade_flag_1.m_FadeFlag == 1)
        {
            // フェードインのフラグを3に変更
            m_Fade_flag_1.m_FadeFlag = 3;
        }

        //********************************************************************************************
        //================================================================================================
        //      回転フラグ、フェードインフラグ処理
        //================================================================================================
        // Dを押したとき
        if (Input.GetKeyDown(InputXBOX360.P1_XBOX_R))
        {
            // 右回転フラグと左回転フラグがFALSEのときだけTRUEにする
            if (m_Right_RotateFlag == false && m_Left_RotateFlag == false)
                m_Right_RotateFlag = true;      // 右回転のフラグをtrueにする
        }
        // Aを押したとき
        else if (Input.GetKeyDown(InputXBOX360.P1_XBOX_L))
        {
            // 右回転フラグと左回転フラグがFALSEのときだけTRUEにする
            if (m_Left_RotateFlag == false && m_Right_RotateFlag == false)
                m_Left_RotateFlag = true;    // 左回転のフラグをtrueにする
        }
        // Shiftが押されたら遷移
        if (Input.GetKeyDown(InputXBOX360.P1_XBOX_START)
            && m_Fade_flag_1.m_FadeFlag <= 1 )       // フェードアウトしているか
        {
            // フェードインのフラグを1に変更
            m_Fade_flag_1.m_FadeFlag = 2;
        }

        // Shiftが押されたら遷移
        if (Input.GetKeyDown(InputXBOX360.P1_XBOX_A)
            && m_Fade_flag_1.m_FadeFlag == 0        // フェードアウトしているか
            && m_Right_RotateFlag == false         // 右回転しているか
            && m_Left_RotateFlag == false)         // 左回転しているか
        {
            // フェードインのフラグを1に変更
            m_Fade_flag_1.m_FadeFlag = 1;
        }
        else if (Input.GetKeyDown(InputXBOX360.P1_XBOX_A) && m_Fade_flag_1.m_FadeFlag == 1)
        {
            // シーン変更フラグをtrueにしてフェードインのフラグを2に変更
            m_SceneFlag = true;
            m_Fade_flag_1.m_FadeFlag = 2;

        }
        else if (Input.GetKeyDown(InputXBOX360.P1_XBOX_B) && m_Fade_flag_1.m_FadeFlag == 1)
        {
            // フェードインのフラグを3に変更
            m_Fade_flag_1.m_FadeFlag = 3;
        }

        //================================================================================================
        //      回転処理
        //================================================================================================
        //フェードインフラグが1の時(チーム再確認中)ではない時回転し、再確認中は回転フラグを常にfalseにする
        if (m_Fade_flag_1.m_FadeFlag == 0)
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

        //================================================================================================
        //      チーム決定後のラベル変更処理
        //================================================================================================
        // 自チームと相手チームのフェードインフラグが2の時
        if (m_Fade_flag_2.m_FadeFlag == 2 && m_Fade_flag_1.m_FadeFlag == 2)
            m_Label.text = "ゲームを開始します!";
        else
            m_Label.text = "相手チームの決定を待っています";

    }


    //=========================================================================================//
    // みぎ回転処理                                                                              //
    //=========================================================================================//
    void Left_Rotate()
    {

        // 左回転フラグがTRUEの時、90°右回転してフラグをFALSEにする
        if (m_Left_RotateFlag == true)
        {
            for (int i = 0; i < 4; i++)
            {
                switch (m_Country[i].m_Flag)
                {
                    case 0:
                        Position[i].x = -2.0f;
                        break;
                    case 1:
                        Position[i].x = -2.0f;
                        break;
                    case 2:
                        if (Position[i].x < -0.2f)
                            Position[i].x += 0.1f;
                        break;
                    case 3:
                        if (Position[i].x < 1.6f)
                            Position[i].x += 0.1f;
                        break;
                }
                // 計算した座標を代入
                m_Country[i].m_Country.transform.position = Position[i];

                // 回転中は全てのモデルを待機モーションにする
                m_Country[i].m_PlayerAnimator.ChangeAnimation(m_Country[i].m_PlayerAnimator.m_isWait);
            }

            //回転回数をカウント
            m_Count++;

            // 回転数が18回になった時
            if (m_Count >= 18)
            {
                for (int i = 0; i < 4; i++)
                {
                    // 国のフラグを加算する
                    m_Country[i].m_Flag++;

                    // フラグが4以上になった場合0にする
                    if (m_Country[i].m_Flag >= 4)
                    {
                        m_Country[i].m_Flag = 0;
                    }
                }

                // 回転数のカウントを0に戻し、左回転フラグをfalseにする
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
        // 右回転フラグがtrueの時
        if (m_Right_RotateFlag == true)
        {

            for (int i = 0; i < 4; i++)
            {
                switch (m_Country[i].m_Flag)
                {
                    case 0:
                        if (Position[i].x > -0.27f)
                            Position[i].x -= 0.1f;
                        break;
                    case 1:
                        Position[i].x = 1.6f;
                        break;
                    case 2:
                        Position[i].x = 1.6f;
                        break;
                    case 3:
                        if (Position[i].x > -2.0f)
                            Position[i].x -= 0.1f;
                        break;
                }
                //計算した座標を代入
                m_Country[i].m_Country.transform.position = Position[i];

                //回転中は全てのモデルを待機モーションにする
                m_Country[i].m_PlayerAnimator.ChangeAnimation(m_Country[i].m_PlayerAnimator.m_isWait);
            }
            // 回転数をカウント
            m_Count++;
            // 回転数が18回になったとき
            if (m_Count >= 18)
            {
                for (int i = 0; i < 4; i++)
                {
                    // 回転フラグを減算する
                    m_Country[i].m_Flag--;

                    // フラグが-1以下になった場合0にする
                    if (m_Country[i].m_Flag <= -1)
                    {
                        m_Country[i].m_Flag = 3;
                    }
                }
                // 回転数のカウントを0にして右回転フラグをfalseにする
                m_Count = 0;
                m_Right_RotateFlag = false;
            }
        }
    }
}

