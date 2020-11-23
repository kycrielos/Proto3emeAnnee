using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateformeTriggerScript : MonoBehaviour
{
    public PlateformeRunique Plateforme;
    public int LevierValue;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Plateforme.Value = LevierValue;
            LevierValue = 0;
        }
    }
}
