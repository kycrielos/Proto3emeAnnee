using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateformeRunique : MonoBehaviour
{
    public int Value;
    public Transform Position1;
    public Transform Position2;

    // Update is called once per frame
    void Update()
    {
        if (Value == 1)
        {
            transform.position = Position1.transform.position;
        }
        else if (Value == 2)
        {
            transform.position = Position2.transform.position;
        }
    }
}
