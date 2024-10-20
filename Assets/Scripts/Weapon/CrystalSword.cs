using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class CrystalSword : WeaponBase
{
    private PlayerCore _core;

    [SerializeField]
    private GameObject _crystalSwordSkillPrefab;

    [SerializeField]
    private AudioClip _crystalSwordSkillClip;


    protected override void Awake()
    {
        base.Awake();
        _core = GetComponentInParent<PlayerCore>();
    }

    public override void DoSkill()
    {
        base.DoSkill();
        CrystalSwordSkill().Forget();
    }

    private async UniTask CrystalSwordSkill()
    {
        _anim.SetTrigger("Skill_1");
        await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
        SoundManager.Instance.PlaySfx(_crystalSwordSkillClip);
        GameObject particle = Instantiate(_crystalSwordSkillPrefab, _core.transform.position + _core.transform.forward, Quaternion.LookRotation(_core.transform.forward));
        await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
        PlayerCore.Instance.eSTATE = PlayerCore.STATE.Normal;
    }
}
