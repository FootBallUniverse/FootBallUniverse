using UnityEngine;
using System.Collections;

public class CCountDownManager : MonoBehaviour {

    private float m_gameFrame;
    private GameObject m_countDownObject;
    private int m_countNo;
    private GameObject m_uiPanel;

	// Use this for initialization
	void Start () {
        m_countNo = 1;
        m_gameFrame = 0.0f;

        // パネルを取得
        m_uiPanel = this.transform.parent.gameObject;
    }
	
	// Update is called once per frame
	void Update () {
        
        m_gameFrame += Time.deltaTime;

        // 1秒経ったら３秒前からカウントしていく
        if (m_gameFrame >= 1.0f && m_countNo < 5)
        {
            GameObject obj = (GameObject)GameObject.Instantiate(Resources.Load("Prefab/Game/count_down_" + m_countNo));
            obj.transform.parent = m_uiPanel.transform;
			obj.GetComponent<UISprite>().depth = 9;
            m_gameFrame = 0.0f;
            m_countNo += 1;

            if (m_countNo == 5)
                GameObject.Destroy(this.gameObject);
        }
	}
}
