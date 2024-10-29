using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public float currHealth;
    public float maxHealth;
    [SerializeField]
    private float initHealth;

    [Header("Events")]
    public UnityEvent OnTakeDamage;
    public UnityEvent OnHeal;
    public UnityEvent OnDie;

    public void Start()
    {
        currHealth = initHealth;
    }
    public void TakeDamage(float damage)
    {
        // Deal damage to the player
        currHealth -= damage;

        // Invoke any other events when taking damage
        OnTakeDamage.Invoke();

        // Check if this lowers the health below 0
        if (currHealth <= 0)
        {
            // If they are, then die
            Die();
        }
        Debug.Log(gameObject.name + " took " + damage + " damage.");
    }

    public void Heal(float healing)
    {
        // Add the amount of healing to the player health
        currHealth += healing;

        // Invoke any other healing events
        OnHeal.Invoke();

        // Check if the hp is still within range
        currHealth = Mathf.Clamp(currHealth, 0, maxHealth);

        Debug.Log(gameObject.name + " healed for " + healing + " points.");
    }

    // Full heal hp
    public void HealToFull()
    {
        currHealth = maxHealth;
    }

    public void Die()
    {
        //Ensure their health is 0
        currHealth = 0;
        // Invoke any other death events
        OnDie.Invoke();
    }

    public float HealthPercentage()
    {
        return currHealth / maxHealth;
    }

}
