using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    public GameObject panel;
    public GameObject resumeButton;
    public GameObject pauseButton;
    public GameObject exit;
    public GameObject skip;

    public static bool isPaused = false;
    

    private void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Escape))
        //{
        //    Pausing();
        //}
        //else
        //{
        //    Continued();
        //}
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void TogglePause()
    {
        if (!isPaused)
        {
            PauseGame();
            Debug.Log("pause");
        }
        else
        {
            ResumeGame();
            Debug.Log("resume");
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        Debug.Log("game pause" + isPaused);
        Time.timeScale = 0f;
        panel.SetActive(true);
        resumeButton.SetActive(true);
        pauseButton.SetActive(false);
    }

    public void ResumeGame()
    {
        isPaused = false;
        Debug.Log("game resume" + isPaused);
        Time.timeScale = 1;
        panel.SetActive(false);
        resumeButton.SetActive(false);
        pauseButton.SetActive(true);
    }

    public void ExitGame()
    {
        isPaused = true;
        Debug.Log("game pause" + isPaused);
        Time.timeScale = 0f;
        exit.SetActive(true);
    }

    public void Skip()
    {
        isPaused = true;
        Debug.Log("game pause" + isPaused);
        Time.timeScale = 0f;
        skip.SetActive(true);
    }
    public void No()
    {
        isPaused = false;
        Time.timeScale = 1;
        skip.SetActive(false);
    }
}

