using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Purchasing;
using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using System;
using Sentry;

public class IAPManager : MonoBehaviour
{
    private static IStoreController storeController;
    private static IExtensionProvider storeExtensionProvider;

    // Product identifiers
    public static string product1RemoveAds = "swampman_remove_ads";
    
    [SerializeField] public GameObject panel;

    [SerializeField] public GameObject adsRemovedSuccessAlert;

    public bool isMenu=false;

    public GameObject menuUIButton;

    public AdmobController admob;

    void Awake()
    {
        // Initialize IAP
        //InitializePurchasing();

        if(PlayerPrefs.GetInt("no_ads_purchased", 0) == 1){
            if(PlayerPrefs.GetInt("buy_alert_showed", 0) == 0){
                PlayerPrefs.SetInt("buy_alert_showed", 1);
                
                adsRemovedSuccessAlert.SetActive(true);
                StartCoroutine(HideAlertAfterDelay(adsRemovedSuccessAlert));
            }

            if(isMenu && menuUIButton){
                menuUIButton.SetActive(false);
            }
        }
        
        // Add listener to purchase button
        //if (purchaseButton != null)
            //purchaseButton.onClick.AddListener(OnPurchaseButtonClicked);
    }


    void Start()
    {
        if(PlayerPrefs.GetInt("no_ads_purchased", 0) == 1){
            //if(admob) admob.DestroyBannerView();
        }
    }

    public bool canShowAds()
    {
        return PlayerPrefs.GetInt("no_ads_purchased", 0) == 0;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        UpdateUI("IAP initialization failed: " + error);
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        UpdateUI("Purchase failed: " + product.metadata.localizedTitle + ", reason: " + failureReason);
    }

    private void UpdateUI(string message)
    {
        Debug.Log("IAP: " + message);

        if(panel)panel.SetActive(false);
        else Application.LoadLevel(Application.loadedLevel);
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        UpdateUI("IAP initialization failed: " + error + " - " + message);
    }

    public void ShowPanel(){
        //Time.timeScale=0;
        SentrySdk.CaptureMessage("IAP Proposed");
        if (panel) panel.SetActive(true);
    }

    public void HidePanel(){
        // Time.timeScale=1;
        if(panel)panel.SetActive(false);
        //Application.LoadLevel(Application.loadedLevel);
    }

    private void HideAlert(){
        if(adsRemovedSuccessAlert)adsRemovedSuccessAlert.SetActive(false);
    }

    public void HandleSuccessfulNoAdsPurchase()
    {
        if(admob)admob.DestroyBannerView();

        if (!panel){
            panel = GameObject.Find("RemoveAdsPanel");
        }

        if(! adsRemovedSuccessAlert && panel) {
            adsRemovedSuccessAlert = panel.GetComponent<IAPPanel>().alertSuccess;
        }

        if(menuUIButton){
            menuUIButton.SetActive(false);
        }
        
        PlayerPrefs.SetInt("no_ads_purchased", 1);
        //if(admob) admob.DestroyBannerView();
        //Application.LoadLevel(Application.loadedLevel);

        if(panel){
            panel.SetActive(false);
        }else{
            Application.LoadLevel(Application.loadedLevel);
            return;
        }

        if(adsRemovedSuccessAlert) {
            adsRemovedSuccessAlert.SetActive(true);
            StartCoroutine(HideAlertAfterDelay(adsRemovedSuccessAlert));
            
            PlayerPrefs.SetInt("buy_alert_showed", 1);
        }
        
        //  HidePanel();
        //  if(adsRemovedSuccessAlert) {
        //     adsRemovedSuccessAlert.SetActive(true);
        //     PlayerPrefs.SetInt("buy_alert_showed", 1);
        //  }

        //  StartCoroutine(HideAlertAfterDelay());
        //  if(admob) admob.DestroyBannerView();

        //  if(isMenu && menuUIButton){
        //     menuUIButton.SetActive(false);
        // }
    }

    public void HandleSuccessfulLevelsPurchase()
    {
        PlayerPrefs.SetInt("levels_purchased", 1);
        Application.LoadLevel(Application.loadedLevel);
    }

    private IEnumerator HideAlertAfterDelay(GameObject panelToHide)
    {
        yield return new WaitForSecondsRealtime(5f); // Uses real time instead of scaled time
        if(this != null && panelToHide != null) // Check if objects still exist
            panelToHide.SetActive(false);
    }

    public void OnPurchased(Product product)
    {
        if(product.definition.id.Equals(product1RemoveAds)){
            HandleSuccessfulNoAdsPurchase();
        }
    }

    public void OnProductsLoaded(ProductCollection products)
    {
        foreach(var product in products.all){
            Debug.Log("Loaded product: " + product.definition.id);
        }
    }
}