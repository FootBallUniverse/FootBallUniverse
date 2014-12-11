using UnityEngine;
using System.Collections;

public class CVictoryPerformanceManager : MonoBehaviour {

	// 勝利演出モーションのステータス
	public enum eSTATUS_VICTORYPERFORMANCE
	{
		eNONE,
		eSTART,
		eFADE_OUT,
		eFADE_IN,
		ePERFORMANCE,
		eEND,
	};

	public eSTATUS_VICTORYPERFORMANCE m_status;

	private GameObject m_mainUIPanel;
	private GameObject m_1p2pUIPanel;
	private GameObject m_3p4pUIPanel;

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
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
