using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackPattern : MonoBehaviour
{
    [Header("�X�e�[�^�X")]

    [SerializeField]
    int maxHp;
    [SerializeField]
    int hpAtk;
    [SerializeField]
    int spAtk;
    [SerializeField, Tooltip("�U������ 0�Ōy�U���A1�ŏd�U��")]
    bool atkAtt;

    [Header("���Ԑݒ�i�b�j")]

    [SerializeField, Tooltip("�U���J�n����")]
    float startTiming;
    [SerializeField, Tooltip("�U���S�̎��ԁi�I�����ԁj")]
    float patternTime;
    [SerializeField, Tooltip("�U���q�b�g����")]
    float attackTiming;
    [SerializeField, Tooltip("�K�[�h�\���ԁi�U���q�b�g�^�C�~���O����̎��ԁj")]
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
    private bool dodgeAndGuardFailed = false;

    [SerializeField ,ReadOnly]
    private float time;


    [Header("�f�o�b�O�p")]

    Text debugHpText;
    Text debugDodgeText;
    Text debugGuardText;
    Text debugDoText;
    [SerializeField]
    int playerMaxHp;

    private int playerHp;

    private string dodgeTimingStr;

    private string guardTimingStr;

    private string doStr;

    void Start()
    {
        time = 0;

        hp = maxHp;

        playerHp = playerMaxHp;

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
            playerHp -= 100;
            dodgeAndGuardFailed = false;
        }
    }

    void PatternController()
    {
        if(time >= startTiming && time <= patternTime)
        {
            Debug.Log("�U���J�n");

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
        debugHpText.text = "�G�l�~�[HP:" + hp + " �v���C���[HP:" + playerHp;

        debugDodgeText.text = "�o�ߎ��� " + time.ToString("F2") + " " + dodgeTimingStr;

        debugGuardText.text = guardTimingStr;

        debugDoText.text = doStr;
    }

    void DodgeController()
    {
        if (time <= attackTiming)
        {
            // �f�o�b�O�p
            dodgeTimingStr = "����ł��Ȃ�";

            if (time >= TimingNum(dodgeTime) && dodged == false && guarded == false)
            {   // ����\����
                if (time >= TimingNum(justDodgeTime))
                {   // �W���X�g����\����
                    dodgeTimingStr = "�W���X�g����\"; // �f�o�b�O�p


                    if (Input.GetKeyDown(KeyCode.D)) // �v���C���[����Input�ɂ��̂����ς���
                    {
                        Debug.Log("�W���X�g��𐬌�");
                        doStr = "�W���X�g��𐬌�"; // �f�o�b�O�p


                        dodgeSuccesed = true;
                        dodged = true;
                    }

                }
                else
                {   // �ʏ����\����
                    dodgeTimingStr = "�ʏ����\"; // �f�o�b�O�p
                    if (Input.GetKeyDown(KeyCode.D)) // �v���C���[����Input�ɂ��̂����ς���
                    {
                        Debug.Log("�ʏ��𐬌�");
                        doStr = "�ʏ��𐬌�"; // �f�o�b�O�p

                        dodgeSuccesed = true;
                        dodged = true;

                    }

                }
            }
            else if (time < TimingNum(dodgeTime))
            {   // �����t���Ԃ�葁��
                dodgeTimingStr = "����ł��Ȃ�"; // �f�o�b�O�p
                if (Input.GetKeyDown(KeyCode.D)) // �v���C���[����Input�ɂ��̂����ς���
                {
                    Debug.Log("����{�^���������̂�������");
                    Debug.Log("������s");
                    doStr = "������s(fast)"; // �f�o�b�O�p


                    dodged = true;

                }
            }

        }
        else if (!dodgeSuccesed && !guardSuccesed)
        {
            Debug.Log("�U���Ώ����s");
            Debug.Log("HP���炷");

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


                    if (Input.GetKeyDown(KeyCode.G)) // �v���C���[����Input�ɂ��̂����ς���
                    {
                        Debug.Log("�W���X�g�K�[�h����");
                        doStr = "�W���X�g�K�[�h����";

                        guardSuccesed = true;
                        guarded = true;
                    }

                }
                else
                {   // �K�[�h�\����
                    guardTimingStr = "�ʏ�K�[�h�\"; // �f�o�b�O�p
                    if (Input.GetKeyDown(KeyCode.G)) // �v���C���[����Input�ɂ��̂����ς���
                    {
                        Debug.Log("�ʏ�K�[�h����");
                        doStr = "�ʏ�K�[�h����";

                        guardSuccesed = true;
                        guarded = true;

                    }

                }
            }
            else if (time < TimingNum(guardTime))
            {   // �K�[�h��t���Ԃ�葁��
                guardTimingStr = "�K�[�h�ł��Ȃ�"; // �f�o�b�O�p
                if (Input.GetKeyDown(KeyCode.G)) // �v���C���[����Input�ɂ��̂����ς���
                {
                    Debug.Log("�K�[�h�{�^���������̂�������");
                    Debug.Log("�K�[�h���s");
                    doStr = "�K�[�h���s";

                    guarded = true;

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
    float TimingNum(float n)
    {
        float timing = attackTiming - n;
        return timing;
    }
}
