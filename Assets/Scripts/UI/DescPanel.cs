using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DescPanel : MonoBehaviour
{
    Shield _shield;

    ItemBase _item;

    [SerializeField]
    Weapon_Item _noWeapon;

    [SerializeField]
    private TextMeshProUGUI _nameText, _descText;

    [SerializeField]
    private Image _img;

    [SerializeField]
    private GameObject _weaponEquipBtns, _shieldEquipBtn;

    [SerializeField]
    private Button _qBtn, _wBtn, _eBtn, _backFromWeaponBtn, _equipShieldBtn,_backFromShieldBtn, _weaponBoxBtn, _shieldBoxBtn;

    private void Awake()
    {
        _shield = FindAnyObjectByType<Shield>();
    }
    private void Start()
    {
        _qBtn.onClick.AddListener(() => PressWeaponSlot(1));
        _wBtn.onClick.AddListener(() => PressWeaponSlot(2));
        _eBtn.onClick.AddListener(() => PressWeaponSlot(3));
        _equipShieldBtn.onClick.AddListener(EquipShield);
        _backFromWeaponBtn.onClick.AddListener(() => BtnCommonFunction(_weaponBoxBtn.gameObject));
        _backFromShieldBtn.onClick.AddListener(() => BtnCommonFunction(_shieldBoxBtn.gameObject));
    }

    public void SetPanel(ItemBase item)
    {
        _item = item;
        _nameText.text = item.itemName;
        _descText.text = item.description;
        _img.sprite = item.icon;

        if (item is Weapon_Item)
        {
            EventSystem.current.SetSelectedGameObject(_weaponEquipBtns.transform.GetChild(0).gameObject);
        }
        else if(item is Shield_Item)
        {
            EventSystem.current.SetSelectedGameObject(_equipShieldBtn.gameObject);
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

    private void EquipShield()
    {
        var data = Equipment.Instance;

        if (data == null)
        {
            return;
        }
        data._equippedShield = (Shield_Item)_item;

        data.ShowShieldObj();
        _shield._durabilityMax = data._equippedShield.durability;
        _shield._durability = _shield._durabilityMax;
        BtnCommonFunction(_shieldBoxBtn.gameObject);
    }

    private void BtnCommonFunction(GameObject selectObj)
    {
        EventSystem.current.SetSelectedGameObject(selectObj);
        gameObject.SetActive(false);
    }

}
