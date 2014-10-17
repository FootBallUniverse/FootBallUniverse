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

		// xz軸をあわせる
		workVec = playerData.transform.position;
		workVec.y = 0.0f;
		this.transform.localPosition = workVec;
		// ポインタのy軸を合わせる
		workVec = playerData.transform.position;
		workVec.x = workVec.z = 0.0f;
		this.pointer.transform.localPosition = workVec;
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

	public void Init(GameObject playerData,Color setColor)
	{
		Mesh workMesh;
		Vector3[] vertices;

		// エラーチェック
		if (playerData == null) return;

		// オブジェクト設定
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

		// Pointer
		/*
		workMesh = this.pointer.GetComponent<MeshFilter>().mesh;
		vertices = workMesh.vertices;

		vertices[0].x = vertices[0].y = 0.0f;
		vertices[0].z += 5.0f;
		vertices[3] = vertices[2] = vertices[1] = vertices[0];

		workMesh.vertices = vertices;
		workMesh.RecalculateBounds();
		 */

		// テクスチャ指定
		//this.quad.renderer.materials[0].mainTexture = Resources.Load("MapMakerTexture") as Texture;
		// 色設定
		this.playerData = playerData;
		setColor.a = 0.5f;
		this.hightPole.renderer.material.color = setColor;
		this.quad.renderer.material.color = setColor;
		this.pointer.renderer.material.color = setColor;
	}
}
