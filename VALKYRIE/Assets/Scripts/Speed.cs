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
        isFast = true;
        Time.timeScale = 1.4f;
    }
    public void LowerSpeed()
    {
        isFast = false;
        Time.timeScale = 1;
    }
}
