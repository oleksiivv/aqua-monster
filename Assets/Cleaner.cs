using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleaner : MonoBehaviour
{
    public GameObject[] all;
    // Start is called before the first frame update
    void Awake()
    {

        foreach(var a in all){
            if(!a.gameObject.name.Contains("mpty"))a.gameObject.GetComponent<MeshRenderer>().enabled=false;
        }


        
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag!="Player" && other.gameObject.name!="MonsterHub001" && other.gameObject.tag!="Star"  && other.gameObject.tag!="Water" && !other.gameObject.name.Contains("mpty"))other.gameObject.GetComponent<MeshRenderer>().enabled=true;
        
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag!="Player" && other.gameObject.name!="MonsterHub001"&& other.gameObject.tag!="Star" && other.gameObject.tag!="Water" && !other.gameObject.name.Contains("mpty"))other.gameObject.GetComponent<MeshRenderer>().enabled=false;
        
    }
}
