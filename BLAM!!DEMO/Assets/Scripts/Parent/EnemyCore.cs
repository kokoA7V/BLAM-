using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyCore
{
    protected List<EnemyPattern> patternList = new List<EnemyPattern>();
    protected EnemyPattern nowPattern;

    protected bool isPatternActive = false;

    // エントリーポイント用
    public void CoreUpdate()
    {
        // パターン起動
        // 動いてない場合新しいパターンを適用
        if (isPatternActive)
        {
            ActivePattern(nowPattern);
        }
        else
        {
            nowPattern = PatternSelect();
            nowPattern.patternTimer = 0;
            isPatternActive = true;
        }
    }

    // パターン起動
    protected void ActivePattern(EnemyPattern _enemyPattern)
    {
        
        if (_enemyPattern.patternTimer == 0)
        {
            _enemyPattern.PatternStart();
            Debug.Log(_enemyPattern.patternTimer);
        }

        if (_enemyPattern.patternTimer == _enemyPattern.patternActiveTime - 1)
        {
            _enemyPattern.PatternEnd();
            isPatternActive = false;
            Debug.Log("endTime "+_enemyPattern.patternTimer);
            _enemyPattern.patternTimer = 0;
        }

        if (_enemyPattern.patternTimer >= 0 && _enemyPattern.patternTimer <= _enemyPattern.patternActiveTime - 1)
        {
            _enemyPattern.PatternUpdate();
            Debug.Log("CoreUpdate動いたよ");
        }
    }

    // パターン変更アルゴリズム
    // override前提
    protected abstract EnemyPattern PatternSelect();
}
