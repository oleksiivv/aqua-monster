using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    public AudioClip[] clips;
    private AudioSource source;
    void Start(){

        source=GetComponent<AudioSource>();

    }
    public void play1(){

        source.clip=clips[0];
        source.enabled=true;
        source.Play();

    }

    public void play2(){

        source.clip=clips[1];
        source.enabled=true;
        source.Play();

    }
}
