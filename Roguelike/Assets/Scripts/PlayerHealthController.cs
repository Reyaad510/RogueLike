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
        // grabbing healthSlider from UIcontroller script and setting the slider values in relation to player health
        UIController.instance.healthSlider.maxValue = maxHealth;
        UIController.instance.healthSlider.value = currentHealth;
        UIController.instance.healthText.text = currentHealth.ToString() + " / " +  maxHealth.ToString();
    }


    void UpdateHealthUI()
    {
        UIController.instance.healthSlider.value = currentHealth;
        UIController.instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
    }



    public void DamagePlayer()
    {
        currentHealth--;
        if (currentHealth <= 0)
        {
            PlayerController.instance.gameObject.SetActive(false);

        }
        UpdateHealthUI();
    }
 


}
