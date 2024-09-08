using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class GetShield : MonoBehaviour, IInteractable
{
    [SerializeField]
    private Shield_Item _shield;

    [SerializeField]
    private AudioClip _getClip;

    private void Start()
    {
        for (int i = 0; i < DataManager.Instance._equipmentData._shieldBox.Count; ++i)
        {
            if (_shield == DataManager.Instance._equipmentData._shieldBox[i])
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void OnInteract()
    {
        GetItemEvent().Forget();
    }

    private async UniTask GetItemEvent()
    {
        if (_shield == null || DataManager.Instance == null)
        {
            return;
        }

        var data = DataManager.Instance._equipmentData._shieldBox;
        for (int i = 0; i < data.Count; i++)
        {
            if (data[i].itemName == _shield.itemName)
            {
                return;
            }
        }

        data.Add(_shield);
        SoundManager.Instance.PlaySfx(_getClip);
        await transform.DOMoveY(transform.position.y + 1f, 0.2f).SetEase(Ease.OutBounce);
        await transform.DOScale(0, 0.2f).SetEase(Ease.InBounce);
        gameObject.SetActive(false);
    }
}
