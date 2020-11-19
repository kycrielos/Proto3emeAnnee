using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaterformMovementScript : MonoBehaviour
{
    public Transform Haut;
    public Transform Bas;
    private float ActualDistance;
    public bool Monte;
    public float Speed = 1;
    // Update is called once per frame
    void Update()
    {
        if (transform.position.y >= Haut.position.y)
        {
            Monte = false;
        }
        else if (transform.position.y <= Bas.position.y)
        {
            Monte = true;
        }

        if (Monte)
        {
            transform.position += new Vector3(0, Time.deltaTime * Speed, 0);
        }
        else
        {
            transform.position -= new Vector3(0, Time.deltaTime * Speed, 0);
        }
    }
}
