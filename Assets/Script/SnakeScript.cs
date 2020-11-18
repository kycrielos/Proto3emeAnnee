using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeScript : MonoBehaviour
{
    public Transform player;

    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            Vector3 targetDirection = new Vector3(player.position.x, transform.position.y, player.position.z) - transform.position;
            float singleStep = speed * Time.deltaTime;
            transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0));
            //transform.LookAt(new Vector3(player.position.x, transform.position.y , player.position.z));
        }
    }
}
