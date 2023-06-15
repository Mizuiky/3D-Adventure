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

    private float _maxHight = 5;
    private float _jumpSpeed;
    private float _timeToPeak = 0.3f; //time to achieve the highest height

    private float _gravity;

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

    private void Start()
    {
        //isolate g variable on 
        _gravity = (2 * _maxHight) / Mathf.Pow(_timeToPeak, 2);

        _jumpSpeed = _gravity * _timeToPeak;
    }

    void Update()
    {
        PlayerInput();

        RotateToSide();

        _isGrounded = GroundCheck();
    }

    private void PlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
            Jump();

        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");

        _isWalking = _horizontal != 0 || _vertical != 0 && _isGrounded;

        _movement = new Vector3(_horizontal, 0, _vertical);

        _xVelocity = _movement * _speed * Time.deltaTime;

        Debug.Log("is walking" + _isWalking);
        _animator.OnRun(_isWalking);

        //_yVelocity += _gravity * Time.deltaTime * Vector3.down;





        _finalVelocity = _xVelocity + _yVelocity;

        _rb.velocity = _finalVelocity;




    }

    private void RotateToSide()
    {
        if(_isWalking)
        {
            var targetRot = Quaternion.LookRotation(_movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, _turnSpeed);
        }
    }

    private void Jump()
    {
       _yVelocity = _jumpSpeed * Vector3.up;
    }
}
