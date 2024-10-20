using UnityEngine;
using DG;
using DG.Tweening;

public class ShakeBush : MonoBehaviour
{
    private Transform originPos;

    private float _duration, _strength, _randomness;
    private int _vibrato;

    private void Start()
    {
        originPos = transform;
        InitShakeValue();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            transform.DOShakePosition(_duration, _strength, _vibrato, _randomness);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            transform.position = originPos.position;
        }
    }

    private void InitShakeValue()
    {
        _duration = 0.1f;
        _strength = 0.1f;
        _vibrato = 25;
        _randomness = 0.1f;
    }
}
