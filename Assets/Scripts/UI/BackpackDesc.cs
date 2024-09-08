using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BackpackDesc : MonoBehaviour
{
    [SerializeField]
    SubMenu_Canvas _subMenuCanvas;

    [SerializeField]
    Consumable_Item _item;

    [SerializeField]
    TextMeshProUGUI _nameText, _descText;

    [SerializeField]
    Image _itemImg;

    [SerializeField]
    Button _useBtn, _backBtn, _backpackBtn;

    [SerializeField]
    int _itemSlotIdx;

    private const string REDWINE = "RedWine";
    private const string CHEESE = "Cheese";

    private void Awake()
    {
        _subMenuCanvas = GetComponentInParent<SubMenu_Canvas>();
    }

    private void Start()
    {
        _backBtn.onClick.AddListener(BackBtn);
    }
    private void OnEnable()
    {
        if(_useBtn != null)
        {
            _useBtn.onClick.RemoveAllListeners();
            _useBtn.onClick.AddListener(InitUseBtn);
            EventSystem.current.SetSelectedGameObject(_useBtn.gameObject);
        }
    }

    public void SetDesc(ItemBase item, int idx)
    {
        if(item == null)
        {
            return;
        }
        _item = (Consumable_Item)item;
        _itemImg.sprite = item.icon;
        _nameText.text = item.name;
        _descText.text = item.description;
        _itemSlotIdx = idx;
    }

    private void InitUseBtn()
    {
        if(_item == null)
        {
            return;
        }

        if(_item.itemName.Equals(REDWINE))
        {
            PlayerCore.Instance._hp += 2;
        }
        else if(_item.itemName.Equals(CHEESE))
        {
            PlayerCore.Instance._mp = PlayerCore.Instance._mpMax;
        }

        DataManager.Instance._equipmentData._consumableBox.RemoveAt(_itemSlotIdx);
        _subMenuCanvas.InitBackpack();
        EventSystem.current.SetSelectedGameObject(_backpackBtn.gameObject);
        gameObject.gameObject.SetActive(false);
    }


    private void BackBtn()
    {
        EventSystem.current.SetSelectedGameObject(_backpackBtn.gameObject);
        gameObject.SetActive(false);
    }
}
