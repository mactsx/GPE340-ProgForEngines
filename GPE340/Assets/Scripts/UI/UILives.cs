using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILives : MonoBehaviour
{
    [Tooltip("The first icon in the list should be when there is 1 life remaining. The last is when there are full lives.")]
    public Image[] lifeIcons;

    void Update()
    {
        // Turn off all icons
        TurnOffAllIcons();
        // Then go through the icons and turn them on if we have more lives than the current icon index
        TurnOnIcons();
    }

    private void TurnOnIcons()
    {
        // Iterate through the array
        for (int i = 0; i < lifeIcons.Length; i++)
        {
            // Turn on the corresponding life
            if (i < GameManager.instance.player.lives)
            {
                lifeIcons[i].enabled = true;
            }
        }
    }

    private void TurnOffAllIcons()
    {
        // Turn off all life icons
        for (int i = 0; i < lifeIcons.Length; i++)
        {
            lifeIcons[i].enabled = false;
        }
    }
}
