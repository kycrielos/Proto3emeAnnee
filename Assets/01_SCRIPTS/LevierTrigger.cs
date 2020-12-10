using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevierTrigger : MonoBehaviour
{
    public LockedDoorScript Door;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Input.GetButtonDown("Action"))
            {
                Door.Unlocked = true;
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Input.GetButtonDown("Action"))
            {
                Door.Unlocked = true;
            }
        }
    }
}
