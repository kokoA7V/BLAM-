using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("ステータス")]
    [SerializeField]
    float maxHp;

    [SerializeField]
    bool debugMode;

    [SerializeField]
    GameObject[] firstPattern;  // 第一形態攻撃パターン

    [SerializeField]
    GameObject[] secondPattern; // 第二形態攻撃パターン
    
    private float _hp;

    private GameObject[] nowPattern;

    private bool patternChange;

    public float Hp
    {
        get { return _hp; }

        set { _hp = value; }
    }

    public bool PatternChange
    {
        get { return patternChange; }

        set { patternChange = value; }
    }

    private AttackPattern attackPattern;
    private int patternNum = 0; // 現在動いているパターン
    private bool isSecondPattern = false;

    private GameObject nowObj;
    private GameObject beforeObj;


    void Start()
    {
        _hp = maxHp;

        nowPattern = firstPattern;

        Instantiate(nowPattern[patternNum],transform);    // 子にパターンのプレハブを生成

        nowObj = gameObject.transform.GetChild(0).gameObject;   // 子のオブジェクトを取得

        attackPattern = nowObj.GetComponent<AttackPattern>();   // 取得したオブジェクトのパターンスクリプトを取得

        attackPattern.DebugMode = debugMode;                    // デバッグモードを有効化

    }

    void Update()
    {
        // HPが半分を切ったら
        if (_hp / maxHp < 0.5f && isSecondPattern == false)
        {
            nowPattern = secondPattern;

            patternChange = true;

            isSecondPattern = true;

        }


        if (attackPattern.PatternEnd == true)
        { // パターンが終わったら




            beforeObj = nowObj; // 終わったパターンオブジェクトを過去のオブジェクトとする

            patternNum++;   // パターン番号を進める


            // 登録されたパターンを1から順番に繰り返す
            if (nowPattern.Length <= patternNum || patternChange == true)
            {
                patternNum = 0;

                patternChange = false;
            }
            Instantiate(nowPattern[patternNum],transform); // 次のパターンを生成


            nowObj = gameObject.transform.GetChild(1).gameObject; // 次のパターンを取得

            attackPattern = nowObj.GetComponent<AttackPattern>(); // パターンのスクリプトを取得

            attackPattern.DebugMode = debugMode;                    // デバッグモードを有効化

            Destroy(beforeObj);                                   // 前のパターンを破棄
        }


    }

}
