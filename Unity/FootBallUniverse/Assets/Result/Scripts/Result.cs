using UnityEngine;
using System.Collections;

public class Result : MonoBehaviour {


    //----------------------------------------------------------------------
    // コンストラクタ
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/10/29  @Update 2014/10/29  @Author T.Kawashita      
    //----------------------------------------------------------------------
	void Start () {
	
	}


    //----------------------------------------------------------------------
    // 更新
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/10/29  @Update 2014/10/29  @Author T.Kawashita      
    //----------------------------------------------------------------------
    void Update () {

        // デバッグ用スペースキーが押されたら強制的にタイトル画面へ
        if (Input.GetKeyDown(KeyCode.Space) ||
            InputXBOX360.IsGetAllStartButton() )
        { 
            // ここにタイトル画面に遷移する時のアニメーションを書く
            // 今は強制的に画面を遷移
            Application.LoadLevel("Title");
            Debug.Log("Title画面に遷移");
        }
	}
}
