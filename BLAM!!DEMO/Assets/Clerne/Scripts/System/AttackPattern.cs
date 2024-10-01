using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackPattern : MonoBehaviour
{
    [Header("ステータス")]

    [SerializeField]
    int maxHp;
    [SerializeField]
    int hpAtk;
    [SerializeField]
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
    [SerializeField]
    float justGuardTime;
    [SerializeField]
    float dodgeTime;
    [SerializeField]
    float justDodgeTime;

    private bool patternEnd = false;

    public bool PatternEnd
    {
        get
        {
            return patternEnd;
        }
        
    }

    private int hp;

    private bool dodged = false;
    private bool dodgeSuccesed = false;
    private bool guarded = false;
    private bool guardSuccesed = false;
    private bool attackEnd = false;

    [SerializeField ,ReadOnly]
    private float time;


    [Header("デバッグ用")]

    Text debugHpText;
    Text debugDodgeText;
    [SerializeField]
    int playerMaxHp;

    private int playerHp;

    private string dodgeTimingStr;

    void Start()
    {
        time = 0;

        hp = maxHp;

        playerHp = playerMaxHp;

        debugDodgeText = GameObject.Find("DebugText/Dodge").GetComponent<Text>();
        debugHpText = GameObject.Find("DebugText/HP").GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        PatternController();

        DebugText();
    }

    void PatternController()
    {
        if(time >= startTiming && time <= patternTime)
        {
            Debug.Log("攻撃開始");

            // デバッグ用
            dodgeTimingStr = "回避できない";

            if (time <= attackTiming)
            {
                if (time >= TimingNum(dodgeTime) && dodged == false)
                {   // 回避可能時間
                    if (time >= TimingNum(justDodgeTime))
                    {   // ジャスト回避可能時間
                        dodgeTimingStr = "ジャスト回避可能"; // デバッグ用


                        if (Input.GetKeyDown(KeyCode.D)) // プレイヤー側のInputにそのうち変える
                        {
                            Debug.Log("ジャスト回避成功");
                            

                            dodgeSuccesed = true;
                            dodged = true;
                        }

                    }
                    else
                    {   // 通常回避可能時間
                        dodgeTimingStr = "通常回避可能"; // デバッグ用
                        if (Input.GetKeyDown(KeyCode.D)) // プレイヤー側のInputにそのうち変える
                        {
                            Debug.Log("通常回避成功");

                            dodgeSuccesed = true;
                            dodged = true;

                        }

                    }
                }
                else if (time < TimingNum(dodgeTime))
                {   // 回避受付時間より早い
                    dodgeTimingStr = "回避できない"; // デバッグ用
                    if (Input.GetKeyDown(KeyCode.D)) // プレイヤー側のInputにそのうち変える
                    {
                        Debug.Log("回避ボタンを押すのが早すぎ");
                        Debug.Log("回避失敗");

                        dodged = true;

                    }
                }

            }
            else if(!dodgeSuccesed)
            {
                Debug.Log("攻撃対処失敗");
                Debug.Log("HP減らす");

                playerHp -= hpAtk;

                dodgeSuccesed = true;
            }




        }
        else if(time >= patternTime)
        {
            patternEnd = true;
        }
    }

    void DebugText()
    {
        debugHpText.text = "エネミーHP:" + hp + " プレイヤーHP:" + playerHp;

        debugDodgeText.text = "経過時間 " + time.ToString("F2") + " " + dodgeTimingStr; 
    }

    float TimingNum(float n)
    {
        float timing = attackTiming - n;
        return timing;
    }
}
