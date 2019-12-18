using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] int healAmount = 1;
    [SerializeField] float waitToBeCollected = 0.5f;

    [Header("SFX Index Number")]
    [SerializeField] int healthPickupSFX;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        WaitToBeCollected();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && waitToBeCollected <= 0)
        {
            PlayerHealthController.instance.HealPlayer(healAmount);
            AudioManager.instance.PlaySFX(healthPickupSFX);
            Destroy(gameObject);
        }
    }


    // Fixes issue where player would IMMEDIATELY pickup item after breaking boxes. Allows time to not pick it up
    void WaitToBeCollected()
    {
        if (waitToBeCollected > 0)
        {
            waitToBeCollected -= Time.deltaTime;
        }
    }

}
