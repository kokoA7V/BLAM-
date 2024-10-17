using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaleController : MonoBehaviour
{
    uint nowPriority = 0;
    uint beforePriority = 10000;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 時間経過速度設定(速度, 優先度)
    /// </summary>
    /// <param name="scale">時間経過速度：1が等速、0は停止</param>
    /// <param name="prio">優先度：0が最優先</param>
    public void TimeScaleSet(float scale ,uint prio)
    {
        nowPriority = prio;

        if(nowPriority <= beforePriority)
        {
            Time.timeScale = scale;
            nowPriority = beforePriority;
        }
        else
        {
            Debug.Log("優先度が低いので実行されません");
        }

    }

    public void TimeScaleReset()
    {
        Time.timeScale = 1;

        beforePriority = 10000;
    }
}
