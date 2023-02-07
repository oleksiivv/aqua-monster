using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LastLevelController : MonoBehaviour
{
    public GameObject panel;
    public GameObject closePanel;
    public Text[] textAtLastLevelPanel;
    public Image starsImage;
    public Sprite winSprite,starSprite;

    public MenuController menu;

    public Text required16;

    public GameObject winPanel;
    

    void Start(){
        required16.text=PlayerPrefs.GetInt("AllStars").ToString()+"/16";

        //PlayerPrefs.SetInt("AllStars",17);
         //PlayerPrefs.SetInt("win",1);

        // PlayerPrefs.SetInt("Completed9",1);
        // PlayerPrefs.SetInt("Completed10",1);
        // PlayerPrefs.SetInt("Completed11",1);
        // PlayerPrefs.SetInt("Completed12",1);
        // PlayerPrefs.SetInt("Completed13",1);
        // PlayerPrefs.SetInt("Completed14",1);
        // PlayerPrefs.SetInt("Completed15",1);
        // PlayerPrefs.SetInt("Completed16",1);

        if(PlayerPrefs.GetInt("win")==1 && PlayerPrefs.GetInt("showedWinPanel")==0){
            winPanel.SetActive(true);
            PlayerPrefs.SetInt("showedWinPanel",1);
        }




        if(PlayerPrefs.GetInt("AllStars")<16){
            Debug.Log("stars less than 16");
            panel.gameObject.SetActive(false);
            closePanel.gameObject.SetActive(false);
            
        }
        else if((PlayerPrefs.GetInt("AllStars")>=16 && PlayerPrefs.GetInt("AllStars")<32)){
            textAtLastLevelPanel[0].text="Complete all levels";
            textAtLastLevelPanel[1].text="And collect 32";
            starsImage.GetComponent<Image>().sprite=starSprite;
            Debug.Log("stars greater than 16");
            panel.gameObject.SetActive(true);
            closePanel.gameObject.SetActive(true);
        }
        if((PlayerPrefs.GetInt("AllStars")>=32 && PlayerPrefs.GetInt("Completed16")==1)  || PlayerPrefs.GetInt("win")==1 ){
            Debug.Log("stars greater than 32");
            panel.gameObject.SetActive(true);
            if(PlayerPrefs.GetInt("win")==1){
                textAtLastLevelPanel[0].text="Mission complete!";
                textAtLastLevelPanel[1].text="Well done";
                starsImage.GetComponent<Image>().sprite=winSprite;
                closePanel.SetActive(false);
            }
            else{
                textAtLastLevelPanel[0].text="Level is opened";
                textAtLastLevelPanel[1].text="Click to start";
                starsImage.gameObject.SetActive(false);
                //starsImage.GetComponent<Image>().sprite=winSprite;
                closePanel.SetActive(false);
            }
        }
 
    }

    public void openLastLevel(int sceneId){

        if(closePanel.activeSelf==false){
            
            menu.openSimpleScene(sceneId);
        }

    }

    public void closeWinPanel(){
        winPanel.SetActive(false);
    }
}
