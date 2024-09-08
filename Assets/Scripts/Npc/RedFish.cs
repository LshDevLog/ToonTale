using UnityEngine;

public class RedFish : MonoBehaviour, IInteractable
{
    private Storage _storagePanel;

    private void Awake()
    {
        _storagePanel = FindAnyObjectByType<Storage>();
    }

    private void Start()
    {
        if(_storagePanel != null && _storagePanel.gameObject.activeSelf)
        {
            _storagePanel.gameObject.SetActive(false);
        }
    }

    public void OnInteract()
    {
        if (_storagePanel != null && PlayerCore.Instance != null)
        {
            _storagePanel.gameObject.SetActive(true);
            _storagePanel.InitStorage();
            PlayerCore.Instance.IsInteracting = true;
        }
    }
}
