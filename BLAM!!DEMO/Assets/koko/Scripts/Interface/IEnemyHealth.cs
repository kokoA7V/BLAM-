using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyHealth
{
    public int maxHitPoint { get; set; }
    public int nowHitPoint { get; set; }
}