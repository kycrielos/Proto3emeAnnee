using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformLocked : MonoBehaviour
{
    public PlaterformMovementVerticalScript Platform;
    public int LevierNumber;
    public bool Unlock;

    // Update is called once per frame
    void Update()
    {
        Activation();
    }

    void Activation()
    {
        if (LevierNumber > 1)
        {
            Unlock = true;
            Platform.enabled = true;
        }
    }
}
