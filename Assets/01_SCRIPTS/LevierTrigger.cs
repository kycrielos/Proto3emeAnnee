using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevierTrigger : MonoBehaviour
{
    public GameObject Pos1;
    public GameObject Pos2;
    public LockedDoorScript Door;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Input.GetButtonDown("Action"))
            {
                Door.Unlocked = true;
                Pos1.SetActive(false);
                Pos2.SetActive(true);
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
                Pos1.SetActive(false);
                Pos2.SetActive(true);
            }
        }
    }
}
