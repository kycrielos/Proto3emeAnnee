using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Rotation
    private float Mousex;
    private float Mousey;
    public float AngularVelocity = 4f;
    private float Rotation;
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        //Retire le curseur
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Player.transform.position;
        //Recupere les inputs
        Mousex = (Mousex + AngularVelocity * Input.GetAxis("Mouse X")) % 360f;
        Mousey = (Mousey + AngularVelocity * Input.GetAxis("Mouse Y")) % 360f;

        //Limite de la rotation
        if (!(Mousey < 60))
        {
            Mousey = 60;
        }
        if (!(Mousey > 0))
        {
            Mousey = 0;
        }

        //Gere la rotation
        transform.eulerAngles = new Vector3(Mousey, Mousex, 0);
    }
}
