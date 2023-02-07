using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishArrow : MonoBehaviour
{
    int dir=1;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(changeDir());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0,1,0)/25*dir*Time.timeScale);
        transform.Rotate(0,-3*Time.timeScale,0);
    }

    IEnumerator changeDir(){
        while(true){
            dir*=-1;
            yield return new WaitForSeconds(0.4f);
        }
    }
}
