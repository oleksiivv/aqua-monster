using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarsAtPlayer : MonoBehaviour
{
    public Image[] currentStars;
    public GameObject[] stars;
    public Image[] starsAtFinish;

    public Color32 available,empty;

    void Start(){

        showStars(currentStars);
        showStars(starsAtFinish);

    }

    public void resetStars(){
        if(PlayerPrefs.GetInt("Completed"+Application.loadedLevel.ToString())==1)return;
        for(int i=0;i<stars.Length;i++){
            PlayerPrefs.SetInt("Get."+Application.loadedLevel.ToString()+"."+stars[i].gameObject.name.ToString(),0);
        }
    }

    public void hideStars(){
        for(int i=0;i<starsAtFinish.Length;i++){
            starsAtFinish[i].gameObject.SetActive(false);
        }
    }
    public void showStars(Image[] st){
        int cnt=0;
        for(int i=0;i<3;i++){
            if(PlayerPrefs.GetInt("Get."+Application.loadedLevel.ToString()+"."+stars[i].gameObject.name.ToString())==1){
                cnt++;

            }
        }
        for(int i=0;i<stars.Length;i++){
            st[i].GetComponent<Image>().color=empty;
        }

        for(int i=0;i<cnt;i++){
            st[i].GetComponent<Image>().color=available;
        }


    }

    public int getStarsNumber(){
        int cnt=0;
        for(int i=0;i<3;i++){
            if(PlayerPrefs.GetInt("Get."+Application.loadedLevel.ToString()+"."+stars[i].gameObject.name.ToString())==1){
                cnt++;

            }
        }
        return cnt;
    }
}
