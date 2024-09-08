using UnityEngine;

public class CrystalSwordSkill : MonoBehaviour
{
    private const string ENEMY_TAG = "Enemy";

    private float _damage;

    private void Start()
    {
        _damage = 6;
    }

    private void OnEnable()
    {
        Destroy(gameObject, 3f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ENEMY_TAG))
        {
            other.GetComponent<IDamagable>().TakeDamage(_damage);
        }
    }
}
