using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance {  get; private set; }

    private const string BgmVolKey = "BgmVol";
    private const string SfxVolKey = "SfxVol";

    [SerializeField]
    private AudioSource _bgmSrc, _sfxSrc;

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

    public void InitVol(Slider slider, string volumeKey)
    {
        if (slider == null)
            return;

        if (PlayerPrefs.HasKey(volumeKey))
        {
            slider.value = PlayerPrefs.GetFloat(volumeKey);
        }
        else
        {
            slider.value = 0.5f;
            PlayerPrefs.SetFloat(volumeKey, slider.value);
        }
    }

    public void SetBgmVol(float vol)
    {
        if (_bgmSrc != null)
        {
            _bgmSrc.volume = vol;
            PlayerPrefs.SetFloat(BgmVolKey, vol);
        }
    }

    public void SetSfxVol(float vol)
    {
        if (_sfxSrc != null)
        {
            _sfxSrc.volume = vol;
            PlayerPrefs.SetFloat(SfxVolKey, vol);
        }
    }

    
    public void PlaySfx(AudioClip clip)
    {
        if (clip == null || _sfxSrc == null)
            return;

        float vol = PlayerPrefs.GetFloat(SfxVolKey);
        _sfxSrc.PlayOneShot(clip, vol);
    }
}
