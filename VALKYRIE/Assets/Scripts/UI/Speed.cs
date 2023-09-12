using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed : MonoBehaviour
{
    public GameObject fastButton;
    private bool isFast = false;
    // Start is called before the first frame update

    public void ToggleSpeed()
    {
        if (!isFast)
        {
            ChangeSpeed();
        }
        else
        {
            LowerSpeed();
        }
    }
    public void ChangeSpeed()
    {
        if(isFast == false)
        {
            isFast = true;
            Time.timeScale = 2;
            EnemyMovement.speed = 15f;
        }

    }
    public void LowerSpeed()
    {
        if(isFast == true)
        {
            isFast = false;
            Time.timeScale = 1;
        }
       
    }
}
