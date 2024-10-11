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
                        // StageSelect�ڍs
                        sceneMode = 1;
                        break;

                    case 1:
                        // option�s
                        Debug.Log("�I�v�V�����J��");
                        sceneMode = 2;
                        optionCanvas.SetActive(true);
                        break;

                    case 2:
                        // �Q�[����~
                        Debug.Log("�Q�[���I��");
#if UNITY_EDITOR
                        UnityEditor.EditorApplication.isPlaying = false;//�Q�[���v���C�I��
#else
    Application.Quit();//�Q�[���v���C�I��
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
                        // �V�[���ΈڂP
                        Debug.Log("�V�[���P");
                        SceneManager.LoadScene(sceneName[0]);
                        break;

                    case 2:
                        // �V�[���ΈڂQ
                        Debug.Log("�V�[���Q");
                        SceneManager.LoadScene(sceneName[1]);
                        break;

                    case 3:
                        // �V�[���ΈڂR
                        Debug.Log("�V�[���R");
                        SceneManager.LoadScene(sceneName[2]);
                        break;

                    case 4:
                        // �V�[���ΈڂS
                        Debug.Log("�V�[���S");
                        SceneManager.LoadScene(sceneName[3]);
                        break;

                    case 5:
                        // �V�[���ΈڂT
                        Debug.Log("�V�[���T");
                        SceneManager.LoadScene(sceneName[4]);
                        break;
                }

                break;

            case 2:
                // �I�v�V�������ɃG���^�[�͂����Ȃ�
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
