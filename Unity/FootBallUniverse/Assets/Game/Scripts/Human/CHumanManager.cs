using UnityEngine;
using System.Collections;

public class CHumanManager : MonoBehaviour {

    private const int m_worldNum = 4;
    private const int m_humanStatusNum = 38;

    public static CJapanHuman m_japanHuman;
    public static CBrasilHuman m_brasilHuman;
    public static CSpainHuman m_spainHuman;
    public static CEnglandHuman m_englandHuman;

    //----------------------------------------------------------------------
    // コンストラクタ
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/10/24  @Update 2014/10/24  @Author T.Kawashita      
    //----------------------------------------------------------------------
    void Awake()
    {
        m_japanHuman = new CJapanHuman();
        m_brasilHuman = new CBrasilHuman();
        m_spainHuman = new CSpainHuman();
        m_englandHuman = new CEnglandHuman();

        this.SetData();
    }

    //----------------------------------------------------------------------
    // 更新
    //----------------------------------------------------------------------
    // @Param   none			
    // @Return	none
    // @Date	2014/10/24  @Update 2014/10/24  @Author T.Kawashita      
    //----------------------------------------------------------------------
    void Update()
    { 
    }

    //----------------------------------------------------------------------
    // データをセット
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/10/24  @Update 2014/10/24  @Author T.Kawashita       
    //----------------------------------------------------------------------
    public void SetData()
    {
        // CSVファイルをロード
        string path = Application.dataPath + "/Resources/CSV/HumanData.csv";
        string[,] csvData = new string[m_worldNum,m_humanStatusNum];
        CCSVLoader.GetInstance().Loader(ref csvData, path,m_worldNum );

        // 変換用ワーク作成
        string[] work = new string[m_humanStatusNum];

        // データをセットしていく
        m_japanHuman.Set(CUtility.ChangeArray(ref work, csvData, 0));

        m_spainHuman.Set(CUtility.ChangeArray(ref work, csvData, 1));
        m_englandHuman.Set(CUtility.ChangeArray(ref work, csvData, 2));
        m_brasilHuman.Set(CUtility.ChangeArray(ref work, csvData, 3));
    }

    //----------------------------------------------------------------------
    // プレイヤーの国を取得
    //----------------------------------------------------------------------
    // @Param	eWORLD  取得したい国名		
    // @Return	CHuman  取得された国のインスタンス
    // @Date	2014/10/24  @Update 2014/10/24  @Author T.Kawashita      
    //----------------------------------------------------------------------
    public static CHuman GetWorldInstance(TeamData.TEAM_NATIONALITY _world)
    {
        switch (_world)
        {
            // 日本
            case TeamData.TEAM_NATIONALITY.JAPAN:
                return m_japanHuman;
            
            // スペイン
            case TeamData.TEAM_NATIONALITY.ESPANA:
                return m_spainHuman;

            // イングランド
            case TeamData.TEAM_NATIONALITY.ENGLAND:
                return m_englandHuman;

            // ブラジル
            case TeamData.TEAM_NATIONALITY.BRASIL:
                return m_brasilHuman;
        }

        return null;
    }
}
