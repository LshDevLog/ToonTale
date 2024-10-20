using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class Dodge : MonoBehaviour
{
    private Animator _anim;

    [SerializeField]
    private AudioClip _dodgeClip;

    public float _dodgeSpeed;

    private float _dodgeCoolTime, _dodgeCoolTimeMax, _rollingTime;

    private bool _isDodging = false;

    private const string DODGE_TRIGGER = "Dodge";
    
    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    private void Start()
    {
        InitDodge();
    }

    private void Update()
    {
        UpdateDodgeCoolTime();
        UpdateDodgeSlider();
        _dodgeSpeed = SetDodgeSpeed();
    }

    private void InitDodge()
    {
        _dodgeCoolTimeMax = 5f;
        _dodgeCoolTime = _dodgeCoolTimeMax;
        _rollingTime = 0.35f;
        if (Main_UI_Canvas.Instance != null)
        {
            Main_UI_Canvas.Instance._dodgeSlider.maxValue = _dodgeCoolTimeMax;
        }
    }

    private void UpdateDodgeCoolTime()
    {
        _dodgeCoolTime = (_dodgeCoolTime < _dodgeCoolTimeMax) ? _dodgeCoolTime += Time.deltaTime : _dodgeCoolTimeMax;
    }

    private void UpdateDodgeSlider()
    {
        var mainUI = Main_UI_Canvas.Instance;

        if (mainUI != null && mainUI._dodgeSlider != null)
        {
            mainUI._dodgeSlider.value = _dodgeCoolTime;
        }
    }

    private float SetDodgeSpeed()
    {
        float speed = (_isDodging) ? 2.5f : 1f;
        return speed;
    }

    public async UniTask DoDodge()
    {
        if (InputManager.Instance._DODGE_KEY && InputManager.Instance._LEFT_TRIGGER_KEY == 0 && _dodgeCoolTime >= _dodgeCoolTimeMax && !_isDodging)
        {
            _isDodging = true;
            SoundManager.Instance.PlaySfx(_dodgeClip);
            _dodgeCoolTime = 0f;
            _anim.SetTrigger(DODGE_TRIGGER);
            await UniTask.Delay(TimeSpan.FromSeconds(_rollingTime));
            _isDodging = false;
        }
    }

}
