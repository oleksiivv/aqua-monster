using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{
    void OnCollisionEnter(Collision other){
        if(other.gameObject.tag=="Player"){
            if(other.gameObject.GetComponent<Move>().diePanel.activeSelf!=true){
                other.transform.eulerAngles=new Vector3(0,0,0);
                other.gameObject.GetComponent<Rigidbody>().freezeRotation=true;

                other.gameObject.transform.parent=gameObject.transform;
            }
        }
    }


    void OnCollisionExit(Collision other){
        if(other.gameObject.tag=="Player"){
            other.gameObject.GetComponent<Rigidbody>().freezeRotation=true;
            other.gameObject.transform.parent=null;
        }
    }
}
