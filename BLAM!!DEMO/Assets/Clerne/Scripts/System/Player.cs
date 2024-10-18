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

    [Header("�G�t�F�N�g")]
    [SerializeField]
    ParticleSystem attackEff;
    [SerializeField]
    ParticleSystem counterEff;
    [SerializeField]
    ParticleSystem guardEff;
    [SerializeField]
    ParticleSystem justGuardEff;
    [SerializeField]
    ParticleSystem dodgeEff;
    [SerializeField]
    ParticleSystem justDodgeEff;
    [SerializeField]
    ParticleSystem hitLightEff;
    [SerializeField]
    ParticleSystem hitHeavyEff;

    [Header("SE")]
    [SerializeField]
    AudioSource audioSource;
    [SerializeField]
    AudioClip attackSe;
    [SerializeField]
    AudioClip counterSe;
    [SerializeField]
    AudioClip guardSe;
    [SerializeField]
    AudioClip justGuardSe;
    [SerializeField]
    AudioClip dodgeSe;
    [SerializeField]
    AudioClip justDodgeSe;
    [SerializeField]
    AudioClip hitLightSe;
    [SerializeField]
    AudioClip hitHeavySe;






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

    public ParticleSystem AttackEff
    {
        get { return attackEff; }
    }
    public ParticleSystem CounterEff
    {
        get { return counterEff; }
    }
    public ParticleSystem GuardEff
    {
        get { return guardEff; }
    }
    public ParticleSystem JustGuardEff
    {
        get { return justGuardEff; }
    }
    public ParticleSystem DodgeEff
    {
        get { return dodgeEff; }
    }
    public ParticleSystem JustDodgeEff
    {
        get { return justDodgeEff; }
    }
    public ParticleSystem HitLightEff
    {
        get { return hitLightEff; }
    }
    public ParticleSystem HitHeavyEff
    {
        get { return hitHeavyEff; }
    }


    public AudioClip AttackSe
    {
        get { return attackSe; }
    }
    public AudioClip CounterSe
    {
        get { return counterSe; }
    }
    public AudioClip GuardSe
    {
        get { return guardSe; }
    }
    public AudioClip JustGuardSe
    {
        get { return justGuardSe; }
    }
    public AudioClip DodgeSe
    {
        get { return dodgeSe; }
    }
    public AudioClip JustDodgeSe
    {
        get { return justDodgeSe; }
    }
    public AudioClip HitLightSe
    {
        get { return hitLightSe; }
    }
    public AudioClip HitHeavySe
    {
        get { return hitHeavySe; }
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

    // �A�j���[�V�����f�B�N�V���i���[
    Dictionary<string, bool> animDic = new Dictionary<string, bool>();
    // �f�B�N�V���i���[�o�^�pList�AInspector����ǉ�
    [SerializeField]
    List<string> animList = new List<string>();

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

        // animList�̗v�f��animDic��Add
        foreach (string item in animList)
        {
            animDic.Add(item, false);
        }
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
        // AttackPattern�̕���false�ɂ���

        anim.SetBool("Damage_Light", _animDamageLight);
        if (_animDamageLight) _animDamageLight = false;

        anim.SetBool("Damage_Heavy", _animDamageHeavy);
        if (_animDamageHeavy) _animDamageHeavy = false;

        anim.SetBool("ChanceTime", _animChanceTime);

        // �f�B�N�V���i���[�Đ�
        foreach (KeyValuePair<string, bool> item in animDic)
        {
            anim.SetBool(item.Key, animDic[item.Key]);
            if (animDic[item.Key]) animDic[item.Key] = false;
        }
    }

    public void SePlayer(AudioClip se)
    {
        if (se != null) audioSource.PlayOneShot(se);
        else Debug.Log("SE�����ĂȂ���I");
    }
}
