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

        nowPattern = firstPattern;

        Instantiate(nowPattern[patternNum],transform);    // �q�Ƀp�^�[���̃v���n�u�𐶐�

        nowObj = gameObject.transform.GetChild(0).gameObject;   // �q�̃I�u�W�F�N�g���擾

        attackPattern = nowObj.GetComponent<AttackPattern>();   // �擾�����I�u�W�F�N�g�̃p�^�[���X�N���v�g���擾

        attackPattern.DebugMode = debugMode;                    // �f�o�b�O���[�h��L����

    }

    void Update()
    {
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


            nowObj = gameObject.transform.GetChild(1).gameObject; // ���̃p�^�[�����擾

            attackPattern = nowObj.GetComponent<AttackPattern>(); // �p�^�[���̃X�N���v�g���擾

            attackPattern.DebugMode = debugMode;                    // �f�o�b�O���[�h��L����

            Destroy(beforeObj);                                   // �O�̃p�^�[����j��
        }


    }

}
