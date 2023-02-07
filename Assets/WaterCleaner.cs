using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCleaner : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Water")other.gameObject.GetComponent<MeshRenderer>().enabled=true;
        
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag=="Water")other.gameObject.GetComponent<MeshRenderer>().enabled=false;
        
    }
}
