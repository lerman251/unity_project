using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class despawnstuff : MonoBehaviour
{
    private void OnCollisionEnter3D(Collision3D collision) 
    {
        if (collision.gameObject.TryGetComponent<EnemyAI>(out Enemy enemyComponent))
        {
            enemyComponent.TakeDamage(1);
        }

        Destroy(gameObject);

    }
    
}