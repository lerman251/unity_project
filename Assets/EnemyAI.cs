using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAi : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    public float health, maxHealth = 3f;
    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    //Attacking
    public float shootForce = 12f, upwardForce = 4f;
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;
    
    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    private void Awake()
    {
        player = GameObject.Find("Twelf").transform;
        agent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        health = maxHealth;
    }
    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;

        if(health <= 0) 
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }
    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();
        if (walkPointSet)
            agent.SetDestination(walkPoint);
        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }
    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }
    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);
        transform.LookAt(player);
        
        if (!alreadyAttacked)
        {
            
            ///Attack code here
            GameObject rb = Instantiate(projectile, transform.position, Quaternion.identity);
            
            rb.GetComponent<Rigidbody>().AddForce(transform.forward *shootForce, ForceMode.Impulse);
            rb.GetComponent<Rigidbody>().AddForce(transform.up * upwardForce, ForceMode.Impulse);
            ///End of attack code
            alreadyAttacked = true;
            Destroy(rb,15);
            
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
            
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }
    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }
    
    public void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == "projectile")
        {
        gameObject.transform.parent = collision.gameObject.transform;
        Destroy(GetComponent<Rigidbody>());
        GetComponent<SphereCollider>().enabled = false;
        }
        if(collision.tag == "Player")
        {
        var healthComponent = GetComponent<Health>();
        if(healthComponent != null)
        {
            healthComponent.TakeDamage(1);
        }
        }
    }

    
}