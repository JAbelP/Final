using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseMenu : MonoBehaviour
{
    public static bool inPause = false;

    public void pause(){
        inPause = true;
        Time.timeScale = 0f;
    }

    public void resume(){
        inPause = false;
        Time.timeScale = 1f;
    }

    public void restart(){
        inPause = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    public void menu(){
        inPause = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
        Time.timeScale = 1f;
    }

}
