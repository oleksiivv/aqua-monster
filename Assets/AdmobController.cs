using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdmobController : MonoBehaviour
{
    private InterstitialAd intersitional;

	private bool showBanner = false;
#if UNITY_IOS
    private string appId="ca-app-pub-4962234576866611~8925183344";
    private string bannerId="ca-app-pub-4962234576866611/6995393691";
    private string intersitionalId="ca-app-pub-4962234576866611/7412791126";
#else
    private string appId="ca-app-pub-4962234576866611~8925183344";
    private string bannerId="ca-app-pub-4962234576866611/6995393691";
    private string intersitionalId="ca-app-pub-4962234576866611/7412791126";
#endif

    public static int adsCounter=0;

    //public UnityAdsController unityAds;

    void Awake(){
        RequestConfiguration requestConfiguration =
            new RequestConfiguration.Builder()
            .SetSameAppKeyEnabled(true).build();
        MobileAds.SetRequestConfiguration(requestConfiguration);

        MobileAds.Initialize(initStatus => {
            LoadLoadInterstitialAd();
            CreateBannerView();

            if(PlayerPrefs.GetInt("first-Open",0)==1 && Application.loadedLevel > 1 && Application.loadedLevel < 17){
                LoadBannerAd();
             }
        });

         PlayerPrefs.SetInt("first-Open",1);
     }

    AdRequest AdRequestBuild(){
         return new AdRequest.Builder().Build();
     }

      public bool showIntersitionalAd(){
        return showIntersitionalUnityAd();
      }

      private InterstitialAd _interstitialAd;

    private BannerView _bannerView;

    public void LoadLoadInterstitialAd()
    {
        // Clean up the old ad before loading a new one.
        if (_interstitialAd != null)
        {
                _interstitialAd.Destroy();
                _interstitialAd = null;
        }

        Debug.Log("Loading the interstitial ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        InterstitialAd.Load(intersitionalId, adRequest,
            (InterstitialAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("interstitial ad failed to load an ad " +
                                    "with error : " + error);
                    return;
                }

                Debug.Log("Interstitial ad loaded with response : "
                            + ad.GetResponseInfo());

                _interstitialAd = ad;

                RegisterEventHandlers(_interstitialAd);
                RegisterReloadHandler(_interstitialAd);
            });
    }


      public bool showIntersitionalUnityAd(){
        if (_interstitialAd != null && _interstitialAd.CanShowAd())
        {
            Debug.Log("Showing interstitial ad.");
            _interstitialAd.Show();

            return true;
        }
        else
        {
            return false;
        }
      }

    public void CreateBannerView()
    {
        Debug.Log("Creating banner view");

        // If we already have a banner, destroy the old one.
        if (_bannerView != null)
        {
            DestroyBannerView();
        }

        // Create a 320x50 banner at top of the screen
        _bannerView = new BannerView(bannerId, AdSize.Banner, AdPosition.Bottom);
    }

    public void LoadBannerAd()
    {
        // create an instance of a banner view first.
        if(_bannerView == null)
        {
            CreateBannerView();
        }

        // create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        Debug.Log("Loading banner ad.");
        _bannerView.LoadAd(adRequest);
    }

    public void DestroyBannerView()
    {
        if (_bannerView != null)
        {
            Debug.Log("Destroying banner view.");
            _bannerView.Destroy();
            _bannerView = null;
        }
    }

    private void RegisterEventHandlers(InterstitialAd interstitialAd)
      {
          interstitialAd.OnAdPaid += (AdValue adValue) =>
          {
              Debug.Log(String.Format("Interstitial ad paid {0} {1}.",
                  adValue.Value,
                  adValue.CurrencyCode));
          };

          interstitialAd.OnAdImpressionRecorded += () =>
          {
              Debug.Log("Interstitial ad recorded an impression.");
          };

          interstitialAd.OnAdClicked += () =>
          {
              Debug.Log("Interstitial ad was clicked.");
          };

          interstitialAd.OnAdFullScreenContentOpened += () =>
          {
              Debug.Log("Interstitial ad full screen content opened.");
          };

          interstitialAd.OnAdFullScreenContentClosed += () =>
          {
              Debug.Log("Interstitial ad full screen content closed.");
          };

          interstitialAd.OnAdFullScreenContentFailed += (AdError error) =>
          {
              Debug.LogError("Interstitial ad failed to open full screen content " +
                          "with error : " + error);
          };
      }

      private void RegisterReloadHandler(InterstitialAd interstitialAd)
      {
          interstitialAd.OnAdFullScreenContentClosed += () =>
          {
              Debug.Log("Interstitial Ad full screen content closed.");

              LoadLoadInterstitialAd();
          };

          interstitialAd.OnAdFullScreenContentFailed += (AdError error) =>
          {
              Debug.LogError("Interstitial ad failed to open full screen content " +
                          "with error : " + error);

              LoadLoadInterstitialAd();
          };
      }
}
