using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateformeTriggerScript : MonoBehaviour
{
    public PlateformeRunique Plateforme;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Plateforme.Value += 1;
        }
    }
}
