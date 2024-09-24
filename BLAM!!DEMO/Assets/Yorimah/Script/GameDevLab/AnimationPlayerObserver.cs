using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlayerObserver : MonoBehaviour, IObserver<int>
{
    [SerializeField]
    Animator enemyAnimator;
    private string m_name;
    public AnimationPlayerObserver(string name)
    {
        m_name = name;
    }
    public void OnCompleted()
    {
        Debug.Log($"通知の受け取りが完了しました");
    }

    public void OnError(Exception error)
    {
        Debug.Log($"次のエラーを受信しました:{error.Message}");
    }

    public void OnNext(int value)
    {
        switch (value)
        {
            case 0:

                break;
            case 1:
                enemyAnimator.Play("VRSuya_Loli_Kami_Requiem");
                Debug.Log("VRSuya_Loli_Kami_Requiem");
                break;
            default:
                break;
        }
        Debug.Log($"{value}を受け取りました");
    }
}
