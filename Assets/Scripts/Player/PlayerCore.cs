using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCore : MonoBehaviour
{
    public static PlayerCore Instance;

    private Animator _anim;
    private Movement _move;
    private Dodge _dodge;
    private Attack _attack;
    private Shield _shield;
    private Skill _skill;

    public float _hp, _hpMax, _mp, _mpMax;

    private bool _isInteracting = false;

    public bool IsInteracting
    {
        get { return _isInteracting; }
        set { _isInteracting = value; }
    }

    public enum STATE
    {
        Normal,
        Attack,
        Movement,
        Skill,
        Shield,
        SubMenu,
        Storage,
        Die
    }

    public STATE eSTATE = STATE.Normal;

    private void Awake()
    {
        Instance = this;

        _anim = GetComponent<Animator>();
        _move = GetComponent<Movement>();
        _dodge = GetComponent<Dodge>();
        _shield = GetComponent<Shield>();
        _attack = GetComponent<Attack>();
        _skill = GetComponent<Skill>();
    }

    private void Start()
    {
        //temp
        _hpMax = 5;
        _hp = _hpMax;
        _mpMax = 3;
        _mp = _mpMax;
    }

    private void Update()
    {
        UpdateHpMpSliderValue();

        if (_isInteracting)
        {
            return;
        }

        StateBehavior();
    }

    private void StateBehavior()
    {
        switch (eSTATE)
        {
            case STATE.Normal:
                _move.DoMove(_anim);
                _attack.DoAttack(_anim);
                break;
            case STATE.Attack:
                break;
            case STATE.Movement:
                _move.DoMove(_anim);
                _dodge.DoDodge().Forget();
                break;
            case STATE.Skill:
                break;
            case STATE.Shield:
                break;
            case STATE.SubMenu:
                break;
            case STATE.Storage:
                break;
            case STATE.Die:
                break;
            default:
                break;
        }
    }

    private void UpdateHpMpSliderValue()
    {
        var mainUI = Main_UI_Canvas.Instance;

        if(mainUI != null)
        {
            if(mainUI._hpSlider != null)
            {
                mainUI._hpSlider.value = _hp;
                mainUI._hpSlider.maxValue = _hpMax;
            }

            if(mainUI._mpSlider != null)
            {
                mainUI._mpSlider.value = _mp;
                mainUI._mpSlider.maxValue = _mpMax;
            }
        }
    }

    public void TakeDamage(float damage)
    {
        if(_shield._durability > damage)
        {
            _shield._durability -= damage;
        }
        else
        {
            float remainingDamage = (damage - _shield._durability);
            _shield._durability = 0;
            _hp -= remainingDamage;
            if( _hp <= 0 && eSTATE != STATE.Die)
            {
                Death().Forget();
            }
        }
    }


    private async UniTask Death()
    {
        eSTATE = STATE.Die;
        _anim.SetTrigger("Death");
        await UniTask.Delay(TimeSpan.FromSeconds(2.5f));
        SceneManager.LoadScene("ToonWorld");
    }
}
