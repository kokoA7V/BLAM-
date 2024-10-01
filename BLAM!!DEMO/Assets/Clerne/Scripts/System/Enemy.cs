using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("�X�e�[�^�X")]
    [SerializeField]
    int maxHp;

    [SerializeField]
    bool debugMode;

    [SerializeField]
    GameObject[] pattern;   // �U���p�^�[��


    private int _hp;

    public int Hp
    {
        get { return _hp; }

        set { _hp = value; }
    }

    private AttackPattern attackPattern;
    private int patternNum = 0; // ���ݓ����Ă���p�^�[��

    private GameObject nowObj;
    private GameObject beforeObj;


    void Start()
    {
        _hp = maxHp;

        Instantiate(pattern[patternNum],transform);    // �q�Ƀp�^�[���̃v���n�u�𐶐�

        nowObj = gameObject.transform.GetChild(0).gameObject;   // �q�̃I�u�W�F�N�g���擾

        attackPattern = nowObj.GetComponent<AttackPattern>();   // �擾�����I�u�W�F�N�g�̃p�^�[���X�N���v�g���擾

        attackPattern.DebugMode = debugMode;                    // �f�o�b�O���[�h��L����

    }

    void Update()
    {
        if(attackPattern.PatternEnd == true)
        { // �p�^�[�����I�������
            beforeObj = nowObj; // �I������p�^�[���I�u�W�F�N�g���ߋ��̃I�u�W�F�N�g�Ƃ���

            patternNum++;   // �p�^�[���ԍ���i�߂�


            // �o�^���ꂽ�p�^�[����1���珇�ԂɌJ��Ԃ�
            if (pattern.Length <= patternNum)
            {
                patternNum = 0;
            }
            Instantiate(pattern[patternNum],transform); // ���̃p�^�[���𐶐�


            nowObj = gameObject.transform.GetChild(1).gameObject; // ���̃p�^�[�����擾

            attackPattern = nowObj.GetComponent<AttackPattern>(); // �p�^�[���̃X�N���v�g���擾

            attackPattern.DebugMode = debugMode;                    // �f�o�b�O���[�h��L����

            Destroy(beforeObj);                                   // �O�̃p�^�[����j��
        }

    }

}
