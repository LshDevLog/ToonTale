using UnityEngine;

public class FloatingObj : MonoBehaviour
{
    [SerializeField]
    float floatingHeight, floatingSpeed;

    Vector3 originTrs;

    private void Start()
    {
        originTrs = transform.position;
    }
    private void Update()
    {
        FloatingRedFish();
    }

    void FloatingRedFish()
    {
        float newY = originTrs.y + Mathf.Sin(Time.time * floatingSpeed) * floatingHeight;

        transform.position = new Vector3(originTrs.x , newY, originTrs.z);
    }
}
