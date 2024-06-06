using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthText : MonoBehaviour
{
    IPlayerHealth playerHealth;
    Text text;

    void Start()
    {
        playerHealth = Locator.Resolve<IPlayerHealth>();

        text = GetComponent<Text>();
    }

    void Update()
    {
        text.text = "player : " + playerHealth.nowHitPoint ;
    }
}
