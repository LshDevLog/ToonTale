using UnityEngine;
using UnityEngine.UI;

public class StorageSlot : MonoBehaviour
{
    private Button _btn;

    private ItemBase _item;

    [SerializeField]
    private Image _img;

    [SerializeField]
    private DescPanel _descPanel;

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

        if(item == null)
        {
            return;
        }

        _item = item;
        _img.sprite = _item.icon;
    }

    private void OpenDescPanel()
    {
        if(_item == null || _descPanel == null)
        {
            return;
        }

        _descPanel.gameObject.SetActive(true);
        _descPanel.SetPanel(_item);
    }
}
