using UnityEngine;
using System.Collections;

public class Maker : MonoBehaviour {
	GameObject mapData;
	GameObject pointer;
	GameObject hightPole;
	GameObject quad;

	public GameObject playerData;
	// Use this for initialization
	void Start () {}


	// Update is called once per frame
	void Update () {
		Vector3 workVec;
		Mesh workMesh = this.hightPole.GetComponent<MeshFilter>().mesh;
		Vector3[] vertices = workMesh.vertices;

		// マップ上の座標を算出する
		workVec    = this.playerData.transform.position;
		workVec.x += this.mapData.GetComponent<Map>().mapsize / 2;
		workVec.y += this.mapData.GetComponent<Map>().mapsize / 2;
		workVec.z += this.mapData.GetComponent<Map>().mapsize / 2;
		workVec    = workVec / this.mapData.GetComponent<Map>().mapsize;
		workVec.x -= 0.5f;
		workVec.y -= 0.5f;
		workVec.z -= 0.5f;

		// xz軸をあわせる
		this.transform.localPosition = new Vector3(workVec.x, 0.0f, workVec.z);
		// ポインタのy軸を合わせる
		this.pointer.transform.localPosition = new Vector3(0.0f,workVec.y,0.0f);
		// ポインタの向きを合わせる
		this.pointer.transform.localEulerAngles = playerData.transform.localEulerAngles;
		
		// 棒の長さを合わせる
		for (int i = 0; i < vertices.Length; i++)
		{
			if (vertices[i].y != 0)
			{
				vertices[i].y = workVec.y;
				if (vertices[i].y == 0) vertices[i].y = 0.0001f;
			}
		}
		workMesh.vertices = vertices;
		workMesh.RecalculateBounds();
	}


	//----------------------------------------------------------------------
	// マップマーカーの初期化
	//----------------------------------------------------------------------
	// @Param   playerData  マーキングしたいゲームオブジェクト
	//          setColor    マーカーの色
	// @Return  none
	// @Date    2014/10/13/09:32
	// @Update  2014/10/20/09:30  @Author T.Takeuchi
	//----------------------------------------------------------------------
	public void Init(GameObject playerData,Color setColor)
	{
		Mesh workMesh;
		Vector3[] vertices;

		// エラーチェック
		if (playerData == null) return;

		// オブジェクト設定
		this.mapData   = this.gameObject.transform.root.gameObject;
		this.pointer = this.gameObject.transform.FindChild("Pointer").gameObject;
		this.hightPole = this.gameObject.transform.FindChild("HeightPole").gameObject;
		this.quad = this.gameObject.transform.FindChild("Quad").gameObject;

		// 高さポールを定位置に設定
		workMesh = this.hightPole.GetComponent<MeshFilter>().mesh;
		vertices = workMesh.vertices;

		for (int i = 0; i < vertices.Length; i++)
		{
			if (vertices[i].y > 0) vertices[i].y = 0.0f;
			else vertices[i].y = 0.0001f;
		}
		workMesh.vertices = vertices;
		workMesh.RecalculateBounds();

		// 色設定
		this.playerData = playerData;
		setColor.a = 0.5f;
		this.hightPole.renderer.material.color = setColor;
		this.quad.renderer.material.color = setColor;
		this.pointer.renderer.material.color = setColor;
	}
}

// End of File
