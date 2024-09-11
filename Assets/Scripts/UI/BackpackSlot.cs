using UnityEngine;
using UnityEngine.UI;

public class BackpackSlot : MonoBehaviour
{
    private Button _slotBtn;

    [HideInInspector]
    public Consumable_Item _item;

    [SerializeField]
    private Image _img;

    [SerializeField]
    private BackpackDescPanel _descPanel;

    private void Awake()
    {
        _slotBtn = GetComponent<Button>();
    }

    private void Start()
    {
        _slotBtn.onClick.AddListener(OpenDescPanel);
    }
    public void SetSlot(Consumable_Item item)
    {
        _item = item;
        _img.sprite = _item.icon;
    }

    private void OpenDescPanel()
    {
        _descPanel.gameObject.SetActive(true);
        _descPanel.SetPanel(_item, int.Parse(gameObject.name));
    }
}
