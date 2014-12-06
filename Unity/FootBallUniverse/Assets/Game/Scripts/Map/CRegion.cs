using UnityEngine;
using System.Collections;

//----------------------------------------------------
// リージョン分割の基本的なクラス
// 基底クラス
//----------------------------------------------------
public class CRegion : MonoBehaviour {

	public float FieldRadius;	// フィールドの半径
	public float Partition;		// フィールドの軸別分割数
	public float AreaScale;		// 分割したエリアの大きさ
	
	public Vector3 RegionArea;	// 分割エリアの軸別番号
	public Vector3 AreaCenter;// エリアの中心座標

	void Start () {

	}
	
	//----------------------------------------------------------------------
	// 初期化
	//----------------------------------------------------------------------
	// @Param   none			
	// @Return	bool    成功か失敗
	// @Date	2014/12/01  @Update 2014/12/01  @Author T.Kaneko      
	//----------------------------------------------------------------------
	protected bool Init()
	{
		FieldRadius = 30.0f;					// フィールドの半径
		Partition = 20;							// フィールド軸別分割数
		AreaScale = (FieldRadius*2) / Partition;// １エリアの大きさ
		
		AreaCenter = new Vector3(0.0f,0.0f,0.0f);

		return true;
	}
	
}
