using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AspirationTriggerScript : MonoBehaviour
{
    public float AspirationTickPerSecond;
    public float AspirationDamagePerTick;
    private float AspirationTickTimer;

    public Transform Head;
    private bool PlayerVisible;
    public Transform player;

    private void Start()
    {
        player = GetComponentInParent<SnakeHeadScript>().player;
    }
    private void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(Head.position, player.position - Head.position, out hit, Mathf.Infinity))
        {
            Debug.DrawRay(Head.position, (player.position - Head.position) * hit.distance, Color.red);
            if (hit.collider.name == "Player")
            {
                PlayerVisible = true;
            }
            else
            {
                PlayerVisible = false;
            }
        }
        else
        {
            PlayerVisible = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && PlayerVisible)
        {
            AspirationTickTimer += Time.deltaTime;
            if (AspirationTickTimer >= 1 / AspirationTickPerSecond)
            {
                other.GetComponent<PlayerController>().HP -= AspirationDamagePerTick;
                AspirationTickTimer = 0;
            }
        }
    }
}
