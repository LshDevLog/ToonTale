using UnityEngine;

public class Attack : MonoBehaviour
{
    public GameObject _attackCol, _hammerskillCol;

    [SerializeField]
    private AudioClip _attackSwingClip;

    private float _coolTime, _coolTimeMax;

    private const string ATTACK_ANIM = "Attack";

    private void Start()
    {
        InitCoolTime();
    }

    private void Update()
    {
        CoolTimeCharge();
        BackToNormalStateAfeterAttacking();
    }

    private void InitCoolTime()
    {
        _coolTimeMax = 0.5f;
        _coolTime = _coolTimeMax;
    }

    private void CoolTimeCharge()
    {
        _coolTime = (_coolTime < _coolTimeMax) ? _coolTime += Time.deltaTime : _coolTimeMax;
    }

    private void BackToNormalStateAfeterAttacking()
    {
        PlayerCore.Instance.eSTATE = (PlayerCore.Instance.eSTATE == PlayerCore.STATE.Attack) ? PlayerCore.STATE.Normal : PlayerCore.Instance.eSTATE;
    }

    public void DoAttack(Animator anim)
    {
        if (InputManager.Instance != null && PlayerCore.Instance != null && InputManager.Instance._ATTACK_KEY)
        {
            if (_coolTime >= _coolTimeMax)
            {
                _coolTime = 0f;
                anim.SetTrigger(ATTACK_ANIM);
                PlayerCore.Instance.eSTATE = PlayerCore.STATE.Attack;
                SoundManager.Instance.PlaySfx(_attackSwingClip);
            }
        }
    }

    //Animation Events
    public void ActivationAttackCircle()
    {
        if(_attackCol != null)
        {
            _attackCol.SetActive(!_attackCol.activeSelf);
        }
    }

    public void ActivationHammerSkillCircle()
    {
        if (_hammerskillCol != null)
        {
            _hammerskillCol.SetActive(!_hammerskillCol.activeSelf);
        }
    }
}
