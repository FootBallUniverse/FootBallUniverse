using UnityEngine;
using System.Collections;

public class CGaugeManager : MonoBehaviour {

    private const int m_gaugeStatusNum = 12;

    public static float m_1p2pUpGaugeRate;
    public static float m_3p4pUpGaugeRate;

    // CSVからのデータ取得領域
    public static float m_initValue;            // ゲージの初期値
    public static float m_minValue;             // ゲージの最低値
    public static float m_maxValue;             // ゲージの最大値
    public static float m_levelBorder0;         // レベル0の範囲
    public static float m_levelBorder1;         // レベル1の範囲
    public static float m_levelBorder2;         // レベル2の範囲
    public static float m_levelBorder3;         // レベル3の範囲
    public static float m_defaultGaugeRate;     // 基本の自然増加量
    public static float m_supporterGaugeRate;   // サポーターの自然増加量
    public static float m_decrementValue1;      // レベル1の減少量
    public static float m_decrementValue2;      // レベル2の減少量
    public static float m_decrementValue3;      // レベル3の減少量

    //----------------------------------------------------------------------
    // コンストラクタ
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/12/31  @Update 2014/12/31  @Author T.Kawashita      
    //----------------------------------------------------------------------
	void Start () {
        
        // 各種初期化
        m_1p2pUpGaugeRate = 0.0f;
        m_3p4pUpGaugeRate = 0.0f;

        // CSVのデータをロードして格納
        this.LoadData();
	}

    //----------------------------------------------------------------------
    // 更新
    //----------------------------------------------------------------------
    // @Param   none			
    // @Return	none
    // @Date	2014/12/31  @Update 2014/12/31  @Author T.Kawashita      
    //----------------------------------------------------------------------
	void Update () {
        
        // ゲーム中の時だけゲージのレートを増やす
        switch (CGameManager.m_nowStatus)
        {
    
            case CGameManager.eSTATUS.eGAME:
                this.CalcRate(ref m_1p2pUpGaugeRate, 0);
                this.CalcRate(ref m_3p4pUpGaugeRate, 1);
                break;
        }

	}

    //----------------------------------------------------------------------
    // 最後の更新
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/12/31  @Update 2014/12/31  @Author T.Kawashita      
    //----------------------------------------------------------------------
    void LateUpdate()
    {
    }

    //----------------------------------------------------------------------
    // CSVからのデータをロード
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/12/31  @Update 2014/12/31  @Author T.Kawashita      
    //----------------------------------------------------------------------
    private void LoadData()
    {
        // CSVファイルをロード
        string path = Application.dataPath + "/Resources/CSV/GaugeDate.csv";
        string[,] csvData = new string[1, m_gaugeStatusNum];
        CCSVLoader.GetInstance().Loader(ref csvData, path, 1);

        // 変換用ワーク作成
        string[] work = new string[m_gaugeStatusNum];

        // データセット
        this.SetData(CUtility.ChangeArray(ref work, csvData, 0));

    }

    //----------------------------------------------------------------------
    // データのセット
    //----------------------------------------------------------------------
    // @Param	string[]    データが格納されたstring配列		
    // @Return	bool        成功か失敗
    // @Date	2014/12/31  @Update 2014/12/31  @Author T.Kawashita      
    //----------------------------------------------------------------------
    private bool SetData(string[] _array)
    {
        m_initValue = float.Parse(_array[0]);
        m_minValue = float.Parse(_array[1]);
        m_maxValue = float.Parse(_array[2]);
        m_levelBorder0 = float.Parse(_array[3]);
        m_levelBorder1 = float.Parse(_array[4]);
        m_levelBorder2 = float.Parse(_array[5]);
        m_levelBorder3 = float.Parse(_array[6]);
        m_defaultGaugeRate = float.Parse(_array[7]);
        m_supporterGaugeRate = float.Parse(_array[8]);
        m_decrementValue1 = float.Parse(_array[9]);
        m_decrementValue2 = float.Parse(_array[10]);
        m_decrementValue3 = float.Parse(_array[11]);

        return true;
    }

    //----------------------------------------------------------------------
    // ゲージのレートの計算
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/12/31  @Update 2014/12/31  @Author T.Kawashita      
    //----------------------------------------------------------------------
    private float CalcRate(ref float _rate, int _teamNo)
    {
        _rate = 0.0f;   // ゲージを一旦クリア
        _rate += (float)TeamData.suppoterByTeam[_teamNo] * m_supporterGaugeRate;
        _rate += m_defaultGaugeRate;

        return _rate;
    }

    //----------------------------------------------------------------------
    // レートの取得
    //----------------------------------------------------------------------
    // @Param   _gauge      ゲージ
    // @Param	_teamNo     チーム番号		
    // @Return	float       計算後のゲージ
    // @Date	2015/1/1  @Update 2015/1/1  @Author T.Kawashita
    // @Update  2015/1/3  ゲージの計算処理もこちらで行うよう設定
    //                  　理由：最大レートをチェックしたいため
    //----------------------------------------------------------------------
    public static float GetGaugeRate(ref float _gauge,int _teamNo)
    {
        // ゲージ量がマックスではないかどうか
        if (_gauge != m_maxValue)
        {
            // プレイヤー1,2のレート
            if (_teamNo == 1)
            {
                _gauge += m_1p2pUpGaugeRate;
            }
            // プレイヤー3,4のレート
            else if (_teamNo == 2)
            {
                _gauge += m_3p4pUpGaugeRate;
            }

            if (_gauge >= m_maxValue)
                _gauge = m_maxValue;
        }

        // 値がマックスならそのまま返す
        return _gauge;
    }

    //----------------------------------------------------------------------
    // レベル1ゲージ減少中の処理
    //----------------------------------------------------------------------
    // @Param	ref float   ゲージ		
    // @Return	bool        ゲージ減少が終了かどうか
    // @Date	2015/1/3  @Update 2015/1/3  @Author T.Kawashita      
    //----------------------------------------------------------------------
    public static bool CalcLevel1Rate(ref float _gauge)
    {
        _gauge -= m_decrementValue1;
        if (_gauge <= m_minValue)
            return true;

        return false;
    }

    //----------------------------------------------------------------------
    // レベル2ゲージ減少中の処理
    //----------------------------------------------------------------------
    // @Param	ref float   ゲージ		
    // @Return	bool        ゲージ減少が終了したかどうか
    // @Date	2015/1/3  @Update 2015/1/3  @Author T.Kawashita      
    //----------------------------------------------------------------------
    public static bool CalcLevel2Rate(ref float _gauge)
    {
        _gauge -= m_decrementValue2;
        if (_gauge <= m_minValue)
            return true;

        return false;
    }

    //----------------------------------------------------------------------
    // レベル3ゲージ減少中の処理
    //----------------------------------------------------------------------
    // @Param	ref float   ゲージ		
    // @Return	bool        ゲージ減少が終了したかどうか
    // @Date	2015/1/3  @Update 2015/1/3  @Author T.Kawashita      
    //----------------------------------------------------------------------
    public static bool CalcLevel3Rate(ref float _gauge)
    {
        _gauge -= m_decrementValue3;
        if (_gauge <= m_minValue)
            return true;

        return false;
    }
}
