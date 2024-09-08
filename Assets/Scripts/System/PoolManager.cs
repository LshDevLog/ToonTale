using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;

    public ObjectPool<NormalAttackParticle> _normalAttackPool { get; private set; }

    [SerializeField]
    NormalAttackParticle _normalAttackPrefab;

    private const int NORMAL_ATTACK_NUM = 5;

    private void Awake()
    {
        Instance = this;

        InitPools();
    }

    private void InitPools()
    {
        _normalAttackPool = new ObjectPool<NormalAttackParticle>(_normalAttackPrefab, NORMAL_ATTACK_NUM, this);
    }
}
