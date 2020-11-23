using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour

{
    //Movement
    Vector3 MoveDirection;
    Vector3 JumpDirection;
    private float Movementx;
    private float Movementy;
    public float MaxSpeed;
    private float ActualSpeed;
    public float JumpForce;
    private float JumpActualForce;
    public float JumpDecreaseSpeed;
    public bool CanMove = true;
    private bool IsJumping;
    public float JumpDelay;
    private float JumpDelayTimer;


    //Physics
    private Rigidbody PlayerRigidbody;
    public bool IsGrounded;
    private Collider PlayerCollider;
    private CustomGravity Gravity;

    //Camera
    public GameObject Cam;

    //Vie
    public int HP;

    // Start is called before the first frame update
    void Start()
    {
        PlayerRigidbody = GetComponent<Rigidbody>();
        PlayerCollider = GetComponent<Collider>();
        Gravity = GetComponent<CustomGravity>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
    }

    void PlayerMovement()
    {
        //Recupere les Input
        Movementx = Input.GetAxisRaw("Horizontal");
        Movementy = Input.GetAxisRaw("Vertical");

        //Diagonale
        if (Movementx !=  0 && Movementy != 0)
        {
            ActualSpeed = MaxSpeed * (Mathf.Sqrt(2) / 2);
        }
        //Avance ou recule
        else if (Movementx != 0 || Movementy != 0)
        {
            ActualSpeed = MaxSpeed;
        }
        //Ne Bouge Pas
        else
        {
            ActualSpeed = 0;
        }

        IsGroundedVerif();

        //Gere le Jump

        if (IsGrounded)
        {
            JumpDelayTimer = 0;
            if (IsJumping)
            {
                IsJumping = false;
            }
            Gravity.enabled = false;
            Jump();
        }

        //Laisse un délai au joueur dans le vide pour sauter
        if (!IsJumping && !IsGrounded)
        {
            JumpDelayTimer += Time.deltaTime;
            if (JumpDelayTimer <= JumpDelay)
            {
                Jump();
            }
        }

        //FRalenti la vitesse de saut
        if (!IsGrounded)
        {
            Gravity.enabled = true;
            if (JumpActualForce > 0)
            {
                JumpActualForce -= JumpDecreaseSpeed;
            }
            else
            {
                JumpActualForce = 0;
            }
        }

        Vector3 controlRight = Vector3.Cross(Cam.transform.up, Cam.transform.forward);
        Vector3 controlForward = Vector3.Cross(Cam.transform.right, Vector3.up);

        JumpDirection = new Vector3(0, JumpActualForce, 0);

        MoveDirection = (Movementx * controlRight + controlForward * Movementy) * ActualSpeed;

        //Deplace Le Joueur
        Vector3 newVel = MoveDirection + JumpDirection;
        PlayerRigidbody.velocity = newVel;
        if (Movementx != 0 || Movementy != 0)
        {
            transform.rotation = Quaternion.LookRotation(MoveDirection);
        }
    }

    public void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            IsJumping = true;
            JumpActualForce = JumpForce;
        }
    }

    public void IsGroundedVerif()
    {
        //Lance un raycast pour checker la distance entre le joueur et le sol
        RaycastHit hit;
        if (Physics.Raycast(PlayerCollider.bounds.center, -Vector3.up, out hit, PlayerCollider.bounds.extents.y + 0.1f))
        {
            Debug.DrawRay(PlayerCollider.bounds.center, -Vector3.up * hit.distance, Color.red);
            if (hit.collider.tag == "Ground")
            {
                IsGrounded = true;
            }
        }
        else
        {
            IsGrounded = false;
        }
    }
<<<<<<< HEAD:Assets/01_SCRIPTS/PlayerController.cs

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "MovablePlateform")
        {
            transform.parent = collision.transform;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "MovablePlateform")
        {
            transform.parent = null;
        }
    }
=======
>>>>>>> parent of 7231b2c... Rename_Prefabs:Assets/Script/PlayerController.cs
}
