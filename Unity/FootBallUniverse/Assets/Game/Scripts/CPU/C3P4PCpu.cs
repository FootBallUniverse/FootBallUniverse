﻿using UnityEngine;
using System.Collections;

public class C3P4PCpu : MonoBehaviour {

    //----------------------------------------------------------------------
    // コンストラクタ
    //----------------------------------------------------------------------
    // @Param   none			
    // @Return	none
    // @Date	2014/12/1  @Update 2014/12/1  @Author T.Kawashita      
    //----------------------------------------------------------------------
    void Start()
    {

    }


    //----------------------------------------------------------------------
    // 更新
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/12/1  @Update 2014/12/1  @Author T.Kawashita      
    //----------------------------------------------------------------------
    void Update()
    {

    }

    //----------------------------------------------------------------------
    // フレーム最後の更新
    //----------------------------------------------------------------------
    // @Param	none		
    // @Return	none
    // @Date	2014/12/1  @Update 2014/12/1  @Author T.Kawashita      
    //----------------------------------------------------------------------
    void LateUpdate()
    {
        CCpuManager.m_cpuManager.m_cpuP3P4 = this.transform;
    }
}
