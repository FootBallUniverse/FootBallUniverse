using UnityEngine;
using System.Collections;

public class CCountDownManager : MonoBehaviour {

    private float m_gameFrame;
    private GameObject m_countDownObject;
    private int m_countNo;
    private GameObject m_uiPanel;
    private bool m_isCount;

	// Use this for initialization
	void Start () {
        m_countNo = 1;
        m_gameFrame = 0.0f;

        m_isCount = false;

        // パネルを取得
        m_uiPanel = this.transform.parent.gameObject;
    }
	
	// Update is called once per frame
	void Update () {
        
        m_gameFrame += Time.deltaTime;

        // 1秒経ったら３秒前からカウントしていく(カウントのみ）
        if (m_gameFrame >= 1.0f && m_countNo < 4)
        {
            GameObject obj = (GameObject)GameObject.Instantiate(Resources.Load("Prefab/Game/count_down_" + m_countNo));
            obj.transform.parent = m_uiPanel.transform;
			obj.GetComponent<UISprite>().depth = 9;
            m_gameFrame = 0.0f;
            m_countNo += 1;

        }

        // Kick
        if (m_countNo == 4 && m_gameFrame >= 1.0f)
        {
            if (m_isCount == false)
            {
                m_countDownObject = (GameObject)GameObject.Instantiate(Resources.Load("Prefab/Game/kick"));
                m_countDownObject.transform.parent = m_uiPanel.transform.FindChild("KickOffPanel").transform;
                m_countDownObject.transform.localPosition = new Vector3(1000.0f, 0.0f, 0.0f);
                m_countDownObject.transform.localScale = new Vector3(500.0f, 140.0f, 0.0f);
                m_countDownObject.GetComponent<UISprite>().depth = 10;
                m_isCount = true;
            }

            if (m_isCount == true && m_countDownObject.GetComponent<TweenPosition>().enabled == false)
            {
                m_countNo++;
                m_isCount = false;
            }
        }

        // OFF
        if (m_countNo == 5)
        {
            if (m_isCount == false)
            {
                m_countDownObject = (GameObject)GameObject.Instantiate(Resources.Load("Prefab/Game/off"));
                m_countDownObject.transform.parent = m_uiPanel.transform.FindChild("KickOffPanel").transform;
                m_countDownObject.transform.localPosition = new Vector3(-1000.0f, 0.0f, 0.0f);
                m_countDownObject.transform.localScale = new Vector3(400.0f, 140.0f, 0.0f);
                m_countDownObject.GetComponent<UISprite>().depth = 10;
                m_isCount = true;

            }
            if (m_isCount == true && m_countDownObject.GetComponent<TweenPosition>().enabled == false)
            {
                m_countNo++;
                m_isCount = false;
            }
        }

        // KickOFFBack
        if (m_countNo == 6)
        {
            if (m_isCount == false)
            {
                m_countDownObject = (GameObject)GameObject.Instantiate(Resources.Load("Prefab/Game/kickOff_back"));
                m_countDownObject.transform.parent = m_uiPanel.transform.FindChild("KickOffPanel").transform;
                m_countDownObject.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
                m_countDownObject.transform.localScale = new Vector3(450.0f, 450.0f, 0.0f);
                m_uiPanel.transform.FindChild("KickOffPanel").GetComponent<TweenScale>().Play(true);
                m_isCount = true;
            }

            if (m_uiPanel.transform.FindChild("KickOffPanel").GetComponent<TweenScale>().enabled == false)
            {
                m_gameFrame = 0.0f;
                m_countNo++;
            }
        }
        if( m_countNo == 7 )
        {
            if (m_gameFrame >= 0.4f)
            {
                m_uiPanel.transform.FindChild("KickOffPanel").GetComponent<TweenAlpha>().Play(true);
                GameObject.Destroy(this.gameObject);
            }
        }
	}
}
