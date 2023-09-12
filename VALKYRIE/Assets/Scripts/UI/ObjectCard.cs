using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ObjectCard : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public GameObject objDrag;
    public GameObject objGame;
    public Canvas canvas;
    private GameObject objDragInstance;
    private GameManager gameManager;
    public Valkyrie valkyrie;
    public static int dpCost;
    public static bool isDeployed;
    
    public void Start()
    {
        //set game manager indtance
        gameManager = GameManager.Instance;
        //set character's cost
        //guard = GetComponent<Guard>();

        if (valkyrie == null)
        {
            Debug.Log("ObjectCard does not have a Valkyrie component!");
        }
    }

    //Event when dragging the object with mouse
    public void OnDrag(PointerEventData eventData)
    {
        //the position of opjectDragInstance will be at the mouse positon when dragging the object
        objDragInstance.transform.position = Input.mousePosition;
    }

    //Event when holding the mouse button
    public void OnPointerDown(PointerEventData eventData)
    {
        //instantiate the objDrag when holding the mouse button
        objDragInstance = Instantiate(objDrag, canvas.transform);
        //set the position of objobjDragInstance at the mouse position
        objDragInstance.transform.position = Input.mousePosition;
        //initialize the objectDragInstance component
        objDragInstance.GetComponent<ObjectDragging>().card = this;

        //set draggingObj of gameManager to objDragInstance
        gameManager.draggingObj = objDragInstance;
        
    }

    //Event when releasing the mouse button
    public void OnPointerUp(PointerEventData eventData)
    {
        if (valkyrie != null && dpCost <= DeployCost.currentCost)
        {
            gameManager.Deploy(valkyrie.deployCost);
            isDeployed = true;
            
        }
        //deploy the character, cost the dpCosts
        gameManager.draggingObj = null;
        //destroy thr dragging game object
        Destroy(objDragInstance);

    }
}
