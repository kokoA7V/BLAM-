using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAttack
{
    protected int hitPointDamage;
    protected int staminaDamege;
    protected int attackType;

    public int attackStartTime;
    public int attackActiveTime;

    public int dodgeTime;
    public int guardTime;

    protected bool dodgeSuccess;
    protected bool guardSuccess;

    protected bool dodged;
    protected bool guarded;

    public IEnemyHealth enemyHealth;
    public IPlayerHealth playerHealth;

    public abstract void AttackStart();
    public abstract void AttackEnd();
    public abstract void AttackUpdate();

    public void CanDodge()
    {
        Debug.Log("dodge�ł����I");
        if (Input.GetKeyDown(KeyCode.D) && !dodged)
        {
            dodgeSuccess = true;
            Debug.Log("dodge������I");
        }
    }

    public void CanGuard()
    {
        Debug.Log("guard�ł����I");
        if (Input.GetKeyDown(KeyCode.G) && !guarded)
        {
            guardSuccess = true;
            Debug.Log("guard������I");
        }
    }

    protected virtual void AddDamage()
    {
        if (dodged)
        {
            enemyHealth.nowHitPoint -= hitPointDamage;
        }
        else if (guarded)
        {
            enemyHealth.nowHitPoint -= hitPointDamage;
        }
        else
        {
            playerHealth.nowHitPoint -= hitPointDamage;
        }

        dodged = false;
        guarded = false;
    }
}