using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthText : MonoBehaviour
{
    IEnemyHealth enemyHealth;
    Text text;

    void Start()
    {
        enemyHealth = Locator.Resolve<IEnemyHealth>();

        text = GetComponent<Text>();
    }

    void Update()
    {
        text.text = "enemy : " + enemyHealth.nowHitPoint;
    }
}
