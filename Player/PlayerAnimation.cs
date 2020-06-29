using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _spriteAnimator;
    private Animator _swordArcAnimator;
    void Start()
    {
        _spriteAnimator = GetComponentInChildren<Animator>();
        _swordArcAnimator = transform.GetChild(1).GetComponent<Animator>();
    }
        
    
    public void Move(float move)
    {
        _spriteAnimator.SetFloat("Move", Mathf.Abs(move)); //Abs = absolute value ie.: -1 is 1. When we move left it would go -1 but animation on plays if greater than zero.
    }

    public void Jump(bool jumping)
    {
        _spriteAnimator.SetBool("Jumping", jumping);
    }

    public void Attack()
    {
        _spriteAnimator.SetTrigger("Attack");
        _swordArcAnimator.SetTrigger("SwordEffect");
    }

    public void Dead()
    {
        _spriteAnimator.SetBool("Death", true);
    }

   
}
