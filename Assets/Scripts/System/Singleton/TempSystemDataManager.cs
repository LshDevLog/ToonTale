using UnityEngine;

public class TempSystemDataManager : MonoBehaviour
{
    public static TempSystemDataManager Instance;

    public bool _activateCompanyLogo = true;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
