using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterEffect : MonoBehaviour
{
    [SerializeField]
    GameObject enemy;

    [SerializeField]
    Player player;

    [SerializeField]
    AttackPattern attackPattern;

    [SerializeField]
    GameObject nowObj;

    private void Start()
    {
        Time.timeScale = 1f;
    }

    private void Update()
    {
        nowObj = enemy.transform.GetChild(5).gameObject; // Ÿ‚Ìƒpƒ^[ƒ“‚ğæ“¾
        attackPattern = nowObj.GetComponent<AttackPattern>();

        Debug.Log(attackPattern.CanCounter);

        if (attackPattern.CanCounter)
        {
            Time.timeScale = 0.2f;
            Debug.Log("Slow!!!");
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
