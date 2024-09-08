using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class SavePoint : MonoBehaviour, IInteractable
{
    [SerializeField]
    private GameObject _diskObject;

    [SerializeField]
    private MeshRenderer _lightPlate;

    [SerializeField]
    private AudioClip _saveClip;

    [SerializeField]
    private Material[] _materials;//0 = black / 1 = light

    [SerializeField]
    private string _savePointName;

    private bool _isDoing;

    void Start()
    {
        _isDoing = false;
        LightOnOff().Forget();
    }

    async UniTask ActivateSave()
    {
        if (_isDoing)
        {
            return;
        }

        _isDoing = true;
        Quaternion originRot = _diskObject.transform.rotation;
        await _diskObject.transform.DOLocalMoveY(4.6f, 0.1f).SetEase(Ease.OutBounce);
        await _diskObject.transform.DOLocalRotate(new Vector3(-90, 0, 180), 0.4f).SetEase(Ease.Linear);
        await _diskObject.transform.DOLocalRotate(new Vector3(-90, 0, 360), 0.4f).SetEase(Ease.Linear);
        await _diskObject.transform.DOLocalMoveY(4.0f, 0.1f).SetEase(Ease.OutBounce);
        _diskObject.transform.rotation = originRot;
        DataManager.Instance.SaveData();
        SoundManager.Instance.PlaySfx(_saveClip);
        await Main_UI_Canvas.Instance.ShowSaveAlarm();
        await OnOffSavePointLights();
        _isDoing = false;
    }

    public async UniTask LightOnOff()
    {
        string activatedSavePoint = string.Empty;
        //string activatedSavePoint = PlayerDataManager.playerData.savedPlace;
        if (activatedSavePoint == _savePointName)
        {
            _lightPlate.material = _materials[1];
        }

        else
        {
            _lightPlate.material = _materials[0];
        }
    }

    async UniTask OnOffSavePointLights()
    {
        SavePoint[] savepoints = FindObjectsOfType<SavePoint>();

        for (int i = 0; i < savepoints.Length; i++)
        {
            await savepoints[i].LightOnOff();
        }
    }

    public void OnInteract()
    {
        if(!_isDoing)
            ActivateSave().Forget();
    }
}
