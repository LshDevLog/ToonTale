using UnityEngine;

public class Attack : MonoBehaviour
{
    public GameObject _attackCol;

    [SerializeField]
    private AudioClip _attackSwingClip;

    private float _coolTime, _coolTimeMax;

    private void Start()
    {
        _coolTimeMax = 0.5f;
        _coolTime = _coolTimeMax;
    }

    private void Update()
    {
        CoolTimeCharge();
    }

    private void CoolTimeCharge()
    {
        if (_coolTime >= _coolTimeMax)
        {
            if(PlayerCore.Instance.eSTATE == PlayerCore.STATE.Attack)
            {
                PlayerCore.Instance.eSTATE = PlayerCore.STATE.Normal;
                return;
            }
            else
            {
                return;
            }
        }

        _coolTime += Time.deltaTime;
    }

    public void DoAttack(Animator anim)
    {
        if (InputManager.Instance != null && PlayerCore.Instance != null && InputManager.Instance._ATTACK_KEY)
        {
            if (_coolTime >= _coolTimeMax)
            {
                _coolTime = 0f;
                anim.SetTrigger("Attack");
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
}
