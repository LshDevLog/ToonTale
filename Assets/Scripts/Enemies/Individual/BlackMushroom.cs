using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BlackMushroom : EnemyCore, IDamagable
{
    private Animator _anim;
    private NavMeshAgent _agent;
    private EnemyDetect _enemyDetect;

    [SerializeField]
    protected ParticleSystem _deathParticle;

    [SerializeField]
    private Collider _attackCircle;

    [SerializeField]
    private float _hp, _maxHp, _mp, _maxMp, _moveSpeed;

    private bool _isBurning, _isStunned, _isFrozen, _isJumpingAnim;

    private float _attack, _height, _heightLimit, _attackDist, _attackCoolTime, _attackCoolTimeMax;

    private bool _isAttacking;

    protected override void Awake()
    {
        base.Awake();
        _anim = GetComponentInChildren<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _enemyDetect = GetComponent<EnemyDetect>();
    }

    private void Start()
    {
        _hp = _maxHp;
        _mp = _maxMp;
        _agent.speed = _moveSpeed;
        _attackDist = 1.7f;
        _attackCoolTimeMax = 2.5f;
        _attackCoolTime = _attackCoolTimeMax;
        _heightLimit = 1.8f;
    }

    protected override void Update()
    {
        base.Update();

        if (!_enemyDetect._detectedPlayer)
        {
            _enemyDetect.Detect();
        }
        else
        {
            Move();
            Attack();
            AttackCoolTime();
        }

        if (!_enemyDetect._detectedPlayer && _hp < _maxHp)
        {
            _enemyDetect._detectedPlayer = true;
        }
    }

    public void TakeDamage(float damage)
    {
        _hp -= damage;
        if (_hp <= 0)
        {
            if (_deathParticle != null)
            {
                _deathParticle.Play();
                _deathParticle.transform.SetParent(null);
                SoundManager.Instance.PlaySfx(_deathClip);
            }
            gameObject.SetActive(false);
        }
    }

    public void Move()
    {
        if (!_isAttacking)
        {
            bool isWalking = (_distFromPlayer > _attackDist);
            _anim.SetBool("Walk", isWalking);
        }

        if(_distFromPlayer > _attackDist)
        {
            _agent.SetDestination(_player.transform.position);
        }
    }

    public void Attack()
    {
        Vector3 mushroomVec = new Vector3(0, transform.position.y, 0);
        Vector3 playerVec = new Vector3(0, _player.transform.position.y, 0);
        _height = Vector3.Distance(mushroomVec, playerVec);

        if (_attackCoolTime >= _attackCoolTimeMax && _distFromPlayer < _attackDist && !_isJumpingAnim && _height <= _heightLimit)
        {
            if (transform.gameObject.activeSelf)
            {
                _agent.SetDestination(transform.position);
            }
            transform.LookAt(_player.transform);
            _attackCoolTime = 0;
            _anim.SetTrigger("Attack");
        }
    }

    public void AttackCoolTime()
    {
        if (_attackCoolTime < _attackCoolTimeMax)
        {
            _attackCoolTime += 1 * Time.deltaTime;
        }
        else if (_attackCoolTime > _attackCoolTimeMax)
        {
            _attackCoolTime = _attackCoolTimeMax;
        }
    }
}
