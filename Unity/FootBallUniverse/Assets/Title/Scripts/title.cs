using UnityEngine;
using System.Collections;

public class title : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
		if(Entry_1.m_upCount >= 10 && Entry_2.m_upCount >= 10 && Entry_3.m_upCount >= 10 && Entry_4.m_upCount >= 10 )
		{
            // ここにアニメーションの開始を入れる

			Application.LoadLevel("ChooseTeam");
		}

        // スペースキーで強制的に次のシーンに飛ばす
		if(Input.GetKeyDown(KeyCode.Space))
		{
			Application.LoadLevel("ChooseTeam");
		}
	}

    //----------------------------------------------------------------------
    // デバッグ用キー
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/10/29  @Update 2014/10/29  @Author T.Kawashita      
    //----------------------------------------------------------------------
    private void DebugKey()
    {
    }

}
