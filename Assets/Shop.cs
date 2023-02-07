using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.Advertisements;
using Yodo1.MAS;

public class Shop : MonoBehaviour
{
    public void openScene(int id){
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

    private List<Item> items = new List<Item>();
    public Text[] available; 

    public GameObject audioController;

    public Text money;

    private string appId="4035249";

    void Start()
    {
        //Advertisement.Initialize(appId,false);
        InitializeSdk();
        SetPrivacy(true, false, false);
        SetDelegates();
        

        StartCoroutine(BannerCoroutine());
        
        if(Yodo1U3dMas.IsBannerAdLoaded()){
            Yodo1U3dMas.ShowBannerAd();
        }

        
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




    //


    public void ShowRewardedAd(){
        // if (Advertisement.IsReady("rewardedVideo"))
        // {
        //     var options = new ShowOptions { resultCallback = HandleShowResult };
        //     Advertisement.Show("rewardedVideo", options);
        // }

        if(Yodo1U3dMas.IsRewardedAdLoaded()){
            Yodo1U3dMas.ShowRewardedAd();
        }
    }

    private void SetPrivacy(bool gdpr, bool coppa, bool ccpa)
    {
        Yodo1U3dMas.SetGDPR(gdpr);
        Yodo1U3dMas.SetCOPPA(coppa);
        Yodo1U3dMas.SetCCPA(ccpa);
    }

    private void InitializeSdk()
    {
        Yodo1U3dMas.InitializeSdk();
    }

    private void SetDelegates()
    {
        Yodo1U3dMas.SetInitializeDelegate((bool success, Yodo1U3dAdError error) =>
        {
            Debug.Log("[Yodo1 Mas] InitializeDelegate, success:" + success + ", error: \n" + error.ToString());

            if (success)
            {
                StartCoroutine(BannerCoroutine());
            }
            else
            {

            }
        });

        Yodo1U3dMas.SetBannerAdDelegate((Yodo1U3dAdEvent adEvent, Yodo1U3dAdError error) =>
        {
            Debug.Log("[Yodo1 Mas] BannerdDelegate:" + adEvent.ToString() + "\n" + error.ToString());
            switch (adEvent)
            {
                case Yodo1U3dAdEvent.AdClosed:
                    Debug.Log("[Yodo1 Mas] Banner ad has been closed.");
                    break;
                case Yodo1U3dAdEvent.AdOpened:
                    Debug.Log("[Yodo1 Mas] Banner ad has been shown.");
                    break;
                case Yodo1U3dAdEvent.AdError:
                    Debug.Log("[Yodo1 Mas] Banner ad error, " + error.ToString());
                    break;
            }
        });

        Yodo1U3dMas.SetInterstitialAdDelegate((Yodo1U3dAdEvent adEvent, Yodo1U3dAdError error) =>
        {
            Debug.Log("[Yodo1 Mas] InterstitialAdDelegate:" + adEvent.ToString() + "\n" + error.ToString());
            switch (adEvent)
            {
                case Yodo1U3dAdEvent.AdClosed:
                    Debug.Log("[Yodo1 Mas] Interstital ad has been closed.");
                    break;
                case Yodo1U3dAdEvent.AdOpened:
                    Debug.Log("[Yodo1 Mas] Interstital ad has been shown.");
                    break;
                case Yodo1U3dAdEvent.AdError:
                    Debug.Log("[Yodo1 Mas] Interstital ad error, " + error.ToString());
                    break;
            }

        });

        Yodo1U3dMas.SetRewardedAdDelegate((Yodo1U3dAdEvent adEvent, Yodo1U3dAdError error) =>
        {
            Debug.Log("[Yodo1 Mas] RewardVideoDelegate:" + adEvent.ToString() + "\n" + error.ToString());
            switch (adEvent)
            {
                case Yodo1U3dAdEvent.AdClosed:
                    Debug.Log("[Yodo1 Mas] Reward video ad has been closed.");
                    break;
                case Yodo1U3dAdEvent.AdOpened:
                    Debug.Log("[Yodo1 Mas] Reward video ad has shown successful.");
                    break;
                case Yodo1U3dAdEvent.AdError:
                    Debug.Log("[Yodo1 Mas] Reward video ad error, " + error);
                    break;
                case Yodo1U3dAdEvent.AdReward:
                    PlayerPrefs.SetInt("Coin",PlayerPrefs.GetInt("Coin")+5);
                    money.text=PlayerPrefs.GetInt("Coin").ToString();
                    Debug.Log("[Yodo1 Mas] Reward video ad reward, give rewards to the player.");
                    break;
            }

        });
    }
    bool isBannerShown = false;
    IEnumerator BannerCoroutine()
    {
        yield return new WaitForSeconds(2.0f);
        if (isBannerShown == false)
        {
            if (Yodo1U3dMas.IsBannerAdLoaded())
            {
                Yodo1U3dMas.ShowBannerAd();
            }
            else
            {
                StartCoroutine(BannerCoroutine());
            }
        }

    }

          /*private void HandleShowResult(ShowResult result)
          {
            switch (result)
            {
              case ShowResult.Finished:
              PlayerPrefs.SetInt("Coin",PlayerPrefs.GetInt("Coin")+5);
              money.text=PlayerPrefs.GetInt("Coin").ToString();
                Debug.Log("The ad was successfully shown.");
                //
                // YOUR CODE TO REWARD THE GAMER
                // Give coins etc.
                break;
              case ShowResult.Skipped:
                Debug.Log("The ad was skipped before reaching the end.");
                break;
              case ShowResult.Failed:
                Debug.LogError("The ad failed to be shown.");
                break;
            }
          }*/

    

}



