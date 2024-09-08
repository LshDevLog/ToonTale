using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    private const string TOON_WORLD_SCENE_NAME = "ToonWorld";

    private void Awake()
    {
        _cam = Camera.main;
        _drAnim = _doctor.GetComponent<Animator>();
    }

    private void Start()
    {
        Opening().Forget();
    }

    async UniTask Opening()
    {
        await _blackScreen.DOFade(0, 1f);
        _drAnim.SetBool("isWalking", true);
        UniTask a = _doctor.transform.DOMoveZ(6.1f, 8f).SetEase(Ease.Linear).ToUniTask();
        UniTask b = _cam.transform.DORotate(new Vector3(17, 0, 0), 8f).SetEase(Ease.Linear).ToUniTask();
        await UniTask.WhenAll(a, b);

        _drAnim.SetBool("isWalking", false);
        _cam.transform.SetLocalPositionAndRotation(_viewTrs_1.position, _viewTrs_1.rotation);
        await UniTask.Delay(TimeSpan.FromSeconds(1f));

        _drAnim.SetTrigger("isReachingOut");
        await UniTask.Delay(TimeSpan.FromSeconds(1.5f));

        _mysteriousObj.GetComponent<IInteractable>().OnInteract();
        await UniTask.Delay(TimeSpan.FromSeconds(1.3f));

        _cam.transform.SetLocalPositionAndRotation(_viewTrs_2.position, _viewTrs_2.rotation);
        await UniTask.Delay(TimeSpan.FromSeconds(1.2f));

        int count = _redLineBox.transform.childCount;
        GameObject[] lines = new GameObject[count];

        for (int i = 0; i < count; i++)
        {
            lines[i] = _redLineBox.transform.GetChild(i).gameObject;
            lines[i].GetComponent<RotateObj>().enabled = false;
        }
        await UniTask.Delay(TimeSpan.FromSeconds(2.0f));

        foreach (var line in lines)
        {
            line.transform.DOScale(0, 0.2f).SetEase(Ease.InBounce);
        }
        await UniTask.Delay(TimeSpan.FromSeconds(3.0f));

        foreach(var eye in _eyes)
        {
            eye.transform.DOScale(10, 2);
        }

        await UniTask.Delay(TimeSpan.FromSeconds(3f));

        _cam.transform.SetLocalPositionAndRotation(_viewTrs_3.position, _viewTrs_3.rotation);
        await UniTask.Delay(TimeSpan.FromSeconds(2.0f));

        await Dialogue();
        await LoadSceneCanvas.Instance.CloseScene();
        SceneManager.LoadScene(TOON_WORLD_SCENE_NAME);
    }

    async UniTask Dialogue()
    {
        await DialogueManager.Instance.OpenBox();
        await DialogueManager.Instance.ShowNameAndDialogue("<color=#FFFF00>Yellow Mask</color>", "Hello World.");//Temp
        await DialogueManager.Instance.InputNext();
        await DialogueManager.Instance.ShowDialogueText("Input Text.", 0.3f);//Temp
        await DialogueManager.Instance.InputNext();
        await DialogueManager.Instance.ShowDialogueText("End Text.", 0.3f);//Temp
        await DialogueManager.Instance.InputNext();
        await DialogueManager.Instance.ClearDialogue();
    }

}
