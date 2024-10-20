using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

public class ScriptableObjectBox : MonoBehaviour
{
    public static ScriptableObjectBox Instance;

    public Weapon_Item[] _weapons;
    public Consumable_Item[] _consumable;

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
    }

    public Weapon_Item GetWeaponData(string weaponName)
    {
        for (int i = 0; i < _weapons.Length; i++)
        {
            if (weaponName == _weapons[i].itemName)
            {
                return _weapons[i];
            }
        }
        return null;
    }

    public Consumable_Item GetConsumalbeItemData(string itemName)
    {
        for (int i = 0; i < _consumable.Length; i++)
        {
            if (itemName == _consumable[i].itemName)
            {
                return _consumable[i];
            }
        }

        return null;
    }


}
