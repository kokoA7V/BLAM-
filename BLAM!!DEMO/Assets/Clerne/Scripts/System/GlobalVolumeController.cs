using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVolumeController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] gVol;


    public void GlobalVolumeChange(int Num)
    {
        Reset();
        gVol[Num].SetActive(true);
    }

    private void Reset()
    {
        for (int i = 0; i < gVol.Length; ++i)
        {
            gVol[i].SetActive(false);
        }
    }
}