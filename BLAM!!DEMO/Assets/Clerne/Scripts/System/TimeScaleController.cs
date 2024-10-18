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
    /// ���Ԍo�ߑ��x�ݒ�(���x, �D��x)
    /// </summary>
    /// <param name="scale">���Ԍo�ߑ��x�F1�������A0�͒�~</param>
    /// <param name="prio">�D��x�F0���ŗD��</param>
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
            Debug.Log("�D��x���Ⴂ�̂Ŏ��s����܂���");
        }

    }

    public void TimeScaleReset()
    {
        Time.timeScale = 1;

        beforePriority = 10000;
    }
}
