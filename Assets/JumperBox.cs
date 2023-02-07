using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperBox : MonoBehaviour
{
    // Start is called before the first frame update

    int dir=1;
    private Rigidbody rb;
    void Start()
    {
        rb=GetComponent<Rigidbody>();
        StartCoroutine(changeDirection());
    }

    // Update is called once per frame
    void Update()
    {

        rb.velocity=Vector3.up*2*dir;

        //transform.Translate(Vector3.up/10*dir);

        
    }

    IEnumerator changeDirection(){

        while(true){
            yield return new WaitForSeconds(0.5f);
            dir*=-1;
        }
    }


    void OnCollisionEnter(Collision other){
        if(other.gameObject.tag=="Death"){
           dir=1;
        }
        
        if(other.gameObject.tag=="Player"){
            if(other.gameObject.GetComponent<Move>().diePanel.activeSelf!=true){
                other.transform.eulerAngles=new Vector3(0,0,0);
                //other.gameObject.GetComponent<Rigidbody>().isKinematic=true;
                other.gameObject.GetComponent<Rigidbody>().freezeRotation=true;

                other.gameObject.transform.parent=gameObject.transform;
            }
        }
    }


    void OnCollisionExit(Collision other){
        if(other.gameObject.tag=="Player"){
            //other.gameObject.GetComponent<Rigidbody>().isKinematic=false;
            other.gameObject.GetComponent<Rigidbody>().freezeRotation=true;
            other.gameObject.transform.parent=null;
        }
    }
}
