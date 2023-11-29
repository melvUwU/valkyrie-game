using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public List<GameObject> Enemies = new List<GameObject>();

    private bool gameOver = false;

    public GameObject gameOverScreen;
    public GameObject completeScreen;
    public GameObject clearedScreen;

    public static int enemyKill;
    public static int lifePoints;
    public int enemyKillCount;
    public int maxKill;

    public enum GameStage
    {
        Stage1,
        Stage2,
        Stage3,
        Stage4,
        Stage5
        // Add more stages as needed
    }

    public GameStage currentStage;

    public Text enemyKillText;
    public Text lifePointsText;

    // Reference to the RewardManager script
    public RewardManager rewardManager;

    // Start is called before the first frame update
    void Start()
    {
        gameOverScreen.SetActive(false);
        completeScreen.SetActive(false);
        clearedScreen.SetActive(false);
        gameOver = false;
        //SetStageCounts(currentStage);
        
    }

    // Update is called once per frame
    public void Update()
    {
        enemyKill = enemyKillCount;

        enemyKillText.text = enemyKill.ToString();
        lifePointsText.text = lifePoints.ToString();

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Enemies = new List<GameObject>(enemies);

        if (lifePoints == 0 && !gameOver)
        {
            //Debug.Log(lifePoints);
            //gameOver = true;
            //gameOverScreen.SetActive(true);
            //Time.timeScale = 0f;
        }
        if (enemyKill == maxKill)
        {
            completeScreen.SetActive(true);
            //Time.timeScale = 0f; //edit

            // Calculate and display rewards when the stage is completed
            rewardManager.CalculateAndDisplayRewards(enemyKill, TotalEnemiesForCurrentStage(), lifePoints);
        }
    }

    public int TotalEnemiesForCurrentStage()
    {
        // Define the total enemy count for each stage here
        switch (currentStage)
        {
            case GameStage.Stage1:
                return 5;
            case GameStage.Stage2:
                return 10;
            case GameStage.Stage3:
                return 15;
            case GameStage.Stage4:
                return 15;
            case GameStage.Stage5:
                return 11;
            // Add more cases for other stages
            default:
                return 0;
        }
    }

    public int MaxEnemiesForCurrentStage()
    {
        // Define the max allowed enemies in the base for each stage here
        switch (currentStage)
        {
            case GameStage.Stage1:
                return 5;
            case GameStage.Stage2:
                return 10;
            case GameStage.Stage3:
                return 15;
            case GameStage.Stage4:
                return 15;
            case GameStage.Stage5:
                return 11;
            // Add more cases for other stages
            default:
                return 0;
        }
    }
}
