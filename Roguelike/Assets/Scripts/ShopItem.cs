using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour
{

    [SerializeField] GameObject buyMessage;
    [SerializeField] int itemCost;

    private bool inBuyZone;
    [SerializeField] bool isHealthRestore, isHealthUpgrade, isWeapon;



    private void Update()
    {
        PurchaseItem();
    }




    private void PurchaseItem()
    {
        if (inBuyZone)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (LevelManager.instance.currentCoins >= itemCost)
                {
                    LevelManager.instance.SpendCoins(itemCost);
                    if (isHealthRestore)
                    {
                        PlayerHealthController.instance.HealPlayer(PlayerHealthController.instance.maxHealth);
                    }
                }
            }
        }
    }






     void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            buyMessage.SetActive(true);
            inBuyZone = true;
        }
    }


     void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            buyMessage.SetActive(false);
            inBuyZone = false;
        }
    }

}
