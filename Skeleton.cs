using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy, IDamageable
{
    public int Health { get; set; }
    public override void Init() //instead of Start()
    {
        base.Init();
        //code here
        Health = base.health;
    }

    public override void Movement()
    {
        base.Movement();        
    }

    public void Damage()
    {
        if(_isDead == true)
        {
            return;
        }
        Debug.Log("Skeleton Damaged!");        
        Health--;
        _anim.SetTrigger("Hit");
        _isHit = true;
        _anim.SetBool("InCombat", true);
        
        if(Health < 1)
        {
            _isDead = true;
            _anim.SetTrigger("Death");
            GameObject diamond = Instantiate(diamondPrefab, transform.position, Quaternion.identity);
            diamond.GetComponent<Diamond>().gems = base.gems;
        }
       
    }
}
