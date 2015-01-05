using UnityEngine;
using System.Collections;

public class CPlayerGauge : MonoBehaviour{

    public enum eGAUGESTATUS
    { 
        eNOTGAME,           // ゲーム中以外の状態
        eNORMAL,            // ゲーム中のデフォルト状態
        eLEVEL1DECREMENT,   // レベル1の解放状態
        eLEVEL2DECREMENT,   // レベル2の解放状態
        eLEVEL3DECREMENT,   // レベル3の解放状態
    };

    public eGAUGESTATUS m_status;   // ゲージのステータス
    public float m_gauge;           // ゲージ量
    public int m_teamNo;            // チームNo

    //----------------------------------------------------------------------
    // コンストラクタ
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/12/31  @Update 2014/12/31  @Author T.Kawashita      
    //----------------------------------------------------------------------
	void Start () {

        // 最初はゲームスタートしていない状態にしておく
        m_status = eGAUGESTATUS.eNOTGAME;

        // ゲージ量を初期値に変更
        m_gauge = CGaugeManager.m_initValue;

	}

    //----------------------------------------------------------------------
    // 更新
    //----------------------------------------------------------------------
    // @Param   none			
    // @Return	none
    // @Date	2014/12/31  @Update 2014/12/31  @Author T.Kawashita      
    //----------------------------------------------------------------------
	void Update () {

        switch (m_status)
        {
            // ゲームプレイ中のデフォルト状態
            case eGAUGESTATUS.eNORMAL:
                m_gauge = CGaugeManager.GetGaugeRate(ref m_gauge, m_teamNo);
                break;
            
            // レベル1ゲージ解放状態
            case eGAUGESTATUS.eLEVEL1DECREMENT:
                // ゲージ減少が終わったら通常状態に戻す
                if (CGaugeManager.CalcLevel1Rate(ref m_gauge) == true)
                    m_status = eGAUGESTATUS.eNORMAL;
                break;

            // レベル2ゲージ解放状態
            case eGAUGESTATUS.eLEVEL2DECREMENT:
                // ゲージ減少が終わったら通常状態に戻す
                if (CGaugeManager.CalcLevel2Rate(ref m_gauge) == true)
                    m_status = eGAUGESTATUS.eNORMAL;
                break;

            // レベル3ゲージ解放状態
            case eGAUGESTATUS.eLEVEL3DECREMENT:
                // ゲージ減少が終わったら通常状態に戻す
                if (CGaugeManager.CalcLevel3Rate(ref m_gauge) == true)
                    m_status = eGAUGESTATUS.eNORMAL;
                break;

            // ゲームが始まっていないときの状態
            case eGAUGESTATUS.eNOTGAME:
                // 特に何もしない
                break;
           
        }



	}

    //----------------------------------------------------------------------
    // ゲージの解放処理
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	int         ゲージ解放するレベル
    // @Date	2015/1/3  @Update 2014/1/3  @Author T.Kawashita      
    //----------------------------------------------------------------------
    public int GaugeDecrement()
    {
        // レベル3
        if (m_gauge >= CGaugeManager.m_decrementValue3)
        {
            m_status = eGAUGESTATUS.eLEVEL3DECREMENT;
            return 3;
        }
        // レベル2
        else if (m_gauge >= CGaugeManager.m_decrementValue2)
        {
            m_status = eGAUGESTATUS.eLEVEL2DECREMENT;
            return 2;
        }
        // レベル1
        else if (m_gauge >= CGaugeManager.m_decrementValue1)
        {
            m_status = eGAUGESTATUS.eLEVEL1DECREMENT;
            return 1;
        }

        // 解放できない
        return 0;
    }
}
