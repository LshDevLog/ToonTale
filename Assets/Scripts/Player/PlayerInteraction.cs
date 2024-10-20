using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    public GameObject _detectedObj;

    [SerializeField]
    private Image _interactionUI;

    private bool _isChecking;

    private const string INTERACTABLE_TAG = "Interactable";

    private void Start()
    {
        _isChecking = false;
    }

    private void Update()
    {
        if (_isChecking && InputManager.Instance._INTERACTION_KEY)
        {
            _detectedObj.GetComponent<IInteractable>().OnInteract();
            EmptyDetectedObj();
        }

        SetInteractionUI();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(INTERACTABLE_TAG))
        {
            PutDetectedObjIn(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(INTERACTABLE_TAG))
        {
            EmptyDetectedObj();
        }
    }

    private void SetInteractionUI()
    {
        Vector2 vec = (_detectedObj != null) ? Vector2.one : Vector2.zero;
        _interactionUI.transform.localScale = vec;
    }
    public void PutDetectedObjIn(GameObject obj)
    {
        _isChecking = true;
        _detectedObj = obj;
    }

    private void EmptyDetectedObj()
    {
        _isChecking = false;
        _detectedObj = null;
    }
}
