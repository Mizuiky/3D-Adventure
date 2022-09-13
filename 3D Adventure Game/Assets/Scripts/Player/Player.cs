using UnityEngine;
using StateMachine;

public enum PlayerStates
{
    IDLE,
    JUMP,
    MOVE
}

public class Player : MonoBehaviour, IGameComponent
{
    [Header("Player Movement")]
    [SerializeField]
    private float _speed;

    [Header("Animation Speed")]
    [SerializeField]
    private KeyCode _keyRun;
    [SerializeField]
    private float _animationSpeedMultiplier;

    [Header("Jump")]
    [SerializeField]
    private float _jumpForce;
    [SerializeField]
    private float _gravity;

    [Header("Check Ground")]
    [SerializeField]
    private Transform _groundPosition;
    [SerializeField]
    private LayerMask _groundLayer;
    [SerializeField]
    private float _sphereRadius;
    [SerializeField]
    private Color _sphereColor;

    [Header("Rotation")]
    [SerializeField]
    private float _turnSpeed;

    #region Private Fields

    private PlayerAnimation _animation;
    private Rigidbody _rb;

    #region Input and Movement

    private Vector3 _movement;

    private bool _canMove;
    private bool _isWalking;
    private bool _isGrounded;

    private float _verticalVelocity;

    private float _horizontal;
    private float _vertical;

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

        InitStateMachine();
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
        //if (_canMove) 
        //    MoveState();
        //if (!_canMove)
        //    IdleState();

        PlayerInput();

        //_isGrounded = GroundCheck();
        _isGrounded = false;

        if (_isGrounded)
        {
            Debug.Log("Grounded true");
            //_verticalVelocity = 0;
        }          
        else
        {
            //applying gravity when jumping
            Debug.Log("Grounded false");
            //_verticalVelocity -= _gravity * Time.deltaTime;
            Debug.Log("_verticalVelocity: " + _verticalVelocity);
        }

        //_movement.y = _verticalVelocity;
        //_rb.velocity = _movement * Time.deltaTime;
    }

    public void FixedUpdate()
    {
        Jump();

        Move();
    }

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

    public void JumpState()
    {
        _rb.velocity = Vector3.up * _jumpForce;
    }

    #endregion

    #region Movement

    private void PlayerInput()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");

        _isWalking = _horizontal != 0 || _vertical != 0;

        _movement = new Vector3 (_horizontal, 0 , _vertical);
        _movement.Normalize();
       
        if (Input.GetKeyDown(_keyRun))
            _animation.SetSpeed(_animationSpeedMultiplier); 
    }

    private void Move()
    {
        if (_isWalking)
        {
            //Applying the new movement vector
            _rb.velocity = _movement * _speed * Time.deltaTime;
        }

        RotateToSide(); 

        _animation.OnRun(_isWalking); 
    }

    private void RotateToSide() 
    {
        //transform.Rotate(Vector3.up * _horizontal * _turnSpeed * Time.deltaTime);

        if(_isWalking)
        {
            Quaternion targetRotation = Quaternion.LookRotation(_movement);
            targetRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _turnSpeed * Time.deltaTime);

            _rb.MoveRotation(targetRotation);
        }
    }

    #region Jump

    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !_isGrounded)
        {
            Debug.Log("JUMP");
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);

            _isGrounded = true;
            //_verticalVelocity = _jumpForce;
            //_rb.velocity = _rb.velocity + (Vector3.up * _jumpForce * Time.deltaTime); 
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = _sphereColor;
        Gizmos.DrawSphere(_groundPosition.position, _sphereRadius);
    }

    public bool GroundCheck()
    {
        Collider[] check = Physics.OverlapSphere(_groundPosition.position, _sphereRadius, _groundLayer.value);

        return check.Length > 0;
    }

    #endregion

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
