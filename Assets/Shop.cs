using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AdmobController))]
public class Shop : MonoBehaviour
{
    public void openScene(int id){
        StartCoroutine(loadAsync(id));
    }

    public GameObject loadingPanel;
    public Slider loadingSlider;
    
    private AdmobController admob;

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

    private List<Item> items = new List<Item>();
    public Text[] available; 

    public GameObject audioController;

    public Text money;

    void Start()
    {
        money.text=PlayerPrefs.GetInt("Coin").ToString();

        items.Add(new Item(1,20,"Magnet"));
        items.Add(new Item(2,150,"Acceleration"));
        items.Add(new Item(3,60,"ExtraLife"));
        items.Add(new Item(4,80,"JetBoots"));
        items.Add(new Item(5,100,"JetPack"));

        //PlayerPrefs.SetInt("money",PlayerPrefs.GetInt("money")+3000);
        if (PlayerPrefs.GetInt("!sound") == 0)
        {
            audioController.GetComponent<AudioSource>().mute=false;
        }
        else
        {
            audioController.GetComponent<AudioSource>().mute = true;
        }

        admob = GetComponent<AdmobController>();

        updateItems();
    }

    void updateItems(){
        for(int i=0;i<available.Length;i++){
            int n=items[i].getCount();
            if(n==0){
                available[i].gameObject.SetActive(false);
            }
            else{
                available[i].gameObject.SetActive(true);
                available[i].GetComponent<Text>().text=n.ToString();
            }
        }
    }


    public void buyItem(int id){
        id-=1;

        if(PlayerPrefs.GetInt("Coin")<items[id].Price){

             return;

         }

         audioController.GetComponent<AudioSource>().Play();

        PlayerPrefs.SetInt("Coin",PlayerPrefs.GetInt("Coin")-items[id].Price);

        PlayerPrefs.SetInt(items[id].Name,PlayerPrefs.GetInt(items[id].Name)+1);
        money.text=PlayerPrefs.GetInt("Coin").ToString();

        updateItems();


    }

    public void ShowRewardedAd(){
        admob.showIntersitionalAd();
        PlayerPrefs.SetInt("Coin",PlayerPrefs.GetInt("Coin")+5);
        money.text=PlayerPrefs.GetInt("Coin").ToString();

        //todo: replace with real ShowRewardedAd
    }

    bool isBannerShown = false;
}



