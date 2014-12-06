using UnityEngine;
using System.Collections;

public class Region : CRegion {

	//----------------------------------------------------------------------
	// コンストラクタ
	//----------------------------------------------------------------------
	// @Param   none			
	// @Return	none    
	// @Date	2014/12/01  @Update 2014/12/01  @Author T.Kaneko      
	//----------------------------------------------------------------------
	void Start () {
		Init ();
	}
	
	//----------------------------------------------------------------------
	// 更新処理
	//----------------------------------------------------------------------
	// @Param   none			
	// @Return	none    
	// @Date	2014/12/01  @Update 2014/12/01  @Author T.Kaneko      
	//----------------------------------------------------------------------
	void Update () {

		// 座標を基にボールの現在のエリアを求める
//		RegionArea.x = (int)((GameObject.FindGameObjectWithTag("SoccerBall").transform.position.x + FieldRadius) / AreaScale);
//		RegionArea.y = (int)((GameObject.FindGameObjectWithTag("SoccerBall").transform.position.y + FieldRadius) / AreaScale);
//		RegionArea.z = (int)((GameObject.FindGameObjectWithTag("SoccerBall").transform.position.z + FieldRadius) / AreaScale);

		// 現在エリアの中心座標を求める
//		AreaCenter.x = (RegionArea.x * AreaScale) + (AreaScale / 2) - FieldRadius;
//		AreaCenter.y = (RegionArea.y * AreaScale) + (AreaScale / 2) - FieldRadius;
//		AreaCenter.z = (RegionArea.z * AreaScale) + (AreaScale / 2) - FieldRadius;

		// デバッグ用表示ボックス移動処理
//		this.transform.position = AreaCenter;
//		this.transform.localScale = new Vector3(AreaScale,AreaScale,AreaScale);	// 1エリアの大きさにスケール変更

	}

	//----------------------------------------------------------------------
	// 渡された座標のエリア番号を返す
	//----------------------------------------------------------------------
	// @Param   _pos	座標		
	// @Return	Area	エリア番号
	// @Date	2014/12/01  @Update 2014/12/01  @Author T.Kaneko      
	//----------------------------------------------------------------------
	public Vector3 GetRegionArea(Vector3 _pos)
	{
		Vector3 Area = new Vector3();
		Area.x = (int)((_pos.x + FieldRadius) / AreaScale);
		Area.y = (int)((_pos.y + FieldRadius) / AreaScale);
		Area.z = (int)((_pos.z + FieldRadius) / AreaScale);

		return Area;
	}

	//----------------------------------------------------------------------
	// 渡されたエリアの中心座標を返す
	//----------------------------------------------------------------------
	// @Param   _area		エリア番号
	// @Return	CenterPos	エリアの中心座標
	// @Date	2014/12/01  @Update 2014/12/01  @Author T.Kaneko      
	//----------------------------------------------------------------------
	public Vector3 GetRegionCenter(Vector3 _area)
	{
		Vector3 CenterPos = new Vector3();
		CenterPos.x = (_area.x * AreaScale) + (AreaScale / 2) - FieldRadius;
		CenterPos.y = (_area.y * AreaScale) + (AreaScale / 2) - FieldRadius;
		CenterPos.z = (_area.z * AreaScale) + (AreaScale / 2) - FieldRadius;

		return CenterPos;
	}
}
