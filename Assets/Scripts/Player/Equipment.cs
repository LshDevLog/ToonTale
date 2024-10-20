using UnityEngine;

public class Equipment : MonoBehaviour
{
    public static Equipment Instance;

    public Weapon_Item _equippedWeapon, _slot1_Weapon, _slot2_Weapon, _slot3_Weapon;

    public Weapon_Item _noWeapon;

    [SerializeField]
    private GameObject[] _weaponObj;

    [SerializeField]
    private AudioClip _changeWeaponClip;

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
    }

    private void Update()
    {
        ChangeEquippedWeapon();
    }


    public void InitEquipment()
    {
        var data = DataManager.Instance._equipmentData;
        var scriptableData = ScriptableObjectBox.Instance._weapons;
        for (int i = 0; i < scriptableData.Length; i++)
        {
            if (data._slot1_Weapon == scriptableData[i].itemName)
            {
                _slot1_Weapon = scriptableData[i];
            }
            if (data._slot2_Weapon == scriptableData[i].itemName)
            {
                _slot2_Weapon = scriptableData[i];
            }
            if (data._slot3_Weapon == scriptableData[i].itemName)
            {
                _slot3_Weapon = scriptableData[i];
            }
            if (data._equippedWeapon == scriptableData[i].itemName)
            {
                _equippedWeapon = scriptableData[i];
            }
        }
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
            if(_equippedWeapon == null || _equippedWeapon.itemName == _noWeapon.itemName)
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
}
