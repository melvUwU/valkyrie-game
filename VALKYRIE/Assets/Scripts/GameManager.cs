using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject draggingObj;
    public GameObject currentContainer;
    public Text costText;
    public Text unitLimitText;

    private Valkyrie currentValkyrie;

    public static int unitLimit;
    public static GameManager Instance;

    public void Awake()
    {
        unitLimit = 9;
        Instance = this;
    }
    private void Update()
    {
        // Check if draggingObj is valid and has a Guard component
        if (draggingObj != null)
        {
            currentValkyrie = draggingObj.GetComponent<Valkyrie>();
            if (currentValkyrie == null)
            {
                Debug.LogError("Dragging object does not have a Valkyrie component!");
            }
        }
        else
        {
            currentValkyrie = null;
        }
    }
    
    //Deploying a character
    public void Deploy(int dp)
    {
        //check if draggingObj and currentContainer is valided
        if (draggingObj != null && currentContainer != null)
        {
            if (currentValkyrie.deployCost <= DeployCost.currentCost) // Compare with DeployCost.currentCost
            {
                Instantiate(draggingObj.GetComponent<ObjectDragging>().card.objGame, currentContainer.transform);
                currentContainer.GetComponent<ObjectContainer>().isFull = true;

                // Deduct the correct cost, which is 'currentGuard.deployCost'
                DeployCost.currentCost -= currentValkyrie.deployCost;

                unitLimit--;
                // Show the current cost text
                unitLimitText.text = unitLimit.ToString();
            }
            else
            {
                Debug.Log("Not enough deploy cost!");
            }
        }
    }

}
