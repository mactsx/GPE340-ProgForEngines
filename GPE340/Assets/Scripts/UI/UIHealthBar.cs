using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIHealthBar : MonoBehaviour
{
    [SerializeField] private Image healthBarImage;

    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Get the player health and null check
        Health playerHealth = GameManager.instance.player.pawn.GetComponent<Health>();
        if (playerHealth != null)
        {
            // Update the health bar image
            healthBarImage.fillAmount = playerHealth.HealthPercentage();
        }
    }
}