using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour {
	int playerNum        = 0;
	public float mapsize = 1;

	public GameObject makerPrefab;
	// Use this for initialization
	void Start () {
		GameObject workObject;
		makerPrefab = Resources.Load("MapMaker") as GameObject;
		
		// マップ用テクスチャ貼り付け
		workObject = this.transform.root.FindChild("MapBaseX").gameObject;
		workObject.renderer.material.mainTexture = Resources.Load("Texture/MapBaseXTex") as Texture;
		workObject = this.transform.root.FindChild("MapBaseY").gameObject;
		workObject.renderer.material.mainTexture = Resources.Load("Texture/MapBaseYTex") as Texture;
		workObject.renderer.material.color = new Color(6.0f / 255.0f, 66.0f / 255.0f, 159.0f / 255.0f, 90.0f / 255.0f);
		// 6 66 159 90
	}
	
	// Update is called once per frame
	void Update () {
	}


	//----------------------------------------------------------------------
	// マップマーカーの作成
	//----------------------------------------------------------------------
	// @Param   playerData  マーキングしたいゲームオブジェクト
	//          setColor    マーカーの色
	// @Return  none
	// @Date    2014/10/13/09:32
	// @Update  2014/10/20/09:30  @Author T.Takeuchi
	//----------------------------------------------------------------------
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

// End of File