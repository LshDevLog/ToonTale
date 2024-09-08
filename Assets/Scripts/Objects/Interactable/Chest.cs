using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    Collider _col;

    [SerializeField]
    GameObject[] _top;

    [SerializeField]
    GameObject _itemPrefab;

    [SerializeField]
    Consumable_Item _item;

    [SerializeField]
    AudioClip _openClip, _getItemClip;

    private const float OPEN_TOP_X_VALUE = -200f;

    private bool _opened = false;
    private void Awake()
    {
        _col = GetComponent<Collider>();
    }

    public void OnInteract()
    {
        GetItemEvent().Forget();
    }


    private async UniTask GetItemEvent()
    {
        if (_item == null || DataManager.Instance == null || PlayerCore.Instance.eSTATE == PlayerCore.STATE.SubMenu || DataManager.Instance._equipmentData._consumableBox.Count >= 8 || _opened)
        {
            return;
        }

        _col.enabled = false;
        _opened = true;

        DataManager.Instance._equipmentData._consumableBox.Add(_item);

        for (int i = 0; i < _top.Length; i++)
        {
            _ = _top[i].transform.DOLocalRotate(new Vector3(OPEN_TOP_X_VALUE, 0, 0), 0.4f).SetEase(Ease.OutBounce);
        }
        SoundManager.Instance.PlaySfx(_openClip);
        await UniTask.Delay(TimeSpan.FromSeconds(0.3f));
        SoundManager.Instance.PlaySfx(_getItemClip);
        await UniTask.Delay(TimeSpan.FromSeconds(0.2f));
        GameObject itemObj = Instantiate(_itemPrefab, transform.position + Vector3.up, Quaternion.identity);
        await itemObj.transform.DOScale(2f, 0.2f).SetEase(Ease.OutBounce);
        await UniTask.Delay(TimeSpan.FromSeconds(1.5f));
        await itemObj.transform.DOScale(0f, 0.2f).SetEase(Ease.InOutBounce);
        itemObj.gameObject.SetActive(false);
    }
}
