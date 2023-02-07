using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstUseController : MonoBehaviour
{
    public Image leftPanel,rightPanel;
    public GameObject rightBox,leftBox, otherBox;
    int curr=0;
    bool studyStarted=false;

    void Start()
    {
         studyStarted=false;
        //  if(PlayerPrefs.GetInt("studyComplete")==0){
        //      startStudy();
            
        //      StartCoroutine(completeStudy());
        //  }
    }

    void Update(){

        // if(Input.GetMouseButtonDown(0) && studyStarted){
        //     curr++;
        //     if(curr==1){
        //         rightPanel.GetComponent<Image>().color=new Color32(255,255,255,0);
        //         rightBox.SetActive(false);

        //         leftPanel.GetComponent<Image>().color=new Color32(255,255,255,100);
        //         leftBox.SetActive(true);
        //     }
        //     else if(curr==2){
        //         leftPanel.GetComponent<Image>().color=new Color32(255,255,255,0);
        //         leftBox.SetActive(false);

        //         otherBox.SetActive(false);
        //     }
        // }

    }

    IEnumerator completeStudy(){
        while(curr<3 && studyStarted){
            yield return new WaitForSeconds(2);
            curr++;
            if(curr==1){
                rightPanel.GetComponent<Image>().color=new Color32(255,255,255,0);
                rightBox.SetActive(false);

                leftPanel.GetComponent<Image>().color=new Color32(255,255,255,100);
                leftBox.SetActive(true);
            }
            else if(curr==2){
                leftPanel.GetComponent<Image>().color=new Color32(255,255,255,0);
                leftBox.SetActive(false);

                otherBox.SetActive(true);
            }
            else{
                PlayerPrefs.SetInt("studyComplete",1);
                otherBox.SetActive(false);
            }

            
        }
    }

    void startStudy(){
        studyStarted=true;

        rightPanel.GetComponent<Image>().color=new Color32(255,255,255,100);
        rightBox.SetActive(true);

    }
}
