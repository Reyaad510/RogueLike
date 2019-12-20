using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGenerator : MonoBehaviour
{

    [SerializeField] int distanceToEnd;
    [SerializeField] float xOffSet = 17f;
    [SerializeField] float yOffSet = 10f;


    [SerializeField] Color startColor, endColor;
    [SerializeField] GameObject layoutRoom;
    [SerializeField] Transform generatorPoint;

    private GameObject endRoom;

   
    // When we add a room will add to new list
    private List<GameObject> layoutRoomObjects = new List<GameObject>();

    // creating a type of variable called Direction
    // The names associated with numbers like an array(hover over them to see)
    public enum Direction { up, right, down, left };
    public Direction selectedDirection;



    // layermask used in unity to choose roomlayout
    public LayerMask whatIsRoom;



    // Start is called before the first frame update
    void Start()
    {
        InstantiateStartRoom();
        InstantiateConnectingRooms();
    }

    // Update is called once per frame
    void Update()
    {
        ReloadSceneForDebug();
    }


    private void InstantiateStartRoom()
    {
        Instantiate(layoutRoom, generatorPoint.position, generatorPoint.rotation).GetComponent<SpriteRenderer>().color = startColor;
        // Casting number into direction
        selectedDirection = (Direction)Random.Range(0, 4);
        MoveGenerationPoint();


    }


    // Create rooms connecting to start room.
    // Physics2D will say if generatorpoint moves to where a room already exists to keep going in same direction until reaches where room isnt there
    // On RoomLayout prefab had to add a boxcollider2d for this to work
    private void InstantiateConnectingRooms()
    {
        for (var i = 0; i < distanceToEnd; i++)
        {
            GameObject newRoom = Instantiate(layoutRoom, generatorPoint.position, generatorPoint.rotation);

            // adding all rooms to List
            // not adding Start or End room
            layoutRoomObjects.Add(newRoom);

            // getting end room = last room
            if(i + 1 == distanceToEnd)
            {
                newRoom.GetComponent<SpriteRenderer>().color = endColor;
                // removing last Room from layoutRoomObjects List
                layoutRoomObjects.RemoveAt(layoutRoomObjects.Count - 1);
                endRoom = newRoom;
            }

            selectedDirection = (Direction)Random.Range(0, 4);
            MoveGenerationPoint();

            // makes sure dont instantiate a room where a room already exists;
            while (Physics2D.OverlapCircle(generatorPoint.position, 0.2f, whatIsRoom))
            {
                MoveGenerationPoint();
            }
        }
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


    private void ReloadSceneForDebug()
    {
        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

}
