using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleUICanvasManager : MonoBehaviour
{
    [SerializeField]
    GameObject titleUI;

    [SerializeField]
    GameObject selectUI;

    [SerializeField]
    TitleManager tm;

    [SerializeField]
    GameObject StageText;
    Text st;

    [SerializeField]
    GameObject BLAM;
    bool isBlam = true;
    bool nowBlam = false;

    int nowSceneNum = 1;
    int nowStageNum = 0;

    float speed = 0;

    private void Start()
    {
        st = StageText.GetComponent<Text>();
    }

    void Update()
    {
        if (tm.GetStageNum() != nowStageNum)
        {
            switch(tm.GetStageNum())
            {
                case 0:
                    break;
                case 1:
                    st.text = "Stage1";
                    break;
                case 2:
                    st.text = "Stage2";
                    break;
                case 3:
                    st.text = "Stage3";
                    break;
                case 4:
                    st.text = "Stage4";
                    break;
                case 5:
                    st.text = "Stage5";
                    break;
            }

            nowStageNum = tm.GetStageNum();
        }

        if (tm.GetSceneNum() != nowSceneNum)
        {
            if (tm.GetSceneNum() == 0)
            {
                titleUI.SetActive(true);
                selectUI.SetActive(false);
                isBlam = true;
            }
            else if (tm.GetSceneNum() == 1)
            {
                titleUI.SetActive(false);
                selectUI.SetActive(true);
                isBlam = false;
            }
            else if (tm.GetSceneNum() == 2)
            {
                titleUI.SetActive(false);
                selectUI.SetActive(false);
                isBlam = false;
            }

            nowSceneNum = tm.GetSceneNum();
        }

        if (isBlam != nowBlam)
        {
            if (isBlam)
            {
                speed = -2;
                Vector3 posy = BLAM.transform.position;
                posy.y = 44.5f;
                BLAM.transform.position = posy;
            }
            else
            {
                speed = 2;
                Vector3 posy = BLAM.transform.position;
                posy.y = 4.5f;
                BLAM.transform.position = posy;
            }
            nowBlam = isBlam;
        }

        Vector3 pos = BLAM.transform.position;
        pos.y += speed;
        BLAM.transform.position = pos;
        speed *= 0.95f;
    }
}
