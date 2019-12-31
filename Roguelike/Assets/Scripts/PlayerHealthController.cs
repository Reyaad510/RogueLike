using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    // Able to access this from any other script
    public static PlayerHealthController instance;

    [SerializeField] int currentHealth;
    public int maxHealth;
    [SerializeField] float damageInvincibilityTime = 1f;
    private float invincibilityCount;

    [Header("SFX Index Number")]
    [SerializeField] int playerDeathSFX;
    [SerializeField] int playerTakeDamageSFX;



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

    private void Update()
    {
        PlayerInvincibility();
    }


    void UpdateHealthUI()
    {
        UIController.instance.healthSlider.value = currentHealth;
        UIController.instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
    }


    public void DamagePlayer()
    {
        // if invinc count <= 0 then we can take damage
        if (invincibilityCount <= 0)
        {
            AudioManager.instance.PlaySFX(playerTakeDamageSFX);
            currentHealth--;
            // reset invinc count so we dont take multiple damage fast
            invincibilityCount = damageInvincibilityTime;

            // changed player alpha to less visible to tell user they are invincible from enemy shots for a little time
            PlayerController.instance.bodySpriteRenderer.color = new Color(PlayerController.instance.bodySpriteRenderer.color.r, PlayerController.instance.bodySpriteRenderer.color.g, PlayerController.instance.bodySpriteRenderer.color.b, 0.5f);

            if (currentHealth <= 0)
            {
                AudioManager.instance.PlaySFX(playerDeathSFX);
                PlayerController.instance.gameObject.SetActive(false);
                UIController.instance.deathScreen.SetActive(true);
                AudioManager.instance.PlayGameOver();
                AudioManager.instance.PlaySFX(playerDeathSFX);

            }
            UpdateHealthUI();
        }
    }

    void PlayerInvincibility()
    {
        // invic counter will be subtracting every frame by Time.deltaTime which is like 0.2f i believe?
        if (invincibilityCount > 0)
        {
            invincibilityCount -= Time.deltaTime;

            // changes character alpha back up to show no longer invincible
            if (invincibilityCount <= 0)
            {
                PlayerController.instance.bodySpriteRenderer.color = new Color(PlayerController.instance.bodySpriteRenderer.color.r, PlayerController.instance.bodySpriteRenderer.color.g, PlayerController.instance.bodySpriteRenderer.color.b, 255f);
            }
        }
    }



    public void DashInvincible(float length)
    {
        invincibilityCount = length;
        PlayerController.instance.bodySpriteRenderer.color = new Color(PlayerController.instance.bodySpriteRenderer.color.r, PlayerController.instance.bodySpriteRenderer.color.g, PlayerController.instance.bodySpriteRenderer.color.b, 255f);

    }

    public void HealPlayer(int healAmount)
    {
        currentHealth += healAmount;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        UpdateHealthUI();
    }



 


}
