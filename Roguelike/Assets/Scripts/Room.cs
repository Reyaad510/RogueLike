using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    // Says if doors close when player enter a room
    public bool doorCloseWhenEnter;
    // Says if a specific room has this specific clear condition
   //  public bool openDoorsWhenEnemiesCleared;

    // Says if player is in that specific room
    [HideInInspector]
    public bool roomActive;
    public GameObject[] doors;

    // public List<GameObject> enemies = new List<GameObject>();


    private void Update()
    {
        // Loop through room and check enemy at each indexes
        // When enemy killed will be removed 
       /* if(enemies.Count > 0 && roomActive && openDoorsWhenEnemiesCleared)
        {
            for(var i = 0; i < enemies.Count; i++)
            {
                if(enemies[i] == null)
                {
                    // i-- to prevent error
                    enemies.RemoveAt(i);
                    i--;
                }
            }
            
            if(enemies.Count == 0)
            {
                OpenDoors();
            }
        } */
    }


    public void OpenDoors()
    {
        foreach (GameObject door in doors)
        {
            door.SetActive(false);
            doorCloseWhenEnter = false;
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            CameraController.instance.ChangeRoom(transform);

            if (doorCloseWhenEnter)
            {
                foreach (GameObject door in doors){
                    door.SetActive(true);
                }
            }


            roomActive = true;
        }
    }


    // If player left room then room isnt active. 
    // We do this to prevent the Update of counting enemies for every single room at any point in time
    // Only want to happen for the room that the player is currently in
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            roomActive = false;
        }
    }
}
