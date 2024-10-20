using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class LanguageManager : MonoBehaviour
{
    public static LanguageManager Instance;

    [SerializeField]
    private int _idx = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        _idx = (PlayerPrefs.HasKey("Lan")) ? PlayerPrefs.GetInt("Lan") : 0;
    }

    private void Start()
    {
        var availableLocales = LocalizationSettings.AvailableLocales.Locales;
        LocalizationSettings.SelectedLocale = availableLocales[_idx];
    }

    public void ChangeLan()
    {
        var availableLocales = LocalizationSettings.AvailableLocales.Locales;

        _idx = (_idx < (availableLocales.Count - 1)) ? ++_idx : 0;

        LocalizationSettings.SelectedLocale = availableLocales[_idx];

        PlayerPrefs.SetInt("Lan", _idx);
    }
}
