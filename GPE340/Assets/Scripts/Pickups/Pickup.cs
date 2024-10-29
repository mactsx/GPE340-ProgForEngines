using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class Pickup : MonoBehaviour
{
    private Collider pkCollider;
    public UnityEvent OnPickup;

    // Make sure collider is a trigger
    public void Awake()
    {
        pkCollider = GetComponent<Collider>();
        pkCollider.isTrigger = true;
    }
    
    // What happens when all pickups are collided with
    public virtual void OnTriggerEnter(Collider other)
    {
        // Destroy itself
        Destroy(gameObject);

        // Invoke the OnPickup event to call what happens based on each pickup
        OnPickup.Invoke();
    }
}
