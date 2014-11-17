﻿using UnityEngine;
using System.Collections;
 
public class Entry_3 : MonoBehaviour 
{
    // 親情報の取得用
    title m_Title;
    // 速度
    public Vector2 SPEED = new Vector2(0.05f, 0.01f);
    // エントリーしたかどうかの確認用フラグ
    public bool m_inFlag = false;
    // 初期位置保存用
    Vector3 Position;

    // Use this for initialization
    void Start()
    {
        // キャンセル用に初期位置を保存
        Position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        m_inFlag = false;

        GameObject Entry_time = this.transform.parent.gameObject;
        m_Title = Entry_time.GetComponent<title>();
    }


    // Update is called once per frame
    void Update()
    {
        // 移動処理
        Move();
    }

    // 移動関数
    void Move()
    {
        // エントリー出来るかどうか
        if (m_inFlag == false)
        {
            // エントリー
            if (Input.GetKeyDown(KeyCode.Alpha3) ||
                Input.GetKeyDown(KeyCode.Space) ||
                Input.GetKeyDown(InputXBOX360.P3_XBOX_A) ||
                InputXBOX360.IsGetAllStartButton() == true)
            {
                Debug.Log("Player3 Entry");
                m_inFlag = true;
                // 代入したPositionに対して大きな値を代入し、テクスチャを画面外へ吹っ飛ばす
                transform.position = new Vector3(transform.position.x, 2048.0f, transform.position.z);
            }
        }

        // エントリーキャンセルする場合
        if (m_inFlag == true && m_Title.m_StartCount < 1)
        {
            // エントリーキャンセル
            if (Input.GetKeyDown(KeyCode.E) ||
                Input.GetKeyDown(InputXBOX360.P3_XBOX_B))
            {
                Debug.Log("Player3 FAILED");
                m_inFlag = false;
                // 代入したPositionに対して大きな値を代入し、テクスチャを画面外へ吹っ飛ばす
                transform.position = Position;
            }
        }
    }
}
