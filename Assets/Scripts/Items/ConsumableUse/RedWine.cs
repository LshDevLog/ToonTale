using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Red Wine"), Serializable]
public class RedWine : Consumable_Item, IUseable
{
    public void Use()
    {
        throw new NotImplementedException();
    }
}
