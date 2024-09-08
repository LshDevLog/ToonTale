using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Storage : MonoBehaviour
{
    [SerializeField]
    Collider _fishCol;

    [SerializeField]
    private Transform _weaponPanel, _shieldPanel;

    [SerializeField]
    Button[] _weaponBtns, _shieldBtns;

    [SerializeField]
    private Button _weaponBtn, _shieldBtn, _closeBtn;

    private void Awake()
    {
        _fishCol = FindFirstObjectByType<RedFish>().GetComponent<Collider>();
    }
    private void Start()
    {
        _closeBtn.onClick.AddListener(CloseBtn);
    }
    public void InitStorage()
    {
        EventSystem.current.SetSelectedGameObject(_weaponBtn.gameObject);

        var data = DataManager.Instance._equipmentData;

        //Weapon===================================================================
        if (data._weaponBox != null || data._weaponBox.Count > 0)
        {
            List<Weapon_Item> weapons = data._weaponBox;

            foreach (Button btn in _weaponBtns)
            {
                btn.gameObject.SetActive(false);
            }

            for (int i = 0; i < weapons.Count && i < weapons.Count; ++i)
            {
                _weaponBtns[i].gameObject.SetActive(true);
                _weaponBtns[i].GetComponent<StorageSlot>().SetSlot(weapons[i]);
            }
        }
        //=========================================================================

        //Shield===================================================================
        if (data._shieldBox != null || data._shieldBox.Count > 0)
        {
            List<Shield_Item> shields = data._shieldBox;

            foreach (Button btn in _shieldBtns)
            {
                btn.gameObject.SetActive(false);
            }

            for (int i = 0; i < shields.Count && i < shields.Count; ++i)
            {
                _shieldBtns[i].gameObject.SetActive(true);
                _shieldBtns[i].GetComponent<StorageSlot>().SetSlot(shields[i]);
            }
        }
        //=========================================================================

        PlayerCore.Instance.eSTATE = PlayerCore.STATE.Storage;
    }

    private void CloseBtn()
    {
        PlayerCore.Instance.eSTATE = PlayerCore.STATE.Normal;
        _fishCol.enabled = false;
        Invoke("CloseAndReset", 0.1f);
    }

    private void CloseAndReset()
    {
        _fishCol.enabled = true;
        PlayerCore.Instance.IsInteracting = false;
    }
}
