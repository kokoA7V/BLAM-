using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace UI
{
    public class OptionButtonSelect : MonoBehaviour
    {
        private Button button;

        private EventTrigger eventTrigger;

        [SerializeField, Header("カーソル合わせた時に出てくるやつ")]
        private GameObject bigImage;

        [SerializeField, Header("ポーズキャンバス")]
        private GameObject pauseCanvas;

        private enum OptionButtonNum
        {
            Master,
            BGM,
            SE,
        }

        [SerializeField, Header("ボタン番号")]
        OptionButtonNum buttonNum;
        void Start()
        {
            bigImage.SetActive(false);
            button = GetComponent<Button>();
            button.onClick.AddListener(ButtonClicked);

            //EventTrigger追加
            eventTrigger = GetComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerEnter;
            entry.callback.AddListener((data) => { OnPointerButton(); });
            eventTrigger.triggers.Add(entry);

            //EventTrigger.Entry onPoiterExit = new EventTrigger.Entry();
            //onPoiterExit.eventID = EventTriggerType.PointerExit;
            //onPoiterExit.callback.AddListener((data) => { OnPointerExitButton(); });
            //eventTrigger.triggers.Add(onPoiterExit);

        }

        private void ButtonClicked()
        {
            switch (buttonNum)
            {
                case OptionButtonNum.Master:
                    //bigImage.SetActive(true);
                    break;
                case OptionButtonNum.BGM:
                    //bigImage.SetActive(true);
                    break;
                case OptionButtonNum.SE:
                    //bigImage.SetActive(true);
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

