using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.Advertisements;
using System;
//using GoogleMobileAds.Api;
using Yodo1.MAS;

public class Move : MonoBehaviour
{
    // Start is called before the first frame update

    private int force=0;
    public CameraMove camera;
    public Text score,best;
    private int iscore=0;
    private int move=1;

    public GameObject pausePanel;
    public GameObject diePanel;
    public GameObject winPanel;
    public ParticleSystem[] particle;

    public StarsAtPlayer stars;

    public CharAnimator anim;

    public Slider forceSlider;
    
    public Image sliderFill;

    public Color32 forcePlusSlider,forceMinusSlider;

    public Text coins;

    public bool extraLife=false;

    private string appId="4035249";

    public bool lastLevel=false;

    public PlayerSound sound;

    void Start()
    {
        //MobileAds.Initialize(appIdAdmob);
        //RequestConfigurationAd();
        //RequestBannerAd();

        sound=GetComponent<PlayerSound>();
        Time.timeScale=1;

        //Advertisement.Initialize(appId,false);
        InitializeSdk();
        SetPrivacy(true, false, false);
        SetDelegates();
        

        StartCoroutine(BannerCoroutine());
        
        if(Yodo1U3dMas.IsBannerAdLoaded()){
            Yodo1U3dMas.ShowBannerAd();
        }

        extraLife=false;

        coins.text=PlayerPrefs.GetInt("Coin").ToString();
        anim.idle();
        Time.timeScale=1;
        camera.player=gameObject;
        if(PlayerPrefs.GetInt("Completed"+Application.loadedLevel.ToString())==1){
            best.gameObject.SetActive(true);
            best.text=PlayerPrefs.GetInt("bestIn"+Application.loadedLevel.ToString())+" s";
        }
        else{
            best.gameObject.SetActive(false);
        }

        StartCoroutine(scoreInc());

        
    }

