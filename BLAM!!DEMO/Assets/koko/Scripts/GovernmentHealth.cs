using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GovernmentHealth : IEnemyHealth
{
    public int maxHitPoint { get; set; } = 100;
    public int nowHitPoint { get; set; } = 100;
}
