using NUnit.Framework;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    public static Equipment Instance;

    public Weapon_Item _equippedWeapon, _slot1_Weapon, _slot2_Weapon, _slot3_Weapon;

    public Shield_Item _equippedShield;

    [SerializeField]
    private Weapon_Item _noWeapon;

    [SerializeField]
    private Shield_Item _noShield;

    [SerializeField]
    private GameObject[] _weaponObj, _shieldObj;

    [SerializeField]
    private AudioClip _changeWeaponClip;

    private const string NO_WEAPON = "NoWeapon";
    private const string NO_SHIELD = "NoShield";

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        InitEquipment();
        ShowWeaponObj();
        ShowShieldObj();
    }

    private void Update()
    {
        ChangeEquippedWeapon();
    }


    private void InitEquipment()
    {
        var data = DataManager.Instance._equipmentData;

        _slot1_Weapon = data._slot1_Weapon ?? _noWeapon;
        _slot2_Weapon = data._slot2_Weapon ?? _noWeapon;
        _slot3_Weapon = data._slot3_Weapon ?? _noWeapon;
        _equippedWeapon = data._equippedWeapon ?? _noWeapon;
        _equippedShield = data._equippedShield ?? _noShield;
    }

    private void ChangeEquippedWeapon()
    {
        if(PlayerCore.Instance == null)
        {
            return;
        }

        var core = PlayerCore.Instance;
        var input = InputManager.Instance;

        if(core.eSTATE == PlayerCore.STATE.Normal || core.eSTATE == PlayerCore.STATE.Movement)
        {
            if (input._Q_KEY)
            {
                _equippedWeapon = (_slot1_Weapon != null) ? _slot1_Weapon : null;
                ShowWeaponObj();
                SoundManager.Instance.PlaySfx(_changeWeaponClip);
            }
            else if (input._W_KEY)
            {
                _equippedWeapon = (_slot2_Weapon != null) ? _slot2_Weapon : null;
                ShowWeaponObj();
                SoundManager.Instance.PlaySfx(_changeWeaponClip);
            }
            else if (input._E_KEY)
            {
                _equippedWeapon = (_slot3_Weapon != null) ? _slot3_Weapon : null;
                ShowWeaponObj();
                SoundManager.Instance.PlaySfx(_changeWeaponClip);
            }
        }
    }


    public void ShowWeaponObj()
    {
        foreach (var item in _weaponObj)
        {
            if(_equippedWeapon == null || _equippedWeapon.itemName == NO_WEAPON)
            {
                item.SetActive(false);
            }
            else
            {
                bool isActive = (_equippedWeapon != null && item.name == _equippedWeapon.itemName);
                item.SetActive(isActive);
            }
        }
    }

    public void ShowShieldObj()
    {
        foreach (var item in _shieldObj)
        {
            if(_equippedShield == null || _equippedShield.itemName == NO_SHIELD)
            {
                item.SetActive(false);
            }
            else
            {
                bool isActive = (_equippedShield != null && item.name == _equippedShield.itemName);
                item.SetActive(isActive);
            }
        }
    }
}
