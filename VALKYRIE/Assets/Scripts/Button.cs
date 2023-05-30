using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    public GameObject panel;
    //public GameObject pauseButton;
    //public GameObject resumeButton;
    
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Back()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void Pause()
    {
        panel.SetActive(true);
    }

    public void Resume()
    {
        panel.SetActive(false);
    }
}
