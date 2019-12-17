using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Config
    [SerializeField] float moveSpeed = 6f;
    [SerializeField] Transform gunArm;
    private Vector2 moveInput;


    // cached component references
    Rigidbody2D myRigidBody;
    Camera theCam;



    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        theCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        WeaponAim();
    }

    private void Run()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y  = Input.GetAxisRaw("Vertical");

        Vector2 playerVelocity = new Vector2(moveInput.x * moveSpeed, moveInput.y * moveSpeed);
        myRigidBody.velocity = playerVelocity;
    }

    private void WeaponAim()
    {
        // getting mouse position
        Vector3 mousePos = Input.mousePosition;
        // getting worldToScreen Point position
        Vector3 screenPoint = theCam.WorldToScreenPoint(transform.localPosition);

        // Flip sprite according to mousePosition in relation of player
        if (mousePos.x < screenPoint.x)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            gunArm.localScale = new Vector3(-1f, -1f, 1f);
        }
        else
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            gunArm.localScale = new Vector3(1f, 1f, 1f);
        }


        // rotate gun hand
        Vector2 offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);
        float angleOfWeaponAndMouse = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        // Quaternion.Euler() converts Rotation into a vector3. In Unity if you debug you will see rotation has other values and vector3 alone wouldnt work so have to do it like this
        gunArm.rotation = Quaternion.Euler(0, 0, angleOfWeaponAndMouse);
    }
}
