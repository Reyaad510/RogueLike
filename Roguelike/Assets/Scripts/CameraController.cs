using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    [Header("Numbers")]
    public float moveSpeed;

    [Header("Objects")]
    public Transform room;

    public Camera mainCamera, bigMapCamera;

    private bool bigMapActive;



    private void Awake()
    {
        instance = this;
    }


    // Update is called once per frame
    void Update()
    {
        if (room != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(room.position.x, room.position.y, transform.position.z), moveSpeed * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (!bigMapActive)
            {
                ActivateBigMap();
            } else
            {
                DeactivateBigMap();
            }

        }
    }

    public void ChangeRoom(Transform newRoom)
    {
        room = newRoom;
    }


    private void ActivateBigMap()
    {
        if (!LevelManager.instance.isPaused)
        {


            bigMapActive = true;
            bigMapCamera.enabled = true;
            mainCamera.enabled = false;

            PlayerController.instance.canMove = false;
            Time.timeScale = 0;
            UIController.instance.mapDisplay.SetActive(false);
            UIController.instance.bigMapText.SetActive(true);
        }
    }
    
    private void DeactivateBigMap()
    {
        if (!LevelManager.instance.isPaused)
        {


            bigMapActive = false;
            bigMapCamera.enabled = false;
            mainCamera.enabled = true;

            PlayerController.instance.canMove = true;
            Time.timeScale = 1f;
            UIController.instance.mapDisplay.SetActive(true);
            UIController.instance.bigMapText.SetActive(false);
        }
    }



}
