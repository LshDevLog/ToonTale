using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class Skill : MonoBehaviour
{
    Animator _anim;

    [SerializeField]
    GameObject _crystalSwordSkillPrefab;

    [SerializeField]
    AudioClip _crystalSwordSkillClip;

    private const string NO_WEAPON = "NoWeapon";
    private const string PLANK = "Plank";
    private const string HAMMER = "Hammer";
    private const string CRYSTAL_SWORD = "CrystalSword";

    private float _neededMp = 0f;
    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(Equipment.Instance._equippedWeapon == null || PlayerCore.Instance == null || InputManager.Instance == null)
        {
            return;
        }

        _neededMp = Equipment.Instance._equippedWeapon.skillMp;

        if (InputManager.Instance._SKILL_KEY)
        {
            if (PlayerCore.Instance.eSTATE == PlayerCore.STATE.Normal && Equipment.Instance._equippedWeapon.itemName != NO_WEAPON && PlayerCore.Instance._mp >= _neededMp)
            {
                PlayerCore.Instance._mp -= _neededMp;
                DoSkill(Equipment.Instance._equippedWeapon.itemName);
            }
        }
    }
    private void DoSkill(string weapon)
    {
        PlayerCore.Instance.eSTATE = PlayerCore.STATE.Skill;

        if(weapon == CRYSTAL_SWORD)
        {
            CrystalSwordSkill().Forget();
        }
    }

    private async UniTask CrystalSwordSkill()
    {
        _anim.SetTrigger("Skill_1");
        await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
        SoundManager.Instance.PlaySfx(_crystalSwordSkillClip);
        GameObject particle = Instantiate(_crystalSwordSkillPrefab, transform.position + Vector3.forward, Quaternion.LookRotation(transform.forward));
        await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
        PlayerCore.Instance.eSTATE = PlayerCore.STATE.Normal;
    }
}
