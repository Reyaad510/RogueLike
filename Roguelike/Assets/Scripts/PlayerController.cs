using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // This means every version of PlayerController that exists in unity will all be set to equal the same instance
    public static PlayerController instance;

    // Config
    [Header("Numbers")]
    [SerializeField] float moveSpeed = 6f;
    [SerializeField] float timeBetweenShots = 0.2f;
    [SerializeField] float shotCounter;
    [SerializeField] Vector2 moveInput;
    [SerializeField] float activeMoveSpeed;

    [Header("SFX Index Number")]
    [SerializeField] int playerDashSFX;
    [SerializeField] int bulletShotSFX;


    [Header("Dash")]
    [SerializeField] float dashSpeed = 8f;
    [SerializeField] float dashLength = 0.5f;
    [SerializeField] float dashCooldown = 1f;
    [SerializeField] float dashInvincibility = 0.5f;
    private float dashCoolCounter;
    [HideInInspector]
    public float dashCounter;

    [Header("Objects")]
    [SerializeField] Transform gunArm;
    [SerializeField] GameObject bulletToFire;
    [SerializeField] Transform firePoint;
    public SpriteRenderer bodySpriteRenderer;

    [HideInInspector]
    public bool canMove = true;



    // cached component references
    Animator playerAnimator;
    Rigidbody2D myRigidBody;
    Camera theCam;


    // instance = this; says PlayerController that is assigned to instance is "this" one. Set for all playercontrollers. 17. Creating & MovingEnemy
    // This only works for this because player is single. Will never have more than one player
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();
        theCam = Camera.main;
        activeMoveSpeed = moveSpeed;
    }

    void Update()
    {
        if (canMove)
        {
            Run();
            WeaponAim();
            FireBullet();
            Dash();
        }
        else
        {
            CantMove();
        }
    }





    private void Run()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y  = Input.GetAxisRaw("Vertical");

        // Normalize makes when player moves diagonal direction that they dont move faster than when they move x or y alone.
        moveInput.Normalize();

        Vector2 playerVelocity = new Vector2(moveInput.x * activeMoveSpeed, moveInput.y * activeMoveSpeed);
        myRigidBody.velocity = playerVelocity;

        PlayerRunAnimation();
     


    }


    private void PlayerRunAnimation()
    {
        // Saying if player is moving x or y direction to allow walk animation to happen
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
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
            AudioManager.instance.PlaySFX(bulletShotSFX);
            shotCounter = timeBetweenShots;

        }

        if (Input.GetMouseButton(0))
        {
            shotCounter -= Time.deltaTime;

            if(shotCounter <= 0)
            {
                Instantiate(bulletToFire, firePoint.transform.position, firePoint.rotation);
                AudioManager.instance.PlaySFX(bulletShotSFX);
                shotCounter = timeBetweenShots;
            }
        }
    }

    private void Dash()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (dashCoolCounter <= 0 && dashCounter <= 0)
            {
                activeMoveSpeed = dashSpeed;
                AudioManager.instance.PlaySFX(playerDashSFX);
                dashCounter = dashLength;
                playerAnimator.SetTrigger("Dash");
                PlayerHealthController.instance.DashInvincible(dashInvincibility);
             
            }
        }

        if(dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;
            if(dashCounter <= 0)
            {
                activeMoveSpeed = moveSpeed;
                dashCoolCounter = dashCooldown;
            }
        }

        if(dashCoolCounter > 0)
        {
            dashCoolCounter -= Time.deltaTime;
        }

    }

    private void CantMove()
    {
        myRigidBody.velocity = Vector2.zero;
        playerAnimator.SetBool("isWalking", false);
    }
}
