using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevierPlatformTrigger : MonoBehaviour
{
    public PlatformLocked Platform;
    private bool Activated;

    public bool SerpentTrigger;
    public GameObject Serpent;

    public GameObject Pos1;
    public GameObject Pos2;
    public GameObject Text;
    
    public AudioSource SFX;

    private void Awake()
    {
        Text = GameObject.Find("Ui_Press_E");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !Activated)
        {
            Text.SetActive(true);
            if (Input.GetButtonDown("Action"))
            {
                SFX.Play();
                Platform.LevierNumber += 1;
                Activated = true;
                Pos1.SetActive(false);
                Pos2.SetActive(true);
                Text.SetActive(false);
                if (SerpentTrigger)
                {
                    Serpent.SetActive(true);
                }
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && !Activated)
        {
            if (Input.GetButtonDown("Action"))
            {
                SFX.Play();
                Platform.LevierNumber += 1;
                Activated = true;
                Pos1.SetActive(false);
                Pos2.SetActive(true);
                Text.SetActive(false);
                if (SerpentTrigger)
                {
                    Serpent.SetActive(true);
                }
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
