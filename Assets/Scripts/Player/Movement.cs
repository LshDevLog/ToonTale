using UnityEngine;

public class Movement : MonoBehaviour
{
    private Dodge _dodge;

    private float _moveSpeed, _rotSpeed;

    public AudioSource audioSource;

    private Vector3 _dirVector, finalMoveVector;

    private void Awake()
    {
        _dodge = GetComponent<Dodge>();
    }

    private void Start()
    {
        _moveSpeed = 5.0f;
        _rotSpeed = 7.0f;
    }

    public void DoMove(Animator anim)
    {
        _dirVector = new Vector3(InputManager.Instance._inputVector.x, 0, InputManager.Instance._inputVector.y);

        finalMoveVector = FinalMoveCalculation(_dirVector);

        //maintain direction
        if (_dirVector.magnitude == 0)
        {
            anim.SetBool("Run", false);
            PlayerCore.Instance.eSTATE = PlayerCore.STATE.Normal;
            return;
        }

        anim.SetBool("Run", true);
        PlayerCore.Instance.eSTATE = PlayerCore.STATE.Movement;
        var rot = Quaternion.LookRotation(_dirVector);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, _rotSpeed);

    }


    Vector3 FinalMoveCalculation(Vector3 dirVector)
    {
        float finalSpeed = _moveSpeed * _dodge._dodgeSpeed * Time.deltaTime;

        transform.position += (dirVector * finalSpeed);
        return dirVector;
    }

    void StepSound()
    {
        audioSource.volume = PlayerPrefs.GetFloat("SfxVolume");
        audioSource.Play();
    }
}
