using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace UI
{
    public class ButtomEffect : MonoBehaviour
    {
        private Button button;

        private EventTrigger eventTrigger;

        [SerializeField, Header("�J�[�\�����킹�����ɏo�Ă�����")]
        private GameObject bigImage;

        [SerializeField, Header("�|�[�Y�L�����o�X")]
        private GameObject pauseCanvas;

        private enum ButtonNum
        {
            StageSelect,
            Restart,
            Option,
            back,
        }

        [SerializeField,Header("�{�^���ԍ�")]
        ButtonNum buttonNum;
        void Start()
        {
            bigImage.SetActive(false);
            button = GetComponent<Button>();
            button.onClick.AddListener(ButtonClicked);

            // EventTrigger�ǉ�
            eventTrigger = GetComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerEnter;
            entry.callback.AddListener((data) => { OnPointerButton(); });
            eventTrigger.triggers.Add(entry);

            EventTrigger.Entry onPoiterExit = new EventTrigger.Entry();
            onPoiterExit.eventID = EventTriggerType.PointerExit;
            onPoiterExit.callback.AddListener((data) => { OnPointerExitButton(); });
            eventTrigger.triggers.Add(onPoiterExit);

        }

        private void ButtonClicked()
        {
            switch (buttonNum)
            {
                case ButtonNum.Restart:

                    break;
                case ButtonNum.StageSelect:

                    break;
                case ButtonNum.Option:
                    pauseCanvas.SetActive(false);
                    break;
                case ButtonNum.back:
                    pauseCanvas.SetActive(false);
                    break;
            }
            Debug.Log("�����ꂽ��");
        }
        private void OnPointerButton()
        {
            bigImage.SetActive(true);
        }

        private void OnPointerExitButton()
        {
            bigImage.SetActive(false);
        }

    }
}

