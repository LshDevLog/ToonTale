using System;
using System.Collections.Generic;

[Serializable]
public class PlayerDataBox
{
    public StatsData _statsData;
    public EquipmentData _equipmentData;
    public SystemData _systemData;

    public PlayerDataBox()
    {
        _statsData = new StatsData();
        _equipmentData = new EquipmentData();
        _systemData = new SystemData();

        _statsData._hpMax = 5;
        _statsData._mpMax = 2;

        _equipmentData._equippedWeapon = null;
        _equipmentData._equippedShield = null;
        _equipmentData._slot1_Weapon = null;
        _equipmentData._slot2_Weapon = null;
        _equipmentData._slot3_Weapon = null;
    }
}

[Serializable]
public class StatsData
{
    public int _hpMax, _mpMax;
}

[Serializable]
public class EquipmentData
{
    public Weapon_Item _equippedWeapon, _slot1_Weapon, _slot2_Weapon, _slot3_Weapon;
    public Shield_Item _equippedShield;

    public List<Weapon_Item> _weaponBox;
    public List<Shield_Item> _shieldBox;
    public List<Consumable_Item> _consumableBox;
}

[Serializable]
public class SystemData
{
    public string _savePoint;
}
