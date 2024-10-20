using UnityEngine;

public class EnemyCore : MonoBehaviour
{
    protected PlayerCore _player;

    [SerializeField]
    protected AudioClip _deathClip;

    protected float _distFromPlayer;

    protected virtual void Awake()
    {
        _player = FindAnyObjectByType<PlayerCore>();
    }


    protected virtual void Update()
    {
        _distFromPlayer = CalculateDistanceFromPlayer();
    }

    protected float CalculateDistanceFromPlayer()
    {
        Vector3 myPos = new Vector3(transform.position.x, 0, transform.position.z);
        Vector3 playerPos = new Vector3(_player.transform.position.x, 0, _player.transform.position.z);
        float dist = Vector3.Distance(myPos, playerPos);

        return dist;
    }
}
