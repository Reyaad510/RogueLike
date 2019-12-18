using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakables : MonoBehaviour
{
    [SerializeField] GameObject[] brokenPart;
    [SerializeField] int maxParts = 5;
    [SerializeField] int minParts = 2;


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
                int partsToDrop = Random.Range(minParts, maxParts);
              
                for(int i = 0; i < partsToDrop; i++)
                {
                    int randomBrokenPart = Random.Range(0, brokenPart.Length);
                    Instantiate(brokenPart[randomBrokenPart], transform.position, transform.rotation);
                }

                
            }
        }
    }


}
