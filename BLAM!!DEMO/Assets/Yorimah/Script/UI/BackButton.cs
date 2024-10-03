using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    private Button button;
    [SerializeField, Header("カーソル合わせた時に出てくるやつ")]
    private GameObject bigImage;

    private EventTrigger eventTrigger;
    // Start is called before the first frame update
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
        transform.parent.gameObject.SetActive(false);
        if (transform.parent.gameObject.name== "PauseCanvas")
        {
            Time.timeScale = 1;
        }
        OnPointerExitButton();
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
