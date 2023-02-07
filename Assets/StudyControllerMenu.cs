using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StudyControllerMenu : MonoBehaviour
{
    public GameObject studyPanel;

    void Start(){
        if(PlayerPrefs.GetInt("studied")==0){
            studyPanel.SetActive(true);
        }
        else{
            studyPanel.SetActive(false);
        }
    }

    public void startStudy(int scene){
        PlayerPrefs.SetInt("studied",1);
        StartCoroutine(loadAsync(scene));
    }

    public void skipStudy(){
        PlayerPrefs.SetInt("studied",1);
        studyPanel.SetActive(false);
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
}
