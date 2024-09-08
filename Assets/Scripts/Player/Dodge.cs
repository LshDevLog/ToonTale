using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class Dodge : MonoBehaviour
{
    private Animator _anim;

    [SerializeField]
    AudioClip _dodgeClip;

    private const string DODGE_TRIGGER = "Dodge";

    public float _dodgeSpeed;

    private float _dodgeCoolTime, _dodgeCoolTimeMax;

    private bool _isDodging = false;
    
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
        UpdateDodgeSliderValue();
        UpdateDodgeCoolTime();
    }

    public async UniTask DoDodge()
    {
        if (InputManager.Instance._DODGE_KEY && InputManager.Instance._LEFT_TRIGGER_KEY == 0)
        {
            if (_dodgeCoolTime >= _dodgeCoolTimeMax && !_isDodging)
            {
                SoundManager.Instance.PlaySfx(_dodgeClip);
                _dodgeSpeed = 2.5f;
                _isDodging = true;
                _dodgeCoolTime = 0f;
                _anim.SetTrigger(DODGE_TRIGGER);
                await UniTask.Delay(TimeSpan.FromSeconds(0.35));
                _dodgeSpeed = 1f;
                _isDodging = false;
            }
        }
    }

    private void UpdateDodgeCoolTime()
    {
        if (_dodgeCoolTime >= _dodgeCoolTimeMax)
        {
            _dodgeCoolTime = _dodgeCoolTimeMax;
            return;
        }
        _dodgeCoolTime += Time.deltaTime;
    }

    private void InitDodge()
    {
        _dodgeCoolTimeMax = 5f;
        _dodgeCoolTime = _dodgeCoolTimeMax;
        if(Main_UI_Canvas.Instance != null)
        {
            Main_UI_Canvas.Instance._dodgeSlider.maxValue = _dodgeCoolTimeMax;
        }
    }

    private void UpdateDodgeSliderValue()
    {
        var mainUI = Main_UI_Canvas.Instance;

        if(mainUI != null && mainUI._dodgeSlider != null)
        {
            mainUI._dodgeSlider.value = _dodgeCoolTime;
        }
    }
}
