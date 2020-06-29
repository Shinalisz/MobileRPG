using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField]
    protected int health;
    [SerializeField]
    protected float speed;
    [SerializeField]
    protected int gems;
    [SerializeField]
    protected Transform pointA, pointB;

    protected Vector3 _currentTarget;
    protected Animator _anim;
    protected SpriteRenderer _spriteRend;
    protected bool _isDead = false;

    protected bool _isHit = false;
    protected Player player;
    [SerializeField]
    protected GameObject diamondPrefab;
    

    public virtual void  Init()
    {
        _anim = GetComponentInChildren<Animator>();
        _spriteRend = GetComponentInChildren<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public virtual void Start()
    {
        Init();
        transform.position = pointA.position;
    }
    
    public virtual void Update()
    { 
       
        if (_anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") && _anim.GetBool("InCombat") == false)
        {
            return;
        }
        if(_isDead == false)
            Movement();
    }

    public virtual void Movement()
    {
        if (transform.position == pointA.position)
        {
            _currentTarget = pointB.position;
            _anim.SetTrigger("Idle");
            _spriteRend.flipX = false;
        }
        else if (transform.position == pointB.position)
        {
            _currentTarget = pointA.position;
            _anim.SetTrigger("Idle");
            _spriteRend.flipX = true;
        }

        if (_isHit == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, _currentTarget, speed * Time.deltaTime);
        }

        float distance = Vector3.Distance(transform.localPosition, player.transform.localPosition);
        if(distance > 2.0f)
        {
            _isHit = false;
            _anim.SetBool("InCombat", false);
        }        

        Vector3 direction = player.transform.localPosition - transform.localPosition;
        if (direction.x < 0 && _anim.GetBool("InCombat") == true)
        {
            _spriteRend.flipX = true;
        }
        else if (direction.x > 0 && _anim.GetBool("InCombat") == true)
        {
            _spriteRend.flipX = false;
        }

    }    
    
}
