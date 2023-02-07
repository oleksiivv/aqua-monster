using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharAnimator : MonoBehaviour
{
    public Animator person;

    public bool dead=false;
    void Start()
    {
        dead=false;
        person.gameObject.transform.GetChild(2).gameObject.GetComponent<BoxCollider>().enabled=false;
        
    }

    public void jump(){
        person.gameObject.transform.GetChild(2).gameObject.GetComponent<BoxCollider>().enabled=false;

        person.SetBool("jump",true);
        person.SetBool("idle",false);
        person.SetBool("death",false);
        

    }

    public void stopJump(){
        person.gameObject.transform.GetChild(2).gameObject.GetComponent<BoxCollider>().enabled=false;

        person.SetBool("idle",true);
        person.SetBool("death",false);
        person.SetBool("jump",false);
    }

    public void stopAll(){
        person.SetBool("death",false);
        person.SetBool("idle",false);
        person.SetBool("jump",false);
    }

    public void die(){
        dead=true;
        //person.gameObject.transform.GetChild(1).gameObject.AddComponent<BoxCollider>();
        

        person.SetBool("death",true);
        person.SetBool("idle",false);
        person.SetBool("jump",false);
        
    }

    public void idle(){
        //person.gameObject.transform.GetChild(2).gameObject.GetComponent<BoxCollider>().enabled=false;
        
        person.SetBool("idle",true);
        person.SetBool("death",false);
        person.SetBool("jump",false);
    }
}
