using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

public class UI_BackpackPanel : MonoBehaviour
{
    private Sub_Menu _subMenu;

    [SerializeField]
    private Image _selectedItemImg;

    [SerializeField]
    private Sprite _emptySprite;

    [SerializeField]
    private LocalizeStringEvent _selectedItemNameText, _selectedItemDescText;

    [SerializeField]
    private LocalizedString _empty;

    [SerializeField]
    private BackpackSlot[] _slots;

    private GameObject _selectedItemSlot;

    [SerializeField]
    private AudioClip _healClip;

    private void Awake()
    {
        _subMenu = GetComponentInParent<Sub_Menu>();
    }

    private void Update()
    {
        UpdateSelectedSlot();
        UpdateSelectedSlotUI();


        if (_selectedItemSlot == null)
            return;

        UseItem();
        DeleteItem();
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
            _selectedItemSlot = (curObj.CompareTag("Slot")) ? curObj : null;
        }
    }

    private void UpdateSelectedSlotUI()
    {
        if (_selectedItemSlot != null)
        {
            GameObject curObj = EventSystem.current.currentSelectedGameObject;

            foreach (var slot in _slots)
            {
                if (slot.gameObject.name == curObj.gameObject.name)
                {
                    _selectedItemImg.sprite = slot._img.sprite;
                    _selectedItemNameText.StringReference = slot._nameLocalKey;
                    _selectedItemDescText.StringReference = slot._desLocalKey;
                }
            }
        }
    }

    private void ResetUI()
    {
        _selectedItemImg.sprite = _emptySprite;
        _selectedItemNameText.StringReference = _empty;
        _selectedItemDescText.StringReference = _empty;
        _selectedItemSlot = null;
    }


    private void UseItem()
    {
        if (InputManager.Instance._Q_KEY)
        {
            int temp = 0;
            for (int i = 0; i < _slots.Length; i++)
            {
                if(_selectedItemSlot.name == _slots[i].name)
                {
                    temp = i;

                }
            }
            ItemEffect();
            DataManager.Instance._equipmentData._consumableBox.RemoveAt(temp);
            _subMenu.SetBackpackSlots();
            ResetUI();
            if (temp > 0)
            {
                EventSystem.current.SetSelectedGameObject(_slots[temp - 1].gameObject);
            }

            if (DataManager.Instance._equipmentData._consumableBox.Count <= 0)
            {
                EventSystem.current.SetSelectedGameObject(_subMenu._backpackBtn.gameObject);
            }
        }
    }

    private void DeleteItem()
    {
        if (InputManager.Instance._E_KEY)
        {
            int temp = 0;
            for (int i = 0; i < _slots.Length; i++)
            {
                if (_selectedItemSlot.name == _slots[i].name)
                {
                    temp = i;
                }
            }
            DataManager.Instance._equipmentData._consumableBox.RemoveAt(temp);
            _subMenu.SetBackpackSlots();
            ResetUI();
            if(temp > 0)
            {
                EventSystem.current.SetSelectedGameObject(_slots[temp - 1].gameObject);
            }

            if (DataManager.Instance._equipmentData._consumableBox.Count <= 0)
            {
                EventSystem.current.SetSelectedGameObject(_subMenu._backpackBtn.gameObject);
            }
        }
    }

    private void ItemEffect()
    {
        string name = _selectedItemSlot.GetComponent<BackpackSlot>()._name;

        if(name == "RedWine")
        {
            PlayerCore.Instance._hp = PlayerCore.Instance._hpMax;
        }
        else if(name == "Cheese")
        {
            PlayerCore.Instance._mp = PlayerCore.Instance._mpMax;
        }
        else
        {
            return;
        }
        SoundManager.Instance.PlaySfx(_healClip);
    }
}
