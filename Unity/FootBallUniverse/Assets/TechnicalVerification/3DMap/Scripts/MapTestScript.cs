using UnityEngine;
using System.Collections;

public class MapTestScript : MonoBehaviour {
	public int playerMax;
	GameObject MapData;
	public int playerNo = 0;

	public GameObject playerData;

	// Use this for initialization
	void Start () {
		GameObject workObject;

		Color setColor;

		this.MapData = GameObject.Find("Map");
		for (int i = 0; i < this.playerMax; i++)
		{
			workObject = GameObject.Find("Player" + i);
			Debug.Log("Player" + i);
			//色設定
			if ((this.playerMax / 2) - 1 < i) setColor = Color.red;
			else                        setColor = Color.blue;
			setColor.a = 0.500f;
			this.MapData.GetComponent<Map>().CreateMaker(workObject,setColor);
		}

		// デバッグ用
		this.playerData = GameObject.Find("Player" + playerNo);
	}
	
	// Update is called once per frame
	void Update () {
		// 切り替え
		if (Input.GetKeyDown(KeyCode.Space))
		{
			this.playerNo++;
			if (this.playerNo == this.playerMax) this.playerNo = 0;
			this.playerData = GameObject.Find("Player" + playerNo);
		}
		// 移動
		if (Input.GetKey(KeyCode.UpArrow))    this.playerData.transform.Translate( 0.0f, 0.0f, 1.0f);
		if (Input.GetKey(KeyCode.DownArrow))  this.playerData.transform.Translate( 0.0f, 0.0f,-1.0f);
		if (Input.GetKey(KeyCode.LeftArrow))  this.playerData.transform.Translate(-1.0f, 0.0f, 0.0f);
		if (Input.GetKey(KeyCode.RightArrow)) this.playerData.transform.Translate( 1.0f, 0.0f, 0.0f);
		if (Input.GetKey(KeyCode.W))          this.playerData.transform.Rotate(new Vector3(1.0f, 0.0f, 0.0f), 0.5f);
		if (Input.GetKey(KeyCode.S))          this.playerData.transform.Rotate(new Vector3(1.0f, 0.0f, 0.0f),-0.5f);
		if (Input.GetKey(KeyCode.A))          this.playerData.transform.Rotate(new Vector3(0.0f, 0.0f, 1.0f), 0.5f);
		if (Input.GetKey(KeyCode.D))          this.playerData.transform.Rotate(new Vector3(0.0f, 0.0f, 1.0f),-0.5f);
		
	}

	void OnGUI()
	{
		GUI.Label(new Rect(0.0f, 0.0f, 100.0f, 20.0f), "PlayerNo "+this.playerNo);
	}
}
