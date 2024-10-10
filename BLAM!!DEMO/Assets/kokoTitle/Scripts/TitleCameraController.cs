using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleCameraController : MonoBehaviour
{
    [SerializeField]
    List<GameObject> virtualCamera = new List<GameObject>();

    [SerializeField]
    TitleManager tm;

    [SerializeField]
    int cameraNum = 0;

    int nowNum = 0;

    private void Start()
    {
        foreach (var item in virtualCamera)
        {
            item.SetActive(false);
        }
        virtualCamera[cameraNum].SetActive(true);
    }

    private void Update()
    {
        if (cameraNum != nowNum)
        {
            virtualCamera[cameraNum].SetActive(true);
            virtualCamera[nowNum].SetActive(false);
            nowNum = cameraNum;
        }

        if (tm.GetSceneNum() == 0)
        {
            cameraNum = 0;
        }
        else if (tm.GetSceneNum() == 1)
        {
            cameraNum = tm.GetStageNum();
        }
        else if (tm.GetSceneNum() == 2)
        {
            // ƒIƒvƒVƒ‡ƒ“
        }

    }
}
