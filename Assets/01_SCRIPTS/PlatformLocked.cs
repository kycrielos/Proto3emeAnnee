using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformLocked : MonoBehaviour
{
    public PlaterformMovementVerticalScript Platform;
    public int LevierNumber;

    // Update is called once per frame
    void Update()
    {
        if (LevierNumber > 1)
        {
            Platform.enabled = true;
        }
    }
}
