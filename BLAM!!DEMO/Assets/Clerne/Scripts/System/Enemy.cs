using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("�X�e�[�^�X")]
    [SerializeField]
    float maxHp;

    [SerializeField]
    bool debugMode;

    [SerializeField]
    GameObject[] firstPattern;  // ���`�ԍU���p�^�[��

    [SerializeField]
    GameObject[] secondPattern; // ���`�ԍU���p�^�[��
    
    private float _hp;

    private GameObject[] nowPattern;

    private bool patternChange;

    // �A�j���[�V�����֘A

    Animator anim;

    private bool _animAttack1;
    private bool _animAttack2;

    // �A�j���f�B�N�V���i���[�ǉ�
    // �o�^�p���X�g�A�p�^�[������ǉ�
    public List<string> animList = new List<string>();
    // �Đ��p�̃f�B�N�V���i���[
    public�@Dictionary<string, bool> animDic = new Dictionary<string, bool>();



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
    private int patternNum = 0; // ���ݓ����Ă���p�^�[��
    private bool isSecondPattern = false;

    private GameObject nowObj;
    private GameObject beforeObj;


    void Start()
    {
        _hp = maxHp;

        anim = gameObject.GetComponent<Animator>();

        nowPattern = firstPattern;

        Instantiate(nowPattern[patternNum],transform);    // �q�Ƀp�^�[���̃v���n�u�𐶐�

        nowObj = gameObject.transform.GetChild(5).gameObject;   // �q�̃I�u�W�F�N�g���擾

        attackPattern = nowObj.GetComponent<AttackPattern>();   // �擾�����I�u�W�F�N�g�̃p�^�[���X�N���v�g���擾

        attackPattern.DebugMode = debugMode;                    // �f�o�b�O���[�h��L����
    }

    void Update()
    {

        AnimController();


        // HP��������؂�����
        if (_hp / maxHp < 0.5f && isSecondPattern == false)
        {
            nowPattern = secondPattern;

            patternChange = true;

            isSecondPattern = true;

        }


        if (attackPattern.PatternEnd == true)
        { // �p�^�[�����I�������




            beforeObj = nowObj; // �I������p�^�[���I�u�W�F�N�g���ߋ��̃I�u�W�F�N�g�Ƃ���

            patternNum++;   // �p�^�[���ԍ���i�߂�


            // �o�^���ꂽ�p�^�[����1���珇�ԂɌJ��Ԃ�
            if (nowPattern.Length <= patternNum || patternChange == true)
            {
                patternNum = 0;

                patternChange = false;
            }
            Instantiate(nowPattern[patternNum],transform); // ���̃p�^�[���𐶐�


            nowObj = gameObject.transform.GetChild(6).gameObject; // ���̃p�^�[�����擾

            attackPattern = nowObj.GetComponent<AttackPattern>(); // �p�^�[���̃X�N���v�g���擾

            attackPattern.DebugMode = debugMode;                    // �f�o�b�O���[�h��L����

            Destroy(beforeObj);                                   // �O�̃p�^�[����j��
        }


    }
    void AnimController()
    {
        anim.SetBool("Attack1", _animAttack1);
        if (_animAttack1) _animAttack1 = false;

        anim.SetBool("Attack2", _animAttack2);
        if (_animAttack2) _animAttack2 = false;

        // �o�^���X�g��ɂ���A�f�B�N�V���i���[�ɂȂ��A�j���[�V������o�^
        foreach (string item in animList)
        {
            if (!animDic.ContainsKey(item))
            {
                animDic.Add(item, false);
            }
        }

        // �A�j���f�B�N�V���i���[�Đ�
        foreach (string item in animList)
        {
            anim.SetBool(item, animDic[item]);
            if (animDic[item]) animDic[item] = false;
        }

    }
}
