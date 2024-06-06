using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyPattern
{
    protected List<EnemyAttack> attackList = new List<EnemyAttack>();

    public int patternActiveTime;
    public int patternTimer;

    public abstract void PatternStart();

    public abstract void PatternEnd();

    public abstract void PatternUpdate();

    // ���X�g���̃A�^�b�N��S�ĉғ��AUpdate�ɓ���邱�Ƒz��
    protected void ActiveAttack()
    {
        patternTimer++;

        foreach (var item in attackList)
        {
            if (patternTimer == item.attackStartTime)
            {
                item.AttackStart();
            }

            if (patternTimer == item.attackActiveTime + item.attackStartTime - 1)
            {
                item.AttackEnd();
            }

            int dodgeStartTime = item.attackStartTime + item.attackActiveTime - item.dodgeTime;
            int guardStartTime = item.attackStartTime + item.attackActiveTime - item.guardTime;

            if (patternTimer >= item.attackStartTime && patternTimer <= item.attackActiveTime + item.attackStartTime - 1)
            {
                item.AttackUpdate();

                if (patternTimer >= dodgeStartTime)
                {
                    item.CanDodge();
                }
                else
                {
                    item.CannotDodge();
                }

                if (patternTimer >= guardStartTime)
                {
                    item.CanGuard();
                }
                else
                {
                    item.CannotGuard();
                }

                // ���o�����
                if (patternTimer == dodgeStartTime)
                {
                    SeManager.Instance.Play("���1");
                }

                if (patternTimer == guardStartTime)
                {
                    SeManager.Instance.Play("���1");
                }
            }

        }
    }
}