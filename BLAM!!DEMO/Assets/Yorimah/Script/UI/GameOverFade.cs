using UnityEngine;
using UnityEngine.UI;

public class GameOverFade : MonoBehaviour
{
    private Image fadePanel;

    [SerializeField, Header("Text‚Ü‚Æ‚ß")]
    private GameObject textBox;
    // Start is called before the first frame update
    void Start()
    {
        fadePanel = this.gameObject.GetComponent<Image>();
        textBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (fadePanel.color.a < 0.5f)
        {
            fadePanel.color += new Color(0, 0, 0, 0.01f);
        }
        else
        {
            textBox.SetActive(true);
        }
    }
}
