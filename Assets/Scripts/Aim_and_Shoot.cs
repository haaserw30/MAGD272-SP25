using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class Aim_and_Shoot : MonoBehaviour
{
    [SerializeField] private GameObject gun;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private float TimeBetweenShots = 1f;
    [SerializeField] private int midairAmmo = 1;

    //Recoil variables
    [SerializeField] private float playerRecoilVelocity = 15f;
    [SerializeField] private LayerMask whatStopsMomentum;
    //Audio
    [SerializeField] private AudioClip cannonShot;
    private AudioSource audioSource;

    private GameObject bulletInst;
    private Vector2 worldPosition;
    private Vector2 direction;
    private float angle;
    private float gunHeat;
    //recoil
    private Vector2 recoilDirection;
    private Rigidbody2D playerRB;
    public bool grounded;
    //ground Detection
    [Header("Ground Detection Options")]
    [Tooltip("Left point which we use to check below us for ground and to the side for a wall.")]
    [SerializeField] Transform leftDetectorPoint;

    [Tooltip("Right point which we use to check below us for ground and to the side for a wall.")]
    [SerializeField] Transform rightDetectorPoint;

    [SerializeField] float groundDetectionDistance = .5f;
    [SerializeField] float wallDetectionDistance = .25f;

    [Tooltip("I will try to run and jump on anything in these layers as if it was ground")]
    [SerializeField] LayerMask whatIsGround;

    private void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        HandleGunRotation();
        HandleGunShooting();
        if (gunHeat > 0)
        {
            gunHeat -= Time.deltaTime;
        }
        if (CheckGround())
        {
            midairAmmo = 1;
        }
    }

    private void FixedUpdate()
    {
       // print(grounded);
        //grounded = Physics2D.CircleCast(transform.position, 1.4f, Vector2.down, 0.05f);
        //grounded = Physics2D.CapsuleCast(transform.position, new Vector2(1f, 0.7f), CapsuleDirection2D.Vertical, 0, Vector2.down, 0.05f);
    }

    private void HandleGunRotation()
    {
        worldPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        direction = (worldPosition - (Vector2)gun.transform.position).normalized;
        gun.transform.right = direction;
        recoilDirection = (direction * -1).normalized;

        //flip the gun when it reaches a 90 degree threshold
        angle = Mathf.Atan2(direction.y, direction.x * Mathf.Rad2Deg);
        Vector3 localScale = new Vector3(1f, 1f, 1f);
        if (angle > 90 || gun.transform.rotation.z < -90)
        {
            localScale.y = -1f;
        }
        else
        {
            localScale.y = 1f;
        }
    }

    private void HandleGunShooting()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (gunHeat <= 0)
            {
                if (CheckGround())
                {
                    gunHeat = TimeBetweenShots;
                    bulletInst = Instantiate(bullet, bulletSpawnPoint.position, gun.transform.rotation);
                    //play sound FX
                    audioSource.clip = cannonShot;
                    audioSource.Play();
                }
                
                else
                {
                    if (midairAmmo > 0)
                    {
                        gunHeat = TimeBetweenShots;
                        bulletInst = Instantiate(bullet, bulletSpawnPoint.position, gun.transform.rotation);
                        //play sound FX
                        audioSource.clip = cannonShot;
                        audioSource.Play();
                        playerRB.velocity = -direction * playerRecoilVelocity;
                        midairAmmo--;
                    }

                
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Stops player movement when contacting this layer
        /*if ((whatStopsMomentum.value & (1 << collision.gameObject.layer)) > 0)
        {
            playerRB.velocity = Vector2.zero;
        }*/

        // Reset gun heat when landing, allowing player to shoot immediately when jumping
        if ((whatIsGround.value & (1 << collision.gameObject.layer)) > 0)
        {
            gunHeat = 0;
        }
    }
    public bool CheckGround()
    {
        RaycastHit2D hitLeft = Physics2D.Raycast(leftDetectorPoint.position, -leftDetectorPoint.up, groundDetectionDistance, whatIsGround);
        RaycastHit2D hitRight = Physics2D.Raycast(rightDetectorPoint.position, -rightDetectorPoint.up, groundDetectionDistance, whatIsGround);

        if (hitLeft.collider || hitRight.collider)
        {
            //Debug.Log(hitLeft.collider + " " + hitRight.collider);
            return true;
        }
        else
        {
            return false;
        }
    }
}
