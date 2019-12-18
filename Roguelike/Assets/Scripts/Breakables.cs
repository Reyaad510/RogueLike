using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakables : MonoBehaviour
{
    [SerializeField] GameObject[] brokenPart;
    [SerializeField] int maxParts = 5;
    [SerializeField] int minParts = 2;

    [Header("Items")]
    public bool shouldDropItem;
    [SerializeField] GameObject[] itemsToDrop;
    [SerializeField] float itemDropPercent;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            // Says if we are dashing then break the breakable
            if (PlayerController.instance.dashCounter > 0)
            {
                Destroy(gameObject);


                // Loop through random amount to determine when break box will show a certain amount of broken box parts
                // Show broken parts
                int partsToDrop = Random.Range(minParts, maxParts);
              
                for(int i = 0; i < partsToDrop; i++)
                {
                    int randomBrokenPart = Random.Range(0, brokenPart.Length);
                    Instantiate(brokenPart[randomBrokenPart], transform.position, transform.rotation);
                }

                // drop items
                if (shouldDropItem)
                {
                    // float random range INCLUDES abilitiy to get top number. If were int it would NOT
                    float dropChance = Random.Range(0f, 100f);

                    // will drop item
                    if(dropChance < itemDropPercent)
                    {
                        // giving list of items to randomly drop
                        int randomItem = Random.Range(0, itemsToDrop.Length);
                        Instantiate(itemsToDrop[randomItem], transform.position, transform.rotation);
                    }

                }

                
            }
        }
    }


}
