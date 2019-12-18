using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public bool doorCloseWhenEnter;
    public GameObject[] doors;





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
        }
    }
}
