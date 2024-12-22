using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupHealth : Pickup
{
    public float amountToHeal;

    // Start is called before the first frame update
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Heal what ran into the pickup
    // Add to the base onTrigger function
    public override void OnTriggerEnter(Collider other)
    {
        // Get the health component
        Health health = other.GetComponent<Health>();

        if (health != null)
        {
            // Check if they are at max health
            if (health.currHealth < health.maxHealth)
            {
                health.Heal(amountToHeal);

                // Then call the base function
                base.OnTriggerEnter(other);
            }
        }        
    }
}
