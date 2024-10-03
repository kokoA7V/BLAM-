using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ExitPointer : MonoBehaviour
{

    private EventTrigger eventTrigger;
    // Start is called before the first frame update
    void Start()
    {
        eventTrigger = GetComponent<EventTrigger>();
        EventTrigger.Entry onPoiterExit = new EventTrigger.Entry();
        onPoiterExit.eventID = EventTriggerType.PointerExit;
        onPoiterExit.callback.AddListener((data) => { OnPointerExitButton(); });
        eventTrigger.triggers.Add(onPoiterExit);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnPointerExitButton()
    {
        Debug.Log("ƒ|ƒCƒ“ƒ^‚Í‚¸‚ê‚½‚æ");
        this.gameObject.SetActive(false);
    }
}