    private bool right=false,left=false;

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(force);
        if(right){
            if(force<100)force++;

            forceSlider.value=Mathf.Abs(force);
            if(sliderFill.GetComponent<Image>().color!=forcePlusSlider){
                sliderFill.GetComponent<Image>().color=forcePlusSlider;
            }
        }
        else if(left){
            if(Mathf.Abs(force)<100)force--;

            forceSlider.value=Mathf.Abs(force);
            if(sliderFill.GetComponent<Image>().color!=forceMinusSlider){
                sliderFill.GetComponent<Image>().color=forceMinusSlider;
            }
        }
        if(Input.GetMouseButtonUp(0) ){
            

            
            anim.jump();
            //gameObject.GetComponent<Rigidbody>().isKinematic=false;
            //if(PlayerPrefs.GetInt("studyComplete")==1){
                    if(gameObject.GetComponent<Rigidbody>().useGravity==false){
                        GetComponent<Rigidbody>().freezeRotation=true;
                        GetComponent<Rigidbody>().AddForce(Vector3.right*-1*force*5*move/4*Time.timeScale);
                        GetComponent<Rigidbody>().AddForce(Vector3.up*Mathf.Abs(force)*5*move/5*Time.timeScale);
                    }
                    else if(gameObject.GetComponent<Rigidbody>().mass<1){
                        GetComponent<Rigidbody>().freezeRotation=true;
                        GetComponent<Rigidbody>().AddForce(Vector3.right*-1*force*5*move/2*Time.timeScale);
                        GetComponent<Rigidbody>().AddForce(Vector3.up*Mathf.Abs(force)*5*move/3*Time.timeScale);
                    }
                    else{
                        
                        GetComponent<Rigidbody>().freezeRotation=false;
                        GetComponent<Rigidbody>().AddForce(Vector3.right*-1*force*5*move*Time.timeScale);
                        GetComponent<Rigidbody>().AddForce(Vector3.up*Mathf.Abs(force)*5*move*Time.timeScale);
                    }
                    right=false;
                    left=false;
            //}
            
            force=0;
            forceSlider.value=Mathf.Abs(force);
        }
        
    }

    public bool death=false;
    float speed=0.02f;

    public ChooseItem chooseItem;

    void OnCollisionEnter(Collision other){

        if(chooseItem.choosen[1].activeSelf==true){
            chooseItem.stopAcceleration();
        }
        //camera.changePos();

        // Vector3 targetDirection = new Vector3(-100,transform.position.y,transform.position.z) - transform.position;

        // float singleStep = speed * Time.deltaTime;

        // Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

        // transform.rotation = Quaternion.LookRotation(newDirection);

        if(Mathf.Abs(transform.rotation.z)>0.35f){
                GetComponent<Animator>().enabled=true;
                GetComponent<Animator>().Play("ResetPos");

                Invoke("cleanAnim",0.8f);
            }
        
        
        


        
        if(other.gameObject.tag=="Death"){
            anim.person.gameObject.transform.GetChild(2).gameObject.GetComponent<BoxCollider>().enabled=true;
                 
            //anim.jump();
            //transform.eulerAngles=new Vector3(0,0,0);
            GetComponent<Rigidbody>().freezeRotation=true;
            Invoke("checkPos",0.1f);
            death=true;
            

        }
        if(other.gameObject.tag=="Finish"){



            
            
            anim.idle();

            particle[0].transform.parent=null;
            particle[0].gameObject.SetActive(true);
            particle[0].Play();

            winPanel.SetActive(true);
            
            

            if(lastLevel){
                PlayerPrefs.SetInt("win",1);
                move=0;
                stars.hideStars();
                return;
            }
            move=0;

            stars.showStars(stars.starsAtFinish);
            if(PlayerPrefs.GetInt("Completed"+Application.loadedLevel.ToString()+".with")<stars.getStarsNumber()){

                PlayerPrefs.SetInt("AllStars",PlayerPrefs.GetInt("AllStars")-PlayerPrefs.GetInt("Completed"+Application.loadedLevel.ToString()+".with")+stars.getStarsNumber());
                
                PlayerPrefs.SetInt("Completed"+Application.loadedLevel.ToString()+".with",stars.getStarsNumber());

                
            }
            
            score.gameObject.SetActive(false);

            
            if(PlayerPrefs.GetInt("bestIn"+Application.loadedLevel.ToString())>iscore || PlayerPrefs.GetInt("Completed"+Application.loadedLevel.ToString())==0){
                PlayerPrefs.SetInt("bestIn"+Application.loadedLevel.ToString(),iscore);
            }

            PlayerPrefs.SetInt("Completed"+Application.loadedLevel.ToString(),1);
            best.gameObject.SetActive(true);
            best.text="Best: "+PlayerPrefs.GetInt("bestIn"+Application.loadedLevel.ToString());
            

            
        }
        if(other.gameObject.tag!="Death" && other.gameObject.tag!="Finish"){
            anim.jump();
            
            

            anim.stopJump();
        }


        if(other.gameObject.name=="Up"){
            Debug.Log("upppp");
            GetComponent<Rigidbody>().useGravity=true;
            GetComponent<Rigidbody>().AddForce(Vector3.up*ChooseItem.fallForce*move*Time.timeScale);
            GetComponent<Rigidbody>().useGravity=false;
        }



        if(other.gameObject.tag=="movablebox"){
            if(death && anim.dead){
                anim.person.gameObject.SetActive(false);
                //GetComponent<Rigidbody>().velocity=Vector3.up*-1000;
            }
        }

        
    }

    void cleanAnim(){
        GetComponent<Animator>().enabled=false;
    }

    void OnCollisionStay(Collision other){
        if(other.gameObject.tag=="Death"){
            if(extraLife){
                //extraLife=false;
                //chooseItem.extraLifeOff();
                return;
            }else{
                anim.person.gameObject.transform.GetChild(2).gameObject.GetComponent<BoxCollider>().enabled=true;

                Invoke("checkPos",0.1f);
                death=true;
            }
            

        }
    }

    bool playedOnce=false;
    public static int cnt=0;
    void checkPos()
    {
        if(extraLife){

            return;
        }
        else{
            if(death){
                gameObject.GetComponent<Rigidbody>().isKinematic=true;
                anim.person.gameObject.transform.GetChild(2).gameObject.GetComponent<BoxCollider>().enabled=true;

               // GetComponent<BoxCollider>().enabled=false;
               // gameObject.transform.GetChild(2).gameObject.GetComponent<BoxCollider>().enabled=false;
                if(chooseItem.choosen[0].activeSelf==true){
                    chooseItem.stopMagnet();
                }

                
                //extraLife=false;
                //chooseItem.extraLifeOff();
                
                            
                if(!playedOnce){
                    if(cnt%3==1){
                        /*if(Advertisement.IsReady("video") ){
                            Advertisement.Show("video");
                        }
                        else{
                            showIntersitionalAd();
                        }*/
                        if(Yodo1U3dMas.IsInterstitialAdLoaded()){
                            Yodo1U3dMas.ShowInterstitialAd();
                        }
                    }
                    cnt++;
                    print("Rotation: "+transform.rotation.z.ToString());
                    anim.stopAll();
                    //if(Mathf.Abs(transform.rotation.z)<0.5f){
                        anim.die();
                    //}
                    playedOnce=true;
                }
                Invoke("deathFunc",1f);
                move=0;

            }
        }
    }


    void deathFunc(){
        
        stars.resetStars();

        diePanel.SetActive(true);

    }

    void OnCollisionExit(Collision other){
        //camera.changePos();
        if(other.gameObject.tag=="Death"){
            if(extraLife ){
                Invoke("offExtraLife",0.5f);
            }
            anim.person.gameObject.transform.GetChild(2).gameObject.GetComponent<BoxCollider>().enabled=false;

        
            
            death=false;
            

        }
    }

    void offExtraLife(){
      if(extraLife&& transform.position.y>0.5f){
          extraLife=false;
          chooseItem.extraLifeOff();
      }
    }
    
    //bool run=true;
    IEnumerator scoreInc(){
        while(move==1){
            if(move!=0 && winPanel.activeSelf==false){
                iscore++;
                score.text=iscore.ToString()+" s";
            }
            yield return new WaitForSeconds(1);
        }
    }

    

    public void pause(){
        Time.timeScale=0;
        pausePanel.SetActive(true);
    }

    public void resume(){
        Time.timeScale=1;
        pausePanel.SetActive(false);
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

    public void back(){
        Time.timeScale=1;
        StartCoroutine(loadAsync(0));
    }

    public void restart(){
 
        Time.timeScale=1;
        StartCoroutine(loadAsync(Application.loadedLevel));

    }


    public void rightBtn(){
        force=0;
        left=false;
        right=true;
        
    }

    public void leftBtn(){
        force=0;
        right=false;
        left=true;
    }


    public void nextLevel(){
        StartCoroutine(loadAsync(Application.loadedLevel+1));
    }

    


    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag=="Star"){
            if(PlayerPrefs.GetInt("!sound")==0)sound.play1();
            Debug.Log("starrrr");
            PlayerPrefs.SetInt("Get."+Application.loadedLevel.ToString()+"."+other.gameObject.name.ToString(),1);
            //particle[1].transform.parent=null;
            particle[1].gameObject.SetActive(true);
            particle[1].Play();
            stars.showStars(stars.currentStars);
            other.gameObject.SetActive(false);


        }

        if(other.gameObject.tag=="Coin"){

            if(PlayerPrefs.GetInt("!sound")==0)sound.play1();

            other.gameObject.SetActive(false);
            PlayerPrefs.SetInt("Coin",PlayerPrefs.GetInt("Coin")+1);
            //particle[2].transform.parent=null;
            particle[2].gameObject.SetActive(true);
            particle[2].Play();
            coins.text=PlayerPrefs.GetInt("Coin").ToString();
        }

        if(other.gameObject.tag=="Finish"){

            
            anim.idle();

            particle[0].transform.parent=null;
            particle[0].gameObject.SetActive(true);
            particle[0].Play();

            winPanel.SetActive(true);

            if(lastLevel){
                stars.hideStars();
                PlayerPrefs.SetInt("win",1);
                return;
            }
            stars.showStars(stars.starsAtFinish);
            if(PlayerPrefs.GetInt("Completed"+Application.loadedLevel.ToString()+".with")<stars.getStarsNumber()){

                PlayerPrefs.SetInt("AllStars",PlayerPrefs.GetInt("AllStars")-PlayerPrefs.GetInt("Completed"+Application.loadedLevel.ToString()+".with")+stars.getStarsNumber());
                
                PlayerPrefs.SetInt("Completed"+Application.loadedLevel.ToString()+".with",stars.getStarsNumber());

                
            }
            
            

            
            if(PlayerPrefs.GetInt("bestIn"+Application.loadedLevel.ToString())>iscore || PlayerPrefs.GetInt("Completed"+Application.loadedLevel.ToString())==0){
                PlayerPrefs.SetInt("bestIn"+Application.loadedLevel.ToString(),iscore);
            }

            PlayerPrefs.SetInt("Completed"+Application.loadedLevel.ToString(),1);
            best.gameObject.SetActive(true);
            best.text="Best: "+PlayerPrefs.GetInt("bestIn"+Application.loadedLevel.ToString());
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

   

    /*private InterstitialAd intersitional;
    private BannerView banner;

    private string appIdAdmob="ca-app-pub-4962234576866611~8649087922";
    private string intersitionalId="ca-app-pub-4962234576866611/2331774749";
    private string bannerId="ca-app-pub-4962234576866611/2972961084";


     AdRequest AdRequestBuild(){
         return new AdRequest.Builder().Build();
     }


      void RequestConfigurationAd(){
          intersitional=new InterstitialAd(intersitionalId);
          AdRequest request=AdRequestBuild();
          intersitional.LoadAd(request);
          intersitional.OnAdLoaded+=this.HandleOnAdLoaded;
          intersitional.OnAdOpening+=this.HandleOnAdOpening;
          intersitional.OnAdClosed+=this.HandleOnAdClosed;

    }


      public bool showIntersitionalAd(){
          if(intersitional.IsLoaded()){
              intersitional.Show();
          }

          return intersitional.IsLoaded();
      }

      private void OnDestroy(){
          DestroyIntersitional();

          intersitional.OnAdLoaded-=this.HandleOnAdLoaded;
          intersitional.OnAdOpening-=this.HandleOnAdOpening;
          intersitional.OnAdClosed-=this.HandleOnAdClosed;

      }

      private void HandleOnAdClosed(object sender, EventArgs e)
      {
          intersitional.OnAdLoaded-=this.HandleOnAdLoaded;
          intersitional.OnAdOpening-=this.HandleOnAdOpening;
          intersitional.OnAdClosed-=this.HandleOnAdClosed;

          //RequestConfigurationAd();

        
      }

     private void HandleOnAdOpening(object sender, EventArgs e)
     {
        
     }

     private void HandleOnAdLoaded(object sender, EventArgs e)
     {
        
     }

     public void DestroyIntersitional(){
         intersitional.Destroy();
     }




    //baner


    AdRequest AdRequestBannerBuild(){
        return new AdRequest.Builder().Build();
    }


    public void RequestBannerAd(){
        banner=new BannerView(bannerId,AdSize.Banner,AdPosition.Bottom);
        AdRequest request = AdRequestBannerBuild();
        banner.LoadAd(request);
    }

    public void DestroyBanner(){
        if(banner!=null){
            banner.Destroy();
        }
    }
    */




}
