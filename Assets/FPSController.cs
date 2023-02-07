using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSController : MonoBehaviour
{
    public Text fpsCount;

  // Start is called before the first frame update
  void Start()
  {
      fpsCount.gameObject.SetActive(false);
      //fpsCount.text = "FPS: "+((int)(1f / Time.unscaledDeltaTime)).ToString();
      Application.targetFrameRate = 60;
  }



  // Update is called once per frame
  void Update()
  {
      
  }
}
