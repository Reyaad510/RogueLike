﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{


    [SerializeField] float enemyMoveSpeed = 3f;
    [SerializeField] float rangeToChasePlayer = 7f;
    [SerializeField] Vector3 moveDirection;



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
        EnemyChaseIfPlayerInRange();

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






}