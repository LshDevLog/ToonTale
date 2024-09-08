using UnityEngine;
using UnityEngine.UI;

public class BackpackSlot : MonoBehaviour
{
    [SerializeField]
    private Button _btn;

    private ItemBase _item;

    [SerializeField]
    private Image _img;

    [SerializeField]
    private BackpackDesc _descPanel;

    private void Awake()
    {
        _btn = GetComponent<Button>();
    }

    private void Start()
    {
        _btn.onClick.AddListener(OpenDescPanel);
    }

    public void SetSlot(ItemBase item)
    {

        _item = null;
        _img.sprite = null;

        if (item == null)
        {
            return;
        }

        _item = item;
        _img.sprite = _item.icon;
    }

    private void OpenDescPanel()
    {
        if (_item == null || _descPanel == null)
        {
            return;
        }

        _descPanel.gameObject.SetActive(true);
        _descPanel.SetDesc(_item, int.Parse(gameObject.name));
    }
}
