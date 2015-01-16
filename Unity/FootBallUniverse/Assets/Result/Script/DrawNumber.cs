using UnityEngine;
using System.Collections;

public class DrawNumber : MonoBehaviour {
	public int number;                       // 表示したい数値
	public bool zeroNumberPlaceVewFlag;      // 左側の桁が０の時、表示するかしないか
	public GameObject[] numberPlace;         // 数字オブジェクト格納（さわっちゃダメ！）
	
	// 本来の初期化は無効
	void Start () {}

    //----------------------------------------------------------------------
    // 初期化
    //----------------------------------------------------------------------
    // @Param   none			
    // @Return	none
    // @Date	2014/12/29  @Update 2014/12/29  @Author T.Takeuchi      
    //----------------------------------------------------------------------
    public void Init()
    {
        uint workNumber;
        uint work = (uint)(System.Math.Pow(10.0, (double)this.numberPlace.Length - 1));
        bool vewFlag = this.zeroNumberPlaceVewFlag;

        for (int i = 0; i < this.numberPlace.Length; i++)
        {
            workNumber = (uint)((this.number % (work * 10) / work));
            work /= 10;

            // ０を表示しない場合
            if (workNumber == 0 && vewFlag == false && i != this.numberPlace.Length - 1)
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

	//----------------------------------------------------------------------
	// 更新
	//----------------------------------------------------------------------
	// @Param	none		
	// @Return	none
	// @Date	2014/12/29  @Update 2014/12/29  @Author T.Takeuchi      
	//----------------------------------------------------------------------
	void Update()
	{
        uint workNumber;
        uint work = (uint)(System.Math.Pow(10.0, (double)this.numberPlace.Length - 1));
        bool vewFlag = this.zeroNumberPlaceVewFlag;

        for (int i = 0; i < this.numberPlace.Length; i++)
        {
            workNumber = (uint)((this.number % (work * 10) / work));
            work /= 10;

            // ０を表示しない場合
            if (workNumber == 0 && vewFlag == false && i != this.numberPlace.Length - 1)
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

// End of File
