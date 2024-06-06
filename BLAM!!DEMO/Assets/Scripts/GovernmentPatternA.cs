using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GovernmentPatternA : EnemyPattern
{
    public GovernmentPatternA()
    {
        attackList.Add(new GovernmentAttackA());
        patternActiveTime = 600;
    }

    public override void PatternStart()
    {
        Debug.Log("startA");
    }

    public override void PatternEnd()
    {
        Debug.Log("EndA");
    }

    public override void PatternUpdate()
    {
        Debug.Log("UpdateA");
        ActiveAttack();
    }
}
