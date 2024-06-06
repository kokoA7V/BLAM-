using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerHealth
{
    public int maxHitPoint { get; set; }
    public int nowHitPoint { get; set; }

    public int maxStamina { get; set; }
    public int nowStamina { get; set; }
}