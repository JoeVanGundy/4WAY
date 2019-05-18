using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Button MuteButton;
    public Button UnmuteButton;
    public Button RestartButton;

    public void TogglePause()
    {
        if (GameManager.isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    private void Awake()
    {
        if (GameManager.isMuted)
        {
            MuteGame();
        }
        else
        {
            UnmuteGame();
        }
    }

    private void Start()
    {
    }


    public void MuteGame()
    {
        PlayerPrefs.SetInt("IsMuted", 1);
        MuteButton.gameObject.SetActive(false);
        UnmuteButton.gameObject.SetActive(true);
        GameManager.isMuted = true;
        AudioListener.volume = 0;
    }

    public void UnmuteGame()
    {
        PlayerPrefs.SetInt("IsMuted", 0);
        MuteButton.gameObject.SetActive(true);
        UnmuteButton.gameObject.SetActive(false);
        GameManager.isMuted = false;
        AudioListener.volume = 1;
    }

    public void PauseGame()
    {
        FindObjectOfType<GameManager>().PauseGame();
    }

    public void ResumeGame()
    {
        FindObjectOfType<GameManager>().ResumeGame();
    }

    public void EndGame()
    {
        FindObjectOfType<GameManager>().EndGame();
    }

    public void RestartGame()
    {
        FindObjectOfType<GameManager>().RestartGame();
    }
}
