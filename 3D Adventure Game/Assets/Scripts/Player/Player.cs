using UnityEngine;
using Machine;

public enum PlayerStates
{
    IDLE,
    JUMP,
    MOVE
}

public class Player : MonoBehaviour, IGameComponent
{
    [Header("Rotation Settings")]

    [SerializeField]
    private float _turnSpeed;

    [Header("Jump Settings")]

    [SerializeField]
    private float _gravity;
    [SerializeField]
    private float _jumpHeight;

    [Header("Movement Settings")]

    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _speedRun;

    [Header("Booleans to track")]

    [SerializeField]
    private bool _isGrounded;
    [SerializeField]
    private bool _isWalking;
    [SerializeField]
    private bool _isJumping;

    [Header("Ground Check")]
    [SerializeField]
    private Transform _groundPosition;
    [SerializeField]
    private LayerMask _groundLayer;
    [SerializeField]
    private float _sphereRadius;

    [Header("Animation Speed")]

    [SerializeField]
    private KeyCode _keyRun;
    [SerializeField]
    private float _animationSpeed;

    #region Private Fields

    private Rigidbody _rb;
    private PlayerAnimation _animation;

    #region Input and Movement

    private Vector3 _move = Vector3.zero;

    private bool _canMove;

    private float _horizontal;
    private float _vertical;

    private float _currentSpeed;
    private Vector3 _movement;

    #endregion

    #endregion

    #region properties

    public bool CanMove
    {
        get => _canMove;
        set => _canMove = value;
    }

    public StateMachine<PlayerStates, Player> PlayerMachine
    {
        get => _playerMachine;
    }

    #endregion

    #region State Machine

    private StateMachine<PlayerStates, Player> _playerMachine;

    #endregion

    public void Awake()
    {
        Activate();
    }
    public void Start()
    {
        Init();
    }

    public void Activate()
    {
        _isWalking = false;
        _canMove = false;
    }

    private void Init()
    {
        _rb = GetComponent<Rigidbody>();
        _animation = GetComponentInChildren<PlayerAnimation>();

        _currentSpeed = _speed;

        //InitStateMachine();
    }

    #region Player State Machine

    private void InitStateMachine()
    {
        _playerMachine = new StateMachine<PlayerStates, Player>(this);
        _playerMachine.Init();

        RegisterPlayerStates();
        _playerMachine.SwitchState(PlayerStates.IDLE);
    }

    private void RegisterPlayerStates()
    {
        _playerMachine.RegisterState(PlayerStates.IDLE, new IdleState());
        _playerMachine.RegisterState(PlayerStates.MOVE, new MoveState());
        _playerMachine.RegisterState(PlayerStates.JUMP, new JumpState());
    }

    public void SwitchPlayerState(PlayerStates state)
    {
        _playerMachine.SwitchState(state);
    }

    #endregion

    public void Deactivate()
    {
        throw new System.NotImplementedException();
    }

    public void Update()
    {
        _isGrounded = GroundCheck();

        PlayerInput();
    }

    public void FixedUpdate()
    {
        if (_rb.velocity.y < 0)
            _movement.y = 0;

        _movement.y += _gravity * Time.deltaTime;

        Move();

        RotateToSide();

        if (_isJumping)
            Jump();
    }

    #region Movement

    private void PlayerInput()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");

        _isWalking = _horizontal != 0 || _vertical != 0;

        _movement = new Vector3(_horizontal, _movement.y, _vertical);

        if (Input.GetKey(_keyRun))
        {
            _currentSpeed = _speedRun;
            _animation.SetSpeed(_animationSpeed);
        }
        else
        {
            _currentSpeed = _speed;
            _animation.SetSpeed(1f);
        }

        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
            _isJumping = true;               
    }

    private void Move()
    {              
        //Applying the new movement vector
        _rb.MovePosition(transform.position + (_movement * _currentSpeed * Time.deltaTime));
        _animation.OnRun(_isWalking);
    }

    #region Jump

    public void Jump()
    {
        _isJumping = false;

        //formula exemple: if jumpheight is 10 so this formula will get the better velocity to achieve this jump height number for the object
        var jumpforce = Mathf.Sqrt(_jumpHeight * -2f * _gravity);

        //_movement.y = jumpforce;
        //_move = new Vector3(_movement.x, jumpforce, _movement.z); 
        //_rb.velocity = _move;

        _rb.AddForce(new Vector3(0, jumpforce, 0), ForceMode.Impulse);

 
    }

    #region Ground Check

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(_groundPosition.position, _sphereRadius);
    }

    public bool GroundCheck()
    {
        Collider[] check = Physics.OverlapSphere(_groundPosition.position, _sphereRadius, _groundLayer.value);
        return check.Length > 0;
    }

    #endregion

    #endregion

    private void RotateToSide()
    {
        if (_isWalking)
        {
            var targetRot = Quaternion.LookRotation(_movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, _turnSpeed);
        }
    }


    #endregion

    #region States

    private void IdleState()
    {
        Idle();
    }

    private void MoveState()
    {
        _rb.velocity = Vector3.forward * Time.deltaTime * _speed;
        //_animation.OnRun();
    }

    #endregion

    public void Idle()
    {
        _canMove = false;

        _rb.velocity = Vector3.zero * Time.deltaTime;
        _animation.OnRun(false);
    }

    public void OnDead()
    {
        _canMove = false;
        _animation.OnDead();
    }
}
