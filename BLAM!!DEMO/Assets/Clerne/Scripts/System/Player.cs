using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("�X�e�[�^�X")]
    [SerializeField, Tooltip("�̗�")]
    private int maxHp;
    [SerializeField, Tooltip("�U����")]
    private int atkPow;
    [SerializeField, Tooltip("�X�^�~�i")]
    private float maxSp;
    [SerializeField, Tooltip("�X�^�~�i�񕜗ʁi�b�j")]
    private float spRecov;
    [SerializeField, Tooltip("�`�����X�^�C�����U���N�[���^�C��")]
    private float chanceAtkCooltime;

    [Header("����")]
    [SerializeField]
    InputAction attackInput;
    [SerializeField]
    InputAction dodgeInput;
    [SerializeField]
    InputAction guardInput;


    private int _hp;
    private float _sp;
    private int _combo;
    private bool _atk;
    private bool _dodge;
    private bool _guard;

    public int Hp
    {
        get { return _hp; }

        set { _hp = value; }
    }

    public float Sp
    {
        get { return _sp; }

        set { _sp = value; }
    }

    public int Combo
    {
        get { return _combo; }

        set { _combo = value; }
    }
    public int AtkPow
    {
        get { return atkPow; }
    }

    public bool AttackInp
    {
        get { return _atk; }
    }
    public bool DodgeInp
    {
        get { return _dodge; }
    }
    public bool GuardInp
    {
        get { return _guard; }
    }



    // �A�j���[�V�����֘A

    Animator anim;

    private bool _animAttack;
    private bool _animDodge;
    private bool _animGuard;
    private bool _animCanCounter;
    private bool _animCounter;
    private bool _animDamageLight;
    private bool _animDamageHeavy;
    private bool _animChanceTime;

    public bool AnimAttack
    {
        set { _animAttack = value; }
    }
    public bool AnimDodge
    {
        set { _animDodge = value; }
    }
    public bool AnimGuard
    {
        set { _animGuard = value; }
    }
    public bool AnimCounter
    {
        set { _animCounter = value; }
    }
    public bool AnimCanCounter
    {
        set { _animCanCounter = value; }
    }
    public bool AnimDamageLight
    {
        set { _animDamageLight = value; }
    }

    public bool AnimDamageHeavy
    {
        set { _animDamageHeavy = value; }
    }
    public bool AnimChanceTime
    {
        set { _animChanceTime = value; }
    }

    void Start()
    {
        _hp = maxHp;

        _sp = maxSp;

        _combo = 0;

        attackInput.Enable();

        dodgeInput.Enable();

        guardInput.Enable();

        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();

        PlayerStates();

        AnimController();
    }

    void PlayerInput()
    {
        if (attackInput.WasPerformedThisFrame()) _atk = true;
        else _atk = false;

        if (dodgeInput.WasPerformedThisFrame()) _dodge = true;
        else _dodge = false;

        if (guardInput.WasPerformedThisFrame()) _guard = true;
        else _guard = false;

    }

    void PlayerStates()
    {
        if (_sp < maxSp) _sp += Time.deltaTime * spRecov;
        else _sp = maxSp;
    }

    void AnimController()
    {
        anim.SetBool("Attack", _animAttack);
        if (_animAttack) _animAttack = false;

        anim.SetBool("Dodge", _animDodge);
        if (_animDodge) _animDodge = false;

        anim.SetBool("Guard", _animGuard);
        if (_animGuard) _animGuard = false;

        anim.SetBool("CanCounter", _animCanCounter);

        anim.SetBool("Counter", _animCounter);

        anim.SetBool("Damage_Light", _animDamageLight);
        if (_animDamageLight) _animDamageLight = false;

        anim.SetBool("Damage_Heavy", _animDamageHeavy);
        if (_animDamageHeavy) _animDamageHeavy = false;

        anim.SetBool("ChanceTime", _animChanceTime);
    }
}