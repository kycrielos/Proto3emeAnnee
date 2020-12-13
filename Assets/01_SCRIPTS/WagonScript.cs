using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WagonScript : MonoBehaviour
{
    public Transform[] PositionTo;
    private int x;
    public int PositionNumber;
    public float Speed;
    public float DistanceTo;
    public float RotateSpeed;
    public PlatformLocked Platform;
    public int Positionlocked;
    public LockedDoorScript Door;

    // Update is called once per frame
    void Update()
    {
        float step = Speed * Time.deltaTime;
        if (Platform.Unlock)
        {
            if (x < PositionNumber)
            {
                transform.position = Vector3.MoveTowards(transform.position, PositionTo[x].position, step);
                DistanceTo = Vector3.Distance(PositionTo[x].position, transform.position);
                float singleStep = RotateSpeed * Time.deltaTime;
                transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, PositionTo[x].position - transform.position, singleStep, 0));

                if (DistanceTo <= 0.05f)
                {
                    if (x != Positionlocked)
                    {
                        x += 1;
                    }
                    else if (Door.Unlocked)
                    {
                        x += 1;
                    }
                }
            }
        }
    }
}
