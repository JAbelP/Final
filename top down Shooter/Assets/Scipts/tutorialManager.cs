using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tutorialManager : MonoBehaviour{
    public GameObject tutorial;
    public Button pauseButton;

    private void Start(){
        if(PlayerPrefs.GetInt("firstTime") == 0){
            Tutorial();
            PlayerPrefs.SetInt("firstTime", 1);
        }
    }


    void Tutorial(){
        tutorial.SetActive(true);
        pauseButton.enabled = false;
        Time.timeScale = 0;
    }
}
