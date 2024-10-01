using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("ステータス")]
    [SerializeField]
    int maxHp;

    [SerializeField]
    bool debugMode;

    [SerializeField]
    GameObject[] pattern;   // 攻撃パターン


    private int _hp;

    public int Hp
    {
        get { return _hp; }

        set { _hp = value; }
    }

    private AttackPattern attackPattern;
    private int patternNum = 0; // 現在動いているパターン

    private GameObject nowObj;
    private GameObject beforeObj;


    void Start()
    {
        _hp = maxHp;

        Instantiate(pattern[patternNum],transform);    // 子にパターンのプレハブを生成

        nowObj = gameObject.transform.GetChild(0).gameObject;   // 子のオブジェクトを取得

        attackPattern = nowObj.GetComponent<AttackPattern>();   // 取得したオブジェクトのパターンスクリプトを取得

        attackPattern.DebugMode = debugMode;                    // デバッグモードを有効化

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

            attackPattern.DebugMode = debugMode;                    // デバッグモードを有効化

            Destroy(beforeObj);                                   // 前のパターンを破棄
        }

    }

}
