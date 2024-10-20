using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    private Animator _anim;

    [SerializeField]
    private WeaponBase[] _weaponBases;

    private float _neededMp = 0f;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(Equipment.Instance._equippedWeapon == null || PlayerCore.Instance == null || InputManager.Instance == null)
        {
            return;
        }

        _neededMp = Equipment.Instance._equippedWeapon.skillMp;

        DoSkill();
    }

    private bool CalculateCoolTime()
    {
        var equip = Equipment.Instance;
        var mainUI = Main_UI_Canvas.Instance;

        Slider slotSlider = GetSlotSlider(equip._equippedWeapon);

        if (slotSlider != null && equip._equippedWeapon.skillCoolTime <= slotSlider.value)
        {
            slotSlider.value = 0;
            return true;
        }
        return false;
    }

    private Slider GetSlotSlider(Weapon_Item equippedWeapon)
    {
        var equip = Equipment.Instance;
        var mainUI = Main_UI_Canvas.Instance;

        if (equippedWeapon.itemName == equip._slot1_Weapon.itemName)
        {
            return mainUI._1SlotSlider;
        }
        else if (equippedWeapon.itemName == equip._slot2_Weapon.itemName)
        {
            return mainUI._2SlotSlider;
        }
        else if (equippedWeapon.itemName == equip._slot3_Weapon.itemName)
        {
            return mainUI._3SlotSlider;
        }
        return null;
    }

    private void DoSkill()
    {
        if (InputManager.Instance._SKILL_KEY)
        {
            if (PlayerCore.Instance.eSTATE == PlayerCore.STATE.Normal && Equipment.Instance._equippedWeapon !=  Equipment.Instance._noWeapon && PlayerCore.Instance._mp >= _neededMp)
            {
                for (int i = 0; i < _weaponBases.Length; i++)
                {
                    if (_weaponBases[i].gameObject.activeSelf)
                    {
                        if (CalculateCoolTime())
                        {
                            _weaponBases[i].DoSkill();
                        }
                    }
                }
            }
        }
    }
}
