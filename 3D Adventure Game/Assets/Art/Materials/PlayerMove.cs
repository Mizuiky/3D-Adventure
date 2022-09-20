using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
{
    [Header("Jump Settings")]

    [SerializeField]
    private float gravity;
    [SerializeField]
    private float jumpHeight;

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

    Rigidbody rb;
    private Vector3 _movement;

    private float _velocity;

    private float _horizontal;
    private float _vertical;

    private Vector3 _move = Vector3.zero;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
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
        if (rb.velocity.y < 0)
            _move.y = 0;
            
        //Gravity is being applied constantly to the object
        _move.y += gravity * Time.deltaTime;

        rb.velocity = _move;

        if (_isJumping)
            Jump();

        Move();     
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
            rb.velocity = _movement * _speed * Time.deltaTime;
            Debug.Log("velocity " + rb.velocity);
        }

        //RotateToSide(); 

        //_animation.OnRun(_isWalking);
    }

    private void Jump()
    {
        _isJumping = false;

        //formula exemple: if jumpheight is 10 so this formula will get the better velocity to achieve this jump height number for the object
        var jumpforce = Mathf.Sqrt(jumpHeight * -2f * gravity);

        _move.y = jumpforce;
    }
}
