using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    [SerializeField] int distanceToEnd;


    [SerializeField] Color startColor, endColor;
    [SerializeField] GameObject layoutRoom;
    [SerializeField] Transform generatorPoint;



    private void Awake()
    {
 
    }



    // Start is called before the first frame update
    void Start()
    {
        Instantiate(layoutRoom, generatorPoint.position, generatorPoint.rotation).GetComponent<SpriteRenderer>().color = startColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
