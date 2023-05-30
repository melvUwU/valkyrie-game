using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject draggingObj;
    public GameObject currentContainer;
    public Text costText;

    public int deployCost;


    public static GameManager Instance;

    public void Awake()
    {
        //initialise Instance variable to this GameManager
        Instance = this;
    }
    private void Update()
    {
        //set the dpCost to start at current cost
        deployCost = DeployCost.currentCost;
    }
    
    //Deploying a character
    public void Deploy(int x)
    {
        //check if draggingObj and currentContainer is valided
        if (draggingObj != null && currentContainer != null)
        {
            //player cannot deploy the character if dpCost < x
            if (deployCost < x)
            {
                Debug.Log("Not enough deploy cost!");
                return;
            }
            //if player have enough cost, deploy the character at the position of that current container
            Instantiate(draggingObj.GetComponent<ObjectDragging>().card.objGame, currentContainer.transform);
            //the taken container is full, so player cannot deploy another character on the same container
            currentContainer.GetComponent<ObjectContainer>().isFull = true;

            //dpCost minus by 13 according to the character cost
            DeployCost.currentCost -= 13;
            //show the current cost text
            costText.text = deployCost.ToString();
        }
    }
}
