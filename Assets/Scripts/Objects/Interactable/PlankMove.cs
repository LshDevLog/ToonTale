using UnityEngine;
using UnityEngine.AI;

public class PlankMove : MonoBehaviour, IInteractable
{
    private PlayerCore _player;
    private Animator _anim;
    private NavMeshAgent _agent;

    [SerializeField]
    private GameObject _worldPlank;
    private GameObject _playerPlank;

    [SerializeField]
    private Transform _dest;

    private bool _isActive = false;
    private float _arrivalRange = 0.02f;

    private void Awake()
    {
        _player = FindAnyObjectByType<PlayerCore>();
        _anim = _player.GetComponent<Animator>();
        _agent = _player.GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        float remainingDistToDestination = RemainingDistToDestination();

        if (!_isActive)
        {
            return;
        }

        if (remainingDistToDestination <= _arrivalRange)
        {
            EndInteract();
        }
    }

    private Vector3 VactorNoYpos(Vector3 input)
    {
        return new Vector3(input.x, 0, input.z);
    }

    private float RemainingDistToDestination()
    {
        Vector3 player = VactorNoYpos(_player.transform.position);
        Vector3 dest = VactorNoYpos(_dest.transform.position);
        return Vector3.Distance(player, dest);
    }

    private void PutPlankOnWorldSpace(bool value)
    {
        if (_playerPlank == null)
        {
            _playerPlank = _player.GetComponentInChildren<Plank>().gameObject;
        }
        _worldPlank.SetActive(value);
        _playerPlank.SetActive(!value);
    }

    private void WalkSlowlyTowardsDestination()
    {
        _anim.SetBool("SlowWalk", true);
        _player.transform.LookAt(_dest.position);
        _agent.SetDestination(_dest.position);
        _agent.speed = 0.5f;
    }

    private void ArrivalToDestinationAndStop()
    {
        _anim.SetBool("SlowWalk", false);
        _agent.ResetPath();
        _agent.speed = 3.5f;
    }

    private void EndInteract()
    {
        PutPlankOnWorldSpace(false);
        ArrivalToDestinationAndStop();
        _isActive = false;
        _player.eSTATE = (_player.eSTATE == PlayerCore.STATE.Interact) ? PlayerCore.STATE.Normal : _player.eSTATE;
    }

    public void OnInteract()
    {
        if(_worldPlank == null || _dest == null)
        {
            return;
        }

        if ((_player.eSTATE == PlayerCore.STATE.Normal || _player.eSTATE == PlayerCore.STATE.Movement) && Equipment.Instance._equippedWeapon.itemName == "Plank" && _isActive == false)
        {
            PutPlankOnWorldSpace(true);
            WalkSlowlyTowardsDestination();
            _isActive = true;
            _player.eSTATE = PlayerCore.STATE.Interact;
        }
    }
}
