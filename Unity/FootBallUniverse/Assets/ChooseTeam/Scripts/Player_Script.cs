using UnityEngine;
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
        public int m_Count;             // モデル回転

    };
	// 速度
	public TEAM_NO[] m_Country = new TEAM_NO[4];
	public Vector2 SPEED = new Vector2(0.05f, 0.01f);
	Vector3[] Position = new Vector3[4];
	// Use this for initialization
	void Start () {
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

        // 初期値の設定
        for(int i = 0; i < 4; i++){
            m_Country[i].m_TeamColor = 0;
            m_Country[i].m_Count     = 0;
            m_Country[i].degree      = 90.0f * i;
            m_Country[i].r           = 0.31f;
            m_Country[i].centerx     = 0.0f;
            m_Country[i].centerz     = 0.0f;
            m_Country[i].radian      = 0.0f;
            m_Country[i].m_Flag      = i;
        }
	}
	
	// Update is called once per frame
	void Update () {
       // if(Input.GetKeyDown(KeyCode.A))
       // {
            for (int i = 0; i < 4; i++)
            {
			    switch(m_Country[i].m_Flag){
				    case 0:
                        Position[i].x = m_Country[i].centerx + m_Country[i].r * Mathf.Cos(m_Country[i].radian);
                        Position[i].z = m_Country[i].centerz + m_Country[i].r * Mathf.Sin(m_Country[i].radian) / 2;
					    m_Country[i].m_Count ++;
                        m_Country[i].degree += 5.0f;
                        if (m_Country[i].m_Count >= 31)
                        {
                            m_Country[i].m_Flag = 1;
                            m_Country[i].m_Count = 0;
					    }
						
					    break;
				    case 1:
                        Position[i].x = m_Country[i].centerx + m_Country[i].r * Mathf.Cos(m_Country[i].radian);
                        Position[i].z = m_Country[i].centerz + m_Country[i].r * Mathf.Sin(m_Country[i].radian) / 2;
					    m_Country[i].m_Count ++;
                        m_Country[i].degree += 5.0f;

                        if (m_Country[i].m_Count >= 31)
                        {
                            m_Country[i].m_Flag = 2;
                            m_Country[i].m_Count = 0;
					    }
						
					    break;
				    case 2:
					    Position[i].x = m_Country[i].centerx + m_Country[i].r * Mathf.Cos(m_Country[i].radian);
                        Position[i].z = m_Country[i].centerz + m_Country[i].r * Mathf.Sin(m_Country[i].radian) / 2;
					    m_Country[i].m_Count ++;
                        m_Country[i].degree += 5.0f;

                        if (m_Country[i].m_Count >= 31)
                        {
                            m_Country[i].m_Flag = 3;
                            m_Country[i].m_Count = 0;
					    }
						
					    break;
				    case 3:
					    Position[i].x = m_Country[i].centerx + m_Country[i].r * Mathf.Cos(m_Country[i].radian);
                        Position[i].z = m_Country[i].centerz + m_Country[i].r * Mathf.Sin(m_Country[i].radian) / 2;
					    m_Country[i].m_Count ++;
                        m_Country[i].degree += 5.0f;

                        if (m_Country[i].m_Count >= 31)
                        {
                            m_Country[i].m_Flag = 0;
                            m_Country[i].m_Count = 0;
					    }
						
					    break;
			    }
                m_Country[i].m_Country.transform.position = Position[i];
		    }
        //}
	}
}