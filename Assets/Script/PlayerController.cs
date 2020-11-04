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
    public float SpeedReduction;
    public float MaxSpeed;
    public float ActualSpeed;
    public float JumpForce;
    private float JumpActualForce;
    public float JumpDecreaseSpeed;
    public bool CanMove = true;

    //Rotation
    private float Mousex;
    private float Mousey;
    public float AngularVelocity = 4f;
    private float Rotation;

    //Physics
    private Rigidbody PlayerRigidbody;
    public bool IsGrounded;
    private Collider PlayerCollider;

    // Start is called before the first frame update
    void Start()
    {
        PlayerRigidbody = GetComponent<Rigidbody>();
        PlayerCollider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
    }

    void PlayerMovement()
    {
        //Recupere les Input
        Movementx = Input.GetAxisRaw("Horizontal") * SpeedReduction;
        Movementy = Input.GetAxisRaw("Vertical");

        //Gere la rotation
        Mousex = (Mousex + AngularVelocity * Input.GetAxis("Mouse X")) % 360f;
        PlayerRigidbody.rotation = Quaternion.AngleAxis(Mousex, Vector3.up);
        Rotation = transform.eulerAngles.y;

        //Avance Diagonale
        if (Movementx != 0 && Movementy > 0)
        {
            ActualSpeed = MaxSpeed * (Mathf.Sqrt(2) / 2);
        }
        //Recule Diagonale
        else if (Movementx != 0 && Movementy < 0)
        {
            ActualSpeed = MaxSpeed * (Mathf.Sqrt(2) / 2) * SpeedReduction;
        }
        //Avance ou Pas de cote
        else if (Movementx != 0 || Movementy > 0)
        {
            ActualSpeed = MaxSpeed;
        }
        //Recule
        else if (Movementy < 0)
        {
            ActualSpeed = MaxSpeed * SpeedReduction;
        }
        //Ne Bouge Pas
        else
        {
            ActualSpeed = 0;
        }

        IsGroundedVerif();

        if (Input.GetButtonDown("Jump"))
        {
            //Gere le Jump
            if (IsGrounded)
            {
                JumpActualForce = JumpForce;
                //PlayerRigidbody.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
                Debug.Log("Cheat");
            }
        }

        if (!IsGrounded)
        {
            if (JumpActualForce > 0)
            {
                JumpActualForce -= JumpDecreaseSpeed;
            }
            else
            {
                JumpActualForce = 0;
            }
        }

        JumpDirection = new Vector3(0, JumpActualForce, 0);

        MoveDirection = new Vector3(Movementx, 0, Movementy) * ActualSpeed;
        MoveDirection = Quaternion.Euler(0, Rotation, 0) * MoveDirection;

        //Deplace Le Joueur
        Vector3 newVel = MoveDirection + JumpDirection;
        PlayerRigidbody.velocity = newVel;
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
}
