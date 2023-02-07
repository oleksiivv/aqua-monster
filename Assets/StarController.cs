using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt("Get."+Application.loadedLevel.ToString()+"."+gameObject.name.ToString())==1 && PlayerPrefs.GetInt("Completed"+Application.loadedLevel.ToString())==1){
            gameObject.SetActive(false);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
