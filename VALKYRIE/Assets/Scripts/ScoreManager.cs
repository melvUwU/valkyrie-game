using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private bool gameOver = false;

    public GameObject gameOverScreen;

    public static int enemyKill;
    public static int lifePoints;


    public Text enemyKillText;
    public Text lifePointsText;
    

    // Start is called before the first frame update
    void Start()
    {
        enemyKill = 0;
        lifePoints = 5;
        gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        enemyKillText.text = enemyKill.ToString();
        lifePointsText.text = lifePoints.ToString();

        if(lifePoints == 0)
        {
            gameOver = true;
            if(gameOver == true)
            {
                gameOverScreen.SetActive(true);
            }
        }
    }
}
