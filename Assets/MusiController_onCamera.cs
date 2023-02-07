using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusiController_onCamera : MonoBehaviour
{
    void Start()
    {
        if(PlayerPrefs.GetInt("!music")==0){
            GetComponent<AudioSource>().enabled=true;
            GetComponent<AudioSource>().Play();
        }
        else{
            GetComponent<AudioSource>().enabled=false;
        }
        
    }

    
}
