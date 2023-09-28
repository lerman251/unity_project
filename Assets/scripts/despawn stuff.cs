using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class despawnStuff : MonoBehaviour



{
    
    public Fps script;
    public float damageAmount;
    private void OnCollisionEnter(Collision collision) 
    {
        if (collision.gameObject.tag == "Player")
        {
            dealDamage(damageAmount);
        }
    }

    public void dealDamage(float damage)
    {
        script.TakeDamage(damage);
    }
    
    
}