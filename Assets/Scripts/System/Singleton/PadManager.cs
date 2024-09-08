using UnityEngine;

public class PadManager : MonoBehaviour
{
    public static PadManager Instance;

    private bool _padConnected;
    public bool _PadConnected => _padConnected;


    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update()
    {
        CheckConnectedGamePad();
    }

    private void CheckConnectedGamePad()
    {
        string[] pad = Input.GetJoystickNames();
        _padConnected = (pad.Length > 0 && !string.IsNullOrEmpty(pad[0]));
    }
}
