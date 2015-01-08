using UnityEngine;
using System.Collections;

public class CGaugeMain : MonoBehaviour {

    public UISprite m_uiSprite;
    public int m_gaugePlayer;
    public int m_gaugeLevel;

    //----------------------------------------------------------------------
    // コンストラクタ
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2015/1/8  @Update 2015/1/8  @Author T.Kawashita      
    //----------------------------------------------------------------------
	void Start () {
        m_uiSprite = this.GetComponent<UISprite>();

        m_uiSprite.spriteName = "gage_0";
        m_gaugeLevel = 0;

        switch (this.name)
        {
            case "1PGaugeMain":
                m_gaugePlayer = 1;
                break;

            case "2PGaugeMain":
                m_gaugePlayer = 2;
                break;

            case "3PGaugeMain":
                m_gaugePlayer = 3;
                break;

            case "4PGaugeMain":
                m_gaugePlayer = 4;
                break;
        }

	}

    //----------------------------------------------------------------------
    // 更新
    //----------------------------------------------------------------------
    // @Param   none			
    // @Return	none
    // @Date	2015/1/8  @Update 2015/1/8  @Author T.Kawashita      
    //----------------------------------------------------------------------
	void Update () {

        float gauge = 0.0f;

        switch (m_gaugePlayer)
        {
            case 1:
                gauge = CGaugeManager.m_p1Gauge;
                break;

            case 2:
                gauge = CGaugeManager.m_p2Gauge;
                break;

            case 3:
                gauge = CGaugeManager.m_p3Gauge;
                break;

            case 4:
                gauge = CGaugeManager.m_p4Gauge;
                break;
        }

        // ゲージのレベルによって画像を変える
        int level = CGaugeManager.GetLevel(gauge);

        if (m_gaugeLevel != level)
        {
            if (level == 3)
                m_uiSprite.spriteName = "gage_3";
            else if (level == 2)
                m_uiSprite.spriteName = "gage_2";
            else if (level == 1)
                m_uiSprite.spriteName = "gage_1";
            else
                m_uiSprite.spriteName = "gage_0";

            m_gaugeLevel = level;
        }
    }
}
