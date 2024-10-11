using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [SerializeField, Range(0, 2)]
    // 0=title, 1=select, 2=option
    int sceneMode = 0;

    //[SerializeField, Range(0, 2)]
    //// 0=start, 1=option, 2=exit
    //int titleMode = 0;

    [SerializeField, Range(0, 5)]
    // 0=exit, 1=1...
    int selectMode = 1;

    [SerializeField]
    string[] sceneName;

    [SerializeField]
    GameObject optionCanvas;

    private void Start()
    {
        BgmManager.Instance.Play("TitleBGM");
    }

    public void InputSelect(int value)
    {
        SeManager.Instance.Play("GunSound");
        switch(sceneMode)
        {
            //case 0:
            //    titleMode += value;
            //    if (titleMode > 2)
            //    {
            //        titleMode = 0;
            //    }
            //    else if(titleMode < 0)
            //    {
            //        titleMode = 2;
            //    }
            //    break;

            case 1:
                selectMode += value;
                if (selectMode > 5)
                {
                    selectMode = 1;
                }
                else if (selectMode < 1)
                {
                    selectMode = 5;
                }
                break;
        }
    }

    public void InputEnter(int value)
    {
        SeManager.Instance.Play("ShotSound");
        switch(sceneMode)
        {
            case 0:

                switch (value)
                {
                    case 0:
                        // StageSelect移行
                        sceneMode = 1;
                        break;

                    case 1:
                        // option行
                        Debug.Log("オプション開く");
                        sceneMode = 2;
                        optionCanvas.SetActive(true);
                        break;

                    case 2:
                        // ゲーム停止
                        Debug.Log("ゲーム終了");
#if UNITY_EDITOR
                        UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
#else
    Application.Quit();//ゲームプレイ終了
#endif
                        break;
                }

                break;

            case 1:

                switch(selectMode)
                {
                    case 0:
                        sceneMode = 0;
                        break;

                    case 1:
                        // シーン偏移１
                        Debug.Log("シーン１");
                        SceneManager.LoadScene(sceneName[0]);
                        break;

                    case 2:
                        // シーン偏移２
                        Debug.Log("シーン２");
                        SceneManager.LoadScene(sceneName[1]);
                        break;

                    case 3:
                        // シーン偏移３
                        Debug.Log("シーン３");
                        SceneManager.LoadScene(sceneName[2]);
                        break;

                    case 4:
                        // シーン偏移４
                        Debug.Log("シーン４");
                        SceneManager.LoadScene(sceneName[3]);
                        break;

                    case 5:
                        // シーン偏移５
                        Debug.Log("シーン５");
                        SceneManager.LoadScene(sceneName[4]);
                        break;
                }

                break;

            case 2:
                // オプション時にエンターはおさない
                break;
        }
    }

    public void PushModeChange(int value)
    {
        SeManager.Instance.Play("ShotSound");
        sceneMode = value;
    }


    public int GetSceneNum()
    {
        return sceneMode;
    }
    public int GetStageNum()
    {
        return selectMode;
    }
}
