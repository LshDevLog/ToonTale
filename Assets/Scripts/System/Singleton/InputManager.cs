using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    public float _h { get; private set; }
    public float _v { get; private set; }

    public Vector2 _inputVector;

    public bool _Q_KEY { get; private set; }

    public bool _W_KEY { get; private set; }

    public bool _E_KEY { get; private set; }

    public bool _ATTACK_KEY { get; private set; }
    public bool _DODGE_KEY { get; private set; }
    public bool _SHIELD_KEY { get; private set; }
    public bool _SKILL_KEY { get; private set; }
    public bool _INTERACTION_KEY { get; private set; }
    public bool _ESC_KEY { get; private set; }
    public float _LEFT_TRIGGER_KEY { get; private set; }


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Update()
    {

        _h = Input.GetAxis("Horizontal");
        _v = Input.GetAxis("Vertical");

        _inputVector = new Vector2(_h, _v);

        //limit max value
        _inputVector = Vector2.ClampMagnitude(_inputVector, 1);


        _Q_KEY = Input.GetButtonDown("Q");
        _W_KEY = Input.GetButtonDown("W");
        _E_KEY = Input.GetButtonDown("E");

        _ATTACK_KEY = Input.GetButtonDown("Attack");

        _DODGE_KEY = Input.GetButtonDown("Space");

        _SHIELD_KEY = Input.GetButtonDown("Shield");

        _SKILL_KEY = Input.GetButtonDown("Skill");

        _INTERACTION_KEY = Input.GetButtonDown("Interaction");

        _ESC_KEY = Input.GetButtonDown("Esc");

        _LEFT_TRIGGER_KEY = Input.GetAxisRaw("LeftTrigger");



    }
}
