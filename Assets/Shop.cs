using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;

public class Shop : MonoBehaviour
{
    public void openScene(int id)
    {
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

#if UNITY_IOS
    private string appId="ca-app-pub-4962234576866611~8925183344";
    private string rewardedId="ca-app-pub-4962234576866611/2728041974";
#else
    private string appId="ca-app-pub-4962234576866611~8925183344";
    private string rewardedId="ca-app-pub-4962234576866611/2728041974";
#endif


    void Start()
    {
        money.text = PlayerPrefs.GetInt("Coin").ToString();

        items.Add(new Item(1, 20, "Magnet"));
        items.Add(new Item(2, 150, "Acceleration"));
        items.Add(new Item(3, 60, "ExtraLife"));
        items.Add(new Item(4, 80, "JetBoots"));
        items.Add(new Item(5, 100, "JetPack"));

        //PlayerPrefs.SetInt("money",PlayerPrefs.GetInt("money")+3000);
        if (PlayerPrefs.GetInt("!sound") == 0)
        {
            audioController.GetComponent<AudioSource>().mute = false;
        }
        else
        {
            audioController.GetComponent<AudioSource>().mute = true;
        }

        RequestConfiguration requestConfiguration =
            new RequestConfiguration.Builder()
            .SetSameAppKeyEnabled(true).build();
        MobileAds.SetRequestConfiguration(requestConfiguration);

        MobileAds.Initialize(initStatus => {
            InitAdmobRewarded();
        });

        updateItems();
    }

    void updateItems()
    {
        for (int i = 0; i < available.Length; i++)
        {
            int n = items[i].getCount();
            if (n == 0)
            {
                available[i].gameObject.SetActive(false);
            }
            else
            {
                available[i].gameObject.SetActive(true);
                available[i].GetComponent<Text>().text = n.ToString();
            }
        }
    }


    public void buyItem(int id)
    {
        id -= 1;

        if (PlayerPrefs.GetInt("Coin") < items[id].Price)
        {

            return;

        }

        audioController.GetComponent<AudioSource>().Play();

        PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") - items[id].Price);

        PlayerPrefs.SetInt(items[id].Name, PlayerPrefs.GetInt(items[id].Name) + 1);
        money.text = PlayerPrefs.GetInt("Coin").ToString();

        updateItems();


    }

    public void ShowRewardedAdSuccessHandler()
    {
        //admob.showIntersitionalAd();
        PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") + 5);
        money.text = PlayerPrefs.GetInt("Coin").ToString();

        //todo: replace with real ShowRewardedAd
    }

    bool isBannerShown = false;
    
    private RewardedAd _rewardedAd;
    void InitAdmobRewarded(){
      if (_rewardedAd != null)
      {
            _rewardedAd.Destroy();
            _rewardedAd = null;
      }

      Debug.Log("Loading the rewarded ad.");

      // create our request used to load the ad.
      var adRequest = new AdRequest();

      // send the request to load the ad.
      RewardedAd.Load(rewardedId, adRequest,
          (RewardedAd ad, LoadAdError error) =>
          {
              // if error is not null, the load request failed.
              if (error != null || ad == null)
              {
                  return;
              }

              _rewardedAd = ad;

              RegisterReloadHandler(_rewardedAd);
              RegisterEventHandlers(_rewardedAd);
          });
    }
    
    private bool ShowRewardBasedGoogleVideo()
    {
      const string rewardMsg =
        "Rewarded ad rewarded the user";

        if (_rewardedAd != null && _rewardedAd.CanShowAd())
        {
          Time.timeScale=0;
            _rewardedAd.Show((Reward reward) =>
            {
              ShowRewardedAdSuccessHandler();
            });

            return true;
        }
        
        return false;
    }

    private void RegisterEventHandlers(RewardedAd ad)
    {
      // Raised when the ad is estimated to have earned money.
      ad.OnAdPaid += (AdValue adValue) =>
      {
          ShowRewardedAdSuccessHandler();

      };
      // Raised when an impression is recorded for an ad.
      ad.OnAdImpressionRecorded += () =>
      {
          Debug.Log("Rewarded ad recorded an impression.");
      };
      // Raised when a click is recorded for an ad.
      ad.OnAdClicked += () =>
      {
          Debug.Log("Rewarded ad was clicked.");
      };
      // Raised when an ad opened full screen content.
      ad.OnAdFullScreenContentOpened += () =>
      {
          Debug.Log("Rewarded ad full screen content opened.");
      };
      // Raised when the ad closed full screen content.
      ad.OnAdFullScreenContentClosed += () =>
      {
          Debug.Log("Rewarded ad full screen content closed.");
      };
      // Raised when the ad failed to open full screen content.
      ad.OnAdFullScreenContentFailed += (AdError error) =>
      {
          Debug.LogError("Rewarded ad failed to open full screen content " +
                        "with error : " + error);
      };
  }

  private void RegisterReloadHandler(RewardedAd ad)
  {
      // Raised when the ad closed full screen content.
      ad.OnAdFullScreenContentClosed += () =>
      {
          Debug.Log("Rewarded Ad full screen content closed.");

          // Reload the ad so that we can show another as soon as possible.
          InitAdmobRewarded();
      };
      // Raised when the ad failed to open full screen content.
      ad.OnAdFullScreenContentFailed += (AdError error) =>
      {
          Debug.LogError("Rewarded ad failed to open full screen content " +
                        "with error : " + error);

          // Reload the ad so that we can show another as soon as possible.
          InitAdmobRewarded();
      };
  }

    public void ShowRewardBasedVideo()
    {
        ShowRewardBasedGoogleVideo();
    }
}



