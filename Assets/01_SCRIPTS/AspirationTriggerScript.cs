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
            RaycastHit hit;
            if (Physics.Raycast(transform.position, other.transform.position - transform.position, out hit, Mathf.Infinity))
            {
                Debug.DrawRay(transform.position, (other.transform.position - transform.position) * hit.distance, Color.yellow);
                if (hit.collider.name == "Player")
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
    }
}
