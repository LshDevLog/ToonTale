using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

[Serializable]
public class PlayerDataBox
{
    public StatsData _statsData;
    public EquipmentData _equipmentData;
    public MapData _mapData;
    public SystemData _systemData;

    public PlayerDataBox()
    {
        _statsData = new StatsData();
        _equipmentData = new EquipmentData();
        _systemData = new SystemData();

        _statsData._hpMax = 5;
        _statsData._mpMax = 2;

        _equipmentData._equippedWeapon = null;
        _equipmentData._slot1_Weapon = null;
        _equipmentData._slot2_Weapon = null;
        _equipmentData._slot3_Weapon = null;

        _equipmentData._weaponBox = null;
        _equipmentData._consumableBox = null;
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
    public string _equippedWeapon, _slot1_Weapon, _slot2_Weapon, _slot3_Weapon;

    public List<string> _weaponBox;
    public List<string> _consumableBox;
}

[Serializable]
public class MapData
{
    public List<int> _chestIdx;

}

[Serializable]
public class SystemData
{
    public string _savePoint;
}
