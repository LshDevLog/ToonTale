using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Threading;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    private Collider _col;

    private CancellationTokenSource _cts;

    [SerializeField]
    private GameObject[] _top;

    [SerializeField]
    private GameObject _itemPrefab;

    [SerializeField]
    private Consumable_Item _item;


    [SerializeField]
    private AudioClip _openClip, _getItemClip;

    private const float OPEN_TOP_X_VALUE = -200f;

    //Each chest has own idx
    [SerializeField]
    private int _idx;

    private bool _opened = false;
    private float _showItemScale = 2.0f;
    private void Awake()
    {
        _col = GetComponent<Collider>();
    }

    private void Start()
    {
        CheckingThisChestUsedOrNotBeforeByIndex();
    }

    private void Update()
    {
        
    }
    private void OnDestroy()
    {
        if(_cts != null)
        {
            _cts.Cancel();
            _cts.Dispose();
        }
    }


    private void CheckingThisChestUsedOrNotBeforeByIndex()
    {
        var data = DataManager.Instance._mapData._chestIdx;
        for (int i = 0; i < data.Count; i++)
        {
            if (data[i] == _idx)
            {
                OpenChest(0);
            }
        }
    }
    private void OpenChest(float openSpeed)
    {
        for (int i = 0; i < _top.Length; ++i)
        {
            _ = _top[i].transform.DOLocalRotate(new Vector3(OPEN_TOP_X_VALUE, 0, 0), openSpeed).SetEase(Ease.OutBounce);
        }
        _col.enabled = false;
        _opened = true;
    }

    private async UniTask OpenChestSound()
    {
        if(SoundManager.Instance != null)
        {
            SoundManager.Instance.PlaySfx(_openClip);
            await UniTask.Delay(TimeSpan.FromSeconds(0.3f), cancellationToken: _cts.Token);
            SoundManager.Instance.PlaySfx(_getItemClip);
            await UniTask.Delay(TimeSpan.FromSeconds(0.2f), cancellationToken: _cts.Token);
        }
    }

    private async UniTask ShowItem()
    {
        GameObject itemObj = Instantiate(_itemPrefab, transform.position + Vector3.up, Quaternion.identity);
        await itemObj.transform.DOScale(_showItemScale, 0.2f).SetEase(Ease.OutBounce).WithCancellation(_cts.Token);
        await UniTask.Delay(TimeSpan.FromSeconds(1.5f), cancellationToken: _cts.Token);
        await itemObj.transform.DOScale(0f, 0.2f).SetEase(Ease.InOutBounce).WithCancellation(_cts.Token);
        itemObj.SetActive(false);
    }

    private async UniTask GetItemEvent()
    {
        if (_item == null || DataManager.Instance == null || PlayerCore.Instance.eSTATE == PlayerCore.STATE.SubMenu || DataManager.Instance._equipmentData._consumableBox.Count >= 8 || _opened)
        {
            return;
        }

        _cts = new CancellationTokenSource();


        DataManager.Instance._equipmentData._consumableBox.Add(_item.itemName);
        DataManager.Instance._mapData._chestIdx.Add(_idx);

        OpenChest(0.4f);
        await OpenChestSound();
        await ShowItem();
    }

    public void OnInteract()
    {
        GetItemEvent().Forget();
    }

}

