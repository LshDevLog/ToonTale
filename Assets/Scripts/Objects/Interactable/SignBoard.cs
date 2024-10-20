using UnityEngine;
using UnityEngine.Localization;

public class SignBoard : MonoBehaviour, IInteractable
{
    PlayerInteraction _playerInteraction;

    public LocalizedString _localizedStr;

    private bool _isActive = false;

    private void Awake()
    {
        _playerInteraction = FindAnyObjectByType<PlayerInteraction>();
    }

    private void Update()
    {
        if(_isActive && InputManager.Instance._INTERACTION_KEY)
        {
            _isActive = false;
            var dia = DialogueManager.Instance;
            dia.ClearDialogue();
            PlayerCore.Instance.eSTATE = PlayerCore.STATE.Normal;
        }
    }
    public void OnInteract()
    {
        if(PlayerCore.Instance.eSTATE == PlayerCore.STATE.Normal || PlayerCore.Instance.eSTATE == PlayerCore.STATE.Movement)
        {
            PlayerCore.Instance.eSTATE = PlayerCore.STATE.Interact;
            var dia = DialogueManager.Instance;
            dia.OpenBox();
            dia.ShowDialogueText(_localizedStr, 0.2f);
            Invoke("IsActive", 0.5f);
        }
    }

    public void IsActive()
    {
        _isActive = true;
    }
}
