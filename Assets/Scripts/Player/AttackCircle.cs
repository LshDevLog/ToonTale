using UnityEngine;

public class AttackCircle : MonoBehaviour
{
    private const string ENEMY_TAG = "Enemy";

    public float damage, buffDamage;
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

        damage = Equipment.Instance._equippedWeapon.attack;
    }

    private void OnTriggerEnter(Collider other)
    {
        float finalDamage = damage + buffDamage;
        if (other.tag.Equals(ENEMY_TAG))
        {
            other.GetComponent<IDamagable>().TakeDamage(finalDamage);
            NormalAttackParticle effect = PoolManager.Instance._normalAttackPool.GetObj();
            effect.transform.position = transform.position;
        }
    }
}
