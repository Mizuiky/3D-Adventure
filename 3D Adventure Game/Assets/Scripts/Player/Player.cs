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
    [Header("Player Movement Fields")]

    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _jumpHeight;
    [SerializeField]
    private float _jumpGravity;

    #region Private Fields

    private PlayerAnimation _animation;
    private Rigidbody _rb;

    #region Input and Movement

    private bool _canMove;

    private float _horizontal;
    private float _vertical;

    private float _XAxis;
    private float _ZAxis;

    private Vector3 _movement;

    #endregion

    #region Jump

    private bool _canJump;

    [SerializeField]
    private Transform _groundPosition;
    [SerializeField]
    private LayerMask _groundLayer;
    [SerializeField]
    private float _sphereRadius;
    [SerializeField]
    private Color _sphereColor;

    #endregion

    #endregion

    #region properties

    public bool CanMove
    {
        get => _canMove;
        set => _canMove = value;
    }

    public bool CanJump
    {
        get => _canJump;
        set => _canJump = value;
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
        _canMove = false;
        _canJump = true;
    }

    private void Init()
    {
        _rb = GetComponent<Rigidbody>();
        _animation = GetComponentInChildren<PlayerAnimation>();

        InitStateMachine();
    }

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

    public void Deactivate()
    {
        throw new System.NotImplementedException();
    }

    public void Update()
    {
        if (_canMove) 
            MoveState();

        //if (!_canJump && !_canMove)
        //    IdleState();

        //PlayerInput();
        //Move();
    }

    private void IdleState()
    {
        Idle();
    }

    private void MoveState()
    {
        _rb.velocity = Vector3.forward * Time.deltaTime * _speed;
        _animation.OnRun();
    }

    public void JumpState()
    {
        _rb.velocity = Vector3.up * _jumpHeight;
        _rb.velocity -= Vector3.up * Time.deltaTime * _jumpGravity;

        if (GroundCheck())
        {

        }
    }

    #region Movement

    private void PlayerInput()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");

        _movement = new Vector3(_horizontal, 0, _vertical).normalized;

        if (Input.GetKeyUp(KeyCode.Space))
            Jump();
    }

    private void Move()
    {
        if(_horizontal != 0 || _vertical != 0)
        {
            _rb.velocity = _movement * Time.deltaTime * _speed;
            _animation.OnRun();
        }
        else
        {
            _animation.OnIdle();
        }
    }

    private void RotateToSide() { }

    #endregion

    #region Jump

    public void Jump()
    {
        if(GroundCheck())
        {
            _canJump = true;
            _rb.velocity = (Vector3.up + _movement) * Time.deltaTime * _jumpHeight;
        }
        else
        {
            _canJump = false;
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = _sphereColor;
        Gizmos.DrawSphere(_groundPosition.position, _sphereRadius);
    }

    public bool GroundCheck()
    {
        Collider [] check = Physics.OverlapSphere(_groundPosition.position, _sphereRadius, _groundLayer.value);

        return check.Length > 0;
    }

    #endregion

    public void Idle()
    {
        _canMove = false;
        _canJump = false;

        _rb.velocity = Vector3.zero * Time.deltaTime;

        _animation.OnIdle();
    }

    public void OnDead()
    {
        _canMove = false;
        _canJump = false;

        _animation.OnDead();
    }
}
