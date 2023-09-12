using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardManager : MonoBehaviour
{
    public Text mat1FullRewardText;
    public Text mat1HalfRewardText;

    public Text mat2FullRewardText;
    public Text mat2HalfRewardText;

    //public Text mat1Text;
    //public Text mat2Text;

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
            DisplayFullRewards();
        }
        else if (enemyKill < totalEnemies)
        {
            // Half rewards if not all enemies are killed
            mat1Reward = GetHalfMat1Reward(currentStage);
            mat2Reward = GetHalfMat2Reward(currentStage);
            DisplayHalfRewards();
        }

        // Display the rewards in the UI Text objects
        mat1FullRewardText.text = mat1Reward.ToString();
        mat2FullRewardText.text = mat2Reward.ToString();
        mat1HalfRewardText.text = mat1Reward.ToString();
        mat2HalfRewardText.text = mat2Reward.ToString();

    }

    // Define the full and half rewards for mat1 and mat2 based on the current stage
    public int GetFullMat1Reward(Material.GameStage stage)
    {
        switch (stage)
        {
            case Material.GameStage.Stage1:
                Rewards.fullMat1Reward += 20;
                return 20;
            case Material.GameStage.Stage2:
                Rewards.fullMat1Reward += 40;
                return 40;
            case Material.GameStage.Stage3:
                Rewards.fullMat1Reward += 60;
                return 60;
            case Material.GameStage.Stage4:
                Rewards.fullMat1Reward += 80;
                return 80;
            case Material.GameStage.Stage5:
                Rewards.fullMat1Reward += 100;
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
                Rewards.fullMat2Reward += 6;
                return 6;
            case Material.GameStage.Stage2:
                Rewards.fullMat2Reward += 10;
                return 10;
            case Material.GameStage.Stage3:
                Rewards.fullMat2Reward += 18;
                return 18;
            case Material.GameStage.Stage4:
                Rewards.fullMat2Reward += 20;
                return 20;
            case Material.GameStage.Stage5:
                Rewards.fullMat2Reward += 30;
                return 30;
            default:
                return 0;
        }
    }

    public int GetHalfMat2Reward(Material.GameStage stage)
    {
        return GetFullMat2Reward(stage) / 2;
    }

    private void DisplayFullRewards()
    {
        mat1FullRewardText.gameObject.SetActive(true);
        mat1HalfRewardText.gameObject.SetActive(false);

        mat2FullRewardText.gameObject.SetActive(true);
        mat2HalfRewardText.gameObject.SetActive(false);
    }

    private void DisplayHalfRewards()
    {
        mat1FullRewardText.gameObject.SetActive(false);
        mat1HalfRewardText.gameObject.SetActive(true);

        mat2FullRewardText.gameObject.SetActive(false);
        mat2HalfRewardText.gameObject.SetActive(true);
    }

}
