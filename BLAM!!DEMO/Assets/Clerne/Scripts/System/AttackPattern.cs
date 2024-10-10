using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackPattern : MonoBehaviour
{
    [Header("�X�e�[�^�X")]

    [SerializeField, Tooltip("�Α̗͍U����")]
    int hpAtk;
    [SerializeField, Tooltip("�΃X�^�~�i�U����")]
    int spAtk;
    [SerializeField, Tooltip("�U������ 0�Ōy�U���A1�ŏd�U��")]
    bool atkAtt;

    [Header("���Ԑݒ�i�b�j")]

    [SerializeField, Tooltip("�ύX���Ȃ��łˁI")]
    private float time;     // �p�^�[�����̌o�ߎ���
    [SerializeField, Tooltip("�U���J�n����")]
    float startTiming;
    [SerializeField, Tooltip("�U���S�̎��ԁi�I�����ԁj")]
    float patternTime;
    [SerializeField, Tooltip("�U���q�b�g����")]
    float attackTiming;
    [SerializeField, Tooltip("�K�[�h�\���ԁi�U���q�b�g�^�C�~���O����̎��ԁj")]
    float guardTime;
    [SerializeField, Tooltip("�W���X�g�K�[�h�\���ԁi�U���q�b�g�^�C�~���O����̎��ԁj")]
    float justGuardTime;
    [SerializeField, Tooltip("����\���ԁi�U���q�b�g�^�C�~���O����̎��ԁj")]
    float dodgeTime;
    [SerializeField,Tooltip("�W���X�g����\���ԁi�U���q�b�g�^�C�~���O����̎��ԁj")]
    float justDodgeTime;

    [Header("�`�����X�^�C���ݒ�")]
    [SerializeField, Tooltip("�`�����X�^�C��")]
    bool chance;

    [Header("�A�j���[�V�����ݒ�")]
    //[SerializeField]
    //bool anim_Attack1;
    //[SerializeField]
    //bool anim_Attack2;
    [SerializeField]
    string animName;    // �ǉ�

    Player player;
    Enemy enemy;
    TimeScaleController timeScaleController;

    private bool timeScaleResetBoolian;
    private bool patternEnd = false; // �p�^�[���I���t���O

    public bool PatternEnd
    {
        get
        {
            return patternEnd;
        }
        
    }



    private bool dodged = false;                // ����s���t���O
    private bool dodgeSuccesed = false;         // ��𐬌��t���O
    private bool guarded = false;               // �K�[�h�s���t���O
    private bool guardSuccesed = false;         // �K�[�h�����t���O
    private bool dodgeAndGuardFailed = false;   // �Ώ��s�����s�t���O
    private bool canCounter = false;            // �J�E���^�[�\�t���O
    private bool counterSuccesed = false;       // �J�E���^�[�����t���O

    // �J�E���^�[���o�p
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

    [Header("�f�o�b�O�p")]


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

        // �f�B�N�V���i���[���ɖ����ꍇ��Add
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

        // �f�o�b�O�p
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
            // damageSE��ŏ����ĂˁI
            SeManager.Instance.Play("damage7");

            player.Hp -= hpAtk;
            player.Combo = 0;   // �R���{�����Z�b�g
            if (atkAtt == false) player.AnimDamageLight = true;   // �y�_���[�W���[�V����
            else player.AnimDamageHeavy = true; // �d�_���[�W���[�V����

            dodgeAndGuardFailed = false;

        }


        // �f�o�b�O�p
        if (debugMode) DebugText();

    }

    void PatternController()
    {
        if(time >= startTiming && time <= patternTime)
        {
            Debug.Log("�U���J�n");

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
            // �f�o�b�O�p
            dodgeTimingStr = "����ł��Ȃ�";

            if (time >= TimingNum(dodgeTime) && dodged == false && guarded == false)
            {   // ����\����

                if (time >= TimingNum(justDodgeTime))
                {   // �W���X�g����\����
                    dodgeTimingStr = "�W���X�g����\"; // �f�o�b�O�p


                    if (player.DodgeInp)
                    {
                        Debug.Log("�W���X�g��𐬌�");

                        // �y�U���̏ꍇ
                        if (atkAtt == false) player.Sp -= spAtk / 2;    // �K�؂łȂ��s���Ȃ̂ŃX�^�~�i��
                        // �d�U���̏ꍇ
                        else canCounter = true; // �K�؂ȍs�����W���X�g�Ȃ̂ŃJ�E���^�[�\��


                        dodgeSuccesed = true;
                        dodged = true;

                        player.AnimDodge = true;


                        // �f�o�b�O�p
                        doStr = "�W���X�g��𐬌�";


                    }

                }
                else
                {   // �ʏ����\����
                    dodgeTimingStr = "�ʏ����\"; // �f�o�b�O�p
                    if (player.DodgeInp) 
                    {
                        Debug.Log("�ʏ��𐬌�");

                        // �y�U���̏ꍇ
                        if (atkAtt == false) player.Sp -= spAtk;    // �K�؂łȂ��s���Ȃ̂ŃX�^�~�i��
                        // �d�U���̏ꍇ
                        else player.Sp -= 0;    // �K�؂ȍs���Ȃ̂ŃX�^�~�i���炸

                        dodgeSuccesed = true;
                        dodged = true;

                        player.AnimDodge = true;


                        // �f�o�b�O�p
                        doStr = "�ʏ��𐬌�";

                    }

                }
            }
            else if (time < TimingNum(dodgeTime))
            {   // �����t���Ԃ�葁��
                dodgeTimingStr = "����ł��Ȃ�"; // �f�o�b�O�p
                if (player.DodgeInp) 
                {
                    Debug.Log("����{�^���������̂�������");
                    Debug.Log("������s");

                    player.Sp -= spAtk;

                    dodged = true;

                    // �f�o�b�O�p
                    doStr = "������s(fast)";



                }
            }

        }
        else if (!dodgeSuccesed && !guardSuccesed)
        {
            Debug.Log("�U���Ώ����s");
            Debug.Log("HP���炷");

            // �f�o�b�O�p
            dodgeTimingStr = "����ł��Ȃ�";
            guardTimingStr = "�K�[�h�ł��Ȃ�";
            doStr = "�Ώ����s(slow)";



            dodgeAndGuardFailed = true;

            dodgeSuccesed = true;
        }

    }

    void GuardController()
    {
        if (time <= attackTiming)
        {
            // �f�o�b�O�p
            guardTimingStr = "�K�[�h�ł��Ȃ�";

            if (time >= TimingNum(guardTime) && guarded == false && dodged == false)
            {   // ����\����
                if (time >= TimingNum(justGuardTime))
                {   // �W���X�g����\����
                    guardTimingStr = "�W���X�g�K�[�h�\"; // �f�o�b�O�p


                    if (player.GuardInp) 
                    {
                        Debug.Log("�W���X�g�K�[�h����");

                        // �y�U���̏ꍇ
                        if (atkAtt == false) canCounter = true; // �K�؂ȍs�����W���X�g�Ȃ̂ŃJ�E���^�[�\��
                        // �d�U���̏ꍇ
                        else player.Sp -= spAtk / 2;    // �K�؂łȂ��s���Ȃ̂ŃX�^�~�i��


                        guardSuccesed = true;
                        guarded = true;

                        player.AnimGuard = true;

                        //canCounter = true;

                        // �f�o�b�O�p
                        doStr = "�W���X�g�K�[�h����";

                    }

                }
                else
                {   // �K�[�h�\����
                    guardTimingStr = "�ʏ�K�[�h�\"; // �f�o�b�O�p
                    if (player.GuardInp) 
                    {
                        Debug.Log("�ʏ�K�[�h����");

                        // �y�U���̏ꍇ
                        if (atkAtt == false) player.Sp -= 0;    // �K�؂ȍs���Ȃ̂ŃX�^�~�i���炸
                        // �d�U���̏ꍇ
                        else player.Sp -= spAtk; // �K�؂łȂ��s���Ȃ̂ŃX�^�~�i��

                        guardSuccesed = true;
                        guarded = true;

                        player.AnimGuard = true;

                        // �f�o�b�O�p
                        doStr = "�ʏ�K�[�h����";


                    }

                }
            }
            else if (time < TimingNum(guardTime))
            {   // �K�[�h��t���Ԃ�葁��
                guardTimingStr = "�K�[�h�ł��Ȃ�"; // �f�o�b�O�p
                if (player.GuardInp)
                {
                    Debug.Log("�K�[�h�{�^���������̂�������");
                    Debug.Log("�K�[�h���s");

                    player.Sp -= spAtk;

                    guarded = true;

                    // �f�o�b�O�p
                    doStr = "�K�[�h���s";

                }
            }

        }
        else if (!guardSuccesed && !dodgeSuccesed)
        {
            Debug.Log("�U���Ώ����s");
            Debug.Log("HP���炷");

            dodgeAndGuardFailed = true;

            guardSuccesed = true;   // ��
        }

    }

    void CounterController()
    {
        player.AnimCanCounter = canCounter;
        if (canCounter && counterSuccesed == false)
        {
            
            if (player.AttackInp)
            {
                Debug.Log("�J�E���^�[����");

                TakeDamage(player.AtkPow * 10);

                counterSuccesed = true;

                player.AnimCounter = true;

                // �f�o�b�O�p
                doStr = "�J�E���^�[�����I";
            }
        }

        // Caunter��false
        if (canCounter == false)
        {
            player.AnimCounter = false;
        }

    }

    void ChanceTimeController()
    {

        Debug.Log("�`�����X�^�C����!");

        if (player.AttackInp)
        {
            // ���Ƃŏ����Ă�
            SeManager.Instance.Play("ShotSound");

            TakeDamage(player.AtkPow);

            player.AnimAttack = true;

        }
        // �f�o�b�O�p
        attStr = "�`�����X�^�C����!";
    }

    void PlayerAnimFlag()
    {
        player.AnimChanceTime = chance;
    }

    void EnemyAnimFlag()
    {
        //if (anim_Attack1) enemy.AnimAttack1 = true;
        //if (anim_Attack2) enemy.AnimAttack2 = true;

        // �Đ�
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
        debugHpText.text = "�G�l�~�[HP:" + enemy.Hp + " �v���C���[HP:" + player.Hp;

        debugSpText.text = "�v���C���[SP:" + player.Sp.ToString("F1");

        debugDodgeText.text = "�o�ߎ��� " + time.ToString("F2") + " " + dodgeTimingStr;

        debugGuardText.text = guardTimingStr;


        if(chance== true)
        {
            attStr = "�`�����X�^�C����!";
        }
        else
        {
            if (atkAtt == false) attStr = "�y�U��";
            else if (atkAtt == true) attStr = "�d�U��";
        }

        debugAttText.text = "�U������:" + attStr;

        debugComboText.text = player.Combo + "�R���{�I";

        debugDoText.text = doStr;
    }

}
