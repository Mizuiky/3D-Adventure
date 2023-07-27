using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cloth;
using NaughtyAttributes;

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

    private bool _clothSpeedChange = false;

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
    public PlayerHealth healthBase;

    public Action<bool> OnEndGame;
    public bool _isAlive;

    [Space]
    [SerializeField]
    private ClothChanger clothChanger;

    private Coroutine _currentClothCorrotine;

    [SerializeField]
    private Transform CameraTransform;

    public void OnValidate()
    {
        if (healthBase == null) healthBase = GetComponent<PlayerHealth>();
        if (collider == null) collider = GetComponent<CapsuleCollider>();
    }

    void Awake()
    {
        OnValidate();

        _rb = GetComponent<Rigidbody>();
        _rb.velocity = Vector3.zero;

        SaveManager.Instance.fileLoaded += LoadPlayerHealth;
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

        Vector3 checkpoint = CheckPointManager.Instance.GetLastCheckPointPosition();

        if (checkpoint != Vector3.zero)
        {
            Debug.Log("player position" + transform.position);
            Debug.Log("player position local" + transform.localPosition);

            SetPosition(checkpoint);

            Debug.Log("new player position" + transform.position);
            Debug.Log("new player position local" + transform.localPosition);
        }

        ChangeCloth(WorldManager.Instance.ClothManager.CurrentCloth, 2f);
    }

    
    [NaughtyAttributes.Button]
    public void SetPosition()
    {
        transform.position = new Vector3(193.25f, -1.40f, -27.68f);
    }

    private void OnDestroy()
    {
        SaveManager.Instance.fileLoaded -= LoadPlayerHealth;
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

    private void LoadPlayerHealth(SaveSetup setup)
    {
       healthBase.LoadLife((int)setup.CurrentPlayerHealth, setup.StartLife);             
    }

    private void SetPosition(Vector3 position)
    {
        _rb.position = position;
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
            if(!_clothSpeedChange)
            {
                _playerSpeed = 1005;
                _animator.SetSpeed(_animationNormalSpeed);
            }         
        }

        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");

        _isWalking = _horizontal != 0 || _vertical != 0 && _isGrounded;

        _animator.OnRun(_isWalking);

    }

    private void FixedUpdate()
    {
        _movement = new Vector3(_horizontal, 0, _vertical);

        _movement = Quaternion.AngleAxis(CameraTransform.transform.eulerAngles.y, Vector3.up) * _movement;

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

        ScreenShake.Instance.Shake(1f, 1f, 0.5f, 0);
        SaveManager.Instance.SavePlayerHealth((int)h._currentLife);
    }

    public void Kill(HealthBase h)
    {
        if(_isAlive)
        {
            Debug.Log("is alive false");
            _isAlive = false;

            StartCoroutine(OnPlayerKill());
        }     
    }

    public IEnumerator OnPlayerKill()
    {
     
        ScreenShake.Instance.Shake(2, 2, 2, 1);

        yield return new WaitForSeconds(.1f);

        _animator.OnDead(true);

        OnEndGame?.Invoke(true);

        yield return new WaitForSeconds(.1f);

        Invoke(nameof(Respawn), 3f);
    }

    public void Respawn()
    {    
        _isAlive = true;

        collider.enabled = true;

        _animator.OnDead(false);

        healthBase.ResetLife();

        var checkpoint = CheckPointManager.Instance.GetLastCheckPointPosition();

        if(checkpoint != Vector3.zero)
        {
            SetPosition(checkpoint);
        }        

        //OnEndGame?.Invoke(false);
    }

    public void Damage(int value, Vector3 dir)
    {
        //flashColor.ForEach(i => i.ChangeColor());
    }

    public void ChangeSpeed(float localSpeed, float duration)
    {
        StartCoroutine(ChangeSpeedCoroutine(localSpeed, duration));
    }

    public IEnumerator ChangeSpeedCoroutine(float localSpeed, float duration)
    {
        _playerSpeed = localSpeed;

        _clothSpeedChange = true;

        yield return new WaitForSeconds(duration);

        _clothSpeedChange = false;
    }

    public void ChangeCloth(ClothSetup setup, float duration)
    {
        if (_currentClothCorrotine != null) StopCoroutine(_currentClothCorrotine);

        _currentClothCorrotine = StartCoroutine(ChangeClothCoroutine(setup, duration));
    }

    public IEnumerator ChangeClothCoroutine(ClothSetup setup, float duration)
    {
        clothChanger.ChangeCloth(setup);

        yield return new WaitForSeconds(duration);

        clothChanger.ResetCloth();
    }
}
