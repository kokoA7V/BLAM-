using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    GameObject[] pattern;

    private AttackPattern attackPattern;
    private int patternNum = 0; // 現在動いているパターン

    private GameObject insPat;
    private GameObject nowObj;
    private GameObject beforeObj;
    void Start()
    {
        Instantiate(pattern[patternNum],transform);

        nowObj = gameObject.transform.GetChild(0).gameObject;

        attackPattern = nowObj.GetComponent<AttackPattern>();

    }

    void Update()
    {
        if(attackPattern.PatternEnd == true)
        {
            beforeObj = nowObj;

            patternNum++;


            // 登録されたパターンを1から順番に繰り返す
            if (pattern.Length <= patternNum)
            {
                patternNum = 0;
            }
            Instantiate(pattern[patternNum],transform);


            nowObj = gameObject.transform.GetChild(1).gameObject;

            attackPattern = nowObj.GetComponent<AttackPattern>();

            Destroy(beforeObj);
        }
    }
}
