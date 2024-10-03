using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("ステータス")]
    [SerializeField, Tooltip("体力")]
    private int maxHp;
    [SerializeField, Tooltip("攻撃力")]
    private int atkPow;
    [SerializeField, Tooltip("スタミナ")]
    private float maxSp;
    [SerializeField, Tooltip("スタミナ回復量（秒）")]
    private float spRecov;

    [Header("入力")]
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
