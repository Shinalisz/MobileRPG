using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy, IDamageable
{
    public int Health { get; set; }
    public GameObject acidEffectPrefab;
    public override void Init() //instead of Start()
    {
        base.Init();
        //code here
        Health = base.health;
    }

    public override void Update()
    {
        
    }

    public void Damage()
    {
        if (_isDead == true)
        {
            return;
        }
        Health--;
        if (Health < 1)
        {
            _isDead = true;
            _anim.SetTrigger("Death");
            GameObject diamond = Instantiate(diamondPrefab, transform.position, Quaternion.identity);
            diamond.GetComponent<Diamond>().gems = base.gems;
        }
    }

    public override void Movement()
    {
        //sit still(don't do enemy movement)
    }

    public void Attack()
    {
        Vector3 pos = new Vector3(-49.232f, 1.628f, 0f);
        Instantiate(acidEffectPrefab, pos, Quaternion.identity);
    }
}
