using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Sub_Menu : MonoBehaviour
{
    [SerializeField]
    GameObject _subMenuMainPanel, _descPanel;

    [SerializeField]
    Button _backpackBtn, _quitBtn;

    [SerializeField]
    BackpackSlot[] _backpackSlots;

    private void Start()
    {
        _quitBtn.onClick.AddListener(QuitGame);
    }

    private void Update()
    {
        ActivateSubMenuPanel();

        var data = DataManager.Instance._equipmentData._consumableBox;
    }


    private void ActivateSubMenuPanel()
    {
        if (Input.GetButtonDown("Esc"))
        {
            _subMenuMainPanel.SetActive(!_subMenuMainPanel.activeSelf);
            _descPanel.SetActive(false);
            if (_subMenuMainPanel.activeSelf)
            {
                PlayerCore.Instance.eSTATE = PlayerCore.STATE.SubMenu;
                EventSystem.current.SetSelectedGameObject(_backpackBtn.gameObject);
                SetBackpackSlots();
            }
            else
            {
                PlayerCore.Instance.eSTATE = PlayerCore.STATE.Normal;
            }
        }
    }

    public void SetBackpackSlots()
    {
        for (int i = 0; i < _backpackSlots.Length; i++)
        {
            _backpackSlots[i]._item = null;
            _backpackSlots[i].gameObject.SetActive(false);
        }

        List<Consumable_Item> data = DataManager.Instance._equipmentData._consumableBox;

        for (int i = 0; i < data.Count; i++)
        {
            _backpackSlots[i].gameObject.SetActive(true);
            _backpackSlots[i].SetSlot(data[i]);
        }
    }


    private void QuitGame()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
