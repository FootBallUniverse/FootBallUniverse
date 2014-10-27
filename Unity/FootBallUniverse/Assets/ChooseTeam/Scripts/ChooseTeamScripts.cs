using UnityEngine;
using System.Collections;

public class ChooseTeamScripts : MonoBehaviour {
	bool m_xFlag,m_zFlag;
	// 速度
	public Vector2 SPEED = new Vector2(0.05f, 0.01f);
	GameObject kaiten;
	// Use this for initialization
	void Start () {
		m_xFlag = false;
		m_zFlag = false;
	}
	
	// Update is called once per frame
	void Update () {
		//if(Input.GetKey(KeyCode.Space)){
		/*if(m_xFlag == false){
			kaiten.transform.position -= transform.right * 0.01f;
			if(kaiten.transform.position.x <=-0.31f)
			{
				m_xFlag = true;
			}
		}else if(m_xFlag == true){
			kaiten.transform.position += transform.right * 0.01f;
			if(kaiten.transform.position.x >= 0.31f)
			{
				m_xFlag = false;
			}
		}
		if(m_zFlag == false){
			kaiten.transform.position -= transform.forward * 0.01f;
			if(kaiten.transform.position.z <=-0.31f)
			{
				m_zFlag = true;
			}
		}else if(m_zFlag == true){
			kaiten.transform.position += transform.forward * 0.01f;
			if(kaiten.transform.position.z >= 0.31f)
			{
				m_zFlag = false;
			}
		}*/
			//kaiten.transform.position += transform.forward * 0.001f;
			//transform.Translates(Vector3.left * 0.01f);
		//}
		// 移動処理
		//Move();
	}
	/*void Move(){
		// 現在位置をPositionに代入
		Vector2 Position = transform.position;
		if(Input.GetKey("UP")){
			transform.Rotate(0.0f,90.0f * Time.deltaTime,0.0f);
			Position.x += transform.forward * 0.1f;
		}
		// 現在の位置に加算減算を行ったPositionを代入する
		transform.position = Position;
	}*/
}


//kaiten.transform.Rotate(0.0f,90.0f * Time.deltaTime,0.0f);
