using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class GetWeapon : MonoBehaviour, IInteractable
{
    [SerializeField]
    private Weapon_Item _weapon;

    [SerializeField]
    private AudioClip _getClip;

    private SphereCollider _col;

    private void Awake()
    {
        _col = GetComponent<SphereCollider>();
    }

    private void Start()
    {
        OffThisWeaponIfHavingIt();
    }

    private void OffThisWeaponIfHavingIt()
    {
        for (int i = 0; i < DataManager.Instance._equipmentData._weaponBox.Count; ++i)
        {
            if (_weapon.itemName == DataManager.Instance._equipmentData._weaponBox[i])
            {
                gameObject.SetActive(false);
            }
        }
    }

    private async UniTask GetWeaponAnim()
    {
        await transform.DOMoveY(transform.position.y + 1f, 0.2f).SetEase(Ease.OutBounce);
        await transform.DOScale(0, 0.2f).SetEase(Ease.InBounce);
        gameObject.SetActive(false);
    }

    private void EquipIfSlotEmpty()
    {
        var data = DataManager.Instance._equipmentData;
        if (data._slot1_Weapon == Equipment.Instance._noWeapon.itemName)
        {
            data._slot1_Weapon = _weapon.itemName;
            Equipment.Instance.InitEquipment();
            return;
        }
        if (data._slot2_Weapon == Equipment.Instance._noWeapon.itemName)
        {
            data._slot2_Weapon = _weapon.itemName;
            Equipment.Instance.InitEquipment();
            return;
        }
        if (data._slot3_Weapon == Equipment.Instance._noWeapon.itemName)
        {
            data._slot3_Weapon = _weapon.itemName;
            Equipment.Instance.InitEquipment();
            return;
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

        _col.enabled = false;

        var data = DataManager.Instance._equipmentData;
        data._weaponBox.Add(_weapon.itemName);

        SoundManager.Instance.PlaySfx(_getClip);

        await GetWeaponAnim();
        EquipIfSlotEmpty();
    }
}
