using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateformMovementHorizontalScript : MonoBehaviour
{
    public Transform Position1;
    public Transform Position2;
    public bool GoPosition1;
    public float Speed = 1;
    public float DistanceTo;


    // Update is called once per frame
    void FixedUpdate()
    {
        if (GoPosition1)
        {
            DistanceTo = Vector3.Distance(Position1.position, transform.position);
            float step = Speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, Position1.position, step);
        }
        else
        {
            DistanceTo = Vector3.Distance(Position2.position, transform.position);
            float step = Speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, Position2.position, step);
        }

        if (DistanceTo <= 0.05)
        {
            GoPosition1 = !GoPosition1;
        }
    }
}
