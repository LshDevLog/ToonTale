using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class Plank : WeaponBase
{
    [SerializeField]
    private AudioClip _plankSkillClip;

    public override void DoSkill()
    {
        base.DoSkill();
        SkillAnimAndParticle().Forget();
    }

    private async UniTask SkillAnimAndParticle()

    {
        SoundManager.Instance.PlaySfx(_plankSkillClip);
        TempDataManager.Instance._plankBuff = true;
        _anim.SetTrigger("Skill_1");
        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        PlayerCore.Instance.eSTATE = PlayerCore.STATE.Normal;
    }
}
