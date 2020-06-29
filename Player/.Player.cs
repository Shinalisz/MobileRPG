using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour, IDamageable
{
    protected Joystick joystick;
    protected AButton aButton;
    protected BButton bButton;
    
    
    [SerializeField]
    public int diamonds;
    private Rigidbody2D _rigidbody2D;
    [SerializeField]
    private float _movementSpeed = 5.0f;
    [SerializeField]
    private float _jumpForce = 3.0f;
    private bool _resetJump = false;
    private bool _grounded = false;

    private PlayerAnimation _playerAnim;
    private SpriteRenderer _playerSprite;
    private SpriteRenderer _swordEffectSprite;

    [SerializeField]
    private Canvas _canvas;

    public int Health { get; set; }
      
   
    void Start()
    {
        joystick = FindObjectOfType<Joystick>();
        aButton = FindObjectOfType<AButton>();
        bButton = FindObjectOfType<BButton>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _playerAnim = GetComponent<PlayerAnimation>();
        _playerSprite = GetComponentInChildren<SpriteRenderer>();
        _swordEffectSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
        Health = 4;
    }

    
    void Update()
    {
        Movement();
        Attack();        
    }

    void Movement()
    {
        
        float horizontalInput = joystick.Horizontal;
        _movementSpeed = 5.0f;
        _grounded = IsGrounded();
        if(horizontalInput > 0)
        {
            Flip(true);            
        }
        else if(horizontalInput < 0)
        {
            Flip(false);
        }
        
        if(bButton.pressed == true || Input.GetKeyDown(KeyCode.Space) && IsGrounded()) //by default like this IsGrounded is true
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpForce);
            StartCoroutine(ResetJumpRoutine());
            _playerAnim.Jump(true);
        }
        _rigidbody2D.velocity = new Vector2(horizontalInput * _movementSpeed, _rigidbody2D.velocity.y);
        _playerAnim.Move(horizontalInput);
    }

    bool IsGrounded()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 1.0f, 1 << 8); //<< this is BITSHIFT = taking the 32bit num sequesnce that is layer 8 and bringing it to 1
        
        if (hitInfo.collider != null)
        {
            Debug.DrawRay(transform.position, Vector2.down, Color.green);
            if (_resetJump == false)
            {
                _playerAnim.Jump(false);
                return true;
            }            
        }

        return false;
    }

    void Flip(bool faceRight)
    {
        if(faceRight == true)
        {
            _playerSprite.flipX = false;            
            _swordEffectSprite.flipY = false;

            Vector3 newPos = _swordEffectSprite.transform.localPosition;
            newPos.x = 0.55f;
            newPos.y = -0.09f;
            _swordEffectSprite.transform.localPosition = newPos;
        }
        else if(faceRight == false)
        {
            _playerSprite.flipX = true;            
            _swordEffectSprite.flipY = true;

            Vector3 newPos = _swordEffectSprite.transform.localPosition;
            newPos.x = -0.05f;
            newPos.y = 0.18f;
            _swordEffectSprite.transform.localPosition = newPos;
        }
    }

    public void Attack()
    {
        if (aButton.pressed || Input.GetMouseButtonDown(0) && IsGrounded())
        {
            _playerAnim.Attack();            
        }        
    }

    IEnumerator ResetJumpRoutine()
    {
        _resetJump = true;
        yield return new WaitForSeconds(1.0f);
        _resetJump = false;
    }

    public void Damage()
    {
        if(Health < 0)
        {
            return;
        }
        Debug.Log("Player Damage called.");
        Health--;
        UIManager.Instance.UpdateLives(Health);
       if(Health == 0)
        {
            _playerAnim.Dead();
        }       

    }

    public void AddGems(int amount)
    {
        diamonds += amount;
        UIManager.Instance.UpdateGemCount(diamonds);
    }
   
}
