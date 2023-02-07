using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicLevel : MonoBehaviour
{
    public AudioClip[] clips;
    void Start()
    {
        GetComponent<AudioSource>().clip=clips[Random.Range(0,clips.Length)];
        if(PlayerPrefs.GetInt("!music")==0){
            GetComponent<AudioSource>().enabled=true;
            GetComponent<AudioSource>().Play();
        }
        else{
            GetComponent<AudioSource>().enabled=false;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
