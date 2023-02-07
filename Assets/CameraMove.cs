using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject player;
    private float offset;

    void Start(){
        offset=transform.position.x-player.transform.position.x;
    }
    public void changePos()
    {
    }
    void Update(){

        
        transform.position=new Vector3(player.transform.position.x+offset,transform.position.y,transform.position.z);
        
        
    }
}
