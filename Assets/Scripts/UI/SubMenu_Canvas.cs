using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SubMenu_Canvas : MonoBehaviour
{
    public static SubMenu_Canvas Instance { get; private set; }

    private const string MAIN_MENU_SCENE_NAME = "MainMenu";
    private const string OPENING_SCENE_NAME = "Opening";

    [SerializeField]
    private GameObject _panel;

    [SerializeField]
    private GameObject[] _uiBoxes;

    [SerializeField]
    private Button _backpackBtn, _optionsBtn, _quitBtn;

    [SerializeField]
    private Button[] _backPackSlots;

    private string _sceneName;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        _quitBtn.onClick.AddListener(() => BackToMainMenu().Forget());
    }

    private void Update()
    {
        DetectScene();

        if (_sceneName.Equals(MAIN_MENU_SCENE_NAME) || _sceneName.Equals(OPENING_SCENE_NAME))
            return;


        if(PlayerCore.Instance != null)
        {
            if(PlayerCore.Instance.eSTATE != PlayerCore.STATE.Storage)
            {
                ActivateSubMenu();
            }
        }
    }

    private void DetectScene()
    {
        if (_sceneName != SceneManager.GetActiveScene().name)
        {
            _panel.SetActive(false);
            _sceneName = SceneManager.GetActiveScene().name;
        }

    }

    private void ActivateSubMenu()
    {
        if (InputManager.Instance != null && InputManager.Instance._ESC_KEY)
        {
            if(PlayerCore.Instance.eSTATE != PlayerCore.STATE.Normal && !_panel.activeSelf)
            {
                return;
            }

            _panel.SetActive(!_panel.activeSelf);

            if (_panel.activeSelf)
            {
                PlayerCore.Instance.eSTATE = PlayerCore.STATE.SubMenu;
                EventSystem.current.SetSelectedGameObject(_backpackBtn.gameObject);
                InitBackpack();
                foreach (var box in _uiBoxes)
                {
                    box.SetActive(false);
                }
                _uiBoxes[0].SetActive(true);
            }
            else
            {
                PlayerCore.Instance.eSTATE = PlayerCore.STATE.Normal;
                foreach (var box in _uiBoxes)
                {
                    box.SetActive(false);
                }
                _uiBoxes[0].SetActive(true);

            }
        }
    }

    private async UniTask BackToMainMenu()
    {
        if(LoadSceneCanvas.Instance != null)
        {
            await LoadSceneCanvas.Instance.CloseScene();
        }
        SceneManager.LoadScene(MAIN_MENU_SCENE_NAME);
    }


    public void InitBackpack()
    {
        var data = DataManager.Instance._equipmentData;

        if (data._consumableBox != null || data._consumableBox.Count > 0)
        {
            List<Consumable_Item> items = data._consumableBox;

            foreach (Button btn in _backPackSlots)
            {
                btn.gameObject.SetActive(false);
            }

            for (int i = 0; i < items.Count; ++i)
            {
                _backPackSlots[i].gameObject.SetActive(true);
                _backPackSlots[i].GetComponent<BackpackSlot>().SetSlot(items[i]);
            }
        }
    }
}
