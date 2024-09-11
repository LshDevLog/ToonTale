using System;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private const string INTERACTABLE_TAG = "Interactable";

    private GameObject _detectedObj;

    private bool _isChecking;

    private void Start()
    {
        _isChecking = false;
    }


    private void Update()
    {
        if (_isChecking)
        {
            if (InputManager.Instance._INTERACTION_KEY)
            {
                _detectedObj.GetComponent<IInteractable>().OnInteract();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(INTERACTABLE_TAG))
        {
            _isChecking = true;
            _detectedObj = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(INTERACTABLE_TAG))
        {
            _isChecking = false;
            _detectedObj = null;
        }
    }
}
