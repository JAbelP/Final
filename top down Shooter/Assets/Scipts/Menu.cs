using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public TextMeshProUGUI textHighScore;

    public void Start(){
        textHighScore.text = "Highest Score: " + PlayerPrefs.GetInt("highScore");
    }


    public void start(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void quit(){
        Application.Quit();
    }


}
