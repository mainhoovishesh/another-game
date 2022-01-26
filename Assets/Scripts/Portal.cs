using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Portal : MonoBehaviour
{
    public VisualEffect portalLtwo;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            //portalLtwo.Play();
            portalLtwo.GetComponent<VisualEffect>().Play();
        }
    }
}
