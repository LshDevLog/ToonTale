using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main_UI_Canvas : MonoBehaviour
{
    public static Main_UI_Canvas Instance {  get; private set; }

    private const string MAIN_MENU_SCENE_NAME = "MainMenu";
    private const string OPENING_SCENE_NAME = "Opening";

    [SerializeField]
    private RectTransform _statePanel, _weaponSlotsPanel;

    public Slider _hpSlider, _mpSlider, _shieldSlider, _dodgeSlider, _1SlotSlider, _2SlotSlider, _3SlotSlider;

    [SerializeField]
    private Image _lSlotImg, _rSlotImg, _1SlotImg, _2SlotImg, _3SlotImg, _saveImg;

    [SerializeField]
    private TextMeshProUGUI _hpText, _mpText, _shieldText, _dodgeText, _1SlotKeyText, _2SlotKeyText, _3SlotKeyText, _1SlotNoticeText, _2SlotNoticeText, _3SlotNoticeText;

    private CancellationTokenSource _cts;

    private Tween _textTween, _scaleTween;

    private Vector2 _offStateVec, _offSlotsVec;

    [SerializeField]
    private bool _showPanelsInScreen = true;

    [SerializeField]
    private float _moveSpeed = 10f;

    private string _curSceneName;

    public bool ShowPanelsInScreen => _showPanelsInScreen;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        _offStateVec = _statePanel.anchoredPosition;
        _offSlotsVec = _weaponSlotsPanel.anchoredPosition;
    }

    private void Start()
    {
        _curSceneName = SceneManager.GetActiveScene().name;
    }

    private void Update()
    {
        SetCurrentSceneName();
        ChangeUI_PadAndKeyboard();
        UpdateSlider(_hpSlider, _hpText);
        UpdateSlider(_mpSlider, _mpText);
        UpdateSlider(_shieldSlider, _shieldText);
        UpdateSlider(_dodgeSlider, _dodgeText);

        if (_curSceneName.Equals(MAIN_MENU_SCENE_NAME) || _curSceneName.Equals(OPENING_SCENE_NAME))
        {
            ResetPanelPos();
        }
        else
        {
            SetPanelPos();
        }

        UpdateSlotImg(_1SlotImg, Equipment.Instance._slot1_Weapon.icon);
        UpdateSlotImg(_2SlotImg, Equipment.Instance._slot2_Weapon.icon);
        UpdateSlotImg(_3SlotImg, Equipment.Instance._slot3_Weapon.icon);
        UpdateSlotImg(_rSlotImg, Equipment.Instance._equippedWeapon.icon);
        UpdateSkillCoolTimeSlider(_1SlotSlider, Equipment.Instance._slot1_Weapon);
        UpdateSkillCoolTimeSlider(_2SlotSlider, Equipment.Instance._slot2_Weapon);
        UpdateSkillCoolTimeSlider(_3SlotSlider, Equipment.Instance._slot3_Weapon);
    }

    private void ResetPanelPos()
    {
        _statePanel.anchoredPosition = _offStateVec;
        _weaponSlotsPanel.anchoredPosition = _offSlotsVec;
    }

    private void SetPanelPos()
    {
        Vector2 targetStatePanelPos = _showPanelsInScreen ? Vector2.zero : _offStateVec;
        Vector2 targetSlotsPanelPos = _showPanelsInScreen ? Vector2.zero : _offSlotsVec;

        _statePanel.anchoredPosition = Vector2.Lerp(_statePanel.anchoredPosition, targetStatePanelPos, _moveSpeed * Time.deltaTime);
        _weaponSlotsPanel.anchoredPosition = Vector2.Lerp(_weaponSlotsPanel.anchoredPosition, targetSlotsPanelPos, _moveSpeed * Time.deltaTime);
    }

    private void SetCurrentSceneName()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if (_curSceneName != sceneName)
        {
            _curSceneName = sceneName;
        }
    }

    private void UpdateSlotImg(Image slotImg, Sprite sprite)
    {
        if(slotImg == null || sprite == null)
        {
            return;
        }

        slotImg.sprite = sprite;
    }

    private void ChangeUI_PadAndKeyboard()
    {
        if(PadManager.Instance != null)
        {
            if (PadManager.Instance._PadConnected)
            {
                _1SlotKeyText.text = "[LT] + [X]";
                _1SlotKeyText.text = "[LT] + [Y]";
                _1SlotKeyText.text = "[LT] + [B]";

            }
            else
            {
                _1SlotKeyText.text = "Q";
                _2SlotKeyText.text = "W";
                _3SlotKeyText.text = "E";

            }
        }
    }

    public async UniTask ShowSaveAlarm()
    {
        _cts = new CancellationTokenSource();

        _scaleTween = _saveImg.transform.DOScale(1, 0.2f);
        await UniTask.Delay(TimeSpan.FromSeconds(1f), cancellationToken: _cts.Token);
        _ = _saveImg.transform.DOScale(0, 0.2f);
        _cts.Dispose();
        _cts = null;
    }

    public void InstantResetSaveAlarm()
    {
        if(_cts != null)
        {
            _cts.Cancel();
        }
        if(_scaleTween != null && _scaleTween.IsActive())
        {
            _scaleTween.Kill();
        }
        if (_textTween != null && _textTween.IsActive())
        {
            _textTween.Kill();
        }

        _saveImg.transform.DOScale(0, 0);
    }

    private void UpdateSlider(Slider slider, TextMeshProUGUI text)
    {
        if (slider != null)
        {
            slider.value = Mathf.Min(slider.value + Time.deltaTime, slider.maxValue);
        }

        if(text != null)
        {
            if(text == _shieldText)
            {
                text.text = "0/0";
            }

            string curValue = slider.value.ToString("F0");
            string maxValue = slider.maxValue.ToString("F0");
            text.text = $"{curValue}/{maxValue}";
        }  
    }

    private void UpdateSkillCoolTimeSlider(Slider slider, Weapon_Item item)
    {
        if(slider != null && item)
        {
            slider.maxValue = item.skillCoolTime;
            slider.value = Mathf.Min(slider.value + Time.deltaTime, slider.maxValue);
        }
    }
}
