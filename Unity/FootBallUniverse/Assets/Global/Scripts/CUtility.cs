using UnityEngine;
using System.Collections;

public class CUtility {

    //----------------------------------------------------------------------
    // ２次元配列を１次元配列（データのみ）変換
    //----------------------------------------------------------------------
    // @Param	ref string[] 変換した後の出力配列
	// @Param   string[,]    変換する前の２次元配列
	// @Param   int          変換したい行番号
    // @Return	string[]     変換した後の１次元配列
    // @Date	2014/10/24  @Update 2014/10/24  @Author T.Kawashita      
    //----------------------------------------------------------------------
    public static string[] ChangeArray(ref string[] _outArray, string[,] _inArray, int _no)
    {
        // ２次元を１次元に変換

        for (int i = 0; i < _outArray.Length; ++i)
        {
            _outArray[i] = _inArray[_no, i];
        }

        return _outArray;
    }


}
