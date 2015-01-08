using UnityEngine;
using System.Collections;

public class CGauge : MonoBehaviour {

    public Vector3 m_scale;
    public int m_gaugePlayer;
    public int m_gaugeLevel;

    public UISprite m_sprite;

    //----------------------------------------------------------------------
    // コンストラクタ
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2015/1/9  @Update 2015/1/9  @Author T.Kawashita      
    //----------------------------------------------------------------------
	void Start () {
        this.transform.localScale = new Vector3(0.0f, 20.0f, 0.0f);
        m_sprite = this.transform.GetComponent<UISprite>();
        m_sprite.spriteName = "gage_00";
        m_scale = this.transform.localScale;

        m_gaugeLevel = 0;

        switch (this.name)
        {
            case "1PGauge":
                m_gaugePlayer = 1;
                break;

            case "2PGauge":
                m_gaugePlayer = 2;
                break;

            case "3PGauge":
                m_gaugePlayer = 3;
                break;

            case "4PGauge":
                m_gaugePlayer = 4;
                break;
        }
    }

    //----------------------------------------------------------------------
    // 更新
    //----------------------------------------------------------------------
    // @Param   none			
    // @Return	none
    // @Date	2015/1/9  @Update 2015/1/9  @Author T.Kawashita      
    //----------------------------------------------------------------------
	void Update () {

        float gauge = 0.0f, upScale = 0.0f;

        // 現在のゲージ取得
        switch (m_gaugePlayer)
        {
            case 1: gauge = CGaugeManager.m_p1Gauge; break;
            case 2:gauge = CGaugeManager.m_p2Gauge; break;
            case 3:gauge = CGaugeManager.m_p3Gauge; break;
            case 4:gauge = CGaugeManager.m_p4Gauge; break;
        }

        // ゲージのレベルによって画像を変える
        int level = CGaugeManager.GetLevel(gauge);

        // 前回までのレベルと違った場合
        if (level != m_gaugeLevel)
        {
            m_gaugeLevel = level;
            if (m_gaugeLevel == 0)
                m_sprite.spriteName = "gage_00";
            else if (m_gaugeLevel == 1)
                m_sprite.spriteName = "gage_01";
            else if (m_gaugeLevel == 2)
                m_sprite.spriteName = "gage_02";
            else if (m_gaugeLevel == 3)
            {
                m_sprite.spriteName = "gage_03";
            }
        }

        if (m_gaugeLevel == 0)
            upScale = CGaugeManager.m_level1GaugeScaleUp;
        
        else if (m_gaugeLevel == 1)
        {
            upScale = CGaugeManager.m_level2GaugeScaleUp;
            gauge -= CGaugeManager.m_levelBorder1;
        }
        else if (m_gaugeLevel == 2)
        {
            upScale = CGaugeManager.m_level3GaugeScaleUp;
            gauge -=CGaugeManager.m_levelBorder2;
        }
        else if (m_gaugeLevel == 3)
        {
            upScale = 1;
            gauge = 160.0f;
        }

        this.transform.localScale = new Vector3(upScale * gauge, 20.0f, 0.0f);
        
	}
}
