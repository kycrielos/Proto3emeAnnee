using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AspirationTriggerScript : MonoBehaviour
{
    public float AspirationTickPerSecond;
    public float AspirationDamagePerTick;
    private float AspirationTickTimer;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            AspirationTickTimer += Time.deltaTime;
            if (AspirationTickTimer >= 1 / AspirationTickPerSecond)
            {
                other.GetComponent<PlayerController>().HP -= AspirationDamagePerTick;
                AspirationTickTimer = 0;
            }
        }
    }
}
