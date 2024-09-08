using UnityEngine;
using DG;
using DG.Tweening;

public class ShakeBush : MonoBehaviour
{
    Transform originPos;

    float _duration, _strength, _randomness;
    int _vibrato = 25;

    private void Start()
    {
        originPos = transform;
        _duration = 0.1f;
        _strength = 0.1f;
        _vibrato = 25;
        _randomness = 0.1f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            transform.DOShakePosition(_duration, _strength, _vibrato, _randomness);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
            transform.position = originPos.position;
    }
}
