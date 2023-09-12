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
        gameOver = false;
        SetStageCounts(currentStage);
        
    }

    public void SetStageCounts(GameStage stage)
    {
        switch (stage)
        {
            case GameStage.Stage1:
                enemyKill = 0;
                lifePoints = 5;
                break;
            case GameStage.Stage2:
                enemyKill = 0;
                lifePoints = 5;
                break;
            case GameStage.Stage3:
                enemyKill = 0;
                lifePoints = 5;
                break;
            case GameStage.Stage4:
                enemyKill = 0;
                lifePoints = 5;
                break;
            case GameStage.Stage5:
                enemyKill = 0;
                lifePoints = 5;
                break;

        }
    }

    // Update is called once per frame
    public void Update()
    {

        enemyKillText.text = enemyKill.ToString();
        lifePointsText.text = lifePoints.ToString();

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Enemies = new List<GameObject>(enemies);

        if (lifePoints == 0 && !gameOver)
        {
            Debug.Log(lifePoints);
            gameOver = true;
            gameOverScreen.SetActive(true);
            Time.timeScale = 0f;
        }
        else if (enemyKill == TotalEnemiesForCurrentStage())
        {
            completeScreen.SetActive(true);
            Time.timeScale = 0f;
            // Calculate and display rewards when the stage is completed
            rewardManager.CalculateAndDisplayRewards(enemyKill, TotalEnemiesForCurrentStage(), lifePoints);
        }
        else if (lifePoints < 5 && Enemies.Count == 0 && enemyKill <= TotalEnemiesForCurrentStage())
        {
            clearedScreen.SetActive(true);
            Time.timeScale = 0f;
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
                return 20;
            case GameStage.Stage5:
                return 25;
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
                return 20;
            case GameStage.Stage5:
                return 25;
            // Add more cases for other stages
            default:
                return 0;
        }
    }
}
