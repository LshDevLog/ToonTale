using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using System;

public class MysteriousObj : MonoBehaviour, IInteractable
{
    [SerializeField]
    GameObject[] _topObj;

    [SerializeField]
    MeshRenderer[] _red;

    [SerializeField]
    Material[] _materials;

    [SerializeField]
    AudioClip _activationClip;

    public void OnInteract()
    {
        ActivateObj().Forget();
    }

    async UniTask ActivateObj()
    {
        for (int i = 0; i < _topObj.Length; i++)
        {
            _ = _topObj[i].transform.DORotate(new Vector3(-90, 0, -180), 1.0f).SetEase(Ease.Linear);
        }

        await UniTask.Delay(TimeSpan.FromSeconds(1.5f));

        for (int i = 0; i < _topObj.Length; i++)
        {
            _ = _topObj[i].transform.DOMoveY(0, 0.5f).SetEase(Ease.Linear);
        }

        await UniTask.Delay(TimeSpan.FromSeconds(0.7f));

        for (int i = 0; i < _red.Length; i++)
        {
            _red[i].material = _materials[1];
        }

    }
}
