using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemsController : MonoBehaviour
{
    public Image[] availableImg;
    public Text[] availableText; 
    public List<Item> items = new List<Item>();
    void Start()
    {
        items.Add(new Item(1,20,"Magnet"));
        items.Add(new Item(2,50,"Acceleration"));
        items.Add(new Item(3,60,"ExtraLife"));
        items.Add(new Item(4,80,"JetBoots"));
        items.Add(new Item(5,100,"JetPack"));

        updateItems();
    }

    public void updateItems(){
        int totalN=0;
        for(int i=0;i<availableImg.Length;i++){
            int n=items[i].getCount();
            totalN+=n;

            if(n==0){
                availableImg[i].gameObject.SetActive(false);
            }
            else{
                availableImg[i].gameObject.SetActive(true);
                availableText[i].GetComponent<Text>().text=n.ToString();
            }
        }
        if(totalN==0){
            gameObject.SetActive(false);
        }
    }
}
