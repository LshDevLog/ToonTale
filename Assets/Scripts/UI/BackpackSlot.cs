using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;

public class BackpackSlot : MonoBehaviour
{
    private Button _btn;

    public Image _img;
    public string _name;
    public LocalizedString _nameLocalKey;
    public LocalizedString _desLocalKey;

    private void Awake()
    {
        _btn = GetComponent<Button>();
    }

    private void Start()
    {
        _btn.onClick.AddListener(ClickBtn);
    }

    private void OnDisable()
    {
        
    }

    private void ClickBtn()
    {

    }


    public void SetSlot(string item)
    {

        _img.sprite = null;

        if (item == null)
        {
            return;
        }

        Consumable_Item itemData = ScriptableObjectBox.Instance.GetConsumalbeItemData(item);

        _name = itemData.name;
        _nameLocalKey = itemData.itemNameLocalKey;
        _img.sprite = itemData.icon;
        _desLocalKey = itemData.descLocalKey;
    }
}
