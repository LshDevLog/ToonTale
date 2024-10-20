using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

public class DescPanel : MonoBehaviour//This Script isn't used.
{
    ItemBase _item;

    [SerializeField]
    Weapon_Item _noWeapon;

    [SerializeField]
    private LocalizeStringEvent _nameText, _descText;

    [SerializeField]
    private Image _img;

    [SerializeField]
    private GameObject _weaponEquipBtns;

    [SerializeField]
    private Button _qBtn, _wBtn, _eBtn, _backFromWeaponBtn, _weaponBoxBtn;

    private void Start()
    {
        _qBtn.onClick.AddListener(() => PressWeaponSlot(1));
        _wBtn.onClick.AddListener(() => PressWeaponSlot(2));
        _eBtn.onClick.AddListener(() => PressWeaponSlot(3));
        _backFromWeaponBtn.onClick.AddListener(() => BtnCommonFunction(_weaponBoxBtn.gameObject));
    }

    public void SetPanel(ItemBase item)
    {
        _item = item;
        _nameText.StringReference = item.itemNameLocalKey;
        _descText.StringReference = item.descLocalKey;
        _img.sprite = item.icon;

        if (item is Weapon_Item)
        {
            EventSystem.current.SetSelectedGameObject(_weaponEquipBtns.transform.GetChild(0).gameObject);
        }
    }

    private void PressWeaponSlot(int slot)
    {
        var data = Equipment.Instance;

        if (data == null)
        {
            return;
        }

        if(data._slot1_Weapon == _item)
        {
            data._slot1_Weapon = _noWeapon;
        }
        if (data._slot2_Weapon == _item)
        {
            data._slot2_Weapon = _noWeapon;
        }
        if (data._slot3_Weapon == _item)
        {
            data._slot3_Weapon = _noWeapon;
        }

        switch (slot)
        {
            case 1:
                data._slot1_Weapon = (Weapon_Item)_item;
                break;
            case 2:
                data._slot2_Weapon = (Weapon_Item)_item;
                break;
            case 3:
                data._slot3_Weapon = (Weapon_Item)_item;
                break;
        }


        if (data._equippedWeapon != data._slot1_Weapon && data._equippedWeapon != data._slot2_Weapon && data._equippedWeapon != data._slot3_Weapon)
        {
            data._equippedWeapon = _noWeapon;
        }

        data.ShowWeaponObj();
        BtnCommonFunction(_weaponBoxBtn.gameObject);
    }

    private void BtnCommonFunction(GameObject selectObj)
    {
        EventSystem.current.SetSelectedGameObject(selectObj);
        gameObject.SetActive(false);
    }

}
