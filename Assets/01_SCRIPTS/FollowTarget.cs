using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public Transform Camera;
    // Start is called before the first frame update
    void Start()
    {
        Camera = GameObject.Find("Camera").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Camera);
    }
}
