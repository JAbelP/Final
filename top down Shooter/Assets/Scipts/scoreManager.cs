using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class scoreManager : MonoBehaviour{
    public TextMeshProUGUI textScore;
    private static int _score;

    private void Start(){
        _score = 0;
    }

    void Update(){
        textScore.text = "Score: " + _score;
    }
    public static void addScore(){
        _score++;
    }
    public static void resetScore(){
        _score = 0;
    }
    public static int getScore(){
        return _score;
    }
}
