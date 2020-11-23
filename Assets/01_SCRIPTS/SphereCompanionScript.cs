using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SphereCompanionScript : MonoBehaviour
{
    private NavMeshAgent Agent;
    public GameObject Player;

    public bool FollowPlayer;
    // Start is called before the first frame update
    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        FollowPlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (FollowPlayer)
        {
            Agent.SetDestination(Player.transform.position);
        }
        else
        {
            Agent.SetDestination(transform.position);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CompanionExitTrigger")
        {
            FollowPlayer = false;
        }
    }
}
