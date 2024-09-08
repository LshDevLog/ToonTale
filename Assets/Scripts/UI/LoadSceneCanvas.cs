using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneCanvas : MonoBehaviour
{
    public static LoadSceneCanvas Instance {  get; private set; }

    [SerializeField]
    private GameObject _top, _btm;

    [SerializeField]
    private string _sceneName;

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

        _sceneName = SceneManager.GetActiveScene().name;
    }

    private void Update()
    {
        UpdateSceneName();
    }

    public async UniTask CloseAndOpen()
    {
        await CloseScene();
        await OpenScene();
    }

    public async UniTask OpenScene()
    {
        _ = _top.transform.DOLocalMoveY(810, 0.2f).SetEase(Ease.OutBounce).ToUniTask();
        _ = _btm.transform.DOLocalMoveY(-810, 0.2f).SetEase(Ease.OutBounce).ToUniTask();

        await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
    }

    public async UniTask CloseScene()
    {
        _ = _top.transform.DOLocalMoveY(270, 0.2f).SetEase(Ease.OutBounce).ToUniTask();
        _ = _btm.transform.DOLocalMoveY(-270, 0.2f).SetEase(Ease.OutBounce).ToUniTask();

        await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
    }

    private void UpdateSceneName()
    {
        if(SceneManager.GetActiveScene().name != _sceneName)
        {
            OpenScene().Forget();
            _sceneName = SceneManager.GetActiveScene().name;
        }
    }
}
