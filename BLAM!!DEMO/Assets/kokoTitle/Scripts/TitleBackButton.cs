using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleBackButton : MonoBehaviour
{
    [SerializeField]
    GameObject backButton;
    private Button button;

    [SerializeField]
    TitleManager tm;

    private void Start()
    {

        button = backButton.GetComponent<Button>();
        button.onClick.AddListener(BackTitle);
    }

    public void BackTitle()
    {
        tm.PushModeChange(0);
    }
}
