using UnityEngine;
using UnityEngine.SceneManagement;

public class TempDataManager : MonoBehaviour
{
    public static TempDataManager Instance;

    //System Temp=====================================
    public bool _activateCompanyLogo = true;
    //================================================

    //Player Temp=====================================
    public float _curHP;
    public float _curMP;
    public float _curShield;
    public float _curDodge;
    public float _curSlot1Cool;
    public float _curSlot2Cool;
    public float _curSlot3Cool;
    public bool _plankBuff = false;
    public bool _plankFire = false;
    //================================================
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

    public void InitTempData()
    {
        _plankBuff = false;
        _plankFire = false;
    }
}
