using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

public class BackpackDescPanel : MonoBehaviour
{
    private Sub_Menu _subMenu;

    [SerializeField]
    private LocalizeStringEvent _nameText, _descriptionText;

    Consumable_Item _Item;

    [SerializeField]
    Image _img;

    [SerializeField]
    Button _useBtn, _backpackBtn;

    private int _idx;

    private void Awake()
    {
        _subMenu = GetComponentInParent<Sub_Menu>();
    }

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(_useBtn.gameObject);
    }

    private void OnDisable()
    {
        EventSystem.current.SetSelectedGameObject(_backpackBtn.gameObject);
    }

    public void SetPanel(string item, int idx)
    {
        Consumable_Item itemData = ScriptableObjectBox.Instance.GetConsumalbeItemData(item);
        _Item = itemData;
        _nameText.StringReference = itemData.itemNameLocalKey;
        _descriptionText.StringReference = itemData.descLocalKey;
        _img.sprite = itemData.icon;
        _idx = idx;
    }
}
