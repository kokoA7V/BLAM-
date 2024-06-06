using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GovernmentAttackA : EnemyAttack
{
    public GovernmentAttackA()
    {
        hitPointDamage = 10;

        attackStartTime = 100;
        attackActiveTime = 200;

        dodgeTime = 100;
        guardTime = 100;
    }

    public override void AttackStart()
    {
        playerHealth = Locator.Resolve<IPlayerHealth>();
        enemyHealth = Locator.Resolve<IEnemyHealth>();

        Debug.Log("attackStart");
    }

    public override void AttackEnd()
    {
        Debug.Log("attackEnd");
        AddDamage();
    }

    public override void AttackUpdate()
    {
        Debug.Log("attackUpdate");
    }
}
