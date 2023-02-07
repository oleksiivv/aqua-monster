using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    public Image[] stars;
    public int levelId=1;
    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.SetInt("Completed2",1);
        
        for(int i=0;i<3;i++){
            stars[i]=gameObject.transform.GetChild(i+1).GetComponent<Image>();
        }

        // if(PlayerPrefs.GetInt("Completed"+(levelId-1).ToString())==1){
        //     gameObject.GetComponent<Image>().color=new Color32(255,255,255,255);
        //     foreach(var star in  stars){
        //         star.gameObject.SetActive(true);
        //         star.GetComponent<Image>().color=new Color32(145,145,145,255);

        //     }


        // }
        if(levelId<9){

            int numOfStars=getStarsNumber();

            if(levelId!=1 && (PlayerPrefs.GetInt("Completed"+(levelId-1).ToString())!=1 && PlayerPrefs.GetInt("Completed"+(levelId).ToString())!=1)){
                gameObject.GetComponent<Image>().color=new Color32(145,145,145,255);

                foreach(var star in  stars){

                    star.gameObject.SetActive(false);

                }

            }
            
            else{
                foreach(var star in  stars){

                    star.GetComponent<Image>().color=new Color32(145,145,145,255);

                }

                for(int i=0;i<numOfStars;i++){
                    stars[i].GetComponent<Image>().color=new Color32(255,255,255,255);
                }
            }
        }
        else if(levelId==9){
            int numOfStars=getStarsNumber();

            if(PlayerPrefs.GetInt("AllStars")<16){
                gameObject.GetComponent<Image>().color=new Color32(145,145,145,255);

                foreach(var star in  stars){

                    star.gameObject.SetActive(false);

                }

            }
            
            else{
                foreach(var star in  stars){

                    star.GetComponent<Image>().color=new Color32(145,145,145,255);

                }

                for(int i=0;i<numOfStars;i++){
                    stars[i].GetComponent<Image>().color=new Color32(255,255,255,255);
                }
            }
        }
        else{
            int numOfStars=getStarsNumber();

            if(PlayerPrefs.GetInt("Completed"+(levelId-1).ToString())!=1 && PlayerPrefs.GetInt("Completed"+(levelId).ToString())!=1){
                gameObject.GetComponent<Image>().color=new Color32(145,145,145,255);

                foreach(var star in  stars){

                    star.gameObject.SetActive(false);

                }

            }
            
            else{
                foreach(var star in  stars){

                    star.GetComponent<Image>().color=new Color32(145,145,145,255);

                }

                for(int i=0;i<numOfStars;i++){
                    stars[i].GetComponent<Image>().color=new Color32(255,255,255,255);
                }
            }
        }
    }

    public int getStarsNumber(){
        int cnt=PlayerPrefs.GetInt("Completed"+levelId.ToString()+".with",-1);

        return cnt;
    }
}
