using UnityEngine;

public class Shield : MonoBehaviour
{
    public float _durability, _durabilityMax;

    private void Start()
    {
        _durability = _durabilityMax;
    }

    private void Update()
    {
        UpdateShieldSliderValue();

    }

    private void UpdateShieldSliderValue()
    {
        var mainUI = Main_UI_Canvas.Instance;

        if (mainUI != null && mainUI._shieldSlider != null)
        {
            mainUI._shieldSlider.value = _durability;
            mainUI._shieldSlider.maxValue = _durabilityMax;
        }
    }
}
