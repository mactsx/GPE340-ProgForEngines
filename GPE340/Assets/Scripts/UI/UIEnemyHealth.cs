using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEnemyHealth : MonoBehaviour
{
    public Health enemyHealth;
    public Image enemyHealthbar;
    public CameraController playerCamera;

    void Update()
    {
        if (enemyHealth != null)
        {
            // If they have a healthbar, fill their hp amount
            if (enemyHealthbar != null)
            {
                enemyHealthbar.fillAmount = enemyHealth.HealthPercentage();
            }
        }
        // Make the canvas always face the camera
        if (playerCamera != null)
        {
            Vector3 direction = transform.position - playerCamera.transform.position;
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
