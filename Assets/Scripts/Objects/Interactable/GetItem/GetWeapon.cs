using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class GetWeapon : MonoBehaviour, IInteractable
{
    [SerializeField]
    private Weapon_Item _weapon;

    [SerializeField]
    private AudioClip _getClip;

    private void Start()
    {
        for (int i = 0; i < DataManager.Instance._equipmentData._weaponBox.Count; ++i)
        {
            if(_weapon == DataManager.Instance._equipmentData._weaponBox[i])
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
        if(_weapon == null || DataManager.Instance == null)
        {
            return;
        }

        var data = DataManager.Instance._equipmentData._weaponBox;
        for (int i = 0; i < data.Count; ++i)
        {
            if (data[i].itemName == _weapon.itemName)
            {
                return;
            }
        }

        data.Add(_weapon);
        SoundManager.Instance.PlaySfx(_getClip);
        await transform.DOMoveY(transform.position.y + 1f, 0.2f).SetEase(Ease.OutBounce);
        await transform.DOScale(0, 0.2f).SetEase(Ease.InBounce);
        gameObject.SetActive(false);
    }
}
