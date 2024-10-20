using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Sub_Menu : MonoBehaviour
{
    [SerializeField]
    private GameObject _subMenuMainPanel;

    [SerializeField]
    private GameObject[] _panels;

    [SerializeField]
    public Button _weaponBtn, _backpackBtn, _quitBtn;

    [SerializeField]
    private Button[] _weaponSlotBtns, _backpackSlotBtns;

    private void Start()
    {
        _quitBtn.onClick.AddListener(QuitGame);
    }

    private void Update()
    {
        ActivateSubMenuPanel();
    }


    private void ActivateSubMenuPanel()
    {
        if (Input.GetButtonDown("Esc"))
        {
            var curState = PlayerCore.Instance.eSTATE;
            if (curState == PlayerCore.STATE.Normal)
            {
                OpenSubMenu();
            }
            else if(curState == PlayerCore.STATE.SubMenu)
            {
                CloseSubMenu();
            }
        }
    }

    private void OpenSubMenu()
    {
        _subMenuMainPanel.SetActive(true);
        PlayerCore.Instance.eSTATE = PlayerCore.STATE.SubMenu;
        EventSystem.current.SetSelectedGameObject(_weaponBtn.gameObject);
        SetWeaponSlots();
        SetBackpackSlots();
    }

    private void CloseSubMenu()
    {
        _subMenuMainPanel.SetActive(false);
        PlayerCore.Instance.eSTATE = PlayerCore.STATE.Normal;
        ResetPanels();
    }

    private void QuitGame()
    {
        if(Main_UI_Canvas.Instance != null)
        {
            Main_UI_Canvas.Instance.InstantResetSaveAlarm();
        }
        SceneManager.LoadScene("MainMenu");
    }

    private void ResetPanels()
    {
        for (int i = 0; i < _panels.Length; i++)
        {
            _panels[i].SetActive(false);
        }
        _panels[0].SetActive(true);
    }

    public void SetWeaponSlots()
    {
        var data = DataManager.Instance._equipmentData;

        if (data._weaponBox != null || data._weaponBox.Count > 0)
        {
            List<string> items = data._weaponBox;

            foreach (Button btn in _weaponSlotBtns)
            {
                btn.gameObject.SetActive(false);
            }

            for (int i = 0; i < items.Count && i < items.Count; ++i)
            {
                _weaponSlotBtns[i].gameObject.SetActive(true);
                _weaponSlotBtns[i].GetComponent<WeaponSlot>().SetSlot(items[i]);
            }
        }
    }

    public void SetBackpackSlots()
    {
        var data = DataManager.Instance._equipmentData;

        if (data._consumableBox != null || data._consumableBox.Count > 0)
        {
            List<string> items = data._consumableBox;

            foreach (Button btn in _backpackSlotBtns)
            {
                btn.gameObject.SetActive(false);
            }

            for (int i = 0; i < items.Count && i < items.Count; ++i)
            {
                _backpackSlotBtns[i].gameObject.SetActive(true);
                _backpackSlotBtns[i].GetComponent<BackpackSlot>().SetSlot(items[i]);
            }
        }
    }
}
