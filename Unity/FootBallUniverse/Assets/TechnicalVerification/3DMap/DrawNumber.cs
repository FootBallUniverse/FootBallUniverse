using UnityEngine;
using System.Collections;

public class DrawNumber : MonoBehaviour {
	public int number;                       // 表示したい数値
	public bool zeroNumberPlaceVewFlag;      // 左側の桁が０の時、表示するかしないか
	public GameObject[] numberPlace;  // 数字オブジェクト格納（さわっちゃダメ！）
	
	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update()
	{
		uint workNumber;
		uint work = (uint)(System.Math.Pow(10.0,(double)this.numberPlace.Length-1));
		bool vewFlag = this.zeroNumberPlaceVewFlag;

		for (int i = 0; i < this.numberPlace.Length; i++)
		{
			workNumber = (uint)((this.number % (work * 10) / work));
			work /= 10;

			// ０を表示しない場合
			if (workNumber == 0 && vewFlag == false && i != this.numberPlace.Length-1)
			{
				this.numberPlace[i].SetActive(false);
				continue;
			}

			// 数字を描画する
			vewFlag = true;
			this.numberPlace[i].SetActive(true);
			this.numberPlace[i].GetComponent<UISprite>().spriteName = "count_" + workNumber;
		}
	}
}
