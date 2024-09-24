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
    public int dodgeJustTime;
    public int guardTime;
    public int guardJustTime;

    protected bool dodgeSuccess;
    protected bool dodgeJustSuccess;
    protected bool guardSuccess;
    protected bool guardJustSuccess;

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
            Debug.Log("dodge�����s������I");

        }
    }
    public void CannotGuard()
    {
        if (Input.GetKeyDown(KeyCode.G) && !guarded)
        {
            guarded = true;
            Debug.Log("guard�����s������I");
        }

    }
    public void CanDodge()
    {
        Debug.Log("dodge�ł����I");
        if (Input.GetKeyDown(KeyCode.D) && !dodged)
        {
            dodgeSuccess = true;
            Debug.Log("dodge������I");
        }
    }

    public void CanJustDodge()
    {
        Debug.Log("justdodge�ł����I");
        if (Input.GetKeyDown(KeyCode.D) && !dodged)
        {
            dodgeJustSuccess = true;
            Debug.Log("justdodge������I");
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

    public void CanJustGuard()
    {
        Debug.Log("justguard�ł����I");
        if (Input.GetKeyDown(KeyCode.G) && !guarded)
        {
            guardJustSuccess = true;
            Debug.Log("justguard������I");
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
        else if (dodgeJustSuccess)
        {
            enemyHealth.nowHitPoint -= hitPointDamage * 2;
            SeManager.Instance.Play("damage6");

            dodgeJustSuccess = false;

        }
        else if (guardSuccess)
        {
            enemyHealth.nowHitPoint -= hitPointDamage;
            SeManager.Instance.Play("damage6");

            guardSuccess = false;
        }
        else if (guardJustSuccess)
        {
            enemyHealth.nowHitPoint -= hitPointDamage * 2;
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