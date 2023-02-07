using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Study_Player : MonoBehaviour
{
    private int force=0;
    public CameraMove camera;
    private int move=1;
    public GameObject winPanel;
    public ParticleSystem[] particle;

    public CharAnimator anim;

    public Slider forceSlider;
    
    public Image sliderFill;

    public Color32 forcePlusSlider,forceMinusSlider;

    public Text coins;

    public bool extraLife=false;

    private string appId="4035249";

    public bool lastLevel=false;

    public PlayerSound sound;

    public GameObject rightPanel;
    public GameObject leftPanel;
    public GameObject[] infoPanel;
    public GameObject diePanel;


    int currStudyPanel;


    void Start()
    {
        currStudyPanel=0;
        sound=GetComponent<PlayerSound>();
        Time.timeScale=1;

        extraLife=false;

        coins.text=PlayerPrefs.GetInt("Coin").ToString();
        anim.idle();
        Time.timeScale=1;
        camera.player=gameObject;

        rightPanel.SetActive(true);


        
    }

    private bool right=false,left=false;

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(force);
        if(right){
            if(currStudyPanel==0){
                rightPanel.SetActive(false);
                leftPanel.SetActive(true);
                currStudyPanel=1;
            }
            
            if(force<100)force++;

            forceSlider.value=Mathf.Abs(force);
            if(sliderFill.GetComponent<Image>().color!=forcePlusSlider){
                sliderFill.GetComponent<Image>().color=forcePlusSlider;
            }
        }
        else if(left){
            if(currStudyPanel==1){
                leftPanel.SetActive(false);
                infoPanel[0].SetActive(true);
                currStudyPanel=2;
            }
            if(Mathf.Abs(force)<100)force--;

            forceSlider.value=Mathf.Abs(force);
            if(sliderFill.GetComponent<Image>().color!=forceMinusSlider){
                sliderFill.GetComponent<Image>().color=forceMinusSlider;
            }
        }
        if(Input.GetMouseButtonUp(0) ){
            
            if(currStudyPanel==2){
                rightPanel.SetActive(false);
                leftPanel.SetActive(false);

                infoPanel[0].SetActive(true);
                //infoPanel[0].SetActive(false);
                currStudyPanel++;
            }
            else{
                currStudyPanel++;
                
                if(currStudyPanel==4){
                    rightPanel.SetActive(false);
                    leftPanel.SetActive(false);

                    infoPanel[1].SetActive(true);
                    infoPanel[0].SetActive(false);
                    currStudyPanel++;
                }
                if(currStudyPanel==6){
                    infoPanel[1].SetActive(false);
                    infoPanel[2].SetActive(true);
                    currStudyPanel++;
                }
                else if(currStudyPanel==8){
                    infoPanel[2].SetActive(false);
                    infoPanel[3].SetActive(true);
                    currStudyPanel++;
                }
                else if(currStudyPanel==10){
                    infoPanel[3].SetActive(false);
                    infoPanel[4].SetActive(true);
                    currStudyPanel++;
                }
                else if(currStudyPanel==11){
                    infoPanel[4].SetActive(false);
                    winPanel.SetActive(true);
                    if(!IsInvoking(nameof(finishStudy))){
                        Invoke(nameof(finishStudy),1f);
                    }
                }
            }
            

            
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

            winPanel.SetActive(true);

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
                    
                    print("Rotation: "+transform.rotation.z.ToString());
                    anim.stopAll();
                    //if(Mathf.Abs(transform.rotation.z)<0.5f){
                        anim.die();
                    //}
                    playedOnce=true;
                }
                
                //Invoke("deathFunc",1f);
                deathFunc();
                move=0;

            }
        }
    }


    void deathFunc(){
        rightPanel.SetActive(false);
        leftPanel.SetActive(false);

        diePanel.SetActive(true);
        foreach(var p in infoPanel){
            p.SetActive(false);
            currStudyPanel=100;
        }

    }



    public void finishStudy(){
        PlayerPrefs.SetInt("studied",1);
        StartCoroutine(loadAsync(0));
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

            
            

            winPanel.SetActive(true);
            Invoke(nameof(finishStudy),2);

            
        }

        
    }
}
