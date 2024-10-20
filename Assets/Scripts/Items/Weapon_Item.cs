using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Item"), Serializable]
public class Weapon_Item : ItemBase
{
    public float attack;
    public float skillMp;
    public float skillCoolTime;
}
