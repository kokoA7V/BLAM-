using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    GameObject[] pattern;   // 攻撃パターン

    private AttackPattern attackPattern;
    private int patternNum = 0; // 現在動いているパターン

    private GameObject nowObj;
    private GameObject beforeObj;
    void Start()
    {
        Instantiate(pattern[patternNum],transform);    // 子にパターンのプレハブを生成

        nowObj = gameObject.transform.GetChild(0).gameObject;   // 子のオブジェクトを取得

        attackPattern = nowObj.GetComponent<AttackPattern>();   // 取得したオブジェクトのパターンスクリプトを取得

    }

    void Update()
    {
        if(attackPattern.PatternEnd == true)
        { // パターンが終わったら
            beforeObj = nowObj; // 終わったパターンオブジェクトを過去のオブジェクトとする

            patternNum++;   // パターン番号を進める


            // 登録されたパターンを1から順番に繰り返す
            if (pattern.Length <= patternNum)
            {
                patternNum = 0;
            }
            Instantiate(pattern[patternNum],transform); // 次のパターンを生成


            nowObj = gameObject.transform.GetChild(1).gameObject; // 次のパターンを取得

            attackPattern = nowObj.GetComponent<AttackPattern>(); // パターンのスクリプトを取得

            Destroy(beforeObj);                                   // 前のパターンを破棄
        }
    }
}
