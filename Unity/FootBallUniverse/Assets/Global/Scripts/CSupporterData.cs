using UnityEngine;
using System.Collections;

public class CSupporterData {

    // シングルトン用
    private static CSupporterData m_supportereData = new CSupporterData();

    private const int DATA_NUM = 21;

    public static int m_winSupporter;                   // 勝利した
    public static int m_drowSupporter;                  // ドローした
    public static int m_loseSupporter;                  // 負けた
    public static int m_point2WinSupporter;             // 2点差以上で勝利
    public static int m_point2Drow;                     // 2得点以上でドロー
    public static int m_getPointSupporter;              // 得点した
    public static int m_smashShootPointSupporter;       // スマッシュシュートで得点した
    public static int m_getDrawPointSupporter;          // 同点に追いつく得点をした
    public static int m_getDrawReversPointSupporter;    // 同点から逆転する得点をした
    public static int m_getBallSupporter;               // ボールを取った
    public static int m_getBallDashSupporter;           // ダッシュでボールを取った
    public static int m_getBallPassSupporter;           // 味方が蹴った（パス）したボールを取った
    public static int m_getBallTackleSupporter;         // 味方がタックルしたボールを取った
    public static int m_getBallGoalSupporter;           // 味方のゴールに向けて撃ったボールを取った
    public static int m_takeBallSupporter;              // 敵のボールを奪った
    public static int m_takeBallDashSupporter;          // 敵のボールをダッシュで奪った
    public static int m_takeBall3secSupporter;          // 奪われたボールを3秒以内に奪い返した
    public static int m_damageTackleSupporter;          // タックルを相手にあてた
    public static int m_damageTackleOnBallSupporter;    // ボールを持っている相手にタックルを当てた
    public static int m_holdBallNearPlayerSupporter;    // 相手が近くにいる状態でボールを持っていてX秒経った
    public static int m_sumashShootSupporter;           // スマッシュシュートをした


    //----------------------------------------------------------------------
    // コンストラクタ
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/12/12  @Update 2014/12/12  @Author T.Kawashita      
    //----------------------------------------------------------------------
	public CSupporterData () {

        LoadData();
	}


    //----------------------------------------------------------------------
    // CSVデータのロード
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/12/12  @Update 2014/12/12  @Author T.Kawashita      
    //----------------------------------------------------------------------
    private void LoadData()
    {
        // CSVファイルをロード
        string path = Application.dataPath + "/Resources/CSV/SupportersData.csv";
        string[,] csvData = new string[1, DATA_NUM];
        CCSVLoader.GetInstance().Loader(ref csvData, path, 1);

        // 変換用ワーク作成
        string[] work = new string[DATA_NUM];

        // データセット
        this.SetData(CUtility.ChangeArray(ref work, csvData, 0));
                
    }

    //----------------------------------------------------------------------
    // データのセット
    //----------------------------------------------------------------------
    // @Param	string[]    データが格納されたstring配列		
    // @Return	bool        成功か失敗
    // @Date	2014/12/12  @Update 2014/12/12  @Author T.Kawashita      
    //----------------------------------------------------------------------
    private bool SetData(string[] _array)
    {
        m_winSupporter = int.Parse(_array[0]);
        m_drowSupporter = int.Parse(_array[1]);
        m_loseSupporter = int.Parse(_array[2]);
        m_point2WinSupporter = int.Parse(_array[3]);
        m_point2Drow = int.Parse(_array[4]);
        m_getPointSupporter = int.Parse(_array[5]);
        m_smashShootPointSupporter = int.Parse(_array[6]);
        m_getDrawPointSupporter = int.Parse(_array[7]);
        m_getDrawReversPointSupporter = int.Parse(_array[8]);
        m_getBallSupporter = int.Parse(_array[9]);
        m_getBallDashSupporter = int.Parse(_array[10]);
        m_getBallPassSupporter = int.Parse(_array[11]);
        m_getBallTackleSupporter = int.Parse(_array[12]);
        m_getBallGoalSupporter = int.Parse(_array[13]);
        m_takeBallSupporter = int.Parse(_array[14]);
        m_takeBallDashSupporter = int.Parse(_array[15]);
        m_takeBall3secSupporter = int.Parse(_array[16]);
        m_damageTackleSupporter = int.Parse(_array[17]);
        m_damageTackleOnBallSupporter = int.Parse(_array[18]);
        m_holdBallNearPlayerSupporter = int.Parse(_array[19]);
        m_sumashShootSupporter = int.Parse(_array[20]);
        return true;
    }


}
