using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
{
    [Header("Rotation Settings")]

    [SerializeField]
    private float _turnSpeed;

    [Header("Jump Settings")]

    //JUMP https://www.youtube.com/watch?v=-lgcCrnFmeg

    private Vector3 _yVelocity;
    private Vector3 _xVelocity;
    private Vector3 _finalVelocity;

    public float _maxHight = 5;
    private float _jumpSpeed;
    public float _timeToPeak = 0.3f; //time to achieve the highest height

    private float _gravity;

    [Header("Movement Settings")]
    [SerializeField]
    public float _playerSpeed = 1005;

    public float _animationSpeed = 1.03f;
    public float _animationNormalSpeed = 1f;

    [Header("Booleans to track")]

    [SerializeField]
    private bool _isGrounded;
    [SerializeField]
    private bool _isWalking;
    [SerializeField]
    private int _jumpingFrames;

    private bool _isJumping = false;

    [Header("Ground Check")]
    [SerializeField]
    private Transform _groundPosition;
    [SerializeField]
    private LayerMask _groundLayer;
    [SerializeField]
    private float _sphereRadius;

    Rigidbody _rb;
    private Vector3 _movement;

    private float _horizontal;
    private float _vertical;

    [SerializeField]
    private PlayerAnimation _animator;

    [Header("Flash Colors")]
    public List<FlashColor> flashColor;
    private CapsuleCollider collider;

    [Header("Life")]
    public HealthBase healthBase;

    public Action<bool> OnEndGame;
    public bool _isAlive;

    public void OnValidate()
    {
        if (healthBase == null) healthBase = GetComponent<HealthBase>();
        if (collider == null) collider = GetComponent<CapsuleCollider>();
    }

    void Awake()
    {
        OnValidate();

        _rb = GetComponent<Rigidbody>();
        _rb.velocity = Vector3.zero;

        healthBase.OnDamage += Damage;
        healthBase.OnKill += Kill;
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

    private void Start()
    {
        //isolate g variable on 
        _gravity = (2 * _maxHight) / Mathf.Pow(_timeToPeak, 2);

        _jumpSpeed = _gravity * _timeToPeak;

        _isAlive = true;

        collider.enabled = true;
    }

    void Update()
    {
        if(_isAlive)
        {
            _isGrounded = GroundCheck();

            PlayerInput();

            RotateToSide();
        }        
    }

    private void PlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _animator.OnRun(false);
            Jump();
        }
            
        else
            _jumpingFrames--;
            

        if(Input.GetKey(KeyCode.Q))
        {
            _playerSpeed = 1300;
            _animator.SetSpeed(_animationSpeed);
        }
        else
        {
            _playerSpeed = 1005;
            _animator.SetSpeed(_animationNormalSpeed);
        }

        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");

        _isWalking = _horizontal != 0 || _vertical != 0 && _isGrounded;

        _animator.OnRun(_isWalking);

    }

    private void FixedUpdate()
    {
        _movement = new Vector3(_horizontal, 0, _vertical);

        _xVelocity = _movement * _playerSpeed * Time.deltaTime;

        _finalVelocity = _xVelocity + _yVelocity;

        _rb.velocity = _finalVelocity;

        if (!_isGrounded)
            _yVelocity += _gravity * Time.deltaTime * Vector3.down;
        else
        {
            if (_jumpingFrames <= 0)
                _yVelocity = Vector3.zero;
        }
    }

    private void RotateToSide()
    {
        if(_movement != Vector3.zero)
        {
            //transform.forward = _movement;

            Quaternion toRotation = Quaternion.LookRotation(_movement, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, _turnSpeed * Time.deltaTime);
        }
    }

    private void Jump()
    {
       _yVelocity = _jumpSpeed * Vector3.up;
       _jumpingFrames = 3;   
    }

    public void Damage(HealthBase h)
    {
        flashColor.ForEach(i => i.ChangeColor());
        EffectManager.Instance.ChangeVinhetColor();
    }

    public void Kill(HealthBase h)
    {
        if(_isAlive)
        {
            Debug.Log("is alive false");
            _isAlive = false;

            collider.enabled = false;

            _animator.OnDead(true);

            OnEndGame?.Invoke(true);

            Invoke(nameof(Respawn), 3f);
        }     
    }

    public void Respawn()
    {    
        _isAlive = true;

        collider.enabled = true;

        _animator.OnDead(false);

        healthBase.ResetLife();

        transform.position = CheckPointManager.Instance.GetLastCheckPointPosition();

        OnEndGame?.Invoke(false);
    }

    public void Damage(int value, Vector3 dir)
    {
        //flashColor.ForEach(i => i.ChangeColor());
    }
}
