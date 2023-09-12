using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rewards : MonoBehaviour
{
    public Text chipText;
    public Text ceciumText;

    public RewardManager rewardManager;

    public static int fullMat1Reward;
    public static int halfMat1Reward;
    public static int fullMat2Reward;
    public static int halfMat2Reward;

    private void Start()
    {
        // Initialize variables to store the accumulated rewards
        int totalFullMat1Reward = fullMat1Reward;
        int totalHalfMat1Reward = halfMat1Reward;
        int totalFullMat2Reward = fullMat2Reward;
        int totalHalfMat2Reward = halfMat2Reward;

        // Iterate through all stages to accumulate rewards
        foreach (Material.GameStage stage in System.Enum.GetValues(typeof(Material.GameStage)))
        {
            totalFullMat1Reward = rewardManager.GetFullMat1Reward(stage);
            totalHalfMat1Reward = rewardManager.GetHalfMat1Reward(stage);
            totalFullMat2Reward = rewardManager.GetFullMat2Reward(stage);
            totalHalfMat2Reward = rewardManager.GetHalfMat2Reward(stage);
        }

        // Calculate the combined rewards for chip and cecium
        int totalChipReward = totalFullMat1Reward + totalHalfMat1Reward;
        int totalCeciumReward = totalFullMat2Reward + totalHalfMat2Reward;

        // Display the combined rewards in the UI text
        chipText.text = totalChipReward.ToString();
        ceciumText.text = totalCeciumReward.ToString();
    }
}
