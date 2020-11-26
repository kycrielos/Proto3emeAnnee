using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeScript : MonoBehaviour
{
    public Transform player;

    public float speed;

    public bool AspirationOn;

    private float AspirationTimer;
    public float AspirationDuration;
    public GameObject AspirationTrigger;

    public Transform target;
    public GameObject Bullet;
    private Vector3 SpawnPos;
    public float SpawnDistance;

    bool test;
    
    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            if (AspirationOn)
            {
                AspirationTrigger.SetActive(true);
                AspirationTimer += Time.deltaTime;
                if (AspirationTimer >= AspirationDuration)
                {
                    AspirationOn = false;
                    AspirationTrigger.SetActive(false);
                    AspirationTimer = 0;
                }
            }
            else
            {
                Vector3 targetDirection = new Vector3(player.position.x, transform.position.y, player.position.z) - transform.position;
                float singleStep = speed * Time.deltaTime;
                transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0));
            }
            if (!test)
            {
                Shoot();
                test = true;
            }
        }
    }

    

    public void Shoot()
    {
        SpawnPos = transform.position + transform.forward * SpawnDistance;
        Bullet.GetComponent<SnakeBulletscript>().target = target;
        Instantiate(Bullet, SpawnPos, transform.rotation);
    }
}
