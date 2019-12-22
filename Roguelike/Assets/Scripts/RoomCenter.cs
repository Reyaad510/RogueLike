using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCenter : MonoBehaviour
{
    // Says if a specific room has this specific clear condition
    public bool openDoorsWhenEnemiesCleared;

    // Lists are easy to change the size of an array
    public List<GameObject> enemies = new List<GameObject>();
    public Room theRoom;

    // Start is called before the first frame update
    void Start()
    {
        if (openDoorsWhenEnemiesCleared)
        {
            theRoom.doorCloseWhenEnter = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Loop through room and check enemy at each indexes
        // When enemy killed will be removed 
        if (enemies.Count > 0 && theRoom.roomActive && openDoorsWhenEnemiesCleared)
        {
            for (var i = 0; i < enemies.Count; i++)
            {
                if (enemies[i] == null)
                {
                    // i-- to prevent error
                    enemies.RemoveAt(i);
                    i--;
                }
            }

            if (enemies.Count == 0)
            {
                theRoom.OpenDoors();
            }
        }
    }
}
