using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;

public class CCSVLoader {

    private static CCSVLoader m_csvLoader = new CCSVLoader();

    //----------------------------------------------------------------------
    // コンストラクタ
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/10/24  @Update 2014/10/24  @Author T.Kawashita      
    //----------------------------------------------------------------------
    private CCSVLoader()
    { 
    }

    //----------------------------------------------------------------------
    // シングルトン実装
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/10/24  @Update 2014/10/24  @Author T.Kawashita      
    //----------------------------------------------------------------------
    public static CCSVLoader GetInstance()
    {       
        return m_csvLoader;
    }

    //----------------------------------------------------------------------
    // CSVのロード
    //----------------------------------------------------------------------
    // @Param	ref string[,] string２次元配列 
	// @Param   string        ファイルパス
	// @param   line          読みこみたい行数（タイトルは数えない）
    // @Return	none
    // @Date	2014/10/24  @Update 2014/10/24  @Author T.Kawashita      
    //----------------------------------------------------------------------
    public void Loader(ref string[,] _data, string _path,int _line)
    {
        FileInfo fi = new FileInfo(_path);
        using (StreamReader sr = new StreamReader(fi.OpenRead(), Encoding.GetEncoding("Shift_JIS")))
        {
            string data = "";

            sr.ReadLine();  // １行目は飛ばす  

            for (int i = 0; i < _line; ++i)
            {
                data = sr.ReadLine();   // １行取り出す
                string[] work = data.Split(',');

                for (int n = 0; n < work.Length; ++n)
                {
                    _data[i, n] = work[n];
                }
            }
        }
    }

}
