using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateformeRunique : MonoBehaviour
{
    public int Value;

    public float Speed = 1;
    public float Duration;
    private float Timer;



    // Update is called once per frame
    void Update()
    {
        if (Value == 0)
        {
            Timer = 0;
        }

        if (Value == 1)
        {
            Timer += Time.deltaTime;
            if (Timer < Duration)
            {
                transform.Translate(Vector3.down * Time.deltaTime * Speed);
            }
            else
            {
                Value = 0;
            }
        }

        else if (Value == 2)
        {
            Timer += Time.deltaTime;
            if (Timer < Duration)
            {
                transform.Translate(Vector3.down * Time.deltaTime * Speed);
            }
            else
            {
                Value = 0;
            }

        }

        else if (Value == 3)
        {
            Timer += Time.deltaTime;
            if (Timer < Duration)
            {
                transform.Translate(Vector3.down * Time.deltaTime * Speed);
            }
            else
            {
                Value = 0;
            }

        }
    }

}
