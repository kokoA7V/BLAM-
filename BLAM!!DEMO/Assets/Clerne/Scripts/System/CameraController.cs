using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] cam;


    public void CameraChange(int Num)
    {
        Reset();
        cam[Num].SetActive(true);
    }

    private void Reset()
    {
        for(int i = 0; i < cam.Length; ++i)
        {
            cam[i].SetActive(false);
        }
    }
}
