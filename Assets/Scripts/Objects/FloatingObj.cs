using UnityEngine;

public class FloatingObj : MonoBehaviour
{
    [SerializeField]
    private float _floatingHeight, _floatingSpeed;

    private Vector3 _originTrs;

    private void Start()
    {
        _originTrs = transform.position;
    }
    private void Update()
    {
        Floating();
    }

    private void Floating()
    {
        float newY = _originTrs.y + Mathf.Sin(Time.time * _floatingSpeed) * _floatingHeight;

        transform.position = new Vector3(_originTrs.x , newY, _originTrs.z);
    }
}
