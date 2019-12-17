using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Config
    [Header("Numbers")]
    [SerializeField] float moveSpeed = 6f;
    [SerializeField] float timeBetweenShots = 0.2f;
    [SerializeField] float shotCounter;

    [Header("Objects")]
    [SerializeField] Transform gunArm;
    [SerializeField] Animator playerAnimator;
    [SerializeField] GameObject bulletToFire;
    [SerializeField] Transform firePoint;
    private Vector2 moveInput;


    // cached component references
    Rigidbody2D myRigidBody;
    Camera theCam;





    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        theCam = Camera.main;
    }

    void Update()
    {
        Run();
        WeaponAim();
        FireBullet();
    }





    private void Run()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y  = Input.GetAxisRaw("Vertical");

        // Normalize makes when player moves diagonal direction that they dont move faster than when they move x or y alone.
        moveInput.Normalize();

        Vector2 playerVelocity = new Vector2(moveInput.x * moveSpeed, moveInput.y * moveSpeed);
        myRigidBody.velocity = playerVelocity;

        // Saying if player is moving x or y direction to allow walk animation to happen
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x)  > Mathf.Epsilon;
        bool playerHasVerticalSpeed = Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon;
        playerAnimator.SetBool("isWalking", playerHasHorizontalSpeed || playerHasVerticalSpeed);
     


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


    private void FireBullet()
    {
        if (Input.GetMouseButtonDown(0)) {

            Instantiate(bulletToFire, firePoint.transform.position, firePoint.rotation);
            shotCounter = timeBetweenShots;

        }

        if (Input.GetMouseButton(0))
        {
            shotCounter -= Time.deltaTime;

            if(shotCounter <= 0)
            {
                Instantiate(bulletToFire, firePoint.transform.position, firePoint.rotation);
                shotCounter = timeBetweenShots;
            }
        }
    }
}
