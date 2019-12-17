using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    // Able to access this from any other script
    public static PlayerHealthController instance;

    [SerializeField] int currentHealth;
    [SerializeField] int maxHealth;


    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        currentHealth = maxHealth;
    }


    void Update()
    {
        
    }



    public void DamagePlayer()
    {
        currentHealth--;
        if (currentHealth <= 0)
        {
            PlayerController.instance.gameObject.SetActive(false);

        }
    }
 


}
