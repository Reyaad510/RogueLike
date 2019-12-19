using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    [SerializeField] int distanceToEnd;
    [SerializeField] float xOffSet = 17f;
    [SerializeField] float yOffSet = 10f;


    [SerializeField] Color startColor, endColor;
    [SerializeField] GameObject layoutRoom;
    [SerializeField] Transform generatorPoint;

    // creating a type of variable called Direction
    // The names associated with numbers like an array(hover over them to see)
    public enum Direction { up, right, down, left };
    public Direction selectedDirection;



    // Start is called before the first frame update
    void Start()
    {
        InstantiateNewRoom();

    }

    // Update is called once per frame
    void Update()
    {
      
    }


    private void InstantiateNewRoom()
    {
        Instantiate(layoutRoom, generatorPoint.position, generatorPoint.rotation).GetComponent<SpriteRenderer>().color = startColor;
        // Casting number into direction
        selectedDirection = (Direction)Random.Range(0, 4);
        MoveGenerationPoint();
    }



    private void MoveGenerationPoint()
    {
        switch (selectedDirection)
        {
            case Direction.up:
                generatorPoint.position += new Vector3(0f, yOffSet, 0f);
                break;

            case Direction.down:
                generatorPoint.position += new Vector3(0f, -yOffSet, 0f);
                break;

            case Direction.right:
                generatorPoint.position += new Vector3(xOffSet, 0, 0f);
                break;

            case Direction.left:
                generatorPoint.position += new Vector3(-xOffSet, 0f, 0f);
                break;


        }
    }


}
