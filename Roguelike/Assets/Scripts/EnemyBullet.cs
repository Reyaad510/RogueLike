using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed;
    private Vector3 bulletDirection;

    [Header("SFX Index Number")]
    [SerializeField] int bulletImpactSFX;


    // Start is called before the first frame update
    void Start()
    {
        // Find the player and take away enemy current position and normalize to fix diagonal speed
        bulletDirection = PlayerController.instance.transform.position - transform.position;
        bulletDirection.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += bulletDirection * bulletSpeed * Time.deltaTime;  
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            PlayerHealthController.instance.DamagePlayer();
        }

        Destroy(gameObject);
        AudioManager.instance.PlaySFX(bulletImpactSFX);

    }





    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }




}
