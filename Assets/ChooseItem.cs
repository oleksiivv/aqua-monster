using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseItem : MonoBehaviour
{
    //public int current=0;
    // public GameObject img;
    // public Text count;

     public ItemsController items;

     public GameObject[] choosen;

     public GameObject[] stars;

     public GameObject jetPack,jetBootR,jetBootL,magnet;

     public Move player;

     public GameObject up;

     public static int fallForce=-20;


    void Start(){
        up.transform.position=new Vector3(up.transform.position.x,8f,up.transform.position.z);

        jetPack.SetActive(false);
        jetBootL.SetActive(false);
        jetBootR.SetActive(false);
        magnet.SetActive(false);
        

        // img=gameObject;
        // count=gameObject.transform.GetChild(0).transform.GetComponent<Text>();
        // items=gameObject.transform.parent.GetComponent<ItemsController>();


    }

    public void extraLifeOff(){
        choosen[2].SetActive(false);
    }


    public void choose(int current){
        
        if(items.items[current].getCount()>0 && choosen[current].activeSelf==false){
            foreach(var ch in choosen)ch.SetActive(false);
            choosen[current].SetActive(true);
            PlayerPrefs.SetInt(items.items[current].Name,items.items[current].getCount()-1);
            items.updateItems();


            switch(current){
                case 0://magnet
                magnet.SetActive(true);
                foreach(var star in stars){

                    star.GetComponent<BoxCollider>().size*=5;

                }
                break;

                case 1://acceleration

                player.gameObject.GetComponent<Rigidbody>().freezeRotation=false;
                player.anim.jump();
                player.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.right*-1*100*5*Time.timeScale);
                player.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up*Mathf.Abs(100)*5*Time.timeScale);




                break;

                case 2://extralife
                if(player.gameObject.transform.position.y<2){
                  player.extraLife=true;
                }
                break;

                case 3://jetboots
                if(player.transform.position.y>1.7f){
                    player.transform.position=new Vector3(player.transform.position.x,1.6f,player.transform.position.z);
                }
                up.transform.position=new Vector3(up.transform.position.x,2f,up.transform.position.z);
                fallForce=-50;
                player.gameObject.GetComponent<Rigidbody>().useGravity=false;
                player.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up*30);
                Invoke("stopJetBoots",10.5f);
                jetBootL.SetActive(true);
                jetBootR.SetActive(true);
                break;

                case 4://jetpack
                if(player.transform.position.y>1.7f){
                    player.transform.position=new Vector3(player.transform.position.x,1.6f,player.transform.position.z);
                }
                up.transform.position=new Vector3(up.transform.position.x,2f,up.transform.position.z);
                fallForce=-50;
                player.gameObject.GetComponent<Rigidbody>().useGravity=false;
                player.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up*30);
                Invoke("stopJetpack",10.5f);
                jetPack.SetActive(true);
                break;
            }
        }

    }

    void stopJetBoots(){
        jetBootL.SetActive(false);
        jetBootR.SetActive(false);

        up.transform.position=new Vector3(up.transform.position.x,8f,up.transform.position.z);
        player.gameObject.GetComponent<Rigidbody>().useGravity=true;
        choosen[3].SetActive(false);
    }

    void stopJetpack(){
        jetPack.SetActive(false);
        
        up.transform.position=new Vector3(up.transform.position.x,8f,up.transform.position.z);
        player.gameObject.GetComponent<Rigidbody>().useGravity=true;
        choosen[4].SetActive(false);
    }


    public void stopAcceleration(){
        choosen[1].gameObject.SetActive(false);
    }

    public void stopMagnet(){
        choosen[0].gameObject.SetActive(false);
        magnet.SetActive(false);
    }


}
