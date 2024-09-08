using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

public class BlackMushroom : EnemyCore, IDamagable
{
    private PlayerCore _player;
    private Animator _anim;
    private NavMeshAgent _agent;

    public Collider _attackCircle;
    //public GameObject ExMark;

    [SerializeField]
    private ParticleSystem _deathParticle;

    private float _dist, _height, _heightLimit, _attackDist, _attackCoolTime, _attackCoolTimeMax, _viewRadius, _viewAngle;

    private bool _detected, _isAttacking;
    public bool _isJumpingAnim;

    private LayerMask _playerLayer, _obstacleLayer;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _agent = GetComponentInParent<NavMeshAgent>();
        _player = GameObject.Find("Player").GetComponent<PlayerCore>();
    }
    private void Start()
    {
        _attackDist = 1.7f;
        _attackCoolTimeMax = 2.5f;
        _attackCoolTime = _attackCoolTimeMax;
        _heightLimit = 1.8f;

        _viewRadius = 8f;
        _viewAngle = 90f;

        _playerLayer = 1 << 7;
        _obstacleLayer = 1 << 6;
    }

    private void Update()
    {
        if(_player == null || _anim == null || _agent == null)
        {
            return;
        }

        Death();
        CalculateDistance();

        if (!_detected)
        {
            Rader();
        }
        else
        {
            WalkAnim();
            JumpAnim();
            AttackCoolTime();
            Attack();
        }

        if (!_detected && _hp < _maxhp)
        {
            _detected = true;
        }
    }

    private void CalculateDistance()
    {
        Vector3 mushroom = new Vector3(transform.position.x, 0, transform.position.z);
        Vector3 player = new Vector3(_player.transform.position.x, 0, _player.transform.position.z);
        _dist = Vector3.Distance(mushroom, player);
    }

    private void AttackCoolTime()
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

    private void Attack()
    {
        Vector3 mushroom = new Vector3(0, transform.position.y, 0);
        Vector3 player = new Vector3(0, _player.transform.position.y, 0);
        _height = Vector3.Distance(mushroom, player);

        if (_attackCoolTime >= _attackCoolTimeMax && _dist < _attackDist && !_isJumpingAnim && _height <= _heightLimit)
        {
            if (transform.parent.gameObject.activeSelf)
            {
                _agent.SetDestination(transform.position);
            }
            transform.parent.LookAt(_player.transform.position);
            _attackCoolTime = 0;
            _anim.SetTrigger("Attack");
        }
    }

    private void WalkAnim()
    {
        if (!_isAttacking)
        {
            bool isWalking = (_dist > _attackDist);
            _anim.SetBool("Walk", isWalking);
        }
    }

    private void JumpAnim()
    {
        if (_isJumpingAnim && !_isAttacking)
        {
            if (transform.parent.gameObject.activeSelf)
            {
                _agent.SetDestination(_player.transform.position);
            }
        }
        else
        {
            transform.localPosition = Vector3.zero;
            if (transform.parent.gameObject.activeSelf)
            {
                _agent.SetDestination(transform.position);
            }
        }
    }

    public void IsAttacking()
    {
        _isAttacking = !_isAttacking;
    }

    public void AttackCircleEvent()
    {
        _attackCircle.gameObject.SetActive(!_attackCircle.gameObject.activeSelf);
    }

    private void Rader()
    {
        Collider[] detectCollider = Physics.OverlapSphere(transform.position, _viewRadius, _playerLayer);

        foreach (Collider target in detectCollider)
        {
            if (target != null)
            {
                Vector3 directionToTarget = (target.transform.position - transform.position).normalized;

                if (Vector3.Angle(transform.forward, directionToTarget) < _viewAngle / 2)
                {

                    RaycastHit hit;
                    if (Physics.Raycast(transform.position, directionToTarget, out hit, _viewRadius, _obstacleLayer))
                    {
                        if (hit.collider.CompareTag("Obstacle"))
                        {
                            _detected = false;
                            return;
                        }
                    }
                    _detected = true;
                }
            }
            else
            {
                _detected = false;
            }
        }
    }

    public void TakeDamage(float damage)
    {
        _hp -= damage;
    }

    private void Death()
    {
        if (_hp <= 0)
        {
            if (_deathParticle != null)
            {
                _deathParticle.Play();
                _deathParticle.transform.SetParent(null);
            }
            transform.parent.gameObject.SetActive(false);
        }
    }

    public void OnAttackCol()
    {
        _attackCircle.gameObject.SetActive(true);
    }

    #region Gizmo
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _viewRadius);


        Gizmos.color = Color.red;
        Vector3 viewAngleA = DirFromAngle(-_viewAngle / 2, false);
        Vector3 viewAngleB = DirFromAngle(_viewAngle / 2, false);

        Gizmos.DrawLine(transform.position, transform.position + viewAngleA * _viewRadius);
        Gizmos.DrawLine(transform.position, transform.position + viewAngleB * _viewRadius);
    }

    private Vector3 DirFromAngle(float angleInDegrees, bool anglesGlobal)
    {
        if (!anglesGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    #endregion
}
