using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour

{
    //Input
    private float Inputx;
    private float Inputy;

    //Movement
    Vector3 MoveDirection;
    Vector3 JumpDirection;
    private float Movementx;
    private float Movementy;
    public float MaxSpeed;
    private float ActualSpeed;
    public float Acceleration;
    public float Deceleration;

    //Jump
    public float JumpForce;
    private float JumpActualForce;
    public float JumpDecreaseSpeed;
    private bool IsJumping;
    public float JumpDelay;
    private float JumpDelayTimer;


    //Physics
    private Rigidbody PlayerRigidbody;
    public bool IsGrounded;
    private Collider PlayerCollider;

    //Camera
    public GameObject Cam;

    //Fall
    public float FallingDuration;
    public float TickPerSecond = 1;
    public float MinimumDamagePerTick = 1;

    //Vie
    public float MaxHP;
    public float HP;
    public Transform Spawnpoint;
    public float Damage;

    // Start is called before the first frame update
    void Start()
    {
        PlayerRigidbody = GetComponent<Rigidbody>();
        PlayerCollider = GetComponent<Collider>();
        HP = MaxHP;
    }

    // Update is called once per frame
    void Update()
    {
        IsDead();
        PlayerMovement();
        FallingDamage();
    }

    void PlayerMovement()
    {
        //Recupere les Input
        Inputx = Input.GetAxisRaw("Horizontal");
        Inputy = Input.GetAxisRaw("Vertical");

        if (Inputx != 0)
        {
            if (Movementx <= 1 && Movementx >= -1)
            {
                Movementx += Mathf.Sqrt(1 - Mathf.Pow(Mathf.Abs(Inputx) - 1, 2)) * Time.deltaTime * Inputx / Mathf.Abs(Inputx) * Acceleration;
            }
            else
            {
                Movementx = Inputx;
            }

            if (Inputy != 0)
            {
                ActualSpeed = MaxSpeed * (Mathf.Sqrt(2) / 2);
            }
            else
            {
                ActualSpeed = MaxSpeed;
            }
        }
        else if (Movementx >= 0.01f || Movementx <= -0.01f)
        {
            Movementx -= Mathf.Sqrt(1 - Mathf.Pow(Mathf.Abs(Movementx) - 1, 2)) * Time.deltaTime * Movementx / Mathf.Abs(Movementx) * Deceleration;
        }
        else
        {
            Movementx = 0;
        }

        if (Inputy != 0)
        {
            if (Movementy <= 1 && Movementy >= -1)
            {
                Movementy += Mathf.Sqrt(1 - Mathf.Pow(Mathf.Abs(Inputy) - 1, 2)) * Time.deltaTime * Inputy / Mathf.Abs(Inputy) * Acceleration;
            }
            else
            {
                Movementy = Inputy;
            }
            if (Inputx == 0)
            {
                ActualSpeed = MaxSpeed;
            }
        }
        else if (Movementy >= 0.01f || Movementy <= -0.01f)
        {
            Movementy -= Mathf.Sqrt(1 - Mathf.Pow(Mathf.Abs(Movementy) - 1, 2)) * Time.deltaTime * Movementy / Mathf.Abs(Movementy) * Deceleration;
        }
        else
        {
            Movementy = 0;
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
            //Gravity.enabled = false;
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

        //Ralenti la vitesse de saut
        if (!IsGrounded)
        {
            //Gravity.enabled = true;
            if (JumpActualForce > 0)
            {
                JumpActualForce -= JumpDecreaseSpeed * Time.deltaTime;
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
            if (hit.collider.tag == "Ground" || hit.collider.tag == "Fire")
            {
                IsGrounded = true;
            }
            else if (hit.collider.tag == "MovablePlateform")
            {
                IsGrounded = true;
                transform.parent = hit.collider.transform;
            }
        }
        else
        {
            IsGrounded = false;
        }
    }

    public void IsDead()
    {
        if (HP <= 0)
        {
            transform.position = Spawnpoint.position;
            transform.rotation = Spawnpoint.rotation;
            HP = MaxHP;
        }
    }

    public void FallingDamage()
    {
        if (JumpActualForce <= 0 && !IsGrounded)
        {
            FallingDuration += Time.deltaTime;
        }

        if (IsGrounded)
        {
            while (FallingDuration >= 1 / TickPerSecond)
            {
                Damage += MinimumDamagePerTick * Mathf.Round(FallingDuration * TickPerSecond);
                FallingDuration -= 1 / TickPerSecond;
                Debug.Log(Damage);
            }
            FallingDuration = 0;
            Damaged();
        }
    }

    public void Damaged()
    {
        HP -= Damage;
        Damage = 0;
    }

    private void OnCollisionStay(Collision collision)
    {
        /*if (collision.gameObject.tag == "MovablePlateform")
        {
            transform.parent = collision.transform;
        }*/
        if (collision.gameObject.tag == "Fire")
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "MovablePlateform")
        {
            transform.parent = null;
        }
        if (collision.gameObject.tag == "Fire")
        {
            GetComponent<Renderer>().material.color = Color.gray;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Checkpoint")
        {
            Spawnpoint = GetComponentInChildren<Transform>();
        }
    }
}
