using UnityEngine;
using System.Collections;

public class MapTestScript : MonoBehaviour {
	public int playerMax;
	GameObject MapData;

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
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
