using UnityEngine;
using System.Collections;
 
public class Entry_3 : MonoBehaviour {
	// 速度
	public Vector2 SPEED = new Vector2(0.05f, 0.01f);
	// エントリーしたかどうかの確認用フラグ
	bool in_flag = false;
	public static int up_count = 0;
	
	// Use this for initialization
	void Start () {
	
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
		
		// 左キーを1度も押していなければ
		if(in_flag == false)
		{
			// 左キーを押し続けていたら
			if(Input.GetKeyDown("right"))
			{
				in_flag = true;
			}
		}
		if(in_flag == true && up_count <=10)
		{
			// 代入したPositionに対して加算減算を行う
			Position.y += SPEED.y;
			// フラグをTrueに変えて再度加算しないようにする
			up_count ++;
		}
		// 現在の位置に加算減算を行ったPositionを代入する
		transform.position = Position;
	}
 
 
}
/*
		if(Input.GetKey("right")){ // 右キーを押し続けていたら
			// 代入したPositionに対して加算減算を行う
			Position.x += SPEED.x;
		} else if(Input.GetKey("up")){ // 上キーを押し続けていたら
			// 代入したPositionに対して加算減算を行う
			Position.y += SPEED.y;
		} else if(Input.GetKey("down")){ // 下キーを押し続けていたら
			// 代入したPositionに対して加算減算を行う
			Position.y -= SPEED.y;
		}
		
		// 現在の位置に加算減算を行ったPositionを代入する
		transform.position = Position;
*/