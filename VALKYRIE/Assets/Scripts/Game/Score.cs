using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public Material.GameStage currentStage;

    private int mat1Reward;
    private int mat2Reward;


    // Calculate and display rewards based on the conditions you specified
    public void CalculateAndDisplayRewards(int enemyKill, int totalEnemies, int lifePoints)
    {
        // Calculate the rewards based on the conditions
        if (lifePoints == 0)
        {
            // No rewards if life points are zero
            mat1Reward = 0;
            mat2Reward = 0;
        }
        else if (enemyKill == totalEnemies)
        {
            // Full rewards if all enemies are killed
            mat1Reward = GetFullMat1Reward(currentStage);
            mat2Reward = GetFullMat2Reward(currentStage);
        }
        else if (enemyKill < totalEnemies)
        {
            // Half rewards if not all enemies are killed
            mat1Reward = GetHalfMat1Reward(currentStage);
            mat2Reward = GetHalfMat2Reward(currentStage);
        }
    }

    // Define the full and half rewards for mat1 and mat2 based on the current stage
    public int GetFullMat1Reward(Material.GameStage stage)
    {
        switch (stage)
        {
            case Material.GameStage.Stage1:
                return 20;
            case Material.GameStage.Stage2:
                return 40;
            case Material.GameStage.Stage3:
                return 60;
            case Material.GameStage.Stage4:
                return 80;
            case Material.GameStage.Stage5:
                return 100;
            default:
                return 0;
        }
    }

    public int GetHalfMat1Reward(Material.GameStage stage)
    {
        return GetFullMat1Reward(stage) / 2;
    }

    public int GetFullMat2Reward(Material.GameStage stage)
    {
        switch (stage)
        {
            case Material.GameStage.Stage1:
                return 6;
            case Material.GameStage.Stage2:
                return 10;
            case Material.GameStage.Stage3:
                return 18;
            case Material.GameStage.Stage4:
                return 20;
            case Material.GameStage.Stage5:
                return 30;
            default:
                return 0;
        }
    }

    public int GetHalfMat2Reward(Material.GameStage stage)
    {
        return GetFullMat2Reward(stage) / 2;
    }
}
