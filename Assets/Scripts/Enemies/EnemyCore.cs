using UnityEngine;

public enum ENEMY_STATE
{
    Idle,
    Move,
    Attack,
    Death
}

public class EnemyCore : MonoBehaviour
{
    [SerializeField]
    protected float _hp;
    [SerializeField]
    protected float _maxhp, _attack;

    private void Start()
    {
        _hp = _maxhp;
    }
}
