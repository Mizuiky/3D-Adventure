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

    [SerializeField]
    private float _gravity;
    [SerializeField]
    private float _jumpHeight;

    [Header("Movement Settings")]
    [SerializeField]
    private float _speed;

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

    Rigidbody _rb;
    private Vector3 _movement;

    private float _horizontal;
    private float _vertical;

    private Vector3 _move = Vector3.zero;

    [SerializeField]
    private PlayerAnimation _animator;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.velocity = Vector3.zero;
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

    void Update()
    {
        PlayerInput();

        _isGrounded = GroundCheck();
    }

    private void FixedUpdate()
    {
        if (_isJumping)
            Jump();

        if (_rb.velocity.y < 0)
            _move.y = 0;

        _move.y += _gravity * Time.deltaTime;
        
        _rb.velocity = _move;

        Move();

        RotateToSide();
    }

    private void PlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
            _isJumping = true;

        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");

        _isWalking = _horizontal != 0 || _vertical != 0;

        _movement = new Vector3(_horizontal, 0, _vertical);
    }

    private void Move()
    {
        if (_isWalking && _isGrounded)
        {
            //Applying the new movement vector
            _rb.velocity = _movement * _speed * Time.fixedDeltaTime;
        }

        _animator.OnRun(_isWalking);
    }

    private void RotateToSide()
    {
        if (_isWalking)
         {
            var targetRot = Quaternion.LookRotation(_movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, _turnSpeed);
        }
    }

    private void Jump()
    {
        //formula exemple: if jumpheight is 10 so this formula will get the better velocity to achieve this jump height number for the object
        var jumpforce = Mathf.Sqrt(_jumpHeight * -2f * _gravity);

        _move = new Vector3(_movement.x, jumpforce, _movement.z);

        _rb.velocity = _move;

        _isJumping = false;
    }
}
