using UnityEngine;
using System.Collections;
struct Line
{
    public GameObject m_Obj;
    public LineRenderer m_LineRende;
    public Vector3 m_inPosition;
    public Vector3 m_outPosition;
}
public class Test : MonoBehaviour 
{
    GameObject m_Parent;
    Vector3 m_CenterPos;
    Line[] m_line = new Line[3];

	// Use this for initialization
	void Start () {
        m_Parent = this.gameObject.transform.parent.gameObject;
        
        m_line[0].m_inPosition.x = 0.0f;
        m_line[0].m_inPosition.y = 0.0f;
        m_line[0].m_inPosition.z = -0.1f;

        m_line[0].m_outPosition.x = 0.1f;
        m_line[0].m_outPosition.y = 0.14f;
        m_line[0].m_outPosition.z = -0.1f;

        m_line[1].m_inPosition.x = 0.1f;
        m_line[1].m_inPosition.y = 0.14f;
        m_line[1].m_inPosition.z = -0.1f;

        m_line[1].m_outPosition.x = 0.2f;
        m_line[1].m_outPosition.y = 0.0f;
        m_line[1].m_outPosition.z = -0.1f;

        m_line[2].m_inPosition.x = 0.2f;
        m_line[2].m_inPosition.y = 0.0f;
        m_line[2].m_inPosition.z = -0.1f;

        m_line[2].m_outPosition.x = 0.0f;
        m_line[2].m_outPosition.y = 0.0f;
        m_line[2].m_outPosition.z = -0.1f;

        m_line[0].m_Obj = this.gameObject.transform.FindChild("Line1").gameObject;
        m_line[1].m_Obj = this.gameObject.transform.FindChild("Line2").gameObject;
        m_line[2].m_Obj = this.gameObject.transform.FindChild("Line3").gameObject;

        m_line[0].m_LineRende = m_line[0].m_Obj.GetComponent<LineRenderer>();
        m_line[1].m_LineRende = m_line[1].m_Obj.GetComponent<LineRenderer>();
        m_line[2].m_LineRende = m_line[2].m_Obj.GetComponent<LineRenderer>();

        //lineRenderer.SetWidth(0.02f,0.02f);
	}
	
	// Update is called once per frame
	void Update () {
        m_CenterPos.x = m_Parent.transform.position.x - 0.3f;
        m_CenterPos.y = m_Parent.transform.position.y + 0.05f;
        m_CenterPos.z = m_Parent.transform.position.z;
        m_line[0].m_LineRende.SetPosition(0, m_line[0].m_inPosition + m_CenterPos);
        m_line[0].m_LineRende.SetPosition(1, m_line[0].m_outPosition + m_CenterPos);

        m_line[1].m_LineRende.SetPosition(0, m_line[1].m_inPosition + m_CenterPos);
        m_line[1].m_LineRende.SetPosition(1, m_line[1].m_outPosition + m_CenterPos);

        m_line[2].m_LineRende.SetPosition(0, m_line[2].m_inPosition + m_CenterPos);
        m_line[2].m_LineRende.SetPosition(1, m_line[2].m_outPosition + m_CenterPos);

        m_line[0].m_LineRende.SetVertexCount(2);
        m_line[1].m_LineRende.SetVertexCount(2);
        m_line[2].m_LineRende.SetVertexCount(2);
	}
}