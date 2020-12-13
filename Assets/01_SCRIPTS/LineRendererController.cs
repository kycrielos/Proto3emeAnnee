using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererController : MonoBehaviour
{
    public Transform Cristal;
    private LineRenderer diagLine;
    public CristalRotate NextCristal;
    private float timer;
    private float desactivationduration = 0.2f;
    public GameObject WinScreen;
    // Start is called before the first frame update
    void Start()
    {
        diagLine = GetComponent<LineRenderer>();
        diagLine.SetPosition(0, Cristal.position + new Vector3(0,0.2f,0));
    }
    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(Cristal.position + new Vector3(0, 0.2f, 0), Cristal.transform.forward *-1 + new Vector3(0, 0.1f, 0), out hit, Mathf.Infinity))
        {
            diagLine.SetPosition(1, hit.point);
            if (NextCristal != null)
            {
                if (hit.collider.tag == "Cristal")
                {
                    NextCristal.ActiveRayon = true;
                    timer = 0;
                }
                else
                {
                    timer += Time.deltaTime;
                    if (timer > desactivationduration)
                    {
                        timer = 0;
                        NextCristal.ActiveRayon = false;
                    }
                }
            }
            else
            {
                if (hit.collider.tag == "CristalDoor")
                {
                    hit.collider.GetComponentInParent<LockedDoorScript>().Unlocked = true;
                }
                else if (hit.collider.tag == ("SnakeHead"))
                {
                    if (WinScreen != null)
                    {
                        WinScreen.SetActive(true);
                    }
                }
            }
        }
    }
}
