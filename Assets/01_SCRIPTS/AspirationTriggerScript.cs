using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AspirationTriggerScript : MonoBehaviour
{
    public Transform Head;
    public Transform player;

    public float Force;

    private void Start()
    {
        player = GetComponentInParent<SnakeHeadScript>().player;
    }

    private void OnTriggerStay(Collider other)
    {
        if (Physics.Linecast(Head.position, player.position, out RaycastHit hitinfo))
        {
            if (hitinfo.collider.tag == "Player")
            {
                player.GetComponent<PlayerController>().controller.Move(-Head.forward * Force * Time.deltaTime);
            }
        }
    }
}
