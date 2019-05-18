using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{

    public Text highScoreField;


    public void SetHighScore() {
        FindObjectOfType<GameManager>().highScore = FindObjectOfType<GameManager>().score;
        PlayerPrefs.SetInt("HighScore", FindObjectOfType<GameManager>().highScore);
        highScoreField.text = FindObjectOfType<GameManager>().highScore.ToString();
    }

    void Start()
    {
        Debug.Log("HIGH SCORE: " + FindObjectOfType<GameManager>().highScore);
        highScoreField.text = FindObjectOfType<GameManager>().highScore.ToString();
    }
}
