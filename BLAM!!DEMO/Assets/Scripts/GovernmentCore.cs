using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GovernmentCore : EnemyCore
{
    public GovernmentCore()
    {
        patternList.Add(new GovernmentPatternA());
        patternList.Add(new GovernmentPatternB());
    }

    protected override EnemyPattern PatternSelect()
    {
        EnemyPattern ep = null;

        //if (nowPattern == patternList[0]) { ep = patternList[1]; }
        //else { ep = patternList[0]; }

        ep = patternList[0];

        return ep;
    }

}
