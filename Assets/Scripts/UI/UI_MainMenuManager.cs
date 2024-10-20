using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_MainMenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _defaultBtnPanel, _optionsPanel, _confirmPanel;

    [SerializeField]
    private Image _title, _logo, _blackScreen;

    [SerializeField]
    private Button _newGameBtn, _loadGameBtn, _optionsBtn, _quitBtn, _lanBtn, _optionBackBtn, _newGameConfirmBackBtn, _newGameStartBtn;

    [SerializeField]
    private Slider _bgmSlider, _sfxSlider;

    [SerializeField]
    private TextMeshProUGUI _warningTxt, _nodataTxt;

    private void Start()
    {
        InitBtns();
    }

    private void InitBtns()
    {
        _optionsBtn.onClick.AddListener(GoToOptionSet);
        _optionBackBtn.onClick.AddListener(BackDefaultBtnSet);
        _newGameConfirmBackBtn.onClick.AddListener(BackDefaultBtnSet);
        _newGameBtn.onClick.AddListener(GoToNewGameConfirmSet);
        _quitBtn.onClick.AddListener(QuitBtn);
        _lanBtn.onClick.AddListener(LanguageManager.Instance.ChangeLan);
    }

    private void BackDefaultBtnSet()
    {
        EventSystem.current.SetSelectedGameObject(_newGameBtn.gameObject);
        _nodataTxt.gameObject.SetActive(false);
    }

    private void GoToOptionSet()
    {
        EventSystem.current.SetSelectedGameObject(_bgmSlider.gameObject);
    }

    private void GoToNewGameConfirmSet()
    {
        EventSystem.current.SetSelectedGameObject(_newGameConfirmBackBtn.gameObject);
    }

    public void Nodata()
    {
        _nodataTxt.gameObject.SetActive(true);
    }

    private void QuitBtn()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void SetUI_AfterOpening()
    {
        _blackScreen.gameObject.SetActive(false);
        _defaultBtnPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(_newGameBtn.gameObject);
    }
    public async UniTask FadeLogo()
    {
        await _logo.DOFade(1, 0.7f);
        await _logo.DOFade(0, 0.7f);
    }

    public void StartAnd_UI_Inactive()
    {
        _title.gameObject.SetActive(false);
        _confirmPanel.SetActive(false);
        _defaultBtnPanel.SetActive(false);
    }
}
