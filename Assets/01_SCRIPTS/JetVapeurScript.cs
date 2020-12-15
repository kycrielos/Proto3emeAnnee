using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetVapeurScript : MonoBehaviour
{
    public float TickPerSecond;
    public float DamagePerTick;
    private float TickTimer;

    private bool Active;

    private float ActiveTimer;
    public float ActiveDelay;


    private void Update()
    {
        ActiveTimer += Time.deltaTime;
        if (ActiveTimer >= ActiveDelay && !Active)
        {
            ActiveTimer = 0;
            Active = true;
        }
        else if (ActiveTimer >= ActiveDelay && Active)
        {
            ActiveTimer = 0;
            Active = false;
        }

        if (Active)
        {
            GetComponent<Renderer>().enabled = true;
        }
        else
        {
            GetComponent<Renderer>().enabled = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && Active)
        {
            TickTimer += Time.deltaTime;
            if (TickTimer >= 1 / TickPerSecond)
            {
                other.GetComponent<PlayerController>().Damage += DamagePerTick;
                other.GetComponent<PlayerController>().Damaged();

                TickTimer = 0;
            }
        }
    }
}
