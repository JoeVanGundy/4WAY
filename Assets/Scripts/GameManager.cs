using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool isPaused = false;
    public static bool isMuted = false;
    public static bool isGameOver = false;
    public float restartDelay = 2f;

    public int score;
    public int highScore;

    public void PauseGame()
    {
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void EndGame()
    {
        if (isGameOver == false) {
            isGameOver = true;
            Invoke("RestartGame", restartDelay);
        }

    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
    }


    public void RestartGame()
    {
        Debug.Log("END GAME");
        FindObjectOfType<AudioManager>().ResetMusic();
        SceneManager.LoadScene("1");
    }

    public void Awake()
    {
        isGameOver = false;
        isPaused = false;
        score = 0;
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        Debug.Log("START ISMUTED: " + PlayerPrefs.GetInt("IsMuted", 0));
        isMuted = PlayerPrefs.GetInt("IsMuted", 0) == 1 ? true : false;
    }

    public void Update()
    {

    }
}
