using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [SerializeField]
    private GameObject _panel;

    [SerializeField]
    private TextMeshProUGUI _nameTxt, _dialogueTxt;

    [SerializeField]
    private Image _nextKey;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public async UniTask OpenBox()
    {
        await _panel.transform.DOScale(1, 0.2f);
    }

    public async UniTask ShowNameAndDialogue(string name, string dialogue)
    {
        UniTask a = ShowNameText( name, 0.3f);
        UniTask b = ShowDialogueText(dialogue, 0.3f);

        await UniTask.WhenAll(a, b);
    }

    public async UniTask ShowNameText(string content, float duration)
    {
        _nameTxt.text = string.Empty;
        await _nameTxt.DOText(content, duration).ToUniTask();
    }

    public async UniTask ShowDialogueText(string content, float duration)
    {
        _dialogueTxt.text = string.Empty;
        await _dialogueTxt.DOText(content, duration).ToUniTask();
    }

    public async UniTask InputNext()
    {
        _nextKey.transform.localScale = Vector3.one;
        await _nextKey.transform.DOScale(1, 0.2f).SetEase(Ease.OutBounce);
        await UniTask.WaitUntil(() => InputManager.Instance._DODGE_KEY);
         _ = _nextKey.transform.localScale = Vector3.zero;
    }

    public async UniTask ClearDialogue()
    {
        _nameTxt.text = string.Empty;
        _dialogueTxt.text = string.Empty;
        await _panel.transform.DOScale(0, 0.2f);
    }
}
