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

    

    void Start()
    {
        _hp = maxHp;

        _sp = maxSp;

        _combo = 0;

        attackInput.Enable();

        dodgeInput.Enable();

        guardInput.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();

        PlayerStates();
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
}
