using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBehaviour : MonoBehaviour
{
    public Transform pos1, pos2;
    public float speed, offset;

    private Transform targetPos;
    private bool changeTarget;

    private GameObject Player;

    void Start()
    {
        targetPos = pos1;
    }

    void Update()
    {
        float distance = Vector3.Distance(targetPos.position, transform.position);
        if (distance <= offset)
        {
            changeTarget = !changeTarget;
        }

        if (changeTarget)
        {
            targetPos = pos2;
        } else
        {
            targetPos = pos1;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPos.position, speed * Time.deltaTime);
    }
}
