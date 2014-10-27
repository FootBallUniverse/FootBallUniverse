using UnityEngine;
using System.Collections;

public class Player_Rotate : MonoBehaviour {

	int m_Flag,m_Count;
	// 速度
	public Vector2 SPEED = new Vector2(0.05f, 0.01f);
	
	// Use this for initialization
	void Start () {
		m_Flag = 0;
		m_Count = 0;
	}
	
	// Update is called once per frame
	void Update () {
		switch(m_Flag){
			case 0:
				transform.position -= transform.right * 0.01f;
				transform.position -= transform.forward * 0.01f;
				m_Count ++;
				
				if(m_Count >= 31){
					m_Flag = 1;
					m_Count = 0;
				}
					
				break;
			case 1:
				transform.position -= transform.right * 0.01f;
				transform.position += transform.forward * 0.01f;
				m_Count ++;
				
				if(m_Count >= 31){
					m_Flag = 2;
					m_Count = 0;
				}
					
				break;
			case 2:
				transform.position += transform.right * 0.01f;
				transform.position += transform.forward * 0.01f;
				m_Count ++;
				
				if(m_Count >= 31){
					m_Flag = 3;
					m_Count = 0;
				}
					
				break;
			case 3:
				transform.position += transform.right * 0.01f;
				transform.position -= transform.forward * 0.01f;
				m_Count ++;
				
				if(m_Count >= 31){
					m_Flag = 0;
					m_Count = 0;
				}
					
				break;
		}
	}
}