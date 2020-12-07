using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeScript : MonoBehaviour
{
    public Transform player;

    public float speed;

    public bool AspirationOn;
    public bool ShootOn;

    private float AspirationTimer;
    public float AspirationDuration;
    public GameObject AspirationTrigger;

    public Transform target;
    public GameObject Bullet;
    private Vector3 SpawnPos;
    public float SpawnDistance;

    public float Cooldown;
    private float CdTimer;
    public Transform Head;

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            AI();
            if (AspirationOn)
            {
                Aspiration();
            }
            else
            {
                FollowPlayer();
            }

            if (ShootOn)
            {
                Shoot();
                ShootOn = false;
                CdTimer = 0;
            }
        }
    }
    public void AI()
    {
        if (!AspirationOn)
        {
            CdTimer += Time.deltaTime;
            RaycastHit hit;
            if (CdTimer > Cooldown && Physics.Raycast(transform.position, player.position - transform.position, out hit, Mathf.Infinity))
            {
                Debug.DrawRay(transform.position, (player.position-transform.position) * hit.distance, Color.yellow);
                if (hit.collider.name == "Player")
                {
                    if (target != null)
                    {
                        ShootOn = true;
                    }
                    else
                    {
                        AspirationOn = true;
                    }
                }
            }
        }
    }

    public void FollowPlayer()
    {
        Vector3 targetDirection = new Vector3(player.position.x, transform.position.y, player.position.z) - transform.position;
        float singleStep = speed * Time.deltaTime;
        transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0));
    }


    public void Aspiration()
    {
        AspirationTrigger.SetActive(true);
        AspirationTimer += Time.deltaTime;
        if (AspirationTimer >= AspirationDuration)
        {
            AspirationOn = false;
            AspirationTrigger.SetActive(false);
            AspirationTimer = 0;
            CdTimer = 0;
        }
    }

    public void Shoot()
    {
        SpawnPos = Head.position + transform.forward * SpawnDistance;
        Bullet.GetComponent<SnakeBulletscript>().target = target;
        Instantiate(Bullet, SpawnPos, transform.rotation);
    }
}
