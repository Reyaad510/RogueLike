using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] int coinValue = 1;
    [SerializeField] float waitToBeCollected = 0.5f;



    private void Update()
    {
        if(waitToBeCollected > 0)
        {
            waitToBeCollected -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && waitToBeCollected <= 0)
        {
            LevelManager.instance.AddCoins(coinValue);
            Destroy(gameObject);

            AudioManager.instance.PlaySFX(5);
        }
    }





}
