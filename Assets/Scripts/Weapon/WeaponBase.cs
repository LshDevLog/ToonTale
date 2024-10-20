using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    protected Animator _anim;

    [SerializeField]
    private Weapon_Item _weaponData;

    private float _neededMp;

    protected virtual void Awake()
    {
        _anim = GetComponentInParent<Animator>();
    }

    private void Start()
    {
        _neededMp = _weaponData.skillMp;
    }
    public virtual void DoSkill()
    {
        if(PlayerCore.Instance._mp < _neededMp)
        {
            return;
        }
        PlayerCore.Instance.eSTATE = PlayerCore.STATE.Skill;
        PlayerCore.Instance._mp -= _neededMp;
    }
}
