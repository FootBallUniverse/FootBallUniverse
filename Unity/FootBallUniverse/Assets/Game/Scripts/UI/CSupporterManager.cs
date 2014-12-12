using UnityEngine;
using System.Collections;

public class CSupporterManager : MonoBehaviour {

	public static GameObject m_uiPanelMain;
	public static GameObject m_uiPanelP1P2;
	public static GameObject m_uiPanelP3P4;

	public static GameObject m_redSupporter_redLabel;
	public static GameObject m_redSupporter_redNum;
	public static GameObject m_redSupporter_blueLabel;
	public static GameObject m_redSupporter_blueNum;
	public static GameObject m_blueSupporter_redLabel;
	public static GameObject m_blueSupporter_redNum;
	public static GameObject m_blueSupporter_blueLabel;
	public static GameObject m_blueSupporter_blueNum;

	// Use this for initialization
	void Start () {

		m_uiPanelMain = GameObject.Find("MainUI").transform.FindChild("Camera").FindChild("Anchor").FindChild("Panel").gameObject;
		m_uiPanelP1P2 = GameObject.Find("P1&P2").transform.FindChild("UI").transform.FindChild("Camera").transform.FindChild("Anchor").transform.FindChild("Panel").gameObject;
		m_uiPanelP3P4 = GameObject.Find("P3&P4").transform.FindChild("UI").transform.FindChild("Camera").transform.FindChild("Anchor").transform.FindChild("Panel").gameObject;

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// 赤チームのサポーターが増えた
	public static void AddSupporter(int _teamNo,int _count)
	{
		// データ上を更新させる
		int num = _count;
		int addSupporter = CalcSupporter (ref num);
		TeamData.AddSupporter (0, addSupporter);
	
		// 表示物を変更させる
		// 赤チーム
		if (_teamNo == 1)
		{
			m_redSupporter_redLabel = (GameObject)Instantiate(Resources.Load("Prefab/Game/Supporter/redSupporter_redLabel"));
			m_redSupporter_redNum = (GameObject)Instantiate(Resources.Load("Prefab/Game/Supporter/redSupporter_redNum"));
			m_redSupporter_redLabel.transform.parent = m_uiPanelP1P2.transform;
			m_redSupporter_redNum.transform.parent = m_uiPanelP1P2.transform;

			m_redSupporter_redLabel.transform.localPosition = new Vector3( -240.0f, 245.0f,0.0f);
			m_redSupporter_redLabel.transform.localScale = new Vector3(40.0f,40.0f,1.0f);

			m_redSupporter_redNum.transform.localPosition = new Vector3(-250.0f,200.0f,0.0f);
			m_redSupporter_redNum.transform.localScale = new Vector3(50.0f,50.0f,1.0f);

			m_redSupporter_redNum.GetComponent<CSupporter>().StartSupporterDraw("+"+_count.ToString());
			m_redSupporter_redLabel.GetComponent<CSupporter>().StartSupporterDraw("サポーター");
		}
		// 青チーム
		else if (_teamNo == 2) 
		{
			m_redSupporter_blueLabel = (GameObject)Instantiate(Resources.Load("Prefab/Game/Supporter/blueSupporter_redLabel"));
			m_redSupporter_blueNum = (GameObject)Instantiate(Resources.Load("Prefab/Game/Supporter/blueSupporter_redNum"));
			m_redSupporter_blueLabel.transform.parent = m_uiPanelP1P2.transform;
			m_redSupporter_blueNum.transform.parent = m_uiPanelP1P2.transform;

			m_redSupporter_blueLabel.transform.localPosition = new Vector3(240.0f,245.0f,0.0f);
			m_redSupporter_blueLabel.transform.localScale = new Vector3(40.0f,40.0f,1.0f);

			m_redSupporter_blueNum.transform.localPosition = new Vector3(250.0f,200.0f,0.0f);
			m_redSupporter_blueNum.transform.localScale = new Vector3(60.0f,50.0f,1.0f);

			m_redSupporter_blueNum.GetComponent<CSupporter>().StartSupporterDraw("+"+_count.ToString());
			m_redSupporter_blueLabel.GetComponent<CSupporter>().StartSupporterDraw("サポーター");
		}
	}

	// 乱数によってサポーターの数を増減させる
	public static int CalcSupporter(ref int _num)
	{

		return _num;
	}
}
