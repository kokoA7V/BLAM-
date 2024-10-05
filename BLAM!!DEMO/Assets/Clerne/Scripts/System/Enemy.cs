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

    // アニメーション関連

    Animator anim;

    private bool _animAttack1;
    private bool _animAttack2;

    // アニメディクショナリー追加
    // 登録用リスト、パターンから追加
    public List<string> animList = new List<string>();
    // 再生用のディクショナリー
    public　Dictionary<string, bool> animDic = new Dictionary<string, bool>();



    public bool AnimAttack1
    {
        set { _animAttack1 = value; }
    }
    public bool AnimAttack2
    {
        set { _animAttack2 = value; }
    }



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

        anim = gameObject.GetComponent<Animator>();

        nowPattern = firstPattern;

        Instantiate(nowPattern[patternNum],transform);    // 子にパターンのプレハブを生成

        nowObj = gameObject.transform.GetChild(5).gameObject;   // 子のオブジェクトを取得

        attackPattern = nowObj.GetComponent<AttackPattern>();   // 取得したオブジェクトのパターンスクリプトを取得

        attackPattern.DebugMode = debugMode;                    // デバッグモードを有効化
    }

    void Update()
    {

        AnimController();


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


            nowObj = gameObject.transform.GetChild(6).gameObject; // 次のパターンを取得

            attackPattern = nowObj.GetComponent<AttackPattern>(); // パターンのスクリプトを取得

            attackPattern.DebugMode = debugMode;                    // デバッグモードを有効化

            Destroy(beforeObj);                                   // 前のパターンを破棄
        }


    }
    void AnimController()
    {
        anim.SetBool("Attack1", _animAttack1);
        if (_animAttack1) _animAttack1 = false;

        anim.SetBool("Attack2", _animAttack2);
        if (_animAttack2) _animAttack2 = false;

        // 登録リスト上にあり、ディクショナリーにないアニメーションを登録
        foreach (string item in animList)
        {
            if (!animDic.ContainsKey(item))
            {
                animDic.Add(item, false);
            }
        }

        // アニメディクショナリー再生
        foreach (string item in animList)
        {
            anim.SetBool(item, animDic[item]);
            if (animDic[item]) animDic[item] = false;
        }

    }
}
