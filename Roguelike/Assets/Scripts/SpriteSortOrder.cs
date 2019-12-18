using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSortOrder : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;


    // Start is called before the first frame update
    void Start()
    {
        // This sets the objects sorting layer according to their y position
        // if y position is higher up then they will be displayed behind other objects
        // Fixes issues if multiple objects next to each other on the y axis and Unity decides what is in front of what. 
        // Have to use Mathf.RounToInt because y position can be a float and sortingOrder can only be an Int. 
        // Multiplying by minus 10 to have a better chance of boxes next to each other that will have same value
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.y * -10f);
    }
}
