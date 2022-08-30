using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]

public class Player : MonoBehaviour 
{
    [SerializeField] private  float _walkSpeed;
    [SerializeField] private  float _jumpForce;
    [SerializeField] private SpriteRenderer _characterSprite;
    
    private MoveState _moveState = MoveState.Idle;
    private Rigidbody2D _rigidbody2D;
    private Animator _animatorController;
    private Vector3 _input;
    private float _walkTime = 0, _walkCooldown = 0.2f;
    
    enum MoveState
    {
        Idle,
        Walk,
        Jump
    }

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animatorController = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_moveState == MoveState.Jump)
        {
            if(_rigidbody2D.velocity == Vector2.zero)
            {
                Idle();
            }
        }
        else if (_moveState == MoveState.Walk)
        {
            _walkTime -= Time.deltaTime;
            
            if(_walkTime <= 0)
            {
                Idle();
            }
        }
    }
    
    public void Move()
    {
        if (_moveState != MoveState.Jump)
        {
            _input = new Vector2(Input.GetAxis("Horizontal"), 0);
            transform.position += _input * _walkSpeed * Time.deltaTime;
            _moveState = MoveState.Walk;

            if (_input.x != 0)
            {
                _characterSprite.flipX = !(_input.x > 0);
            }
            
            _walkTime = _walkCooldown;
            _animatorController.Play("Walk");
        }
    }
    
    public void Jump()
    {
        if (_moveState != MoveState.Jump)
        {
            _rigidbody2D.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);
            _moveState = MoveState.Jump;
        }
    }

    private void Idle()
    {
        _moveState = MoveState.Idle;
        _animatorController.Play("Idle");
    }
}