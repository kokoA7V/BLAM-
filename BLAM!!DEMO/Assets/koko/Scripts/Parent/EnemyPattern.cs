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

    // リスト内のアタックを全て稼働、Updateに入れること想定
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

                // 音出すやつ
                if (patternTimer == dodgeStartTime)
                {
                    SeManager.Instance.Play("薬莢1");
                }

                if (patternTimer == guardStartTime)
                {
                    SeManager.Instance.Play("薬莢1");
                }
            }

        }
    }
}