using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGenerator : MonoBehaviour
{

    // space between rooms
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

    public RoomPrefabs rooms;

    private List<GameObject> generatedOutlines = new List<GameObject>();

    public RoomCenter centerStart, centerEnd;
    public RoomCenter[] potentialCenters;



    // Start is called before the first frame update
    void Start()
    {
        InstantiateStartRoom();
        InstantiateConnectingRooms();
    }

    // Update is called once per frame
    void Update()
    {
        // will only run in Unity editor
#if Unity_Editor
          ReloadSceneForDebug();
#endif
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

        GetRoomPositionsForGeneratingOutlines();

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


    private void GetRoomPositionsForGeneratingOutlines()
    {
        // create room outline
        CreateRoomOutline(Vector3.zero);
        foreach (GameObject room in layoutRoomObjects)
        {
            CreateRoomOutline(room.transform.position);
        }
        CreateRoomOutline(endRoom.transform.position);


        foreach(GameObject outline in generatedOutlines)
        {
            bool generateCenter = true;

            if(outline.transform.position == Vector3.zero)
            {
                Instantiate(centerStart, outline.transform.position, transform.rotation).theRoom = outline.GetComponent<Room>();

                generateCenter = false;
            }

            if(outline.transform.position == endRoom.transform.position)
            {
                Instantiate(centerEnd, outline.transform.position, transform.rotation).theRoom = outline.GetComponent<Room>();
                generateCenter = false;
            }


            if (generateCenter)
            {
                // creates the tiles on the outlines
                int centerSelect = Random.Range(0, potentialCenters.Length);
                Instantiate(potentialCenters[centerSelect], outline.transform.position, transform.rotation).theRoom = outline.GetComponent<Room>();
            }
        }

    }



    public void CreateRoomOutline(Vector3 roomPosition)
    {

        bool roomAbove = Physics2D.OverlapCircle(roomPosition + new Vector3(0f, yOffSet, 0f), 0.2f, whatIsRoom);
        bool roomBelow = Physics2D.OverlapCircle(roomPosition + new Vector3(0f, -yOffSet, 0f), 0.2f, whatIsRoom);
        bool roomLeft = Physics2D.OverlapCircle(roomPosition + new Vector3(-xOffSet, 0f, 0f), 0.2f, whatIsRoom);
        bool roomRight = Physics2D.OverlapCircle(roomPosition + new Vector3(xOffSet, 0f, 0f), 0.2f, whatIsRoom);

        int directionCount = 0;

        // When we make a new room we will check how many rooms are around it and add that to a counter
        if (roomAbove)
        {
            directionCount++;
        }
        if (roomBelow)
        {
            directionCount++;
        }
        if (roomLeft)
        {
            directionCount++;
        }
        if (roomRight)
        {
            directionCount++;
        }

        switch (directionCount)
        {
            // depending on counter number will tell us how many rooms are near us
            // We can then find right room amount to generate the correct outline
            case 0:
                Debug.Log("Found no room exists!");
                break;

            case 1:
                if (roomAbove)
                {
                    generatedOutlines.Add(Instantiate(rooms.singleUp, roomPosition, transform.rotation));
                }
                if (roomBelow)
                {
                    generatedOutlines.Add(Instantiate(rooms.singleDown, roomPosition, transform.rotation));
                }
                if (roomLeft)
                {
                    generatedOutlines.Add(Instantiate(rooms.singleLeft, roomPosition, transform.rotation));
                }
                if (roomRight)
                {
                    generatedOutlines.Add(Instantiate(rooms.singleRight, roomPosition, transform.rotation));
                }
                break;
            case 2:
                if(roomAbove && roomBelow)
                {
                    generatedOutlines.Add(Instantiate(rooms.doubleUpDown, roomPosition, transform.rotation));
                }
                if (roomLeft && roomRight)
                {
                    generatedOutlines.Add(Instantiate(rooms.doubleLeftRight, roomPosition, transform.rotation));
                }
                if (roomAbove && roomRight)
                {
                    generatedOutlines.Add(Instantiate(rooms.doubleUpRight, roomPosition, transform.rotation));
                }
                if (roomRight && roomBelow)
                {
                    generatedOutlines.Add(Instantiate(rooms.doubleRightDown, roomPosition, transform.rotation));
                }
                if (roomBelow && roomLeft)
                {
                    generatedOutlines.Add(Instantiate(rooms.doubleDownLeft, roomPosition, transform.rotation));
                }
                if (roomLeft && roomAbove)
                {
                    generatedOutlines.Add(Instantiate(rooms.doubleLeftUp, roomPosition, transform.rotation));
                }

                break;
            case 3:
                if (roomAbove && roomRight && roomBelow)
                {
                    generatedOutlines.Add(Instantiate(rooms.tripleUpRightDown, roomPosition, transform.rotation));
                }
                if (roomRight && roomBelow && roomLeft)
                {
                    generatedOutlines.Add(Instantiate(rooms.tripleRightDownLeft, roomPosition, transform.rotation));
                }
                if (roomBelow && roomLeft && roomAbove)
                {
                    generatedOutlines.Add(Instantiate(rooms.tripleDownLeftUp, roomPosition, transform.rotation));
                }
                if (roomLeft && roomAbove && roomRight)
                {
                    generatedOutlines.Add(Instantiate(rooms.tripleLeftUpRight, roomPosition, transform.rotation));
                }

                break;
            case 4:
                if (roomAbove && roomRight && roomBelow && roomLeft)
                {
                    generatedOutlines.Add(Instantiate(rooms.fourway, roomPosition, transform.rotation));
                }

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



[System.Serializable]
public class RoomPrefabs
{
    public GameObject singleUp, singleDown, singleRight, singleLeft,
        doubleUpDown, doubleLeftRight, doubleUpRight, doubleRightDown, doubleDownLeft, doubleLeftUp,
        tripleUpRightDown, tripleRightDownLeft, tripleDownLeftUp, tripleLeftUpRight,
        fourway;
}
