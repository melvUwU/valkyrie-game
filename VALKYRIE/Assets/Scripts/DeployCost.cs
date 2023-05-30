using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeployCost : MonoBehaviour
{
    public int maxCost = 99;
    public float updateInterval = 0.05f;
    public Text costText;

    public static int currentCost = 0;
    private float timer = 0.0f;

    void Start()
    {
        //initialize cost text to be shown
        costText.text = currentCost.ToString();
    }

    void Update()
    {
        //set the timer
        timer += Time.deltaTime;
        //if timer is greater than updateInterval and the currentCost is less than maxCost, update the currentCost
        if (timer >= updateInterval && currentCost < maxCost)
        {
            currentCost++;
            costText.text = currentCost.ToString();
            //reset timer
            timer = 0.0f;
        }
    }
}
