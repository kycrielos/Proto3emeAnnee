using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHeadScript : MonoBehaviour
{
    public Transform player;
    public Transform FollowTarget;
    public float speed;
    public SnakeScript Snake;
    // Start is called before the first frame update
    void Start()
    {
        speed = Snake.speed;
        player = Snake.player;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = FollowTarget.transform.position;
        if (!Snake.AspirationOn)
        {
            Vector3 targetDirection = player.position - transform.position;
            float singleStep = speed * Time.deltaTime;
            transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0));
            
        }
    }
}
