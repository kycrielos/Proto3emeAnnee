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
        if (Physics.Raycast(Head.position, player.position - Head.position, out RaycastHit hitinfo))
        {
            if (hitinfo.collider.tag == "Player")
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
                other.GetComponent<PlayerController>().Damage += AspirationDamagePerTick;
                other.GetComponent<PlayerController>().Damaged();
                AspirationTickTimer = 0;
            }
        }
    }
}
