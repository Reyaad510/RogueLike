using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenParts : MonoBehaviour
{

    [Header("Numbers")]
    [SerializeField] float brokenPartSpeed = 3f;
    [SerializeField] float deceleration = 5f;
    [SerializeField] float lifeTime = 3f;
    [SerializeField] float fadeSpeed = 2.5f;
    private Vector3 brokenPartMoveDirection;

    [Header("Objects")]
    [SerializeField] SpriteRenderer brokenPartSpriteRenderer;




    // Start is called before the first frame update
    void Start()
    {
        brokenPartMoveDirection.x = Random.Range(-brokenPartSpeed, brokenPartSpeed);
        brokenPartMoveDirection.y = Random.Range(-brokenPartSpeed, brokenPartSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        BrokenPartsMovement();

    }


    void BrokenPartsMovement()
    {
        transform.position += brokenPartMoveDirection * Time.deltaTime;

        // Lerp takes in two values and makes a number go extremely close to zero. 
        // Different from Mathf.MoveTowards bcuz Lerp() moves very fast initiall and then slows down
        // Variable we always want to change is direction, we want value to go towards zero on x,y,z axis, and then how fast should it work to get to the value
        brokenPartMoveDirection = Vector3.Lerp(brokenPartMoveDirection, Vector3.zero, deceleration * Time.deltaTime);

        LifeTime();
    }


    void LifeTime()
    {
        // Timer so broken parts will eventually destroy itself
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            // Mathf.MoveTowards(x,y) will go to a target number equally 
            // First is value we want to change being the alpha value, we want to go towards 0f, how fast we want it to happen is fadeSpeed * Time.deltaTime
            brokenPartSpriteRenderer.color = new Color(brokenPartSpriteRenderer.color.r, brokenPartSpriteRenderer.color.g, brokenPartSpriteRenderer.color.b, Mathf.MoveTowards(brokenPartSpriteRenderer.color.a, 0f, fadeSpeed * Time.deltaTime));

            if(brokenPartSpriteRenderer.color.a == 0f)
            {
                Destroy(gameObject);
            }
          
        }
    }



}
