using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackPattern : MonoBehaviour
{
    [Header("ステータス")]

    [SerializeField, Tooltip("最大体力")]
    int maxHp; 
    [SerializeField, Tooltip("対体力攻撃力")]
    int hpAtk;
    [SerializeField, Tooltip("対スタミナ攻撃力")]
    int spAtk;
    [SerializeField, Tooltip("攻撃属性 0で軽攻撃、1で重攻撃")]
    bool atkAtt;

    [Header("時間設定（秒）")]

    [SerializeField, Tooltip("攻撃開始時間")]
    float startTiming;
    [SerializeField, Tooltip("攻撃全体時間（終了時間）")]
    float patternTime;
    [SerializeField, Tooltip("攻撃ヒット時間")]
    float attackTiming;
    [SerializeField, Tooltip("ガード可能時間（攻撃ヒットタイミングからの時間）")]
    float guardTime;
    [SerializeField, Tooltip("ジャストガード可能時間（攻撃ヒットタイミングからの時間）")]
    float justGuardTime;
    [SerializeField, Tooltip("回避可能時間（攻撃ヒットタイミングからの時間）")]
    float dodgeTime;
    [SerializeField,Tooltip("ジャスト回避可能時間（攻撃ヒットタイミングからの時間）")]
    float justDodgeTime;

    private bool patternEnd = false; // パターン終了フラグ

    Player player;

    public bool PatternEnd
    {
        get
        {
            return patternEnd;
        }
        
    }

    private int hp; //

    private bool dodged = false;                // 回避行動フラグ
    private bool dodgeSuccesed = false;         // 回避成功フラグ
    private bool guarded = false;               // ガード行動フラグ
    private bool guardSuccesed = false;         // ガード成功フラグ
    private bool dodgeAndGuardFailed = false;   // 対処行動失敗フラグ

    [SerializeField ,ReadOnly]
    private float time;     // パターン内の経過時間


    [Header("デバッグ用")]

    Text debugHpText;
    Text debugDodgeText;
    Text debugGuardText;
    Text debugDoText;

    private string dodgeTimingStr;

    private string guardTimingStr;

    private string doStr;

    void Start()
    {
        time = 0;

        hp = maxHp;


        player = GameObject.Find("Player").GetComponent<Player>();

        debugDodgeText = GameObject.Find("DebugText/Dodge").GetComponent<Text>();

        debugGuardText = GameObject.Find("DebugText/Guard").GetComponent<Text>();

        debugDoText = GameObject.Find("DebugText/Do").GetComponent<Text>();

        debugHpText = GameObject.Find("DebugText/HP").GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        PatternController();

        DebugText();

        if (dodgeAndGuardFailed)
        {
            player.Hp -= 100;
            dodgeAndGuardFailed = false;
        }
    }

    void PatternController()
    {
        if(time >= startTiming && time <= patternTime)
        {
            Debug.Log("攻撃開始");

            DodgeController();

            GuardController();


        }
        else if(time >= patternTime)
        {
            patternEnd = true;
        }
    }

    void DebugText()
    {
        debugHpText.text = "エネミーHP:" + hp + " プレイヤーHP:" + player.Hp;

        debugDodgeText.text = "経過時間 " + time.ToString("F2") + " " + dodgeTimingStr;

        debugGuardText.text = guardTimingStr;

        debugDoText.text = doStr;
    }

    void DodgeController()
    {
        if (time <= attackTiming)
        {
            // デバッグ用
            dodgeTimingStr = "回避できない";

            if (time >= TimingNum(dodgeTime) && dodged == false && guarded == false)
            {   // 回避可能時間
                if (time >= TimingNum(justDodgeTime))
                {   // ジャスト回避可能時間
                    dodgeTimingStr = "ジャスト回避可能"; // デバッグ用


                    if (player.DodgeInp)
                    {
                        Debug.Log("ジャスト回避成功");
                        doStr = "ジャスト回避成功"; // デバッグ用


                        dodgeSuccesed = true;
                        dodged = true;
                    }

                }
                else
                {   // 通常回避可能時間
                    dodgeTimingStr = "通常回避可能"; // デバッグ用
                    if (player.DodgeInp) 
                    {
                        Debug.Log("通常回避成功");
                        doStr = "通常回避成功"; // デバッグ用

                        dodgeSuccesed = true;
                        dodged = true;

                    }

                }
            }
            else if (time < TimingNum(dodgeTime))
            {   // 回避受付時間より早い
                dodgeTimingStr = "回避できない"; // デバッグ用
                if (player.DodgeInp) 
                {
                    Debug.Log("回避ボタンを押すのが早すぎ");
                    Debug.Log("回避失敗");
                    doStr = "回避失敗(fast)"; // デバッグ用


                    dodged = true;

                }
            }

        }
        else if (!dodgeSuccesed && !guardSuccesed)
        {
            Debug.Log("攻撃対処失敗");
            Debug.Log("HP減らす");

            dodgeAndGuardFailed = true;

            dodgeSuccesed = true;
        }

    }

    void GuardController()
    {
        if (time <= attackTiming)
        {
            // デバッグ用
            guardTimingStr = "ガードできない";

            if (time >= TimingNum(guardTime) && guarded == false && dodged == false)
            {   // 回避可能時間
                if (time >= TimingNum(justGuardTime))
                {   // ジャスト回避可能時間
                    guardTimingStr = "ジャストガード可能"; // デバッグ用


                    if (player.GuardInp) 
                    {
                        Debug.Log("ジャストガード成功");
                        doStr = "ジャストガード成功";

                        guardSuccesed = true;
                        guarded = true;
                    }

                }
                else
                {   // ガード可能時間
                    guardTimingStr = "通常ガード可能"; // デバッグ用
                    if (player.GuardInp) 
                    {
                        Debug.Log("通常ガード成功");
                        doStr = "通常ガード成功";

                        guardSuccesed = true;
                        guarded = true;

                    }

                }
            }
            else if (time < TimingNum(guardTime))
            {   // ガード受付時間より早い
                guardTimingStr = "ガードできない"; // デバッグ用
                if (player.GuardInp)
                {
                    Debug.Log("ガードボタンを押すのが早すぎ");
                    Debug.Log("ガード失敗");
                    doStr = "ガード失敗";

                    guarded = true;

                }
            }

        }
        else if (!guardSuccesed && !dodgeSuccesed)
        {
            Debug.Log("攻撃対処失敗");
            Debug.Log("HP減らす");

            dodgeAndGuardFailed = true;

            guardSuccesed = true;   // 仮
        }

    }
    float TimingNum(float n)
    {
        float timing = attackTiming - n;
        return timing;
    }
}
