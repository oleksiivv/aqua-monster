using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item{

    public Item(int id,int price,string name){
         this.id=id;
         this.price=price;
         this.name=name;
    }
    private int id;
    private int price;
    private string name;

    public string Name{get{
        return this.name;
      }
      set{
        this.name=value;
    }}


    public int Price{get{
        return this.price;
      }
      set{
        this.price=value;
    }}


    public int getCount(){
        return PlayerPrefs.GetInt(this.name);
    }

}
