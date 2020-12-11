using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoorScript : MonoBehaviour
{
    public bool Unlocked;

    // Update is called once per frame
    void Update()
    {
        if (Unlocked)
        {
            this.gameObject.SetActive(false);
        }
    }
}
