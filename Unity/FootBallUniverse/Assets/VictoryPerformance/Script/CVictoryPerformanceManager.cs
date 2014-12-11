using UnityEngine;
using System.Collections;

public class CVictoryPerformanceManager : MonoBehaviour {

	// 勝利演出モーションのステータス
	public enum eSTATUS_VICTORYPERFORMANCE
	{
		eNONE,
		eSTART,
		eINIT_FADE,
		eFADE_OUT,
		eFADE_IN,
		ePERFORMANCE,
		eEND,
	};

	public eSTATUS_VICTORYPERFORMANCE m_status;

	private float m_flame;

	private GameObject m_motionPlayer;

	private GameObject m_mainUIPanel;
	private GameObject m_1p2pUIPanel;
	private GameObject m_3p4pUIPanel;

	private GameObject m_mainCamera;
	private GameObject m_1p2pCamera;
	private GameObject m_3p4pCamera;

	private GameObject m_resultMain;
	private GameObject m_resultSub1;
	private GameObject m_resultSub2;

	// Use this for initialization
	void Start () {
		m_status = eSTATUS_VICTORYPERFORMANCE.eFADE_IN;

		// パネル取得
		m_mainUIPanel = GameObject.Find ("MainUI").transform.FindChild ("Camera").transform.FindChild ("Anchor").transform.FindChild ("Panel").gameObject;	
		m_1p2pUIPanel = GameObject.Find ("1p2pUI").transform.FindChild ("Camera").transform.FindChild ("Anchor").transform.FindChild ("Panel").gameObject;
		m_3p4pUIPanel = GameObject.Find ("3p4pUI").transform.FindChild ("Camera").transform.FindChild ("Anchor").transform.FindChild ("Panel").gameObject;

		m_mainUIPanel.transform.FindChild ("BlackOut").gameObject.AddComponent<CFadeIn> ();
		m_1p2pUIPanel.transform.FindChild ("BlackOut").gameObject.AddComponent<CFadeIn> ();
		m_3p4pUIPanel.transform.FindChild ("BlackOut").gameObject.AddComponent<CFadeIn> ();
	
		m_motionPlayer = GameObject.Find ("player_victory").gameObject;

		m_mainCamera = GameObject.Find ("MainCamera").gameObject;
		m_1p2pCamera = GameObject.Find ("1p2pCamera").gameObject;
		m_3p4pCamera = GameObject.Find ("3p4pCamera").gameObject;

		m_flame = 0.0f;


//		m_motionPlayer.animation.Stop ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		switch (m_status) 
		{
			case eSTATUS_VICTORYPERFORMANCE.eFADE_IN:
			// フェードインが終わったら
			if (m_mainUIPanel.transform.FindChild("BlackOut").gameObject.GetComponent<TweenAlpha> ().enabled == false &&	
			    m_1p2pUIPanel.transform.FindChild("BlackOut").gameObject.GetComponent<TweenAlpha> ().enabled == false &&
			    m_3p4pUIPanel.transform.FindChild("BlackOut").gameObject.GetComponent<TweenAlpha>().enabled ==  false ) 
			{
				m_status = eSTATUS_VICTORYPERFORMANCE.ePERFORMANCE;
			}
			break;

		case eSTATUS_VICTORYPERFORMANCE.ePERFORMANCE:
			// カメラの位置が変更されたら
			if(m_mainCamera.transform.localPosition.z == m_mainCamera.GetComponent<TweenPosition>().to.z &&
			   m_1p2pCamera.transform.localPosition.z == m_mainCamera.GetComponent<TweenPosition>().to.z &&
			   m_3p4pCamera.transform.localPosition.z == m_mainCamera.GetComponent<TweenPosition>().to.z )
			{
				m_status = eSTATUS_VICTORYPERFORMANCE.eINIT_FADE;
			}
			break;

		case eSTATUS_VICTORYPERFORMANCE.eINIT_FADE:
			m_flame += Time.deltaTime;
			if( m_flame >= 0.5f ){
				m_status = eSTATUS_VICTORYPERFORMANCE.eFADE_OUT;
				m_mainUIPanel.transform.FindChild ("BlackOut").gameObject.AddComponent<CVictoryPerformanceFadeOut> ();
				m_1p2pUIPanel.transform.FindChild ("BlackOut").gameObject.AddComponent<CVictoryPerformanceFadeOut> ();
				m_3p4pUIPanel.transform.FindChild ("BlackOut").gameObject.AddComponent<CVictoryPerformanceFadeOut> ();
				
			}
			break;
		
		case eSTATUS_VICTORYPERFORMANCE.eFADE_OUT:
			// フェードアウトが終わったら
			if (m_mainUIPanel.transform.FindChild("BlackOut").gameObject.GetComponent<TweenAlpha> ().enabled == false &&	
			    m_1p2pUIPanel.transform.FindChild("BlackOut").gameObject.GetComponent<TweenAlpha> ().enabled == false &&
			    m_3p4pUIPanel.transform.FindChild("BlackOut").gameObject.GetComponent<TweenAlpha>().enabled ==  false ) 
			{
				m_status = eSTATUS_VICTORYPERFORMANCE.eNONE;
				GameObject.Instantiate(Resources.Load("Prefab/Result/Manager"));
				m_resultMain = (GameObject)GameObject.Instantiate(Resources.Load("Prefab/Result/ResultPrefabMain"));
				m_resultSub1 = (GameObject)GameObject.Instantiate(Resources.Load("Prefab/Result/ResultPrefabSub0"));
                m_resultSub2 = (GameObject)GameObject.Instantiate(Resources.Load("Prefab/Result/ResultPrefabSub1"));
			}
			break;
		}

	}
}
