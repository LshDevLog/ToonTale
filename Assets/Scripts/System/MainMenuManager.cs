using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public Image _title, _logo, _blackScreen;
    
    public Button _newGameBtn, _loadGameBtn, _optionsBtn, _quitBtn, _lanBtn, _optionBackBtn, _newGameConfirmBackBtn, _newGameStartBtn;

    public Slider _bgmSlider, _sfxSlider;

    public GameObject _defaultBtnPanel, _optionsPanel, _confirmPanel;

    public TextMeshProUGUI _warningTxt, _nodataTxt;

    public StandaloneInputModule _standalone;

    public Weapon_Item[] _tempWeapons;//Temp
    public Shield_Item[] _tempShields;//Temp
    public Weapon_Item _emptyWeapon;
    public Shield_Item _emptyShield;

    private void Start()
    {
        SetOpening();
    }

    private async void SetOpening()
    {
        if (TempSystemDataManager.Instance._activateCompanyLogo)
        {
            await _logo.DOFade(1, 0.7f);
            await _logo.DOFade(0, 0.7f);
            LoadSceneCanvas.Instance.CloseAndOpen().Forget();
            await UniTask.Delay(TimeSpan.FromSeconds(0.4f));
            _logo.gameObject.SetActive(false);
        }

        _blackScreen.gameObject.SetActive(false);
        _defaultBtnPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(_newGameBtn.gameObject);
    }

    public void BackDefaultBtnSet()
    {
        EventSystem.current.SetSelectedGameObject(_newGameBtn.gameObject);
        _nodataTxt.gameObject.SetActive(false);
    }

    public void GoToOptionSet()
    {
        EventSystem.current.SetSelectedGameObject(_bgmSlider.gameObject);
    }

    public void GoToNewGameConfirmSet()
    {
        EventSystem.current.SetSelectedGameObject(_newGameConfirmBackBtn.gameObject);
    }

    public void NewGameStart()
    {
        NewStartGameCo().Forget();
    }

    private async UniTask NewStartGameCo()
    {
        if(DataManager.Instance != null)
        {
            DataManager.Instance.SaveData();
        }

        _title.gameObject.SetActive(false);
        _confirmPanel.SetActive(false);
        _defaultBtnPanel.SetActive(false);
        TempSystemDataManager.Instance._activateCompanyLogo = false;
        await Camera.main.transform.DOMoveZ(27, 1.5f).SetEase(Ease.Linear);
        InitData();//Temp
        SceneManager.LoadScene("Opening");
    }

    private async UniTask StartGameCo()
    {
        _confirmPanel.SetActive(false);
        _defaultBtnPanel.SetActive(false);
        TempSystemDataManager.Instance._activateCompanyLogo = false;
        await LoadSceneCanvas.Instance.CloseScene();
        DataManager.Instance.LoadData();//Temp
        SceneManager.LoadScene("ToonWorld");
    }

    public void LoadGame()
    {
        if(DataManager.Instance != null)
        {
            if (DataManager.Instance.ExistingSaveData())
            {
                StartGameCo().Forget();
            }
            else
            {
                Nodata();
            }
        }
    }

    private void Nodata()
    {
        _nodataTxt.text = "No data";
        _nodataTxt.gameObject.SetActive(true);
    }

    public void QuitBtn()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void InitData()//Temp
    {
        if(DataManager.Instance != null)
        {
            var data = DataManager.Instance._equipmentData;

            data._equippedShield = _emptyShield;
            data._equippedWeapon = _emptyWeapon;
            data._slot1_Weapon = _emptyWeapon;
            data._slot2_Weapon = _emptyWeapon;
            data._slot3_Weapon = _emptyWeapon;

            data._weaponBox.Clear();
            data._shieldBox.Clear();
            data._consumableBox.Clear();

            DataManager.Instance.SaveData();
        }
    }
}
