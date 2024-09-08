using UnityEngine;

public class RotateObj : MonoBehaviour
{
    [SerializeField]
    float _rotSpeed;

    private void Start()
    {
        if (_rotSpeed == 0)
            _rotSpeed = 1.0f;
    }
    private void Update()
    {
        Rotation();
    }

    void Rotation()
    {
        transform.Rotate(Vector3.up, _rotSpeed * Time.deltaTime);
    }
}
