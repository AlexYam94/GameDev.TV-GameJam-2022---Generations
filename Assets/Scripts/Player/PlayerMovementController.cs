using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] float _moveSpeed;
    [SerializeField] float _jumpForce;
    [SerializeField] SpriteRenderer _playerSprite;
    [SerializeField] float _groundDetectDistance = .7f;
    [SerializeField] LayerMask _groundLayer;
    [SerializeField] BoxCollider2D _playerCollider;
    [SerializeField] float _dashSpeed;
    [SerializeField] float _dashTime;
    [SerializeField] float _waitAfterDashing;

    Rigidbody2D _rb;
    PlayerAnimationController _animationController;
    bool _canDoubleJump = false;
    PlayerAbilityController _abilityController;
    float _dashCounter;
    float _dashRechargeCounter;

    public bool canInput { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animationController = GetComponent<PlayerAnimationController>();
        canInput = true;
        _abilityController = GetComponent<PlayerAbilityController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!canInput) return;
        float horizontal = Input.GetAxisRaw("Horizontal");
        if (horizontal < 0)
        {
            Flip(false);
        }
        else if (horizontal > 0)
        {
            Flip(true);
        }
        Move();
    }

    private void Move()
    {
        Vector2 velocity = _rb.velocity;
        float horizontal = Input.GetAxisRaw("Horizontal");
        Debug.Log(horizontal);
        velocity.x = horizontal * _moveSpeed;
        _animationController.Move(Mathf.Abs(horizontal));

        //Dash
        if (_dashRechargeCounter > 0)
        {
            _dashRechargeCounter = Mathf.Max(_dashRechargeCounter - Time.deltaTime, 0);
        }
        //else if (_abilitiesController.canDash && Input.GetButtonDown("Fire2") && _canStand)
        else if (_abilityController.canDash && Input.GetKeyDown(KeyCode.K) )
        {
            _dashCounter = _dashTime;
            _dashRechargeCounter = _waitAfterDashing;
        }

        if (_dashCounter > 0)
        {
            _dashCounter -= Time.deltaTime;
            _rb.velocity = new Vector2(_dashSpeed * transform.localScale.x, 0);
            return;
        }

        // Jump
        bool grounded = IsGrounded();
        if ((_abilityController.canDoubleJump && _canDoubleJump || grounded) && Input.GetButtonDown("Jump"))
        {
            velocity.y = _jumpForce;

            _animationController.Jump(!grounded);
            if (grounded)
            {
                _canDoubleJump = true;
            }
            else
            {
                //_animationController.DoubleJump();
                _canDoubleJump = false;
            }
        }
        _rb.velocity = velocity;
    }

    private bool IsGrounded()
    {
        RaycastHit2D hitLeft;
        RaycastHit2D hitMiddle;
        RaycastHit2D hitRight;
        Vector2 middle = transform.position;
        Vector2 left = transform.position;
        Vector2 right = transform.position;
        middle.y -= _playerCollider.size.y / 2;
        left.y -= _playerCollider.size.y / 2;
        right.y -= _playerCollider.size.y / 2;
        left.x += _playerCollider.size.x / 2;
        right.x -= _playerCollider.size.x / 2f;
        hitMiddle = Physics2D.Raycast(middle, Vector2.down, _groundDetectDistance, _groundLayer);
        hitLeft = Physics2D.Raycast(left, Vector2.down, _groundDetectDistance, _groundLayer);
        hitRight = Physics2D.Raycast(right, Vector2.down, _groundDetectDistance, _groundLayer);
        Debug.DrawRay(middle, Vector2.down * _groundDetectDistance, Color.red);
        Debug.DrawRay(left, Vector2.down * _groundDetectDistance, Color.red);
        Debug.DrawRay(right, Vector2.down * _groundDetectDistance, Color.red);
        bool isGround = hitLeft.collider != null || hitMiddle.collider != null || hitRight.collider != null;
        _animationController.Jump(!isGround);
        return isGround;

    }

    private void Flip(bool facingRight)
    {
        //playerSprite.flipX = !facingRight;
        Vector3 scale = _playerSprite.transform.localScale;
        if (!facingRight && scale.x < 0)
        {
            scale.x = 1;
        }
        else if(facingRight && scale.x > 0)
        {
            scale.x = -1;
        }
        //else scale.x *= 1;

        _playerSprite.transform.localScale = scale;
    }
}
