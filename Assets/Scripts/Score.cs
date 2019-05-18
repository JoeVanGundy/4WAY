using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static Text scoreField;
    private static Color successColor = new Color(0.455f, 0.882f, 0.486f, 1f);

    // Start is called before the first frame update
    void Start()
    {
        scoreField = GetComponent<Text>();
        FindObjectOfType<AudioManager>().ChangeMusic("Song1");
        FindObjectOfType<GameManager>().score = 0;
        scoreField.text = "SWIPE!";
        scoreField.color = Color.white;
    }

    public static void ScorePoint()
    {
        AudioManager am = FindObjectOfType<AudioManager>();
        scoreField.color = Color.white;
        FindObjectOfType<GameManager>().score++;

        if (FindObjectOfType<GameManager>().score > FindObjectOfType<GameManager>().highScore) {
            FindObjectOfType<HighScore>().SetHighScore();
        }


        if (FindObjectOfType<GameManager>().score <= 50) {
            if (FindObjectOfType<GameManager>().score % 10 == 0)
            {
                scoreField.color = successColor;
                am.PlaySuccess();
                FindObjectOfType<AudioManager>().PlaySound("Cheer");
            }
        }
        else {
            if (FindObjectOfType<GameManager>().score % 25 == 0)
            {
                scoreField.color = successColor;
                am.PlaySuccess();
                FindObjectOfType<AudioManager>().PlaySound("Cheer");
            }
        }


        if (FindObjectOfType<GameManager>().score >= 0 && FindObjectOfType<GameManager>().score < 10)
        {
            am.ChangeMusic("Song1");
        }
        else if (FindObjectOfType<GameManager>().score >= 10 && FindObjectOfType<GameManager>().score < 20)
        {
            am.ChangeMusic("Song2");
        }
        else if (FindObjectOfType<GameManager>().score >= 20 && FindObjectOfType<GameManager>().score < 30)
        {
            am.ChangeMusic("Song3");
        }
        else if (FindObjectOfType<GameManager>().score >= 30 && FindObjectOfType<GameManager>().score < 50)
        {
            am.ChangeMusic("Song4");
        }
        else if (FindObjectOfType<GameManager>().score >= 50 && FindObjectOfType<GameManager>().score < 75)
        {
            am.ChangeMusic("Song5");
        }
        else if (FindObjectOfType<GameManager>().score >= 75 && FindObjectOfType<GameManager>().score < 100)
        {
            am.ChangeMusic("Song6");
        }
        else if (FindObjectOfType<GameManager>().score >= 100 && FindObjectOfType<GameManager>().score < 125)
        {
            am.ChangeMusic("Song7");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<GameManager>().score != 0) {
            scoreField.text = FindObjectOfType<GameManager>().score.ToString();
        }
    }
}
