using UnityEngine;

public class HammerSkillCircle : MonoBehaviour
{
    [SerializeField]
    private AudioClip _attackClip;

    public float _damage, _buffDamage;

    private const string ENEMY_TAG = "Enemy";

    private void Start()
    {
        _damage = 10;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag(ENEMY_TAG))
        {
            _buffDamage = (TempDataManager.Instance._plankBuff) ? 3 : 0;
            float finalDamage = _damage + _buffDamage;
            other.GetComponent<IDamagable>().TakeDamage(finalDamage);
            AttackEffect();
            SoundManager.Instance.PlaySfx(_attackClip);
        }
    }

    private void AttackEffect()
    {
        PlankAttackParticle effect = PoolManager.Instance._plankAttackPool.GetObj();
        effect.transform.position = transform.position;
        TempDataManager.Instance._plankBuff = false;
    }
}
