using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Require a health component
[RequireComponent(typeof(Health))]
public class DeathActionDestory : GameAction
{
    [SerializeField]
    private float delayBeforeDestroy;

    // Start is called before the first frame update
    public override void Start()
    {
        // Get health component
        Health health = GetComponent<Health>();

        // Register to OnDie event
        health.OnDie.AddListener(DestroyOnDeath);
    }

    private void DestroyOnDeath()
    {
        Destroy(gameObject, delayBeforeDestroy);
    }
}
