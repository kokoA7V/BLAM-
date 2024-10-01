using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("ステータス")]
    [SerializeField, Tooltip("体力")]
    private int maxHp;

    [Header("入力")]
    [SerializeField]
    InputAction dodgeInput;
    [SerializeField]
    InputAction guardInput;


    private int _hp;
    private bool _dodge;
    private bool _guard;

    public int Hp
    {
        get { return _hp; }

        set { _hp = value; }
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

        dodgeInput.Enable();

        guardInput.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();
    }

    void PlayerInput()
    {
        if (dodgeInput.WasPerformedThisFrame())
        {
            _dodge = true;
        }
        else
        {
            _dodge = false;
        }
        if (guardInput.WasPerformedThisFrame())
        {
            _guard = true;
        }
        else
        {
            _guard = false;
        }

    }
}
