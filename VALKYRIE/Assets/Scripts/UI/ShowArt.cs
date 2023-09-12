using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShowArt : MonoBehaviour
{
    public Image[] characterArts;  // Array of character arts images
    private int currentArtIndex = -1;  // Index of the currently shown art, initialized to -1 (no art shown)

    public string[] upgradeScenes;  // Array of upgrade scene names corresponding to each character

    // Start is called before the first frame update
    void Start()
    {
        // Initially, no art is shown
        HideCurrentArt();
    }

    // Function to hide the currently shown art (if any)
    void HideCurrentArt()
    {
        if (currentArtIndex >= 0 && currentArtIndex < characterArts.Length)
        {
            characterArts[currentArtIndex].enabled = false;
        }
    }

    // Function to show the art for a specific character
    void ShowCharacterArt(int characterIndex)
    {
        if (characterIndex >= 0 && characterIndex < characterArts.Length)
        {
            characterArts[characterIndex].enabled = true;
            currentArtIndex = characterIndex;
        }
    }

    public void OnButtonClick(int characterIndex)
    {
        if (characterIndex == currentArtIndex)
        {
            // Clicking the same button again, hide the art
            HideCurrentArt();
            currentArtIndex = -1;
        }
        else
        {
            // Clicking a different button, hide the current art (if any) and show the art for the clicked character
            HideCurrentArt();
            ShowCharacterArt(characterIndex);
        }
    }

    public void OnUpgradeButtonClick()
    {
        if (currentArtIndex >= 0 && currentArtIndex < upgradeScenes.Length)
        {
            // Check if there's a valid upgrade scene name for the current character
            string sceneName = upgradeScenes[currentArtIndex];
            if (!string.IsNullOrEmpty(sceneName))
            {
                // Load the corresponding upgrade scene
                SceneManager.LoadScene(sceneName);
            }
        }
    }
}
