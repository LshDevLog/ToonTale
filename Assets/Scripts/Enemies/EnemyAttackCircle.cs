using UnityEngine;

public class EnemyAttackCircle : MonoBehaviour
{
    [SerializeField]
    private float _damage = 1f;

    private const string PLAYER_TAG = "Player";

    private void OnEnable()
    {
        Invoke("OffAttackCircle", 0.3f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PLAYER_TAG))
        {
            other.GetComponentInParent<PlayerCore>().TakeDamage(_damage);
        }
    }

    private void OffAttackCircle()
    {
        gameObject.SetActive(false);
    }
}
