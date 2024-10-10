using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackPattern : MonoBehaviour
{
    [Header("ステータス")]

    [SerializeField, Tooltip("対体力攻撃力")]
    int hpAtk;
    [SerializeField, Tooltip("対スタミナ攻撃力")]
    int spAtk;
    [SerializeField, Tooltip("攻撃属性 0で軽攻撃、1で重攻撃")]
    bool atkAtt;

    [Header("時間設定（秒）")]

    [SerializeField, Tooltip("変更しないでね！")]
    private float time;     // パターン内の経過時間
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

    [Header("チャンスタイム設定")]
    [SerializeField, Tooltip("チャンスタイム")]
    bool chance;

    [Header("アニメーション設定")]
    //[SerializeField]
    //bool anim_Attack1;
    //[SerializeField]
    //bool anim_Attack2;
    [SerializeField]
    string animName;    // 追加

    Player player;
    Enemy enemy;
    TimeScaleController timeScaleController;

    private bool timeScaleResetBoolian;
    private bool patternEnd = false; // パターン終了フラグ

    public bool PatternEnd
    {
        get
        {
            return patternEnd;
        }
        
    }



    private bool dodged = false;                // 回避行動フラグ
    private bool dodgeSuccesed = false;         // 回避成功フラグ
    private bool guarded = false;               // ガード行動フラグ
    private bool guardSuccesed = false;         // ガード成功フラグ
    private bool dodgeAndGuardFailed = false;   // 対処行動失敗フラグ
    private bool canCounter = false;            // カウンター可能フラグ
    private bool counterSuccesed = false;       // カウンター成功フラグ

    // カウンター演出用
    public bool CanCounter
    {
        get
        {
            return canCounter;
        }
    }

    public bool CounterSuccesed
    {
        get
        {
            return counterSuccesed;
        }
    }

    [Header("デバッグ用")]


    Text debugHpText;
    Text debugSpText;
    Text debugDodgeText;
    Text debugGuardText;
    Text debugDoText;
    Text debugAttText;
    Text debugComboText;

    private bool debugMode;

    public bool DebugMode
    {
        set { debugMode = value; }
    }

    private string dodgeTimingStr;

    private string guardTimingStr;

    private string attStr;

    private string doStr;

    void Start()
    {
        time = 0;

        player = GameObject.Find("Player").GetComponent<Player>();
        enemy = gameObject.transform.parent.gameObject.GetComponent<Enemy>();
        timeScaleController = GameObject.Find("TimeScaleController").GetComponent<TimeScaleController>();

        // ディクショナリー内に無い場合はAdd
        //if (enemy.animDic.ContainsKey(animName) == false)
        //{
        //    enemy.animDic.Add(animName, false);
        //}
        if (animName != "")
        {
            if (!enemy.animList.Contains(animName))
            {
                enemy.animList.Add(animName);
            }
        }

        // デバッグ用
        if (debugMode)
        {
            debugHpText = GameObject.Find("DebugText/HP").GetComponent<Text>();

            debugSpText = GameObject.Find("DebugText/SP").GetComponent<Text>();

            debugDodgeText = GameObject.Find("DebugText/Dodge").GetComponent<Text>();

            debugGuardText = GameObject.Find("DebugText/Guard").GetComponent<Text>();

            debugAttText = GameObject.Find("DebugText/Att").GetComponent<Text>();

            debugComboText = GameObject.Find("DebugText/Combo").GetComponent<Text>();

            debugDoText = GameObject.Find("DebugText/Do").GetComponent<Text>();


        }


    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        PatternController();

        PlayerAnimFlag();

        TimeScaleSetting();


        if (dodgeAndGuardFailed)
        {
            // damageSE後で消してね！
            SeManager.Instance.Play("damage7");

            player.Hp -= hpAtk;
            player.Combo = 0;   // コンボをリセット
            if (atkAtt == false) player.AnimDamageLight = true;   // 軽ダメージモーション
            else player.AnimDamageHeavy = true; // 重ダメージモーション

            dodgeAndGuardFailed = false;

        }


        // デバッグ用
        if (debugMode) DebugText();

    }

    void PatternController()
    {
        if(time >= startTiming && time <= patternTime)
        {
            Debug.Log("攻撃開始");

            EnemyAnimFlag();

            timeScaleController.TimeScaleReset();

            if (chance) ChanceTimeController();
            else
            {
                DodgeController();

                GuardController();

            }

            CounterController();


            if(enemy.PatternChange == true)
            {
                patternEnd = true;

            }

        }
        else if(time >= patternTime)
        {
            patternEnd = true;
        }
    }


    void DodgeController()
    {
        if (time <= attackTiming)
        {
            timeScaleController.TimeScaleSet(1.2f, 2);
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

                        // 軽攻撃の場合
                        if (atkAtt == false) player.Sp -= spAtk / 2;    // 適切でない行動なのでスタミナ減
                        // 重攻撃の場合
                        else canCounter = true; // 適切な行動かつジャストなのでカウンター可能に


                        dodgeSuccesed = true;
                        dodged = true;

                        player.AnimDodge = true;


                        // デバッグ用
                        doStr = "ジャスト回避成功";


                    }

                }
                else
                {   // 通常回避可能時間
                    dodgeTimingStr = "通常回避可能"; // デバッグ用
                    if (player.DodgeInp) 
                    {
                        Debug.Log("通常回避成功");

                        // 軽攻撃の場合
                        if (atkAtt == false) player.Sp -= spAtk;    // 適切でない行動なのでスタミナ減
                        // 重攻撃の場合
                        else player.Sp -= 0;    // 適切な行動なのでスタミナ減らず

                        dodgeSuccesed = true;
                        dodged = true;

                        player.AnimDodge = true;


                        // デバッグ用
                        doStr = "通常回避成功";

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

                    player.Sp -= spAtk;

                    dodged = true;

                    // デバッグ用
                    doStr = "回避失敗(fast)";



                }
            }

        }
        else if (!dodgeSuccesed && !guardSuccesed)
        {
            Debug.Log("攻撃対処失敗");
            Debug.Log("HP減らす");

            // デバッグ用
            dodgeTimingStr = "回避できない";
            guardTimingStr = "ガードできない";
            doStr = "対処失敗(slow)";



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

                        // 軽攻撃の場合
                        if (atkAtt == false) canCounter = true; // 適切な行動かつジャストなのでカウンター可能に
                        // 重攻撃の場合
                        else player.Sp -= spAtk / 2;    // 適切でない行動なのでスタミナ減


                        guardSuccesed = true;
                        guarded = true;

                        player.AnimGuard = true;

                        //canCounter = true;

                        // デバッグ用
                        doStr = "ジャストガード成功";

                    }

                }
                else
                {   // ガード可能時間
                    guardTimingStr = "通常ガード可能"; // デバッグ用
                    if (player.GuardInp) 
                    {
                        Debug.Log("通常ガード成功");

                        // 軽攻撃の場合
                        if (atkAtt == false) player.Sp -= 0;    // 適切な行動なのでスタミナ減らず
                        // 重攻撃の場合
                        else player.Sp -= spAtk; // 適切でない行動なのでスタミナ減

                        guardSuccesed = true;
                        guarded = true;

                        player.AnimGuard = true;

                        // デバッグ用
                        doStr = "通常ガード成功";


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

                    player.Sp -= spAtk;

                    guarded = true;

                    // デバッグ用
                    doStr = "ガード失敗";

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

    void CounterController()
    {
        player.AnimCanCounter = canCounter;
        if (canCounter && counterSuccesed == false)
        {
            
            if (player.AttackInp)
            {
                Debug.Log("カウンター成功");

                TakeDamage(player.AtkPow * 10);

                counterSuccesed = true;

                player.AnimCounter = true;

                // デバッグ用
                doStr = "カウンター成功！";
            }
        }

        // Caunterをfalse
        if (canCounter == false)
        {
            player.AnimCounter = false;
        }

    }

    void ChanceTimeController()
    {

        Debug.Log("チャンスタイム中!");

        if (player.AttackInp)
        {
            // あとで消してね
            SeManager.Instance.Play("ShotSound");

            TakeDamage(player.AtkPow);

            player.AnimAttack = true;

        }
        // デバッグ用
        attStr = "チャンスタイム中!";
    }

    void PlayerAnimFlag()
    {
        player.AnimChanceTime = chance;
    }

    void EnemyAnimFlag()
    {
        //if (anim_Attack1) enemy.AnimAttack1 = true;
        //if (anim_Attack2) enemy.AnimAttack2 = true;

        // 再生
        enemy.animDic[animName] = true;

    }
    void TimeScaleSetting()
    {
        if (canCounter) timeScaleController.TimeScaleSet(0.3f, 1);

    }

    float TimingNum(float n)
    {
        float timing = attackTiming - n;
        return timing;
    }

    void TakeDamage(int damage)
    {
        enemy.Hp -= damage;
        player.Combo++;
    }

    void DebugText()
    {
        debugHpText.text = "エネミーHP:" + enemy.Hp + " プレイヤーHP:" + player.Hp;

        debugSpText.text = "プレイヤーSP:" + player.Sp.ToString("F1");

        debugDodgeText.text = "経過時間 " + time.ToString("F2") + " " + dodgeTimingStr;

        debugGuardText.text = guardTimingStr;


        if(chance== true)
        {
            attStr = "チャンスタイム中!";
        }
        else
        {
            if (atkAtt == false) attStr = "軽攻撃";
            else if (atkAtt == true) attStr = "重攻撃";
        }

        debugAttText.text = "攻撃属性:" + attStr;

        debugComboText.text = player.Combo + "コンボ！";

        debugDoText.text = doStr;
    }

}
