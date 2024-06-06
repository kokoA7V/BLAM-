using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    EnemyCore enemyCore = new GovernmentCore();

    void Update()
    {
        enemyCore.CoreUpdate();
    }
}
