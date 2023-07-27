using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Results : MonoBehaviour
{
    public Text lpText;
    public Text objText;

    public Text chipsText;
    public Text materialText;

    public static int chipsInt;
    public static int materialInt;

    private string resultsCases;

    // Start is called before the first frame update
    void Start()
    {
        chipsInt = 0;
        materialInt = 0;
    }

    // Update is called once per frame
    void Update()
    {
        lpText.text = ScoreManager.lifePoints.ToString();
        objText.text = ScoreManager.enemyKill.ToString();
    }

    public void ResultsCase()
    {
        if(ScoreManager.enemyKill == 5)
        {
            SetResultsCases("Best");
        }
        else if(ScoreManager.enemyKill < 5)
        {
            SetResultsCases("Good");
        }
        else
        {
            SetResultsCases("Poor");
        }
    }

    public void SetResultsCases(string resultCase)
    {
        switch(resultCase)
        {
            case "Best":
                if(resultsCases == "Best")
                {
                    break;
                }
                resultsCases = "Best";
                chipsInt = 3;
                materialInt = 10;
                break;
            case "Good":
                if(resultsCases == "Good")
                {
                    break;
                }
                resultsCases = "Good";
                chipsInt = 2;
                materialInt = 4;
                break;
            case "Poor":
                if (resultsCases == "Poor")
                {
                    break;
                }
                resultsCases = "Poor";
                chipsInt =0;
                materialInt = 0;
                break;
        }
    }
}
