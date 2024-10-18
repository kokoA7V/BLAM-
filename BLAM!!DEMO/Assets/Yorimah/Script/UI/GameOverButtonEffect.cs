using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameOverButtonEffect : MonoBehaviour
{

    private enum ButtonNum
    {
        StageSelect,
        Restart,
        EXIT,
    }

    [SerializeField,Header("ボタンの種類")]
    private ButtonNum buttonNum;

    private Button button;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ButtonClicked);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ButtonClicked()
    {
        switch (buttonNum)
        {
            case ButtonNum.Restart:
                // シーン再読み込み
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                break;
            case ButtonNum.StageSelect:
                // ステージセレクト
                SceneManager.LoadScene("TitleScene");
                break;
            case ButtonNum.EXIT:
                // ゲーム終了
                break;
        }
        Debug.Log("押されたよ");
    }
}
