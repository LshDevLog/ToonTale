using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;

public class WeaponSlot : MonoBehaviour
{
    private Button _btn;

    public Weapon_Item _weapon;

    public Image _img;
    public string _name;
    public LocalizedString _nameLocalKey;
    public LocalizedString _descLocalKey;

    private void Awake()
    {
        _btn = GetComponent<Button>();
    }

    private void Start()
    {
        _btn.onClick.AddListener(ClickBtn);
    }

    private void ClickBtn()
    {

    }

    public void SetSlot(string item)
    {
        if (item == null)
        {
            return;
        }

        Weapon_Item itemData = ScriptableObjectBox.Instance.GetWeaponData(item);

        _name = itemData.name;
        _nameLocalKey = itemData.itemNameLocalKey;
        _img.sprite = itemData.icon;
        _descLocalKey = itemData.descLocalKey;
    }
}
