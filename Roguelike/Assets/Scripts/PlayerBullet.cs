using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{

    [SerializeField] float bulletSpeed = 20f;
    [SerializeField] GameObject impactEffect;

    // cached
    Rigidbody2D bulletRigidBody;

    // Start is called before the first frame update
    void Start()
    {
        bulletRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        bulletRigidBody.velocity = transform.right * bulletSpeed;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        // particle effect will be destroyed by going clicking on it in unity and look for "Stop Action" and choose Destroy
        Instantiate(impactEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }


    // This is an actual Unity Method
    // Says once bullet is offscreen then will destroy them
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }




}
