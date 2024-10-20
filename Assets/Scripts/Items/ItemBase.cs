using System;
using UnityEngine;
using UnityEngine.Localization;

[Serializable]
public abstract class ItemBase : ScriptableObject
{
    public string itemName;
    public LocalizedString itemNameLocalKey;
    public LocalizedString descLocalKey;
    public Sprite icon;
}


interface IUseable
{
    public void Use();
}
