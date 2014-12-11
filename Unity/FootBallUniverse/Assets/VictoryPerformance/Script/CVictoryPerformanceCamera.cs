using UnityEngine;
using System.Collections;

public class CVictoryPerformanceCamera : MonoBehaviour {

	private float m_frame;
	private bool m_isCamera;

	// Use this for initialization
	void Start () {
		m_frame = 0.0f;
		m_isCamera = false;
	}
	
	// Update is called once per frame
	void Update () {
		m_frame += Time.deltaTime;

		if (m_frame >= 4.5f && m_isCamera == false) {
			TweenPosition.Begin (this.gameObject,0.25f,new Vector3(this.transform.localPosition.x,
			                                           this.transform.localPosition.y,
			                                           -0.7f));
			m_isCamera = true;
		}

	}
}
