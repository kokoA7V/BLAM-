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

        [SerializeField, Header("カーソル合わせた時に出てくるやつ")]
        private GameObject bigImage;

        [SerializeField, Header("ポーズキャンバス")]
        private GameObject pauseCanvas;

        private enum ButtonNum
        {
            StageSelect,
            Restart,
            Option,
            back,
        }

        [SerializeField,Header("ボタン番号")]
        ButtonNum buttonNum;
        void Start()
        {
            bigImage.SetActive(false);
            button = GetComponent<Button>();
            button.onClick.AddListener(ButtonClicked);

            // EventTrigger追加
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
            Debug.Log("押されたよ");
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

