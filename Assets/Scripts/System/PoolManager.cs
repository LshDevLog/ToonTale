using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;

    public ObjectPool<NormalAttackParticle> _normalAttackPool { get; private set; }
    public ObjectPool<PlankAttackParticle> _plankAttackPool { get; private set; }

    [SerializeField]
    private NormalAttackParticle _normalAttackPrefab;
    [SerializeField]
    private PlankAttackParticle _plankAttackPrefab;

    private const int NORMAL_ATTACK_NUM = 5;
    private const int PLANK_ATTACK_NUM = 2;

    private void Awake()
    {
        Instance = this;

        InitPools();
    }

    private void InitPools()
    {
        _normalAttackPool = new ObjectPool<NormalAttackParticle>(_normalAttackPrefab, NORMAL_ATTACK_NUM, this);
        _plankAttackPool = new ObjectPool<PlankAttackParticle>(_plankAttackPrefab, PLANK_ATTACK_NUM, this);
    }
}
