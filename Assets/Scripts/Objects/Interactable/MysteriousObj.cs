using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using System;

public class MysteriousObj : MonoBehaviour, IInteractable
{
    [SerializeField]
    private GameObject[] _topObj;

    [SerializeField]
    private MeshRenderer[] _red;

    [SerializeField]
    private Material[] _materials;

    [SerializeField]
    private AudioClip _activationClip;

    public void OnInteract()
    {
        Activatiton().Forget();
    }

    private async UniTask Activatiton()
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
