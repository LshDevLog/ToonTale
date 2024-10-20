using UnityEngine;

public class AttackCircle : MonoBehaviour
{
    [SerializeField]
    private AudioClip _attackClip, _firePlankClip;

    public float _damage, _buffDamage;

    private const string ENEMY_TAG = "Enemy";
    private const string FIRE_TAG = "Fire";

    private void OnEnable()
    {
        DamageChange();
    }

    private void DamageChange()
    {
        if(Equipment.Instance == null)
        {
            return;
        }

        _damage = Equipment.Instance._equippedWeapon.attack;
    }

    private void OnTriggerEnter(Collider other)
    {

        if(other.CompareTag(ENEMY_TAG))
        {
            _buffDamage = (TempDataManager.Instance._plankBuff) ? 3 : 0;
            float finalDamage = _damage + _buffDamage;
            other.GetComponent<IDamagable>().TakeDamage(finalDamage);
            AttackEffect();
            SoundManager.Instance.PlaySfx(_attackClip);
        }

        if (other.CompareTag(FIRE_TAG))
        {
            if (Equipment.Instance._equippedWeapon.itemName.Equals("Plank"))
            {
                TempDataManager.Instance._plankFire = true;
                SoundManager.Instance.PlaySfx(_firePlankClip);
            }
        }
    }

    private void AttackEffect()
    {
        if (TempDataManager.Instance._plankBuff)
        {
            PlankAttackParticle effect = PoolManager.Instance._plankAttackPool.GetObj();
            effect.transform.position = transform.position;
            TempDataManager.Instance._plankBuff = false;
        }
        else
        {
            NormalAttackParticle effect = PoolManager.Instance._normalAttackPool.GetObj();
            effect.transform.position = transform.position;
        }
    }
}
