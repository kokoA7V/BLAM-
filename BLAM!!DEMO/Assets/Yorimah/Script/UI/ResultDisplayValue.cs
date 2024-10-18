using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultDisplayValue : MonoBehaviour
{
    [SerializeField, Header("DisplayName")]
    private string displayName;

    [SerializeField, Header("“n‚µ‚½‚¢’l")]
    private GameObject scoreObject;

    private int scoreValue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreValue++;
        this.gameObject.GetComponent<Text>().text = displayName + " : " + scoreValue;
    }
}
