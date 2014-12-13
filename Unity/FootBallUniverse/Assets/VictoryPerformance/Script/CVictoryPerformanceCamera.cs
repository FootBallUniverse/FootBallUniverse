using UnityEngine;
using System.Collections;

public class CVictoryPerformanceCamera : MonoBehaviour {

	private float m_frame;
	private bool m_isCamera;

	// Use this for initialization
	void Start () {
		m_frame = 0.0f;
		m_isCamera = false;

		if (TeamData.GetWinTeamNo () == 2) 
		{
			this.transform.localPosition = new Vector3 (0.0f, 0.0f, -0.7f);
			this.transform.localRotation = Quaternion.Euler (new Vector3 (0.0f, 0.0f, 0.0f));
		}
	}
	
	// Update is called once per frame
	void Update () {
		m_frame += Time.deltaTime;

		if (TeamData.GetWinTeamNo() != 2 && m_frame >= 3.5f && m_isCamera == false) 
        {
			if (TeamData.GetWinTeamNo () == 1) {
				this.GetComponent<TweenPosition> ().from = new Vector3 (this.transform.localPosition.x, this.transform.localPosition.y, this.transform.localPosition.z);
				this.GetComponent<TweenPosition> ().to = new Vector3 (this.transform.localPosition.x, this.transform.localPosition.y, this.transform.localPosition.z + 0.5f);
			} 
            else if (TeamData.GetWinTeamNo () == 0)
            {
				this.GetComponent<TweenPosition> ().from = new Vector3 (this.transform.localPosition.x, this.transform.localPosition.y, this.transform.localPosition.z);
				this.GetComponent<TweenPosition> ().to = new Vector3 (this.transform.localPosition.x, this.transform.localPosition.y, this.transform.localPosition.z - 0.5f);
			}

            this.GetComponent<TweenPosition>().Play(true);
            m_isCamera = true;
		}
		else if(TeamData.GetWinTeamNo() == 2 && m_frame >= 2.5f && m_isCamera == false )
		{
            this.GetComponent<TweenPosition>().from = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, this.transform.localPosition.z);
            this.GetComponent<TweenPosition>().to = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, this.transform.localPosition.z);
            this.GetComponent<TweenPosition>().Play(true);
            m_isCamera = true;
        }

	}
}
