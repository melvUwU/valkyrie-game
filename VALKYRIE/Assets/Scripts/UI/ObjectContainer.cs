using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectContainer : MonoBehaviour
{
    public bool isFull;
    public GameManager gameManager;
    public Image backgroundImage;
    public ObjectDragging objDragging;


    public void Start()
    {
        //initialize gameManager to GameManager.instance
        gameManager = GameManager.Instance;
    }

    //Display the tile when collides with the draggingObject
    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (gameManager.draggingObj != null && isFull == false)
        {
            gameManager.currentContainer = this.gameObject;
            backgroundImage.enabled = true;
        }
    }

    //Disable the tile when not colliding with the draggingObj
    public void OnTriggerExit2D(Collider2D collision)
    {
        gameManager.currentContainer = null;
        backgroundImage.enabled = false;
    }



}
