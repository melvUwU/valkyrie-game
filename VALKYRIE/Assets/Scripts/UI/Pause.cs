using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject Panel;
    public static bool gameIsPaused = false;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && Button.isPaused != true)
        {
            gameIsPaused = !gameIsPaused;
            PauseGame();
        }
    }
    public void PauseGame()
    {
        if (gameIsPaused)
        {
            Time.timeScale = 0f;
            Panel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            Panel.SetActive(false);
        }
    }

}
