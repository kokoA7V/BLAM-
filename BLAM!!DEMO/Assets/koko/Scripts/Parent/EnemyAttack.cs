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

    public void CannotDodge()
    {
        if (Input.GetKeyDown(KeyCode.D) && !dodged)
        {
            dodged = true;
            Debug.Log("dodgeを失敗したよ！");

        }
    }
    public void CannotGuard()
    {
        if (Input.GetKeyDown(KeyCode.G) && !guarded)
        {
            guarded = true;
            Debug.Log("guardを失敗したよ！");
        }

    }
    public void CanDodge()
    {
        Debug.Log("dodgeできるよ！");
        if (Input.GetKeyDown(KeyCode.D) && !dodged)
        {
            dodgeSuccess = true;
            Debug.Log("dodgeしたよ！");
        }
    }

    public void CanGuard()
    {
        Debug.Log("guardできるよ！");
        if (Input.GetKeyDown(KeyCode.G) && !guarded)
        {
            guardSuccess = true;
            Debug.Log("guardしたよ！");
        }
    }

    protected virtual void AddDamage()
    {
        if (dodgeSuccess)
        {
            enemyHealth.nowHitPoint -= hitPointDamage;
            SeManager.Instance.Play("damage6");

            dodgeSuccess = false;

        }
        else if (guardSuccess)
        {
            enemyHealth.nowHitPoint -= hitPointDamage;
            SeManager.Instance.Play("damage6");

            guardSuccess = false;
        }
        else
        {
            playerHealth.nowHitPoint -= hitPointDamage;
            SeManager.Instance.Play("damage7");

        }
        dodged = false;
        guarded = false;

    }
}