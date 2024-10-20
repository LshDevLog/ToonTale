using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private UI_MainMenuManager _uiManager;

    public StandaloneInputModule _standalone;

    public Weapon_Item[] _tempWeapons;//Temp
    public Weapon_Item _emptyWeapon;

    private const string NO_WEAPON = "NoWeapon";
    private void Awake()
    {
        _uiManager = FindAnyObjectByType<UI_MainMenuManager>();
    }

    private void Start()
    {
        SetOpening();
    }

    private async void SetOpening()
    {
        if (TempDataManager.Instance._activateCompanyLogo)
        {
            await _uiManager.FadeLogo();
            LoadSceneCanvas.Instance.CloseAndOpen().Forget();
            await UniTask.Delay(TimeSpan.FromSeconds(0.4f));
        }

        _uiManager.SetUI_AfterOpening();
    }

    public void NewGameStart()
    {
        if (TempDataManager.Instance != null)
        {
            TempDataManager.Instance.InitTempData();
        }
        NewStartGameCo().Forget();
    }

    private async UniTask NewStartGameCo()
    {
        if(DataManager.Instance != null)
        {
            DataManager.Instance.SaveData();
        }

        _uiManager.StartAnd_UI_Inactive();
        TempDataManager.Instance._activateCompanyLogo = false;
        await Camera.main.transform.DOMoveZ(27, 1.5f).SetEase(Ease.Linear);
        InitData();//Temp
        SceneManager.LoadScene("Opening");
    }

    private async UniTask LoadGameStartCo()
    {
        _uiManager.StartAnd_UI_Inactive();
        TempDataManager.Instance._activateCompanyLogo = false;
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
                if(TempDataManager.Instance != null)
                {
                    TempDataManager.Instance.InitTempData();
                }
                LoadGameStartCo().Forget();
            }
            else
            {
                _uiManager.Nodata();
            }
        }
    }

    private void InitData()//Temp
    {
        if(DataManager.Instance != null)
        {
            var data = DataManager.Instance;

            data._equipmentData._equippedWeapon = NO_WEAPON;
            data._equipmentData._slot1_Weapon = NO_WEAPON;
            data._equipmentData._slot2_Weapon = NO_WEAPON;
            data._equipmentData._slot3_Weapon = NO_WEAPON;

            data._equipmentData._weaponBox.Clear();
            data._equipmentData._consumableBox.Clear();
            data._mapData._chestIdx.Clear();

            DataManager.Instance.SaveData();
        }
    }
}
