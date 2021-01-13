using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevierTrigger : MonoBehaviour
{
    public GameObject Pos1;
    public GameObject Pos2;
    public LockedDoorScript Door;
    public GameObject Text;
    public AudioSource SFX_Levier;

    private void Awake()
    {
        Text = GameObject.Find("Ui_Press_E");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Door.Unlocked == false)
            {
                Text.SetActive(true);
            }
            if (Input.GetButtonDown("Action"))
            {
                Door.Unlocked = true;
                Pos1.SetActive(false);
                Pos2.SetActive(true);
                Text.SetActive(false);
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Input.GetButtonDown("Action"))
            {
                Door.Unlocked = true;
                Pos1.SetActive(false);
                Pos2.SetActive(true);
                Text.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Text.SetActive(false);
        }
    }
}
