using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinFade : MonoBehaviour
{
    private bool animationEnd;

    [SerializeField, Header("ScoreText")]
    private GameObject scoreTextObject;
    // Start is called before the first frame update
    void Start()
    {
        animationEnd = false;
        scoreTextObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (animationEnd)
        {
            this.GetComponent<Text>().color -= new Color(0, 0, 0, 0.03f);
            if (this.GetComponent<Text>().color.a<0)
            {
                scoreTextObject.SetActive(true);
            }
        }        
    }

    public void WinAnimationEnd()
    {
        animationEnd = true;        
    }
}
