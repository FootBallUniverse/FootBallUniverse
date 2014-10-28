using UnityEngine;
using System.Collections;

public class CHumanManager{

    // シングルトンのためのインスタンス
    private static CHumanManager m_humanManager = new CHumanManager();

    private const int m_worldNum = 4;
    private const int m_humanStatusNum = 27;

    public static CJapanHuman m_japanHuman;
    public static CBrazilHuman m_brazilHuman;
    public static CSpainHuman m_spainHuman;
    public static CEnglandHuman m_englandHuman;

    /// <summary>
    /// 国情報
    /// </summary>
    public enum eWORLD
    {
        eJAPAN,
        eSPAIN,
        eENGLAND,
        eBRAZIL
    };

    //----------------------------------------------------------------------
    // コンストラクタ
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/10/24  @Update 2014/10/24  @Author T.Kawashita      
    //----------------------------------------------------------------------
    public CHumanManager()
    {
        m_japanHuman = new CJapanHuman();
        m_brazilHuman = new CBrazilHuman();
        m_spainHuman = new CSpainHuman();
        m_englandHuman = new CEnglandHuman();
    
        this.SetData();
    }

    //----------------------------------------------------------------------
    // シングルトン実装
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	CHumanManager このクラスの唯一のインスタンス
    // @Date	2014/10/24  @Update 2014/10/24  @Author T.Kawashita      
    //----------------------------------------------------------------------
    public static CHumanManager GetInstance()
    {
        return m_humanManager;
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
        m_brazilHuman.Set(CUtility.ChangeArray(ref work, csvData, 3));
    }

    //----------------------------------------------------------------------
    // プレイヤーの国を取得
    //----------------------------------------------------------------------
    // @Param	eWORLD  取得したい国名		
    // @Return	CHuman  取得された国のインスタンス
    // @Date	2014/10/24  @Update 2014/10/24  @Author T.Kawashita      
    //----------------------------------------------------------------------
    public CHuman GetWorldInstance(eWORLD _world)
    {
        switch (_world)
        {
            // 日本
            case eWORLD.eJAPAN:
                return m_japanHuman;
            
            // スペイン
            case eWORLD.eSPAIN:
                return m_spainHuman;

            // イングランド
            case eWORLD.eENGLAND:
                return m_englandHuman;

            // ブラジル
            case eWORLD.eBRAZIL:
                return m_brazilHuman;
        }

        return null;
    }
}
