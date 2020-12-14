using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CristalTableScript : MonoBehaviour
{
    private bool Activated;
    public Cinemachine.CinemachineFreeLook CamScript;
    private GameObject Player;
    public Transform CamTransform;
    private float timer;
    public float delay;
    public GameObject Text;

    private void Awake()
    {
        Player = GameObject.Find("Player");
        Text = GameObject.Find("Ui_Press_E");
    }
    private void Start()
    {
        Text.SetActive(false);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer < delay)
        {
            CamScript.m_YAxis.m_InputAxisName = "Null";
            CamScript.m_XAxis.m_InputAxisName = "Null";
        }
        else
        {
            CamScript.m_YAxis.m_InputAxisName = "Mouse Y";
            CamScript.m_XAxis.m_InputAxisName = "Mouse X";
            if (Activated)
            {
                Player.GetComponent<CharacterController>().enabled = false;
                Player.GetComponent<MeshRenderer>().enabled = false;
                Player.transform.position = new Vector3(CamTransform.position.x, Player.transform.position.y, CamTransform.position.z);
                if (Input.GetButtonDown("Action"))
                {
                    CamScript.Priority = 0;
                    Activated = false;
                    timer = 0;
                }
            }
            else if (timer < (delay+1))
            {
                Player.GetComponent<CharacterController>().enabled = true;
                Player.GetComponent<MeshRenderer>().enabled = true;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && Input.GetButtonDown("Action") && timer >= delay)
        {
            if (!Activated)
            {
                Text.SetActive(false);
                CamScript.Priority = 11;
                Activated = true;
                timer = 0;
                Player.GetComponent<CharacterController>().enabled = false;
                Player.GetComponent<MeshRenderer>().enabled = false;
            }
        }
        else if (other.gameObject.tag == "Player")
        {
            Text.SetActive(true);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && Input.GetButtonDown("Action") && timer >= delay)
        {
            if (!Activated)
            {
                Text.SetActive(false);
                CamScript.Priority = 11;
                Activated = true;
                timer = 0;
                Player.GetComponent<CharacterController>().enabled = false;
                Player.GetComponent<MeshRenderer>().enabled = false;
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
