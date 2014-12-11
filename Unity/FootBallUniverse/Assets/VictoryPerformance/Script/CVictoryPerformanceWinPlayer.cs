using UnityEngine;
using System.Collections;

public class CVictoryPerformanceWinPlayer : MonoBehaviour {


    //----------------------------------------------------------------------
    // コンストラクタ
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/12/12  @Update 2014/12/12  @Author T.Kawashita    
    //----------------------------------------------------------------------
	void Start () {
        
        // 1Pの勝利時は1pのゴール付近に近づける
        if (TeamData.GetWinTeamNo() == 0)
        {
            this.transform.localPosition = new Vector3(this.transform.localPosition.x,
                                                       this.transform.localPosition.y,
                                                       this.transform.localPosition.z - 20.0f);
            this.transform.localRotation = Quaternion.Euler(0.0f, 270.0f, 0.0f);

            GameObject.Find("MainCamera").transform.LookAt(this.transform);

            GameObject.Find("MainCamera").transform.localPosition = new Vector3(this.transform.localPosition.x,
                                                                                this.transform.localPosition.y + 0.92f,
                                                                                this.transform.localPosition.z + +1.3f);

            GameObject.Find("1p2pCamera").transform.localPosition = GameObject.Find("MainCamera").transform.localPosition;
            GameObject.Find("3p4pCamera").transform.localPosition = GameObject.Find("MainCamera").transform.localPosition;  
        }


        // 2Pの勝利時は2pのゴール付近に近づける
        else if (TeamData.GetWinTeamNo() == 1)
        {
            this.transform.localPosition = new Vector3(this.transform.localPosition.x,
                                                       this.transform.localPosition.y,
                                                       this.transform.localPosition.z + 20.0f);

            GameObject.Find("MainCamera").transform.localPosition = new Vector3(this.transform.localPosition.x,
                                                                                this.transform.localPosition.y + 0.92f,
                                                                                this.transform.localPosition.z + -1.3f);


            GameObject.Find("1p2pCamera").transform.localPosition = GameObject.Find("MainCamera").transform.localPosition;
            GameObject.Find("3p4pCamera").transform.localPosition = GameObject.Find("MainCamera").transform.localPosition;  
        }

        else if (TeamData.GetWinTeamNo() == 2)
        {
            this.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);

            GameObject.Find("MainCamera").transform.localRotation = Quaternion.Euler(0.0f, -90.0f, 0.0f);
            GameObject.Find("1p2pCamera").transform.localRotation = GameObject.Find("MainCamera").transform.localRotation;
            GameObject.Find("3p4pCamera").transform.localRotation = GameObject.Find("MainCamera").transform.localRotation;

            GameObject.Find("MainCamera").transform.localPosition = new Vector3(this.transform.localPosition.x + 1.3f,
                                                                                this.transform.localPosition.y + 0.92f,
                                                                                this.transform.localPosition.z);
            GameObject.Find("1p2pCamera").transform.localPosition = GameObject.Find("MainCamera").transform.localPosition;
            GameObject.Find("3p4pCamera").transform.localPosition = GameObject.Find("MainCamera").transform.localPosition;  

        }
	
	}

    //----------------------------------------------------------------------
    // 更新
    //----------------------------------------------------------------------
    // @Param   none			
    // @Return	none
    // @Date	2014/12/12  @Update T.Kawashita  @Author T.Kawashita      
    //----------------------------------------------------------------------
    void Update () {
	
	}
}
