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

    [SerializeField,Header("�{�^���̎��")]
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
                // �V�[���ēǂݍ���
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                break;
            case ButtonNum.StageSelect:
                // �X�e�[�W�Z���N�g
                SceneManager.LoadScene("TitleScene");
                break;
            case ButtonNum.EXIT:
                // �Q�[���I��
                break;
        }
        Debug.Log("�����ꂽ��");
    }
}
