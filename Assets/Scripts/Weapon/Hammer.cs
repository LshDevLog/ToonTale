using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class Hammer : WeaponBase
{
    public override void DoSkill()
    {
        base.DoSkill();
        HammerSkill().Forget();
    }


    private async UniTask HammerSkill()
    {
        _anim.SetTrigger("PowerSwing");
        await UniTask.Delay(TimeSpan.FromSeconds(2f));
        PlayerCore.Instance.eSTATE = PlayerCore.STATE.Normal;
    }
}
