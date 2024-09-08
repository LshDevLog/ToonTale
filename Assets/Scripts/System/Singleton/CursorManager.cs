using UnityEngine;
using UnityEngine.EventSystems;

public class CursorManager : MonoBehaviour
{
    public static CursorManager Instance;

    public GameObject _lastSelectedUI;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (Instance != this)
            {
                Destroy(gameObject);
            }
        }
    }

    private void Update()
    {
        DeleteCusor();
        LastSelectedUI();
        MouseClickReset();
    }

    private void DeleteCusor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void LastSelectedUI()
    {
        if(EventSystem.current.currentSelectedGameObject != null)
        {
            _lastSelectedUI = EventSystem.current.currentSelectedGameObject.gameObject;
        }
    }

    private void MouseClickReset()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
        {
            if (_lastSelectedUI == null)
            {
                return;
            }

            EventSystem.current.SetSelectedGameObject(_lastSelectedUI);
        }
    }
}
