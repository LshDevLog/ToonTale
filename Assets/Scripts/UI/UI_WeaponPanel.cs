using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

public class UI_WeaponPanel : MonoBehaviour
{
    [SerializeField]
    private Image _selectedWeaponImg;

    [SerializeField]
    private Sprite _emptySprite;

    [SerializeField]
    private LocalizeStringEvent _selectedWeaponNameText, _selectedWeaponDescText;

    [SerializeField]
    private LocalizedString _empty;

    [SerializeField]
    private WeaponSlot[] _slots;

    [SerializeField]
    private Image[] _showCurSlots;

    private GameObject _selectedWeaponSlot;

    [SerializeField]
    private AudioClip _equipClip;


    private void Update()
    {
        UpdateSelectedSlot();
        UpdateSelectedSlotUI();

        if (_selectedWeaponSlot == null)
            return;

        var input = InputManager.Instance;
        if (input._Q_KEY || input._W_KEY || input._E_KEY)
        {
            EquipWeapon();
        }

    }

    private void OnEnable()
    {
        ShowCurSlots();
    }

    private void OnDisable()
    {
        ResetUI();
    }

    private void UpdateSelectedSlot()
    {
        if (gameObject.activeSelf)
        {
            GameObject curObj = EventSystem.current.currentSelectedGameObject;
            _selectedWeaponSlot = (curObj.CompareTag("Slot")) ? curObj : null;           
        }
    }

    private void UpdateSelectedSlotUI()
    {
        if(_selectedWeaponSlot != null)
        {
            GameObject curObj = EventSystem.current.currentSelectedGameObject;

            foreach (var slot in _slots)
            {
                if(slot.gameObject.name == curObj.gameObject.name)
                {
                    _selectedWeaponImg.sprite = slot._img.sprite;
                    _selectedWeaponNameText.StringReference = slot._nameLocalKey;
                    _selectedWeaponDescText.StringReference = slot._descLocalKey;
                }
            }
        }
    }

    private void ResetUI()
    {
        _selectedWeaponImg.sprite = _emptySprite;
        _selectedWeaponNameText.StringReference = _empty;
        _selectedWeaponDescText.StringReference = _empty;
        _selectedWeaponSlot = null;
    }

    private void ShowCurSlots()
    {
        for (int i = 0; i < ScriptableObjectBox.Instance._weapons.Length; i++)
        {
            if(DataManager.Instance._equipmentData._slot1_Weapon == ScriptableObjectBox.Instance._weapons[i].itemName)
            {
                _showCurSlots[0].sprite = ScriptableObjectBox.Instance._weapons[i].icon;
            }
        }

        for (int i = 0; i < ScriptableObjectBox.Instance._weapons.Length; i++)
        {
            if (DataManager.Instance._equipmentData._slot2_Weapon == ScriptableObjectBox.Instance._weapons[i].itemName)
            {
                _showCurSlots[1].sprite = ScriptableObjectBox.Instance._weapons[i].icon;
            }
        }

        for (int i = 0; i < ScriptableObjectBox.Instance._weapons.Length; i++)
        {
            if (DataManager.Instance._equipmentData._slot3_Weapon == ScriptableObjectBox.Instance._weapons[i].itemName)
            {
                _showCurSlots[2].sprite = ScriptableObjectBox.Instance._weapons[i].icon;
            }
        }
    }


    private void EquipWeapon()
    {
        string selectedWeapon = string.Empty;

        for (int i = 0; i < _slots.Length; i++)
        {
            if (_slots[i].name == _selectedWeaponSlot.name)
            {
                selectedWeapon = _slots[i]._name;
            }
        }

        if(InputManager.Instance._Q_KEY)
        {
            if(selectedWeapon == DataManager.Instance._equipmentData._slot2_Weapon)
            {
                DataManager.Instance._equipmentData._slot2_Weapon = "NoWeapon";
            }
            if (selectedWeapon == DataManager.Instance._equipmentData._slot3_Weapon)
            {
                DataManager.Instance._equipmentData._slot3_Weapon = "NoWeapon";
            }
            DataManager.Instance._equipmentData._slot1_Weapon = selectedWeapon;
            SoundManager.Instance.PlaySfx(_equipClip);
        }
        else if (InputManager.Instance._W_KEY)
        {
            if (selectedWeapon == DataManager.Instance._equipmentData._slot1_Weapon)
            {
                DataManager.Instance._equipmentData._slot1_Weapon = "NoWeapon";
            }
            if (selectedWeapon == DataManager.Instance._equipmentData._slot3_Weapon)
            {
                DataManager.Instance._equipmentData._slot3_Weapon = "NoWeapon";
            }
            DataManager.Instance._equipmentData._slot2_Weapon = selectedWeapon;
            SoundManager.Instance.PlaySfx(_equipClip);
        }
        else if (InputManager.Instance._E_KEY)
        {
            if (selectedWeapon == DataManager.Instance._equipmentData._slot1_Weapon)
            {
                DataManager.Instance._equipmentData._slot1_Weapon = "NoWeapon";
            }
            if (selectedWeapon == DataManager.Instance._equipmentData._slot2_Weapon)
            {
                DataManager.Instance._equipmentData._slot2_Weapon = "NoWeapon";
            }
            DataManager.Instance._equipmentData._slot3_Weapon = selectedWeapon;
            SoundManager.Instance.PlaySfx(_equipClip);
        }

        Equipment.Instance.InitEquipment();
        Equipment.Instance.ShowWeaponObj();
        ShowCurSlots();
    }
}
