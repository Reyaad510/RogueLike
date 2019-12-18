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



    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        if (room != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(room.position.x, room.position.y, transform.position.z), moveSpeed * Time.deltaTime);
        }
    }

    public void ChangeRoom(Transform newRoom)
    {
        room = newRoom;
    }



}
