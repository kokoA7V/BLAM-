using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiberatorHealth : IPlayerHealth
{
    public int maxHitPoint { get; set; } = 100;
    public int nowHitPoint { get; set; } = 100;

    public int maxStamina { get; set; } = 100;
    public int nowStamina { get; set; } = 100;
}
