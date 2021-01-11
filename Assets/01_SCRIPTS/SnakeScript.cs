﻿using System.Collections;
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

    private float delayTimer;
    public float Delay;

    public GameObject DelayFeedBack;

    public GameObject SFX_Prev;
    public AudioSource SFX_Crachat;

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
        Debug.DrawLine(Head.position, player.position, Color.white);
        if (!AspirationOn)
        {
            CdTimer += Time.deltaTime;
            if (CdTimer > Cooldown && Physics.Linecast(Head.position, player.position, out RaycastHit hitinfo))
            {
                if (hitinfo.collider.tag == "Player")
                {
                    SFX_Prev.SetActive(true);
                    DelayFeedBack.SetActive(true);
                    delayTimer += Time.deltaTime;
                    if (delayTimer >= Delay)
                    {
                        SFX_Prev.SetActive(false);
                        DelayFeedBack.SetActive(false);
                        if (target != null)
                        {
                            SFX_Crachat.Play();
                            ShootOn = true;
                        }
                        else
                        {
                            AspirationOn = true;
                        }
                        delayTimer = 0;
                    }
                }
                else
                {
                    delayTimer = 0;
                    DelayFeedBack.SetActive(false);
                    SFX_Prev.SetActive(false);
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
