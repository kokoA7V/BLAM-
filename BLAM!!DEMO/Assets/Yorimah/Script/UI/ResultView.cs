using System.Collections.Generic;
using UnityEngine;

public class ResultView : MonoBehaviour
{
    [SerializeField]
    List<GameObject> textBox;

    private float timer = 0;

    private int textNum;
    void Start()
    {
        textNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 2)
        {
            if (textBox.Count <= textNum)
            {
                return;
            }
            textBox[textNum].SetActive(true);
            textNum++;
            timer = 0;
        }

    }
}
