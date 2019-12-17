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

    

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        WeaponPosition();
    }

    private void Run()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y  = Input.GetAxisRaw("Vertical");

        Vector2 playerVelocity = new Vector2(moveInput.x * moveSpeed, moveInput.y * moveSpeed);
        myRigidBody.velocity = playerVelocity;
    }

    private void WeaponPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);

        // rotate gun hand
        Vector2 offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);
        float angleOfWeaponAndMouse = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        // Quaternion.Euler() converts Rotation into a vector3. In Unity if you debug you will see rotation has other values and vector3 alone wouldnt work so have to do it like this
        gunArm.rotation = Quaternion.Euler(0, 0, angleOfWeaponAndMouse);
    }
}
