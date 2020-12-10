using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevierPlatformTrigger : MonoBehaviour
{
    public PlatformLocked Platform;
    private bool Activated;

    public bool SerpentTrigger;
    public GameObject Serpent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !Activated)
        {
            if (Input.GetButtonDown("Action"))
            {
                Platform.LevierNumber += 1;
                Activated = true;
                if (SerpentTrigger)
                {
                    Serpent.SetActive(true);
                }
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && !Activated)
        {
            if (Input.GetButtonDown("Action"))
            {
                Platform.LevierNumber += 1;
                Activated = true;
                if (SerpentTrigger)
                {
                    Serpent.SetActive(true);
                }
            }
        }
    }
}
