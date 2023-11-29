using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayUI : MonoBehaviour
{
    public GameObject exit;
    public GameObject retry;
    public void OnRetry()
    {
        SceneManager.GetActiveScene();
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);

        Debug.Log(Time.time);
    }
}
