using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField]
    GameObject pauseScreen;
   public void ButtonMenu()
    {
        Time.timeScale = 0;
        pauseScreen.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void BackToGame()
    {
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Retry()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Play()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
}
