using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour {
	int playerNum = 0;

	public GameObject makerPrefab;
	// Use this for initialization
	void Start () {
		makerPrefab = Resources.Load("MapMaker") as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void CreateMaker(GameObject playerData,Color setColor)
	{
		GameObject newMaker;
		// エラーチェック
		if (playerData == null) return;
		// 新しいマーカー生成
		newMaker = Instantiate(this.makerPrefab) as GameObject;
		newMaker.transform.parent = this.transform;
		newMaker.name = "Marker" + this.playerNum;
		// 初期化
		newMaker.GetComponent<Maker>().Init(playerData,setColor);
		// 加算
		this.playerNum++;
	}
}
