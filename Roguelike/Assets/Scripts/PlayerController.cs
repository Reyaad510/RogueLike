using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Config
    [SerializeField] float moveSpeed = 6f;
    private Vector2 moveInput;

    // cached component references
    Rigidbody2D myRigidBody;

    

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
    }

    private void Run()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y  = Input.GetAxisRaw("Vertical");

        Vector2 playerVelocity = new Vector2(moveInput.x * moveSpeed, moveInput.y * moveSpeed);
        myRigidBody.velocity = playerVelocity;

        


    }
}
