using UnityEngine;
using UnityEngine.UI;

public class SetSlider : MonoBehaviour
{
    Slider _slider;

    private const string BGM_SLIDER = "Slider - BGM";
    private const string BGM_VOL = "BgmVol";
    private const string SFX_VOL = "SfxVol";

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void Start()
    {
        if (gameObject.name.Equals(BGM_SLIDER))
        {
            SoundManager.Instance.InitVol(_slider, BGM_VOL);
            _slider.onValueChanged.AddListener(SoundManager.Instance.SetBgmVol);
        }
        else
        {
            SoundManager.Instance.InitVol(_slider, SFX_VOL);
            _slider.onValueChanged.AddListener(SoundManager.Instance.SetSfxVol);
        }
    }
}
