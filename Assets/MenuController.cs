using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.Advertisements;

[RequireComponent(typeof(AdmobController))]
public class MenuController : MonoBehaviour
{
    public Text starsTotal;
    public Text money;

    public GameObject levelsPanel;



    public GameObject[] panels;
    public int storyLen=3;

    public GameObject plane;

    /*
      After your home was destroyed by bomb, which has fallen into the lake you was living in, you need to find new one.

      But other lakes are far away. So you need to jump between aquariums in your way. 
      There are also items llike coins, magnets, jetpacks etc in your way. And you need to collect stars to open new levels. 
      You can buy useful items in shop by collected coins too.


      And remember that aquariums with different colors have different featuresfeatures, 
      meaning that them are not as easy as it can seems.
    */
    
    private AdmobController admob;

    void Start()
    {
      
      //PlayerPrefs.SetInt("Completed9",1);

        if(PlayerPrefs.GetInt("first")==0){
            PlayerPrefs.SetInt("first",1);
            PlayerPrefs.SetInt("Magnet",2);
            PlayerPrefs.SetInt("Acceleration",4);
            PlayerPrefs.SetInt("ExtraLife",1);
            PlayerPrefs.SetInt("JetBoots",2);
            PlayerPrefs.SetInt("JetPack",1);
        }

        starsTotal.text=PlayerPrefs.GetInt("AllStars").ToString();
        money.text=PlayerPrefs.GetInt("Coin").ToString();

        if(PlayerPrefs.GetInt("storyShowed")==0){
          storyLine=false;
          nextPanel(-1);
          PlayerPrefs.SetInt("storyShowed",1);
        }

        admob = GetComponent<AdmobController>();
        
        // PlayerPrefs.SetInt("Completed0",1);
        // PlayerPrefs.SetInt("Completed1",1);
        // PlayerPrefs.SetInt("Completed2",1);
        // PlayerPrefs.SetInt("Completed3",1);
        // PlayerPrefs.SetInt("Completed4",1);
        // PlayerPrefs.SetInt("Completed5",1);
        // PlayerPrefs.SetInt("Completed6",1);
        // PlayerPrefs.SetInt("Completed7",1);
    }

    
    bool storyLine=true;
    public void nextPanel(int currentPanel){

      foreach(var panel in panels){
        panel.SetActive(false);
      }

      if(storyLine && currentPanel+1==storyLen){

        return;

      }
      
      if(currentPanel+1!=panels.Length){
        panels[currentPanel+1].SetActive(true);
      }

    }

    public void startStoryPanel(){
      storyLine=true;
      panels[0].SetActive(true);
    }

    public void showInfoPanel(){
      panels[storyLen].SetActive(true);

    }

    // Update is called once per frame
    public void OpenScene(int id){
      if(id<9){
        if((id==1 || (PlayerPrefs.GetInt("Completed"+(id-1).ToString())==1) || PlayerPrefs.GetInt("Completed"+(id).ToString())==1)){
            StartCoroutine(loadAsync(id));
        }
      }
      else{
        if((id==9 &&  PlayerPrefs.GetInt("AllStars")>=16|| (PlayerPrefs.GetInt("Completed"+(id-1).ToString())==1) || PlayerPrefs.GetInt("Completed"+(id).ToString())==1)){
            StartCoroutine(loadAsync(id));
        }
      }
    }
    

    public void OpenLevelPanel(){
      Invoke("secondPartOn",1f);

        levelsPanel.SetActive(true);

    }

    public void openSimpleScene(int id){
        StartCoroutine(loadAsync(id));
    }



    public GameObject loadingPanel;
    public Slider loadingSlider;

    IEnumerator loadAsync(int id)
    {
        AsyncOperation operation = Application.LoadLevelAsync(id);
        loadingPanel.SetActive(true);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            loadingSlider.value = progress;
            Debug.Log(progress);
            yield return null;

        }
    }


    public void ShowRewardedAd()
    {
        admob.showIntersitionalAd();
        PlayerPrefs.SetInt("Coin",PlayerPrefs.GetInt("Coin")+5);
        money.text=PlayerPrefs.GetInt("Coin").ToString();

        //todo: replace with real ShowRewardedAd
    }


    bool isBannerShown = false;
    
    public GameObject secondPartLevels;
    public void nextLevelsPanel(){
      levelsPanel.GetComponent<Animator>().SetBool("open",false);
      levelsPanel.GetComponent<Animator>().SetBool("close",true);
      Invoke("secondPartOn",0.5f);

    }

    public void prevLevelsPanel(){
      levelsPanel.GetComponent<Animator>().SetBool("close",false);
      levelsPanel.GetComponent<Animator>().SetBool("open",true);


    }

    void secondPartOn(){
      //levelsPanel.GetComponent<Animator>().SetBool("close",false);
      secondPartLevels.SetActive(true);
    }

    public void levelsClose(){
      levelsPanel.SetActive(false);
      secondPartLevels.SetActive(false);
    }


    public void rateGame(){
      Application.OpenURL("https://play.google.com/store/apps/details?id=com.VertexStudioGame.Aqua");
    }
}
