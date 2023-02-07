using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdditionalItemsController : MonoBehaviour
{
    public GameObject[] additionalItems;
    public int maxLength;


    public int maxNumber;

    public Slider length;
    public int mapLength;
    public GameObject player;
    private float from;
    void Start()
    {
        from=Mathf.Abs(player.transform.position.x);
        length.maxValue=mapLength+from;
        length.minValue=from;

        maxLength=-maxLength;
        for(int i=0;i<maxNumber;i++){
            int create=Random.Range(0,2);
            if(create!=1){
                int index=Random.Range(0,additionalItems.Length);
               Instantiate(additionalItems[index],
                new Vector3(Random.Range(0,maxLength),additionalItems[index].transform.position.y,additionalItems[index].transform.position.z),
                additionalItems[index].transform.rotation);
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        length.value=from+Mathf.Abs(player.transform.position.x);
    }
}
