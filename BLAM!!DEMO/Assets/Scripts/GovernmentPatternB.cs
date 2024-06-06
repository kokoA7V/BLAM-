using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GovernmentPatternB : EnemyPattern
{
    public GovernmentPatternB()
    {
        // attackList.Add(new GovernmentAttack1());
        patternActiveTime = 600;
    }

    public override void PatternStart()
    {
        Debug.Log("startB");
    }

    public override void PatternEnd()
    {
        Debug.Log("EndB");
    }

    public override void PatternUpdate()
    {
        Debug.Log("UpdateB");
        ActiveAttack();
    }
}
