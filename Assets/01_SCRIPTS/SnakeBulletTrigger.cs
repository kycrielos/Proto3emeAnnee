using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBulletTrigger : MonoBehaviour
{
    public Transform Target;
    public SnakeScript Snake;
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            Snake.target = Target;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Snake.target = null;
        }

    }
}
