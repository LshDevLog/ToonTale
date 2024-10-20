using UnityEngine;

public class EnemyDetect : MonoBehaviour
{
    public bool _detectedPlayer;

    private float _viewRadius, _viewAngle;

    private LayerMask _playerLayer, _obstacleLayer;

    private void Start()
    {
        _viewRadius = 8f;
        _viewAngle = 90f;

        _playerLayer = 1 << 7;
        _obstacleLayer = 1 << 6;
    }


    private void Update()
    {
        if (!_detectedPlayer)
        {
            Detect();
        }
    }

    public void Detect()
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
                            _detectedPlayer = false;
                            return;
                        }
                    }
                    _detectedPlayer = true;
                }
            }
            else
            {
                _detectedPlayer = false;
            }
        }
    }

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
}
