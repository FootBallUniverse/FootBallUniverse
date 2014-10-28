using UnityEngine;
using System.Collections;


//----------------------------------------------------------------------
// CGameManager
//----------------------------------------------------------------------
// @Info ゲームの管理クラス
// @Date 2014/10/27 @Update 2014/10/27 @Author T.Kawashita      
//----------------------------------------------------------------------
public class CGameManager : MonoBehaviour {

    private float m_frame;            // タイマー調整用フレーム

    //----------------------------------------------------------------------
    // コンストラクタ
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/10/27  @Update 2014/10/27  @Author T.Kawashita      
    //----------------------------------------------------------------------
    void Awake () {

        m_frame = 0;

        CGameData.GetInstance().Init();
        this.LoadData();
	}

    //----------------------------------------------------------------------
    // 更新
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/10/27  @Update T.Kawashita  @Author T.Kawashita      
    //----------------------------------------------------------------------
	void Update () {
        this.PlayTime();
    }

    //----------------------------------------------------------------------
    // ゲームタイマー
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	bool
    // @Date	2014/10/27  @Update 2014/10/27  @Author T.Kawashita      
    //----------------------------------------------------------------------
    private bool PlayTime()
    {
        
        // タイマーがONなら時間を進める
        if (CGameData.m_isTimer == true)
        {
            m_frame += Time.deltaTime;

            if (m_frame >= 1.0f)
            {
                m_frame = 0;
                CGameData.m_gamePlayTime--;

                if (CGameData.m_gamePlayTime <= 0)
                {
                    Debug.Log("Game END");
                    return true;
                }
            }
        }

        return false;
    }

    //----------------------------------------------------------------------
    // CSVファイルからグローバルデータを読み込み
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	bool    成功か失敗
    // @Date	2014/10/27  @Update 2014/10/27  @Author 2014/10/27      
    //----------------------------------------------------------------------
    private bool LoadData()
    {
        // CSVファイルをロード
        string path = Application.dataPath + "/Resources/CSV/GlobalData.csv";
        string[,] csvData = new string[1, CGameData.m_dataNum];
        CCSVLoader.GetInstance().Loader(ref csvData, path, 1);

        // 変換用ワーク作成
        string[] work = new string[CGameData.m_dataNum];

        // データをセット
        CGameData.SetData(CUtility.ChangeArray(ref work,csvData,0 ));

        return true;
    }
}
