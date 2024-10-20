using UnityEngine;

public class Movement : MonoBehaviour
{
    private Animator _anim;
    private Dodge _dodge;

    [SerializeField]
    private AudioClip _footStepClip;

    private float _moveSpeed, _rotSpeed;

    private Vector3 _dir;

    private const string RUN_ANIM = "Run";

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _dodge = GetComponent<Dodge>();
    }

    private void Start()
    {
        InitSpeed();
    }

    private void Update()
    {
        UpdateAnim();
    }

    private void InitSpeed()
    {
        _moveSpeed = 5.0f;
        _rotSpeed = 7.0f;
    }

    private void UpdateAnim()
    {
        bool runAnim = (PlayerCore.Instance.eSTATE == PlayerCore.STATE.Movement) ? true : false;
        _anim.SetBool(RUN_ANIM, runAnim);
    }

    private Vector3 DirVecNoYpos(Vector3 input)
    {
        Vector3 vec = new Vector3(input.x, 0, input.y);
        return vec;
    }

    private void ChangeState()
    {
        PlayerCore.Instance.eSTATE = (_dir.magnitude == 0) ? PlayerCore.STATE.Normal : PlayerCore.STATE.Movement;
    }

    private void UpdateRot()
    {
        if(PlayerCore.Instance.eSTATE == PlayerCore.STATE.Movement)
        {
            var rot = Quaternion.LookRotation(_dir);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, _rotSpeed);
        }
    }

    public void DoMove()
    {
        _dir = DirVecNoYpos(InputManager.Instance._inputVector);

        float finalSpeed = _moveSpeed * _dodge._dodgeSpeed * Time.deltaTime;

        transform.position += (_dir * finalSpeed);

        ChangeState();
        UpdateRot();
    }

    //AnimEvents
    public void StepSound()
    {
        SoundManager.Instance.PlaySfx(_footStepClip);
    }
}
