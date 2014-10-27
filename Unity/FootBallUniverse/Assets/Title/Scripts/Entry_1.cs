using UnityEngine;
using System.Collections;
 
public class Entry_1 : MonoBehaviour {

	// 速度
	public Vector2 SPEED = new Vector2(0.05f, 0.01f);

    // エントリーしたかどうかの確認用フラグ
	private bool m_inFlag;
	public static int m_upCount;
	
	// Use this for initialization
	void Start () {
        m_inFlag = false;
        m_upCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
		// 移動処理
		Move();
	}
	
	// 移動関数
	void Move(){
	
		// 現在位置をPositionに代入
		Vector2 Position = transform.position;
		
        // エントリーできるかどうか
		if(m_inFlag == false)
		{
			// エントリー
			if(Input.GetKeyDown(KeyCode.Alpha1) ||
               Input.GetKeyDown(InputXBOX360.P1_XBOX_A) )
			{
                Debug.Log("Player1 Entry");
				m_inFlag = true;
			}
		}

		if(m_inFlag == true && m_upCount <=10)
		{
			// 代入したPositionに対して加算減算を行う
			Position.y += SPEED.y;
			// フラグをTrueに変えて再度加算しないようにする
			m_upCount ++;
		}

		// 現在の位置に加算減算を行ったPositionを代入する
		transform.position = Position;
	}
 
 
}
