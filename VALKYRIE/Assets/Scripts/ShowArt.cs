using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowArt : MonoBehaviour
{
    public Image splashArt;
    private bool isArtShown = false;

    // Start is called before the first frame update
    void Start()
    {
        splashArt.enabled = false;

    }

    public void OnButtonClick()
    {
        if(isArtShown)
        {
            splashArt.enabled = false;
        }
        else
        {
            splashArt.enabled = true;
        }
    }

    public void Update()
    {
        if(isArtShown && Input.GetMouseButtonDown(0))
        {
            splashArt.enabled = false;
            isArtShown = false;
        }
    }
}
