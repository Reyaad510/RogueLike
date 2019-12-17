using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [Header("Enemy Numbers")]
    [SerializeField] float enemyMoveSpeed;
    [SerializeField] float rangeToChasePlayer;
    [SerializeField] Vector3 moveDirection;
    [SerializeField] int enemyHealth = 150;
    [SerializeField] float fireRate;
    [SerializeField] float shootRange;
    private float fireCounter;

    [Header("Objects")]
    [SerializeField] GameObject[] deathSplatter;
    [SerializeField] GameObject hitEffect;
    [SerializeField] GameObject enemyBullet;
    [SerializeField] Transform firePoint;
    [SerializeField] SpriteRenderer enemyBody;

    [Header("Boolean")]
    [SerializeField] bool shouldShoot;


    // cache
    Rigidbody2D enemyRigidBody;
    Animator enemyAnimator;




    void Start()
    {
        enemyRigidBody = GetComponent<Rigidbody2D>();
        enemyAnimator = GetComponent<Animator>();
    }

  
    void Update()
    {
        if (enemyBody.isVisible)
        {
            EnemyChaseIfPlayerInRange();
            ShouldShoot();
        }
    }


    private void EnemyChaseIfPlayerInRange()
    {
        // Distance return distance between two points
        // checking if player in range on enemy
        if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < rangeToChasePlayer)
        {
            // if in range then enemy will chase player
            moveDirection = PlayerController.instance.transform.position - transform.position;
        }
        else
        {
            // This means Vector3(0,0,0) and means when player out of range then enemy movement will be 0
            moveDirection = Vector3.zero;
        }

        // used to make enemy diagonal movement speed not super fast. Will be same as x or y
        moveDirection.Normalize();

        enemyRigidBody.velocity = moveDirection * enemyMoveSpeed;

        // If enemy in range then switch to chase animation
        EnemyChaseAnimation();


    }


    private void EnemyChaseAnimation()
    {
        if (moveDirection != Vector3.zero)
        {
            enemyAnimator.SetBool("isChasing", true);
        }
        else
        {
            enemyAnimator.SetBool("isChasing", false);
        }
    }



    private void ShouldShoot()
    {
        // Enemy will fire bullet if within range
        if (shouldShoot && Vector3.Distance(transform.position,PlayerController.instance.transform.position) < shootRange)
        {
            fireCounter -= Time.deltaTime;

            if(fireCounter <= 0)
            {
                fireCounter = fireRate;
                Instantiate(enemyBullet, firePoint.position, firePoint.rotation);
            }
        }
    }




    public void DamageEnemy(int damage)
    {
        enemyHealth -= damage;

        Instantiate(hitEffect, transform.position, transform.rotation);

        if(enemyHealth <= 0)
        {
            EnemyDie();
        }
    }


    private void EnemyDie()
    {
        Destroy(gameObject);

        int selectedSplatter = Random.Range(0, deathSplatter.Length);

        int rotation = Random.Range(0, 4);

        var splat = Instantiate(deathSplatter[selectedSplatter], transform.position, Quaternion.Euler(0f,0f, rotation * 90f));
        Destroy(splat, 2f);
        
    }






}
