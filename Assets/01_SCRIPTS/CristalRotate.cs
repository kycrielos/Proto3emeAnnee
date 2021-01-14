﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CristalRotate : MonoBehaviour
{
    public Transform Cam;
    public float speed;
    public GameObject Rayon;
    public bool ActiveRayon;
    public GameObject Fx;
    public GameObject SFX_CristalHit;

    public bool IsFirst;

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, Cam.position-transform.position, step, 0));
        if (ActiveRayon)
        {
            if (!IsFirst)
            {
                SFX_CristalHit.SetActive(true);
            }
            Fx.SetActive(true);
            Rayon.SetActive(true);
        }
        else
        {
            Fx.SetActive(false);
            Rayon.SetActive(false);
        }
    }
}
