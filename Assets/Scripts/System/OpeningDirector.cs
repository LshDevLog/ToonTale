using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Threading;
using UnityEngine.Localization;

public class OpeningDirector : MonoBehaviour
{
    private Camera _cam;

    private Animator _drAnim;

    [SerializeField]
    private Image _blackScreen;

    [SerializeField]
    private GameObject _doctor, _redLineBox;

    [SerializeField]
    private Transform _viewTrs_1, _viewTrs_2, _viewTrs_3;

    [SerializeField]
    private MysteriousObj _mysteriousObj;

    [SerializeField]
    private GameObject[] _eyes;

    [SerializeField]
    private TextMeshProUGUI _skipText;

    [SerializeField]
    private LocalizedString _openingA, _openingB, _openingC, _openingD;

    private CancellationTokenSource _cts;

    private bool _isSkiping = false;

    private const string TOON_WORLD_SCENE_NAME = "ToonWorld";

    private void Awake()
    {
        _cam = Camera.main;
        _drAnim = _doctor.GetComponent<Animator>();
        _cts = new CancellationTokenSource();
    }

    private void Start()
    {
        Opening(_cts.Token).Forget();
    }

    private void OnDestroy()
    {
        _cts?.Cancel();
        _cts?.Dispose();
    }

    async UniTask Update()
    {
        if (InputManager.Instance._ESC_KEY && _isSkiping == false)
        {
            _isSkiping = true;
            _cts.Cancel();
            await LoadSceneCanvas.Instance.CloseScene();
            SceneManager.LoadScene(TOON_WORLD_SCENE_NAME);
        }
    }
    async UniTask Opening(CancellationToken token)
    {
        try
        {
            await _blackScreen.DOFade(0, 1f).WithCancellation(token);
            _skipText.gameObject.SetActive(true);
            _drAnim.SetBool("isWalking", true);

            UniTask a = _doctor.transform.DOMoveZ(6.1f, 8f).SetEase(Ease.Linear).ToUniTask(cancellationToken: token);
            UniTask b = _cam.transform.DORotate(new Vector3(17, 0, 0), 8f).SetEase(Ease.Linear).ToUniTask(cancellationToken: token);
            await UniTask.WhenAll(a, b);

            _drAnim.SetBool("isWalking", false);
            _cam.transform.SetLocalPositionAndRotation(_viewTrs_1.position, _viewTrs_1.rotation);
            await UniTask.Delay(TimeSpan.FromSeconds(1f), cancellationToken: token);

            _drAnim.SetTrigger("isReachingOut");
            await UniTask.Delay(TimeSpan.FromSeconds(1.5f), cancellationToken: token);

            _mysteriousObj.GetComponent<IInteractable>().OnInteract();
            await UniTask.Delay(TimeSpan.FromSeconds(1.3f), cancellationToken: token);

            _cam.transform.SetLocalPositionAndRotation(_viewTrs_2.position, _viewTrs_2.rotation);
            await UniTask.Delay(TimeSpan.FromSeconds(1.2f), cancellationToken: token);

            int count = _redLineBox.transform.childCount;
            GameObject[] lines = new GameObject[count];

            for (int i = 0; i < count; i++)
            {
                lines[i] = _redLineBox.transform.GetChild(i).gameObject;
                lines[i].GetComponent<RotateObj>().enabled = false;
            }

            await UniTask.Delay(TimeSpan.FromSeconds(2.0f), cancellationToken: token);

            foreach (var line in lines)
            {
                line.transform.DOScale(0, 0.2f).SetEase(Ease.InBounce).WithCancellation(token);
            }

            await UniTask.Delay(TimeSpan.FromSeconds(3.0f), cancellationToken: token);

            foreach (var eye in _eyes)
            {
                eye.transform.DOScale(10, 2).WithCancellation(token);
            }

            await UniTask.Delay(TimeSpan.FromSeconds(3f), cancellationToken: token);

            _cam.transform.SetLocalPositionAndRotation(_viewTrs_3.position, _viewTrs_3.rotation);
            await UniTask.Delay(TimeSpan.FromSeconds(2.0f), cancellationToken: token);

            await Dialogue(token);
            await LoadSceneCanvas.Instance.CloseScene();
            SceneManager.LoadScene(TOON_WORLD_SCENE_NAME);
        }
        catch (OperationCanceledException)
        {
            Debug.Log("Opening scene was canceled.");
        }
    }

    async UniTask Dialogue(CancellationToken token)
    {
        await DialogueManager.Instance.OpenBox();
        await DialogueManager.Instance.ShowNameAndDialogue(_openingD, _openingA);
        await DialogueManager.Instance.InputNext();
        await DialogueManager.Instance.ShowDialogueText(_openingB, 0.3f);
        await DialogueManager.Instance.InputNext();
        await DialogueManager.Instance.ShowDialogueText(_openingC, 0.3f);
        await DialogueManager.Instance.InputNext();
        await DialogueManager.Instance.ClearDialogue();
    }
}
