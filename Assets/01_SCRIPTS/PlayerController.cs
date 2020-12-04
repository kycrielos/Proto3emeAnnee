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
    private bool IsJumping;
    public float JumpDelay;
    private float JumpDelayTimer;


    //Physics
    private Collider PlayerCollider;
    public float GravityScale;
    public CharacterController controller;

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

    public float GravityForce = 9.87f;
    float turnSmoothVelocity;
    public float turnSmoothTime = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
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
                Movementx += Mathf.Sqrt(1 - Mathf.Pow(Mathf.Abs(Inputx) - 1, 2)) * Inputx / Mathf.Abs(Inputx) * Acceleration;
            }
            else
            {
                Movementx = Inputx;
            }

            /*if (Inputy != 0)
            {
                ActualSpeed = MaxSpeed * (Mathf.Sqrt(2) / 2);
            }
            else
            {
                ActualSpeed = MaxSpeed;
            }*/
        }
        else if (Movementx >= 0.01f || Movementx <= -0.01f)
        {
            Movementx -= Mathf.Sqrt(1 - Mathf.Pow(Mathf.Abs(Movementx) - 1, 2)) * Movementx / Mathf.Abs(Movementx) * Deceleration;
        }
        else
        {
            Movementx = 0;
        }

        if (Inputy != 0)
        {
            if (Movementy <= 1 && Movementy >= -1)
            {
                Movementy += Mathf.Sqrt(1 - Mathf.Pow(Mathf.Abs(Inputy) - 1, 2)) * Inputy / Mathf.Abs(Inputy) * Acceleration;
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
            Movementy -= Mathf.Sqrt(1 - Mathf.Pow(Mathf.Abs(Movementy) - 1, 2)) * Movementy / Mathf.Abs(Movementy) * Deceleration;
        }
        else
        {
            Movementy = 0;
        }

        //Gere le Jump

        if (controller.isGrounded)
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
        if (!IsJumping && !controller.isGrounded)
        {
            JumpDelayTimer += Time.deltaTime;
            if (JumpDelayTimer <= JumpDelay)
            {
                Jump();
            }
        }

        //Ralenti la vitesse de saut
        if (!controller.isGrounded)
        {
            if (JumpActualForce > -GravityForce)
            {
                JumpActualForce -= GravityScale * Time.deltaTime;
            }
            else
            {
                JumpActualForce = -GravityForce;
            }
        }

        JumpDirection = new Vector3(0, JumpActualForce * Time.deltaTime, 0);
        MoveDirection = new Vector3(Movementx, 0, Movementy).normalized;

        //Deplace Le Joueur
        if (MoveDirection.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(MoveDirection.x, MoveDirection.z) * Mathf.Rad2Deg + Cam.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir * ActualSpeed * Time.deltaTime + JumpDirection);
        }
        else
        {
            controller.Move(JumpDirection);
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
        if (JumpActualForce <= 0 && !controller.isGrounded)
        {
            FallingDuration += Time.deltaTime;
        }

        if (controller.isGrounded)
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
        if (collision.gameObject.tag == "Fire")
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Fire")
        {
            GetComponent<Renderer>().material.color = Color.gray;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "MovablePlateform")
        {
            transform.parent = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "MovablePlateform")
        {
            transform.parent = null;
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
