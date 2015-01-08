﻿using UnityEngine;
using System.Collections;
 
public class Entry_2 : MonoBehaviour
{
    // 親情報の取得用
    title m_Title;
    SEPlay m_SE;
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
        m_SE = Entry_time.GetComponent<SEPlay>();
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
            if (Input.GetKeyDown(KeyCode.Alpha2) ||
                Input.GetKeyDown(KeyCode.Space) ||
                Input.GetKeyDown(InputXBOX360.P2_XBOX_A) ||
                InputXBOX360.IsGetAllStartButton() == true)
            {
                m_SE.VolumeSE(0.1f);
                m_SE.PlaySE("title/entry_go");
                m_inFlag = true;
                // 代入したPositionに対して大きな値を代入し、テクスチャを画面外へ吹っ飛ばす
                transform.position = new Vector3(transform.position.x, 2048.0f, transform.position.z);
            }
        }

        // エントリーキャンセルする場合　フラグがTRUE、尚且つキャラ選択画面への遷移のカウントが一定値以内の時
        if (m_inFlag == true && m_Title.m_StartCount == 0)
        {
            // エントリーキャンセル
            if (Input.GetKeyDown(KeyCode.W) ||
                Input.GetKeyDown(InputXBOX360.P2_XBOX_B))
            {
                m_SE.VolumeSE(0.1f);
                m_SE.PlaySE("title/entry_cancel");
                m_inFlag = false;
                // 代入したPositionに対して大きな値を代入し、テクスチャを画面外へ吹っ飛ばす
                transform.position = Position;
            }
        }
    }
}