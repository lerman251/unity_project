using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireball : MonoBehaviour
{
    public EnemyAi enemy;
    public float fireballDamageAmount;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            dealDamage(fireballDamageAmount);
        }
    }

    public void dealDamage(float damage)
    {
        enemy.TakeDamage(damage);
    }
}
