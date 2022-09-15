using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
{
    public float fallingGravityScale;
    public float gravity;
    public float jumpForce;

    public bool isGrounded;

    Rigidbody rb;

    private float _velocity;
    private bool _isJumping;

    private Vector3 _move;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionStay()
    {
        isGrounded = true;
        _velocity = 0;
    }

    void OnCollisionExit()
    {
        isGrounded = false;
    }

    void Update()
    {
        if (rb.velocity.y > 0)
        {
            //Velocity. y >= 0 means the player is jumping
            _velocity += gravity * fallingGravityScale * Time.deltaTime;
        }

        _move.y = _velocity;
        rb.velocity = _move;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            _isJumping = true;
    }

    private void FixedUpdate()
    {
        if (_isJumping)
            Jump();
    }

    private void Jump()
    {
        _isJumping = false;

        _velocity = jumpForce;

        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);  
    }
}
