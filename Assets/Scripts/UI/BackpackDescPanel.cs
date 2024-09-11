using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BackpackDescPanel : MonoBehaviour
{
    private Sub_Menu _subMenu;

    [SerializeField]
    TextMeshProUGUI _nameText, _descriptionText;

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
    private void Start()
    {
        _useBtn.onClick.AddListener(UseItem);
    }

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(_useBtn.gameObject);
    }

    private void OnDisable()
    {
        EventSystem.current.SetSelectedGameObject(_backpackBtn.gameObject);
    }

    public void SetPanel(Consumable_Item item, int idx)
    {
        _Item = item;
        _nameText.text = item.itemName;
        _descriptionText.text = item.description;
        _img.sprite = item.icon;
        _idx = idx;
    }

    public void UseItem()
    {
        if(_Item != null)
        {
            string item = _Item.itemName;

            if(item == "RedWine")
            {
                ++PlayerCore.Instance._hp;
            }
            else if(item == "Cheese")
            {
                PlayerCore.Instance._mp = PlayerCore.Instance._mpMax;
            }
        }

        DataManager.Instance._equipmentData._consumableBox.RemoveAt(_idx);

        _subMenu.SetBackpackSlots();
    }
}
