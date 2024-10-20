using UnityEngine;

public class RotateObj : MonoBehaviour
{
    [SerializeField]
    private float _rotSpeed;

    private void Start()
    {
        InitIfNoInputValue();
    }
    private void Update()
    {
        Rotation();
    }

    private void InitIfNoInputValue()
    {
        if (_rotSpeed == 0)
        {
            _rotSpeed = 1.0f;
        }
    }

    private void Rotation()
    {
        transform.Rotate(Vector3.up, _rotSpeed * Time.deltaTime);
    }
}
