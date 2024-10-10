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

    private Animator anim;

    AnimatorStateInfo animState;
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

        anim = transform.parent.gameObject.GetComponent<Animator>();
        
    }

    private void ButtonClicked()
    {

        if (transform.parent.gameObject.name == "PauseCanvas")
        {
            anim.Play("ExitPauseAnimation");
        }
        if (transform.parent.gameObject.name == "OptionCanvas")
        {
            anim.Play("ExitOptionAnimation");
        }

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
