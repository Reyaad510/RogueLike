using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [Header("Bullet Numbers")]
    [SerializeField] float bulletSpeed;
    [SerializeField] int bulletDamage;

    [Header("Objects")]
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


        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyController>().DamageEnemy(bulletDamage);
        }
    }



    // This is an actual Unity Method
    // Says once bullet is offscreen then will destroy them
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }




}
