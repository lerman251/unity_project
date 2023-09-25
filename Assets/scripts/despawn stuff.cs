using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class despawnstuff : MonoBehaviour
{
    private void OnCollisionEnter(Collider collision) 
    {
        if (collision.gameObject.TryGetComponent<EnemyAI>(out Steve SteveComponent))
        {
            SteveComponent.TakeDamage(1);
        }

        Destroy(gameObject);

    }
    
}