using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class SavePoint : MonoBehaviour, IInteractable
{
    [SerializeField]
    private GameObject _diskObj;

    [SerializeField]
    private MeshRenderer _lightPlate;

    [SerializeField]
    private AudioClip _saveClip;

    [SerializeField]
    private Material[] _materials;//0 = black / 1 = light

    [SerializeField]
    private string _savePointName;

    private bool _isDoingSave;

    private void Start()
    {
        _isDoingSave = false;
        LightTurnOnOff();
    }

    private void LightTurnOnOff()
    {
        string activatedSavePoint = DataManager.Instance._systemData._savePoint;
        _lightPlate.material = (_savePointName == activatedSavePoint) ? _materials[1] : _materials[0];
    }

    private async UniTask MovingStatue()
    {
        Quaternion originRot = _diskObj.transform.rotation;
        await _diskObj.transform.DOLocalMoveY(4.6f, 0.1f).SetEase(Ease.OutBounce);
        await _diskObj.transform.DOLocalRotate(new Vector3(-90, 0, 180), 0.4f).SetEase(Ease.Linear);
        await _diskObj.transform.DOLocalRotate(new Vector3(-90, 0, 360), 0.4f).SetEase(Ease.Linear);
        await _diskObj.transform.DOLocalMoveY(4.0f, 0.1f).SetEase(Ease.OutBounce);
        _diskObj.transform.rotation = originRot;
    }

    private async UniTask ActivateSave()
    {
        if (_isDoingSave)
        {
            return;
        }

        _isDoingSave = true;
        await MovingStatue();
        DataManager.Instance.SaveData();
        SoundManager.Instance.PlaySfx(_saveClip);
        await Main_UI_Canvas.Instance.ShowSaveAlarm();
        LightTurnOnOff();
        _isDoingSave = false;
    }

    public void OnInteract()
    {
        if (!_isDoingSave)
        {
            ActivateSave().Forget();
        }
    }
}
