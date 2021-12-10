using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class heartManager : MonoBehaviour
{
    public List<GameObject> hearts;
    private int heart = 3;

    public bool powerUp(){
        if (heart < 3){
            heart++;
            changeHp();
            return true;
        }
        else
            return false;
    }
    public void hited(){
        heart--;
        changeHp();
    }

    public void changeHp(){
        switch (heart){
            case 3:
                hearts[2].SetActive(true);
                hearts[1].SetActive(true);
                hearts[0].SetActive(true);
                break;
            case 2:
                hearts[2].SetActive(false);
                hearts[1].SetActive(true);
                hearts[0].SetActive(true);
                break;
            case 1:
                hearts[2].SetActive(false);
                hearts[1].SetActive(false);
                hearts[0].SetActive(true);
                break;
            case 0:
                if (scoreManager.getScore() > PlayerPrefs.GetInt("highScore"))
                    PlayerPrefs.SetInt("highScore", scoreManager.getScore());
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
                break;
        }
    }



}
